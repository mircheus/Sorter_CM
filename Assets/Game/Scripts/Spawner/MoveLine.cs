using Game.Scripts.FigureFactory;
using UnityEngine;

namespace Game.Scripts.Spawner
{
    public class MoveLine : MonoBehaviour
    {
        [Header("References: ")]
        [SerializeField] private Transform startPoint;
        [SerializeField] private DeathZone deathZone;

        public Transform StartPoint => startPoint;
        public DeathZone DeathZone => deathZone;
    }
}