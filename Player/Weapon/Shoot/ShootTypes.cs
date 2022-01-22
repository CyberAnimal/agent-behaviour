using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    [CreateAssetMenu]
    public class ShootTypes : ScriptableObject
    {
        [SerializeField] private List<Shoot> _shootTypes = new List<Shoot>(4);
        public List<Shoot> Types => _shootTypes;
    }
}