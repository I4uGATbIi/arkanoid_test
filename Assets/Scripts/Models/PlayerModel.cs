using UniRx;
using Zenject;

namespace Scripts.Models
{
    public interface IPlayerModel : IInitializable
    {
        public IReactiveProperty<float> Speed { get; set; }
        
        void ResetState();
    }

    public class PlayerModel : IPlayerModel
    {
        public IReactiveProperty<float> Speed { get; set; }
        
        public void ResetState()
        {
            Speed.Value = 0f;
        }

        public void Initialize()
        {
            Speed = new ReactiveProperty<float>(0);
        }
    }
}