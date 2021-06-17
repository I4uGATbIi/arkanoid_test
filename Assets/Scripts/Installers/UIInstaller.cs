using Scripts.Controllers;
using UnityEngine;
using Zenject;

namespace Scripts.Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private UIController _ui;
    
        public override void InstallBindings()
        {
            Container.BindInterfacesTo(_ui.GetType()).FromInstance(_ui);
        }
    }
}