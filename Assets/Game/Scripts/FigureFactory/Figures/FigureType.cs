using UnityEngine;

namespace Game.Scripts.FigureFactory.Figures
{
    [CreateAssetMenu(fileName = "FigureType", menuName = "Game/FigureType", order = 1)]
    public class FigureType : ScriptableObject
    {
        [SerializeField] private Sprite figureSprite;

        public Sprite FigureSprite => figureSprite;
    }
}