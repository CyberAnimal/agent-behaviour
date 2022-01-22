using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public readonly struct Locks
    {
        private readonly SideOrientation[] _sides;
        public SideOrientation[] Sides => _sides;

        public Locks(params SideOrientation[] orientations)
        {
            _sides = orientations.Clone() as SideOrientation[];
        }
    }
}
