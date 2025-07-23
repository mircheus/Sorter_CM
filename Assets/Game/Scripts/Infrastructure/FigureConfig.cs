using UnityEngine;

namespace Game.Scripts.Infrastructure
{
    [CreateAssetMenu(fileName = "FigureConfig", menuName = "Game/FigureConfig", order = 1)]
    public class FigureConfig : ScriptableObject
    {
        [Header("Figure Settings: ")]
        public float FigureSpeedMin = 1f;
        public float FigureSpeedMax = 3f;
    }
}