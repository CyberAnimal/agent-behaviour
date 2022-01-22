using UnityEngine;

namespace Game.Enemy
{
    public readonly struct ConditionData
    {
        private readonly Enemy _enemy;
        private readonly GameObject _target;
        private readonly float _ratio;

        public Enemy Enemy => _enemy;
        public GameObject Target => _target;

        public Vector3 EnemyPosition => _enemy.transform.position;
        public Vector3 TargetPosition => _target.transform.position;
        public float Ratio => _ratio;

        public float Health => _enemy.Health;
        public float Armor => _enemy.Armor;
        public float Damage => _enemy.Damage;
        public float Speed => _enemy.Speed;

        public ConditionData(Enemy enemy, GameObject target, float ratio)
        {
            _enemy = enemy;
            _target = target;
            _ratio = ratio;
        }
    }
}