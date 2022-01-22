using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class Patron : IPatron
    {
        private float _damage;

        public Patron(float damage) => _damage = damage;

        public float MakeDamage() => _damage;
    }
}
