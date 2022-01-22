using UnityEngine;

namespace Game.Enemy
{
    public class HerbivoreHandler : InterestHandler
    {
        public override Priority GetPriority(GameObject enemy, GameObject agent, double radius,
                                             PrioritiesReckoner.FunctionType function,
                                             PrioritiesReckoner.FunctionForm type)
        {
            PrioritiesReckoner reckoner = new PrioritiesReckoner();
            Priority priority = reckoner.CalculateWithDistance(agent, enemy.transform.position,
                                                               radius, function, type);

            return priority;
        }

        public override Interest UpdateAffected(Interest interest,
                                                GameObject agent, double radius,
                                                PrioritiesReckoner.FunctionType function,
                                                PrioritiesReckoner.FunctionForm type)
        {
            if (IsAffected(interest, interest.Sense))
            {
                PrioritiesReckoner reckoner = new PrioritiesReckoner();
                Priority priority = reckoner.CalculateWithDistance(agent, interest.Position,
                                                                   radius, function, type);

                Interest newInterest = new Interest(interest.Sense, priority,
                                                    interest.InterestObject);
                return newInterest;
            }

            else return interest;
        }
    }
}