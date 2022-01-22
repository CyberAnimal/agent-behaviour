using UnityEngine;

namespace Game.Enemy
{
    public struct Route
    {
        private float _angular;
        public float Angular => _angular;

        private Vector3 _linear;
        public Vector3 Linear => _linear;

        public Route(float angular = 0.0f)
        {
            _angular = angular;
            _linear = Vector3.zero;
        }

        public Route(float angular, Vector3 linear)
        {
            _angular = angular;
            _linear = linear;
        }
    }
}
