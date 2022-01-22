using System.Collections.Generic;

namespace Game.Enemy
{
    public class TransitionAttack : Transition
    {
        public override List<ÑheckType> ÑheckTypes => new List<ÑheckType>()
        {
            ÑheckType.Distance,
            ÑheckType.DistanceAndHealth
        };

        public override State Target => new StateAttack();
    }
}
