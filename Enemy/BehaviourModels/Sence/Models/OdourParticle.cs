using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class OdourParticle : MonoBehaviour
    {
        private float _timer;

        public float Timespan;
        public int Parent;

        private void Start()
        {
            if (Timespan < 0f)
                Timespan = 0f;

            _timer = Timespan;
        }

        private void Update()
        {
            _timer -= Time.deltaTime;

            if (_timer < 0f)
                Destroy(gameObject);
        }
    }
}
