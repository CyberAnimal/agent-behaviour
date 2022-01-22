using System.Collections.Generic;

namespace Game.Enemy
{
    public class TransitionSeek : Transition
    {
        public override List<ÑheckType> ÑheckTypes => new List<ÑheckType>()
        {
            ÑheckType.Distance,
            ÑheckType.Health,
            ÑheckType.DistanceOrHealth
        };
        public override State Target => new StateSeek();
    }
}
