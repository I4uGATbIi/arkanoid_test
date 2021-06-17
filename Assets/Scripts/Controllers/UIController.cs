using System;
using Scripts.Models;
using Sirenix.OdinInspector;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Animations;
using Zenject;

namespace Scripts.Controllers
{
    public class UIController : SerializedMonoBehaviour, IInitializable, IDisposable
    {
        [Inject] private IGameModel _gameModel;
        [Inject] private IUserModel _userModel;

        private readonly CompositeDisposable _subscriptions = new CompositeDisposable();

        [SerializeField] private TMP_Text _currentScore;
        [SerializeField] private TMP_Text _highScore;
        [SerializeField] private GameObject _losePanel;
        [SerializeField] private GameObject _controlsHint;

        public void Initialize()
        {
            _gameModel.State.Subscribe(OnGameStateChange).AddTo(_subscriptions);
            _gameModel.Score.Subscribe(OnScoreChanged).AddTo(_subscriptions);
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
                    _highScore.text = "High\n" + _userModel.HighScore.Value;
                    break;
                case GameState.GameCycle:
                    _controlsHint.SetActive(false);
                    break;
                case GameState.Lose:
                    _losePanel.SetActive(true);
                    break;
                case GameState.Restart:
                    _losePanel.SetActive(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void OnScoreChanged(int score)
        {
            _currentScore.text = "Score\n" + score;
            if (score > _userModel.HighScore.Value)
            {
                _userModel.HighScore.Value = score;
                _highScore.text = "High\n" + score;
            }
        }

        public void Restart()
        {
            _gameModel.SetState(GameState.Restart);
        }
    }
}