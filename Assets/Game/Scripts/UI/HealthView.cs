using TMPro;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private TMP_Text healthText;

        public void UpdateHealth(int health)
        {
            healthText.text = health.ToString();
        }
    }
}