using System;
using Scripts.Models;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Scripts.Controllers
{
    public class GameController : SerializedMonoBehaviour, IInitializable, IDisposable
    {
        [Inject] private IGameModel _gameModel;
        [Inject] private IPlayerModel _playerModel;

        private readonly CompositeDisposable _subscriptions = new CompositeDisposable();
        
        public UnityEvent OnLose;
        public UnityEvent OnGame;

        private void OnDestroy()
        {
            ((IDisposable) this).Dispose();
        }

        public void Dispose()
        {
            _subscriptions.Dispose();
        }
        
        public void Initialize()
        {
            _gameModel.State.Subscribe(OnStateChanged).AddTo(_subscriptions);
        }
        
        private void OnStateChanged(GameState state)
        {
            switch (state)
            {
                case GameState.Start:
                    break;
                case GameState.GameCycle:
                    OnGame?.Invoke();
                    break;
                case GameState.Lose:
                    Lose();
                    break;
                case GameState.Restart:
                    StartLevel();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void Lose()
        {
            OnLose?.Invoke();
        }
        
        private void StartLevel()
        {
            _playerModel.ResetState();
            _gameModel.ResetState();
        }

        [Button]
        private void SetState(GameState state)
        {
            _gameModel.State.Value = state;
        }
    }
}