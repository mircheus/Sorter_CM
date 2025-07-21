using System;
using Game.Scripts.FigureFactory.Figures;
using UnityEngine;

namespace Game.Scripts.FigureFactory.Factories
{
    public interface IFigureFactory
    {
        Figure GetFromPool(Vector3 spawnPosition, Transform parent = null);
        void ReturnToPool(Figure figure);
        Type FigureType { get; } // <--- For registration
    }
}