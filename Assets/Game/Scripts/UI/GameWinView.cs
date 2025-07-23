using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class GameWinView : EndGameView
    {
        [SerializeField] private TMP_Text score;
        
        public void SetScore(int scoreValue)
        {
            score.text = scoreValue.ToString();
        }
    }
}