using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class GunAdapter
    {
        public float Damage { get; private set; }
        public float AttackSpeed { get; private set; }
        public uint ClipSize { get; private set; }

        private GunAdapter(float damage, float attackSpeed, uint clipSize)
        {
            Damage = damage;
            AttackSpeed = attackSpeed;
            ClipSize = clipSize;
        }

        public static GunAdapter CreateAdapter(IGunType.Type gunType)
        {
            var type = new GunType();
            var parametrs = type.ParametrsOfType[(GunType.Type)gunType];

            return new GunAdapter(parametrs.Damage, parametrs.AttackSpeed, parametrs.ClipSize);
        }
    }
}