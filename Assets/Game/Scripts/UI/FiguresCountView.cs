using TMPro;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class FiguresCountView :MonoBehaviour
    {
        [SerializeField] private TMP_Text figuresCount;
        
        public void UpdateScore(int score)
        {
            figuresCount.text = score.ToString();
        }
    }
}