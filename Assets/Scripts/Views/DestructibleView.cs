using UniRx;
using UnityEngine;

namespace Scripts.Views
{
    public class DestructibleView : MonoBehaviour
    {
        public ReactiveProperty<bool> IsHidden = new ReactiveProperty<bool>();

        public void Hide()
        {
            gameObject.SetActive(false);
            IsHidden.Value = true;
        }

        public void Show()
        {
            gameObject.SetActive(true);
            IsHidden.Value = false;
        }
    }
}