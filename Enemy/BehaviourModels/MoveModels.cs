using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class MoveModels : MonoBehaviour
    {
        private readonly Dictionary<MoveType, Move> _models =
            new Dictionary<MoveType, Move>()
            {

            };

        private MoveType _type;

        public void Set(MoveType type) => _type = type;

        public void Move(Vector3 direction)
        {
           
        }
    }
}

