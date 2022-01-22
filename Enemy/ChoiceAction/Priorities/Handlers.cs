using System.Collections.Generic;

namespace Game.Enemy
{
    public class Handlers
    {
        private readonly Dictionary<BehaviourType, InterestHandler> _handlersWithType =
            new Dictionary<BehaviourType, InterestHandler>()
            {
                [BehaviourType.Move] = new HerbivoreHandler(),
                [BehaviourType.Attack] = new HerbivoreHandler()
            };

        public InterestHandler GetHandler(BehaviourType type) => _handlersWithType[type];
    }
}