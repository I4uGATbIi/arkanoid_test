using Scripts.Controllers;
using Scripts.Models;
using Scripts.Views;
using UnityEngine;
using Zenject;

namespace Scripts.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerView _view;
        [SerializeField] private PlayerController _controller;

        public override void InstallBindings()
        {
            Container.Bind(typeof(IPlayerModel), typeof(IInitializable)).To<PlayerModel>().AsSingle();
            Container.BindInterfacesTo(_view.GetType()).FromInstance(_view);
            Container.BindInterfacesTo(_controller.GetType()).FromInstance(_controller);
        }
    }
}