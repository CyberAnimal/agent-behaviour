using System.Collections.Generic;

namespace Game.Player
{
    public class ShootSelector
    {
        private ShootTypes _shootTypes;
        private List<Shoot> _shoots;
        public List<Shoot> ShootTypes { get; private set; }

        public List<Shoot> GetShootTypes() => _shoots = _shootTypes.Types;
    }

    public static class Extention
    {
        public static Shoot ChangeShoot(this Weapon weapon, ShootSelector selector)
        {
            IGun gun = weapon.CurrentGun;
            IGunType.Type type = gun.GunType;
            Shoot shootType = default;

            foreach (Shoot shoot in selector.ShootTypes)
            {
                IGunType.Type curent = shoot.GunType;

                if (curent == type)
                    shootType = shoot;
            }

            return shootType;
        }
    }
}