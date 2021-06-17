using Configs;
using UnityEngine;
using Zenject;

namespace Scripts.Installers
{
    [CreateAssetMenu(fileName = "ConfigInstaller", menuName = "Installers/ConfigInstaller")]
    public class ConfigInstaller : ScriptableObjectInstaller<ConfigInstaller>
    {
        [SerializeField] private BallConfig _ballConfig;
        [SerializeField] private PlayerConfig _playerConfig;

        public override void InstallBindings()
        {
            Container.BindInstances(_ballConfig, _playerConfig);
        }
    }
}