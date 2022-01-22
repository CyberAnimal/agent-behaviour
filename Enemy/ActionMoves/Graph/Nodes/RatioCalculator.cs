using System;
using UnityEngine;

namespace Game.Enemy
{
    public class RatioCalculator
    {
        private float _minValue = 0.1f;
        private float _maxValue = 1.0f;

        public float GetRatio(ObjectType type) => type switch
        {
            ObjectType.Wall => System.Random.Range(_minValue, _maxValue),
            ObjectType.Plane => throw new NotImplementedException(),
            ObjectType.Enemy => throw new NotImplementedException(),
            ObjectType.Agent => throw new NotImplementedException(),
        };
    }
}
