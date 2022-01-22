using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class SituationChecker : MonoBehaviour
    {
        private readonly Situations _situations = new Situations();

        public ToggleAction GetActionStatic(State current, ConditionData date)
        {
            List<Func<State, ConditionData, ToggleAction>> currentChecks =
                _situations.GetSituationsStatic();

            foreach (var check in currentChecks)
            {
                ToggleAction toggle = check.Invoke(current, date);

                if (toggle.IsActive)
                    return new ToggleAction(true, toggle.Target);

                else continue;
            }

            return new ToggleAction(false, default);
        }

        public ToggleAction GetActionDynamic(State current, ConditionData date)
        {
            List<Func<State, ConditionData, ToggleAction>> currentChecks =
                _situations.GetSituationsDynamic();

            foreach (var check in currentChecks)
            {
                ToggleAction toggle = check.Invoke(current, date);

                if (toggle.IsActive)
                    return new ToggleAction(true, toggle.Target);

                else continue;
            }

            return new ToggleAction(false, default);
        }
    }
}