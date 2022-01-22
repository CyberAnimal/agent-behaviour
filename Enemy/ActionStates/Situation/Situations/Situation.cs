using System.Collections.Generic;

namespace Game.Enemy
{
    public abstract class Situation
    {
        protected ConditionExecute Condition;

        public readonly struct Variation
        {
            private readonly ÑheckType _type;
            private readonly Action _target;

            public readonly ÑheckType ÑheckType => _type;
            public readonly Action TargetAction => _target;

            public Variation(ÑheckType checkType, Action target)
            {
                _type = checkType;
                _target = target;
            }
        }

        protected readonly Dictionary<State, Variation> Variations;
        public Dictionary<State, Variation> Availables => Variations;

        public Action GetTargetActions
            (State state) => Availables[state].TargetAction;

        public Situation()
        {
            Condition = new ConditionExecute();
            Variations = new Dictionary<State, Variation>();
        }

        public ToggleAction CanActiveStatiñ(State curerntState, ConditionData data)
        {
            Variation current = Variations[curerntState];
            ÑheckType ñheckType = current.ÑheckType;

            if (Condition.GetConditionStatiñ(ñheckType, data))
                return new ToggleAction(true, current.TargetAction);

            else return new ToggleAction(false, default);
        }

        public ToggleAction CanActiveDynamic(State curerntState, ConditionData data)
        {
            Variation current = Variations[curerntState];
            ÑheckType ñheckType = current.ÑheckType;

            if (Condition.GetConditionDynamic(ñheckType, data))
                return new ToggleAction(true, current.TargetAction);

            else return new ToggleAction(false, default);
        }
    };
}
