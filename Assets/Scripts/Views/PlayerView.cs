using System;
using Configs;
using Scripts.Models;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using Zenject;

namespace Scripts.Views
{
    public class PlayerView : MonoBehaviour, IInitializable, IDisposable
    {
        [Inject] private IGameModel _gameModel;
        [Inject] private IPlayerModel _playerModel;

        private readonly CompositeDisposable _subscriptions = new CompositeDisposable();

        public Vector3 StartPosition;

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
                    _isMovementAllowed = true;
                    break;
                case GameState.GameCycle:
                    break;
                case GameState.Lose:
                    _isMovementAllowed = false;
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

            transform.position += transform.right * (_playerModel.Speed.Value * Time.deltaTime);
        }

        private void Reset()
        {
            transform.position = StartPosition;
        }

        [Button]
        private void SetCurrentAsStartPosition()
        {
            StartPosition = transform.position;
        }
    }
}