using System;
using Configs;
using Scripts.Models;
using Scripts.Views;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using Zenject;

namespace Scripts.Controllers
{
    public class PlayerController : SerializedMonoBehaviour, IInitializable, IDisposable
    {
        [Inject] private IPlayerModel _playerModel;
        [Inject] private IBallModel _ballModel;
        [Inject] private IGameModel _gameModel;
        [Inject] private PlayerConfig _config;

        [SerializeField] private PlayerView _playerView;

        private bool _isShootEnabled;
        private bool _isMovementEnabled;

        private readonly CompositeDisposable _subscriptions = new CompositeDisposable();

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
                    _isShootEnabled = true;
                    _isMovementEnabled = true;
                    break;
                case GameState.GameCycle:
                    _isShootEnabled = false;
                    break;
                case GameState.Lose:
                    _isMovementEnabled = false;
                    break;
                case GameState.Restart:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void Update()
        {
            if (_isShootEnabled && Input.GetButton("Jump"))
            {
                _ballModel.MoveDirection.Value = Vector3.up;
                _gameModel.SetState(GameState.GameCycle);
            }

            if (_isMovementEnabled)
            {
                _playerModel.Speed.Value = _config.MaxSpeed * Input.GetAxis("Horizontal");
            }
        }
    }
}