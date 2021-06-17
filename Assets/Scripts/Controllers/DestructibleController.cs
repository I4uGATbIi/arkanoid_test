using System;
using Extensions;
using Scripts.Models;
using Scripts.Views;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Scripts.Controllers
{
    public class DestructibleController : SerializedMonoBehaviour, IInitializable, IDisposable
    {
        [Inject] private IGameModel _gameModel;

        private readonly CompositeDisposable _subscriptions = new CompositeDisposable();

        [SerializeField] private DestructibleView _destructible;
        [SerializeField] private Collider _spawnBounds;

        public void Initialize()
        {
            _gameModel.State.Subscribe(OnGameStateChange).AddTo(_subscriptions);
            _destructible.IsHidden.Subscribe(OnIsHiddenChange).AddTo(_subscriptions);
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
                    ReplaceDestructible();
                    break;
                case GameState.GameCycle:
                    break;
                case GameState.Lose:
                    break;
                case GameState.Restart:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void OnIsHiddenChange(bool isHidden)
        {
            if (isHidden)
                ReplaceDestructible();
        }

        private void ReplaceDestructible()
        {
            _destructible.transform.position = _spawnBounds.GetRandomPointInside();
            _destructible.Show();
        }
    }
}