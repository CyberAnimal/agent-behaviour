using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Game.Enemy
{
    public class ConditionExecute
    {
        private static readonly ConditionDynamic _dynamic = new ConditionDynamic();
        private static readonly ConditionStatic _static = new ConditionStatic();
        private static readonly ConditionFunction _function = new ConditionFunction();

        public bool GetConditionStatiñ(ÑheckType ñheck, ConditionData data) => _conditionStatic[ñheck].Invoke(data);

        public bool GetConditionDynamic(ÑheckType ñheck, ConditionData data) => _conditionDynamic[ñheck].Invoke(data).Result;

        private readonly Dictionary<ÑheckType, Func<ConditionData, bool>> _conditionStatic =
            new Dictionary<ÑheckType, Func<ConditionData, bool>>()
            {
                [ÑheckType.Distance] = (a) => _static.CanDistance(a.EnemyPosition, a.TargetPosition,
                                                                  a.Ratio, _function.Increasestatic),

                [ÑheckType.Health] = (a) => _static.CanHealth(a.Health, a.Ratio, _function.Increasestatic),

                [ÑheckType.DistanceOrHealth] = (a) => _static.CanDistanceOrHealth(a.EnemyPosition, a.TargetPosition, a.Ratio,
                                                                                  a.Health, a.Ratio, _function.Increasestatic),

                [ÑheckType.DistanceAndHealth] = (a) => _static.CanDistanceAndHealth(a.EnemyPosition, a.TargetPosition, a.Ratio,
                                                                                    a.Health, a.Ratio, _function.Increasestatic)
            };

        private readonly Dictionary<ÑheckType, Func<ConditionData, Task<bool>>> _conditionDynamic =
            new Dictionary<ÑheckType, Func<ConditionData, Task<bool>>>()
            {
                [ÑheckType.Distance] = async (a) => await _dynamic.CanDistance(a.Enemy, a.Target, _function.Increasestatic),

                [ÑheckType.Health] = async (a) => await _dynamic.CanHealth(a.Enemy, _function.Increasestatic),

                [ÑheckType.DistanceAndHealth] = async (a) => await _dynamic.CanHealtAndArmor(a.Enemy, _function.Increasestatic)
            };
    }
}
