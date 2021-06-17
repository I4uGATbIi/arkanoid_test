using System;
using Scripts.Controllers;
using Scripts.Models;
using UnityEngine;
using Zenject;

namespace Scripts.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameController _game;

        public override void InstallBindings()
        {
            Container.Bind(typeof(IGameModel), typeof(IInitializable)).To<GameModel>().AsSingle();
            Container.BindInterfacesAndSelfTo(_game.GetType()).FromInstance(_game);
        }
    }
}