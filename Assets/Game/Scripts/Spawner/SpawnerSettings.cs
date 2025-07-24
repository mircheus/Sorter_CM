using UnityEngine;

namespace Game.Scripts.Spawner
{
    [CreateAssetMenu(menuName = "Create SpawnerSettings", fileName = "SpawnerSettings", order = 0)]
    public class SpawnerSettings : ScriptableObject
    {
        [Header("Timeout settings: ")]
        [SerializeField] private float minSpawnTimeout = 1f;
        [SerializeField] private float maxSpawnTimeout = 3f;
        [Header("Figure speed settings: ")]
        [SerializeField] private float minSpeed = 1f;
        [SerializeField] private float maxSpeed = 5f;
        
        public float MinSpawnTimeout => minSpawnTimeout;
        public float MaxSpawnTimeout => maxSpawnTimeout;
        public float MinSpeed => minSpeed;
        public float MaxSpeed => maxSpeed;
    }
}