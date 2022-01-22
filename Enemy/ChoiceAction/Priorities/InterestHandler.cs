using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public abstract class InterestHandler
    {
        protected InterestSources Sources;

        protected Dictionary<Sense, Func<Interest, bool>> AffectedsTable =
            new Dictionary<Sense, Func<Interest, bool>>()
            {
                [Sense.Sight] = (x) => IsAffectedSight(x),
                [Sense.Sound] = (x) => IsAffectedSound(x),
                [Sense.Smell] = (x) => IsAffectedSmell(x)
            };

        public abstract Priority GetPriority(GameObject enemy, GameObject agent, double radius,
                                             PrioritiesReckoner.FunctionType function,
                                             PrioritiesReckoner.FunctionForm type);

        public abstract Interest UpdateAffected(Interest interest,
                                                GameObject agent, double radius,
                                                PrioritiesReckoner.FunctionType function,
                                                PrioritiesReckoner.FunctionForm type);

        protected bool IsAffected(Interest interest, Sense sense) => AffectedsTable[sense].Invoke(interest);

        private static bool IsAffectedSight(Interest interest) => false;

        private static bool IsAffectedSound(Interest interest) => false;

        private static bool IsAffectedSmell(Interest interest) => false;
    }
}
