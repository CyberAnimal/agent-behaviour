using System.Collections.Generic;

namespace Game.Enemy
{
    public class TransitionIdle : Transition
    {
        public override List<�heckType> �heckTypes => new List<�heckType>()
        {
            �heckType.Distance
        };

        public override State Target => new StateIdle();
    }
}
