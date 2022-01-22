using System.Collections.Generic;

namespace Game.Player
{
    public interface IWeapon
    {
        public List<IGun> Guns { get; }
    }
}
