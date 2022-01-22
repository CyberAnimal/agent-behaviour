using UnityEngine;

namespace Game.Enemy
{
    public class WalkModels : MonoBehaviour
    {
        private WalkType _type;

        public void Set(WalkType type) => _type = type;
    }
}