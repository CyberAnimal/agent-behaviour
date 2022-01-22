using System.Collections.Generic;

namespace Game.Enemy
{
    public class TransitionSeek : Transition
    {
        public override List<�heckType> �heckTypes => new List<�heckType>()
        {
            �heckType.Distance,
            �heckType.Health,
            �heckType.DistanceOrHealth
        };
        public override State Target => new StateSeek();
    }
}
