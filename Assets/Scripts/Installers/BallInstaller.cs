using Scripts.Models;
using Scripts.Views;
using UnityEngine;
using Zenject;

namespace Scripts.Installers
{
    public class BallInstaller : MonoInstaller
    {
        [SerializeField] private BallView m_view;

        public override void InstallBindings()
        {
            Container.Bind(typeof(IBallModel), typeof(IInitializable)).To<BallModel>().AsSingle();
            Container.BindInterfacesTo(m_view.GetType()).FromInstance(m_view);
        }
    }
}