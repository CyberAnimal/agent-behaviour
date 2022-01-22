using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private EnemyFactory _enemyFactory;

        private List<GameObject> _enemys;

		private void Awake() => _enemys = new List<GameObject>();

        public void CreateEnemy(EnemyType type)
        {
            Enemy enemy = _enemyFactory.Get(type);

            _enemys.Add(enemy.gameObject);
        }
    }
}
