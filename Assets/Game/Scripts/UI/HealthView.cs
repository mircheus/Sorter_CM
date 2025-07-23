using TMPro;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private TMP_Text healthPoints;

        public void UpdateHealth(int health)
        {
            healthPoints.text = health.ToString();
        }
    }
}