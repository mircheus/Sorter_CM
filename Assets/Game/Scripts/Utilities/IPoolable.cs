using UnityEngine;

public interface IPoolable
{
    void OnSpawn(Vector3 position);
    void OnDespawn();
}