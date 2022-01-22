using System.Collections.Generic;

namespace Game.Player
{
    public class Weapon : IWeapon
    {
        private GunGenerator _generator;

        private IGun _currentGun;
        public IGun CurrentGun => _currentGun;

        private List<IGun> _guns;
        public List<IGun> Guns => _guns;

        public Weapon()
        {
            _guns = new List<IGun>();
            SetDefaultGuns();
        }

        public void PutGunOnPlayer(IGun gun)
        {
            IGun gunForWeapon = CurrentGun;

            foreach (IGun curent in Guns)
            {
                if (curent == gun)
                {
                    Guns.Add(gunForWeapon);
                    _currentGun = curent;
                    Guns.Remove(CurrentGun);
                }
            }
        }

        public void AddGun(IGun gun)
        {
            if (gun != null)
                Guns.Add(gun);
        }

        public void RemoveGun(IGun gun)
        {
            foreach (IGun element in Guns)
            {
                if (element.GunType == gun.GunType)
                    Guns.Remove(element);
            }
        }

        private void SetDefaultGuns()
        {
            _generator = new GunGenerator();
            _guns = _generator.GenerateAllGuns();
        }
    }

    public class GunGenerator
    {
        private List<IGunType.Type> _types;

        public GunGenerator()
        {
            GenerateAllGuns();
        }

        public IGun Generate(IGunType.Type type)
        {
            return new Gun(type);
        }

        public List<IGun> GenerateAllGuns()
        {
            List<IGun> guns = new List<IGun>();

            for (int i = 0; i < _types.Count; i++)
            {
                IGunType.Type type = _types[i];
                IGun gun = Generate(type);
                guns.Add(gun);
            }

            return guns;
        }

        private void SetToList()
        {
            _types = new List<IGunType.Type>();
            for (int i = 0; i < 4; i++)
            {
                IGunType.Type type = (IGunType.Type)System.Enum.GetValues(typeof(IGunType.Type)).GetValue(i);
                _types.Add(type);
            }
        }
    }
}

    