using TMPro;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class ScoreView :MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        
        public void UpdateScore(int score)
        {
            scoreText.text = score.ToString();
        }
    }
}