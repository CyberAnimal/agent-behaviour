using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class GunType : IGunType
    {
        public enum Type
        {
            Pistol = 0,
            Automatic = 1,
            Shotgun = 2,
            MachineGun = 3
        }

        public class GunParametrs
        {
            private readonly float _damage;
            private readonly float _attackSpeed;
            private readonly uint _clipSize;
            public float Damage => _damage;
            public float AttackSpeed => _attackSpeed;
            public uint ClipSize => _clipSize;

            public GunParametrs(float damage, float attackSpeed, uint clipSize)
            {
                _damage = damage;
                _attackSpeed = attackSpeed;
                _clipSize = clipSize;
            }
        }

        public Dictionary<Type, GunParametrs> ParametrsOfType { get; }
             = new Dictionary<Type, GunParametrs>
             {
                 [Type.Automatic] = new GunParametrs(GetRandom(0, 1.0f), GetRandom(0, 1.0f), GetRandom(10, 20)),
                 [Type.MachineGun] = new GunParametrs(GetRandom(0, 1.0f), GetRandom(0, 1.0f), GetRandom(10, 20)),
                 [Type.Pistol] = new GunParametrs(GetRandom(0, 1.0f), GetRandom(0, 1.0f), GetRandom(10, 20)),
                 [Type.Shotgun] = new GunParametrs(GetRandom(0, 1.0f), GetRandom(0, 1.0f), GetRandom(10, 20)),
             };

        private static float GetRandom(float min, float max)
        {
            return Random.Range(min, max);
        }

        private static uint GetRandom(uint min, uint max)
        {
            return (uint)UnityEngine.Random.Range(min, max);
        }
    }
}
