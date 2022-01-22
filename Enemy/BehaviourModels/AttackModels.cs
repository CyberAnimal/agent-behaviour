using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    [System.Serializable]
    public class AttackModels : MonoBehaviour
    {
        private readonly Dictionary<AttackType, Attack> _models =
            new Dictionary<AttackType, Attack>()
            {
                [AttackType.Automatic] = new AutomaticShoot(),
                [AttackType.Balistic] = new BallisticShoot(),
                [AttackType.Pistol] = new PistolShoot(),
                [AttackType.Collision] = new CollisionAttack()
            };

        private AttackType _type;

        public void Set(AttackType type)
        {
            _type = type;
            AddComponent(type);
        }

        public void Attack() => _models[_type].CanAttack();

        private void AddComponent(AttackType type)
        {
            if (type == AttackType.Automatic)
                gameObject.AddComponent<AutomaticShoot>();

            if (type == AttackType.Balistic)
                gameObject.AddComponent<BallisticShoot>();

            if (type == AttackType.Pistol)
                gameObject.AddComponent<PistolShoot>();

            if (type == AttackType.Collision)
                gameObject.AddComponent<CollisionAttack>();
        }
    }
}
