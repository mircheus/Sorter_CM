using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.FigureFactory.Figures
{
    [CreateAssetMenu(fileName = "FiguresList", menuName = "Game/FiguresList", order = 1)]
    public class FiguresList : ScriptableObject
    {
        [SerializeField] private List<FigureType> figureTypes;
        
        public List<FigureType> Figures => figureTypes;
    }
}