using System;
using UnityEngine;

namespace Game.Enemy
{
    public class ConditionStatic
    {
        public bool CanDistance(Vector3 originPos, Vector3 targetPos,
                                float maxDistance, Func<float, float, bool> function)
        {
            float distance = Vector3.Distance(originPos, targetPos);

            return function.Invoke(distance, maxDistance);
        }

        public bool CanHealth(float enemyHealth, float healthRatio,
                              Func<float, float, bool> function) => function.Invoke(enemyHealth, healthRatio);

        public bool CanDistanceAndHealth(Vector3 originPos, Vector3 targetPos, float maxDistance,
                                         float enemyHealth, float healthRatio, Func<float, float, bool> function)
        {
            float distance = Vector3.Distance(originPos, targetPos);

            return function.Invoke(distance, maxDistance) &&
                   function.Invoke(enemyHealth, healthRatio);
        }

        public bool CanDistanceOrHealth(Vector3 originPos, Vector3 targetPos, float maxDistance,
                                         float enemyHealth, float healthRatio, Func<float, float, bool> function)
        {
            float distance = Vector3.Distance(originPos, targetPos);

            return function.Invoke(distance, maxDistance) ||
                   function.Invoke(enemyHealth, healthRatio);
        }

        public bool HealthAndArmor(float enemyHealth, float armor,
                                   Func<float, float, bool> function) => function.Invoke(enemyHealth, armor);
    }
}
