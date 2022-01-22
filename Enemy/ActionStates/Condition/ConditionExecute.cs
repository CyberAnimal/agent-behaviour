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

        public bool GetConditionStati�(�heckType �heck, ConditionData data) => _conditionStatic[�heck].Invoke(data);

        public bool GetConditionDynamic(�heckType �heck, ConditionData data) => _conditionDynamic[�heck].Invoke(data).Result;

        private readonly Dictionary<�heckType, Func<ConditionData, bool>> _conditionStatic =
            new Dictionary<�heckType, Func<ConditionData, bool>>()
            {
                [�heckType.Distance] = (a) => _static.CanDistance(a.EnemyPosition, a.TargetPosition,
                                                                  a.Ratio, _function.Increasestatic),

                [�heckType.Health] = (a) => _static.CanHealth(a.Health, a.Ratio, _function.Increasestatic),

                [�heckType.DistanceOrHealth] = (a) => _static.CanDistanceOrHealth(a.EnemyPosition, a.TargetPosition, a.Ratio,
                                                                                  a.Health, a.Ratio, _function.Increasestatic),

                [�heckType.DistanceAndHealth] = (a) => _static.CanDistanceAndHealth(a.EnemyPosition, a.TargetPosition, a.Ratio,
                                                                                    a.Health, a.Ratio, _function.Increasestatic)
            };

        private readonly Dictionary<�heckType, Func<ConditionData, Task<bool>>> _conditionDynamic =
            new Dictionary<�heckType, Func<ConditionData, Task<bool>>>()
            {
                [�heckType.Distance] = async (a) => await _dynamic.CanDistance(a.Enemy, a.Target, _function.Increasestatic),

                [�heckType.Health] = async (a) => await _dynamic.CanHealth(a.Enemy, _function.Increasestatic),

                [�heckType.DistanceAndHealth] = async (a) => await _dynamic.CanHealtAndArmor(a.Enemy, _function.Increasestatic)
            };
    }
}
