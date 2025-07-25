using System.Collections.Generic;
using Game.Scripts.FigureFactory.Figures;
using Game.Scripts.Spawner;
using UnityEngine;

namespace Game.Scripts
{
    public class ResetController : MonoBehaviour
    {
        private List<IResettable> _resettables = new List<IResettable>();

        private void Start()
        {
            // var ressetables = FindObjectsByType(Figure, FindObjectsInactive.Exclude);
            // FindObjectsByType<IResettable>(out ressetables);
            var foundCanvasObjects = FindObjectsByType<Figure>(FindObjectsSortMode.None);
        }

        public void Register(IResettable resettable)
        {
            _resettables.Add(resettable);
        }

        public void ResetLevel()
        {
            foreach (var r in _resettables)
            {
                r.ResetState();
            }
        }
    }
}