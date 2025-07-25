using TMPro;
using UnityEngine;

namespace Game.Scripts.UI.Views
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