using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class Situations : MonoBehaviour
    {
        private readonly List<Situation> _situations = new List<Situation>()
        {
            new DiscoveredObject(),
            new DiscoveredAgent(),
            new GoneObject(),
            new GoneAgent()
        };

        public List<Func<State, ConditionData, ToggleAction>> GetSituationsStatic()
        {
            List<Func<State, ConditionData, ToggleAction>> currentChecks =
                new List<Func<State, ConditionData, ToggleAction>>();

            foreach (var situation in _situations)
                currentChecks.Add((a, b) => situation.CanActiveStatiñ(a, b));

            return currentChecks;
        }

        public List<Func<State, ConditionData, ToggleAction>> GetSituationsDynamic()
        {
            List<Func<State, ConditionData, ToggleAction>> currentChecks =
                new List<Func<State, ConditionData, ToggleAction>>();

            foreach (var situation in _situations)
                currentChecks.Add((a, b) => situation.CanActiveDynamic(a, b));

            return currentChecks;
        }
    }
}