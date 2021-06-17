using UniRx;
using UnityEngine;
using Zenject;

namespace Scripts.Models
{
    public interface IBallModel : IInitializable
    {
        public IReactiveProperty<float> Speed { get; set; }
        public IReactiveProperty<Vector3> MoveDirection { get; set; }
        
        void ResetState();
    }

    public class BallModel : IBallModel
    {
        public IReactiveProperty<float> Speed { get; set; }
        public IReactiveProperty<Vector3> MoveDirection { get; set; }
        
        public void ResetState()
        {
            Speed.Value = 0f;
        }

        public void Initialize()
        {
            Speed = new ReactiveProperty<float>(0);
            MoveDirection = new ReactiveProperty<Vector3>(Vector3.zero);
        }
    }
}