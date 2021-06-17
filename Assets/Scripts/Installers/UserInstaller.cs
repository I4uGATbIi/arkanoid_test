using System;
using Scripts.Models;
using UnityEngine;
using Zenject;

namespace Scripts.Installers
{
    [CreateAssetMenu(fileName = "UserInstaller", menuName = "Installers/UserInstaller")]
    public class UserInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(IUserModel), typeof(IInitializable), typeof(IDisposable)).To<UserModel>().AsSingle();
        }
    }
}