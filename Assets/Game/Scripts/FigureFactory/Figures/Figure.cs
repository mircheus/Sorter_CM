using UnityEngine;

namespace Game.Scripts.FigureFactory.Figures
{
    public abstract class Figure : MonoBehaviour, IPoolable
    {
        public virtual void OnSpawn(Vector3 position)
        {
            transform.position = position;
            gameObject.SetActive(true);
        }

        public virtual void OnDespawn()
        {
            gameObject.SetActive(false);
        }
    }
}