using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Enemy
{
    public class ConditionDynamic
    {
        public async Task<bool> CanDistance(Enemy enemy, GameObject target, Func<float, float, bool> function)
        {
            Vector3 originPos = enemy.transform.position;
            Vector3 targetPos = target.transform.position;
            float distance = Vector3.Distance(originPos, targetPos);

            float count = 0;

            Task<float> newDistance = await Task.WhenAny(DistanceCount(enemy, target, count));

            while (newDistance.IsCompleted == false)
                await Task.Yield();

            return function.Invoke(newDistance.Result, distance);
        }

        private async Task<float> DistanceCount(Enemy enemy, GameObject target, float count)
        {
            count++;

            while (count < 50)
                await Task.Yield();

            Vector3 originPos = enemy.transform.position;
            Vector3 targetPos = target.transform.position;
            float newDistance = Vector3.Distance(originPos, targetPos);

            return newDistance;
        }

        public async Task<bool> CanHealth(Enemy enemy, Func<float, float, bool> function)
        {
            float heath = enemy.Health;
            float count = 0;

            Task<float> newHealth = await Task.WhenAny(HealthCount(enemy, count));

            while (newHealth.IsCompleted == false)
                await Task.Yield();

            return function(newHealth.Result, heath);
        }

        private async Task<float> HealthCount(Enemy enemy, float count)
        {
            count++;

            while (count < 50)
                await Task.Yield();

            float newHealth = enemy.Health;

            return newHealth;
        }

        public async Task<bool> CanHealtAndArmor(Enemy enemy, Func<float, float, bool> function)
        {
            float heath = enemy.Health;
            float count = 0;

            Task<float> newHealth = await Task.WhenAny(HealthCount(enemy, count));

            while (newHealth.IsCompleted == false)
                await Task.Yield();

            return newHealth.Result < heath;
        }
    }
}
