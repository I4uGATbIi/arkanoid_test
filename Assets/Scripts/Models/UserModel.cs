using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Scripts.Models
{
    public interface IUserModel : IInitializable
    {
        IReactiveProperty<int> HighScore { get; set; }
    }

    public sealed class UserModel : IUserModel, IDisposable
    {
        public IReactiveProperty<int> HighScore { get; set; }

        public void Dispose()
        {
            PlayerPrefs.SetInt("HighScore", HighScore.Value);

            PlayerPrefs.Save();
        }


        public void Initialize()
        {
            HighScore = new ReactiveProperty<int>()
            {
                Value = PlayerPrefs.GetInt("Level", 0)
            };
        }
    }
}