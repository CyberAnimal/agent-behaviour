using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class Clip : IClip
    {
        public Stack<IPatron> Patrons { get; private set; }

        private IPatron _patron;
        private uint _size;
        private float _damage;

        public Clip(uint size, float damage)
        {
            _size = size;
            _damage = damage;
            Patrons = new Stack<IPatron>((int)_size);
            FillClip(_damage);
        }

        public void ReductionClip()
        {
            var patron = Patrons.Pop();
        }

        public void TakeNew()
        {
            Patrons = new Stack<IPatron>((int)_size);
            FillClip(_damage);
        }

        private void FillClip(float damage)
        {
            for (int i = 0; i < _size; i++)
            {
                var patron = new Patron(damage);
                Patrons.Push(patron);
            }
        }
    }
}
