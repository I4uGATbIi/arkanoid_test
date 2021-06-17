using System.Collections.Generic;
using System.IO;
using UniRx;
using Zenject;

namespace Scripts.Models
{
    public enum GameState
    {
        Start,
        GameCycle,
        Lose,
        Restart
    }

    public interface IGameModel : IInitializable
    {
        IReactiveProperty<GameState> State { get; set; }
        IReactiveProperty<int> Score { get; set; }

        void ResetState();

        void SetState(GameState state);
    }

    public class GameModel : IGameModel
    {
        public IReactiveProperty<GameState> State { get; set; }
        public IReactiveProperty<int> Score { get; set; }

        //TODO: Find out how to fix this Hardcode!
        private readonly Dictionary<GameState, GameState> NextPrevStateValidity = new Dictionary<GameState, GameState>()
        {
            {GameState.Start, GameState.Restart},
            {GameState.GameCycle, GameState.Start},
            {GameState.Lose, GameState.GameCycle},
            {GameState.Restart, GameState.Lose}
        };

        public void ResetState()
        {
            State.Value = GameState.Start;
            Score.Value = 0;
        }

        public void SetState(GameState state)
        {
            if (State.Value != NextPrevStateValidity[state])
                throw new InvalidDataException("GameState mismatch");
            State.Value = state;
        }

        void IInitializable.Initialize()
        {
            State = new ReactiveProperty<GameState>(GameState.Start);
            Score = new ReactiveProperty<int>(0);
        }
    }
}