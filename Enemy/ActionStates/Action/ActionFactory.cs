using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class ActionFactory
    {
        private readonly Dictionary<ActionType, Action> _actions =
            new Dictionary<ActionType, Action>()
            {
                [ActionType.Sleep] = new ActionSleep(),
                [ActionType.Walk] = new ActionWalk(),
                [ActionType.Pursue] = new ActionPursue(),
                [ActionType.Escape] = new ActionEscape(),
                [ActionType.Eat] = new ActionEat(),
                [ActionType.Attack] = new ActionAttack(),
            };

        public Action CreateAction(ActionType type) => _actions[type];
    }
}
