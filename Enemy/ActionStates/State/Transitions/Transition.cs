using System.Collections.Generic;

namespace Game.Enemy
{
    public abstract class Transition
    {
        public abstract List<ÑheckType> ÑheckTypes { get; }
        public abstract State Target { get; }

        public bool CanActive(ConditionData data)
        {
            ConditionExecute execute = new ConditionExecute();

            foreach (var check in ÑheckTypes)
            {
                if (execute.GetConditionDynamic(check, data))
                    return true;

                else continue;
            }

            return false;
        }
    }
}
