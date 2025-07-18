using System;
using Game.Scripts.FigureFactory.Figures;
using UnityEngine;

namespace Game.Scripts.FigureFactory
{
    public interface IFigureFactory
    {
        Figure GetFromPool(Vector3 spawnPosition);
        void ReturnToPool(Figure figure);
        // Type FigureType { get; } // <--- For registration
    }
}