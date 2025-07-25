using UnityEngine;

namespace Game.Scripts.Infrastructure
{
    [CreateAssetMenu(menuName = "Create Level Settings", fileName = "LevelSettings", order = 0)]
    public class LevelSettings : ScriptableObject
    {
        [Header("Level init settings: ")] 
        [SerializeField] private int healthPoints = 10;
        [SerializeField] private int figuresToSortMin = 3;
        [SerializeField] private int figuresToSortMax = 3;
        [SerializeField] private int score = 0;

        public int HealthPoints => healthPoints;
        public int FiguresToSortMin => figuresToSortMin;
        public int FiguresToSortMax => figuresToSortMax;
        public int Score => score;
    }
}