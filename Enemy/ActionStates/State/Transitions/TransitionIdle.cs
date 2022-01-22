using System.Collections.Generic;

namespace Game.Enemy
{
    public class TransitionIdle : Transition
    {
        public override List<ÑheckType> ÑheckTypes => new List<ÑheckType>()
        {
            ÑheckType.Distance
        };

        public override State Target => new StateIdle();
    }
}
