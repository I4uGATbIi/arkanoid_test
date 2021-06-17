using System;
using Configs;
using Scripts.Models;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.Animations;
using Zenject;

namespace Scripts.Views
{
    public class BallView : MonoBehaviour, IInitializable, IDisposable
    {
        [Inject] private IGameModel _gameModel;
        [Inject] private IBallModel _ballModel;
        [Inject] private BallConfig _config;

        private readonly CompositeDisposable _subscriptions = new CompositeDisposable();

        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private PositionConstraint _positionConstraint;
        [SerializeField] private GameObject _view;
        [SerializeField] private Rigidbody _rigidbody;

        private bool _isMovementAllowed;

        public void Initialize()
        {
            _gameModel.State.Subscribe(OnGameStateChange).AddTo(_subscriptions);
        }

        public void Dispose()
        {
            _subscriptions.Dispose();
        }

        private void OnGameStateChange(GameState state)
        {
            switch (state)
            {
                case GameState.Start:
                    _isMovementAllowed = false;
                    _positionConstraint.constraintActive = true;
                    break;
                case GameState.GameCycle:
                    _isMovementAllowed = true;
                    _positionConstraint.constraintActive = false;
                    break;
                case GameState.Lose:
                    _isMovementAllowed = false;
                    _view.SetActive(false);
                    _rigidbody.velocity = Vector3.zero;
                    _rigidbody.angularVelocity = Vector3.zero;
                    _rigidbody.Sleep();
                    break;
                case GameState.Restart:
                    Reset();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void Update()
        {
            if (!_isMovementAllowed)
                return;
            
            transform.position += _ballModel.MoveDirection.Value * (_config.MaxSpeed * Time.deltaTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            GameObject collidedObj = collision.gameObject;
            WallsView wallsView = collidedObj.GetComponentInParent<WallsView>();
            if (wallsView != null)
            {
                if (wallsView.GetWallType(collidedObj) == WallType.Death)
                    _gameModel.SetState(GameState.Lose);
            }

            DestructibleView destructibleView = collidedObj.GetComponent<DestructibleView>();
            if (destructibleView != null)
            {
                destructibleView.Hide();
                _gameModel.Score.Value++;
            }

            _ballModel.MoveDirection.Value =
                Vector3.Reflect(_ballModel.MoveDirection.Value, collision.contacts[0].normal);
        }

        private void Reset()
        {
            transform.position = _startPosition;
            _positionConstraint.constraintActive = true;
            _view.SetActive(true);
        }

        [Button]
        private void SetCurrentAsStartPosition()
        {
            _startPosition = transform.position;
        }
    }
}