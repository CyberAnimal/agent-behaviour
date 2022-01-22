using System.Collections.Generic;

namespace Game.Enemy
{
    public class TransitionAttack : Transition
    {
        public override List<�heckType> �heckTypes => new List<�heckType>()
        {
            �heckType.Distance,
            �heckType.DistanceAndHealth
        };

        public override State Target => new StateAttack();
    }
}
