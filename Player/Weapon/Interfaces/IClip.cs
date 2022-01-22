using System.Collections.Generic;

namespace Game.Player
{
    public interface IClip
    {
        public Stack<IPatron> Patrons { get; }

        public void ReductionClip();

        void TakeNew();
    }
}