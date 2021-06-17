using Scripts.Controllers;
using Scripts.Models;
using UnityEngine;
using Zenject;

namespace Scripts.Installers
{
    public class DestructibleInstaller : MonoInstaller
    {
        [SerializeField] private DestructibleController _controller;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo(_controller.GetType()).FromInstance(_controller);
        }
    }
}