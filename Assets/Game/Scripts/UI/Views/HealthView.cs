using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game.Scripts.UI.Views
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Transform uiGameObject;
        [SerializeField] private TMP_Text healthText;

        public void UpdateHealth(int health)
        {
            uiGameObject.DOShakeScale(.3f, .5f);
            healthText.text = health.ToString();
        }
    }
}