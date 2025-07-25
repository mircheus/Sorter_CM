using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game.Scripts.UI.Views
{
    public class ScoreView :MonoBehaviour
    {
        [SerializeField] private Transform uiGameObject;
        [SerializeField] private TMP_Text scoreText;
        
        public void UpdateScore(int score)
        {
            uiGameObject.DOShakeScale(.3f, .5f);
            scoreText.text = score.ToString();
        }
    }
}