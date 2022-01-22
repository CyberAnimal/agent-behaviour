using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class ActionHighLevel
    {
        private ActionFactory _factory = new ActionFactory();
        private List<Action> _currents;

        public ActionHighLevel(ActionType type = ActionType.Sleep)
        {
            _currents = new List<Action>();
            Action action = _factory.CreateAction(type);
            _currents.Add(action);
        }

        public void Execute(Vector3 target, State current, ConditionData data)
        {
            foreach (Action action in _currents)
            {
                action.CanAction(target);

                foreach (var situation in action.Situations)
                    if (situation.CanActiveStatiñ(current, data).IsActive)
                        _currents.Add(situation.GetTargetActions(current));

                if (action.IsComplete)
                    _currents.RemoveAt(_currents.IndexOf(action));
            }

            if (_currents.Count == 0)
                _currents = new List<Action>()
                { 
                    _factory.CreateAction(ActionType.Walk) 
                };
        }
    }
}
