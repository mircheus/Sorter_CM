using UnityEngine;

namespace Game.Scripts.Spawner
{
    public class MoveLine : MonoBehaviour
    {
        [SerializeField] private Transform startPoint;

        public Transform StartPoint => startPoint;
    }
}