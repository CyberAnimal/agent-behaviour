using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class Smeller : SenseModel, ISence
    {
        private Sense _sense = Sense.Smell;
        private Vector3 _target;
        private Dictionary<int, GameObject> _particles;

        public override event Action<GameObject, Sense> ObjectDetected;

        private void Start()
        {
            _particles = new Dictionary<int, GameObject>();
        }

        public override void TriggerEnter(Collider other)
        {
            GameObject obj = other.gameObject;
            OdourParticle particle;

            particle = obj.GetComponent<OdourParticle>();

            if (particle == null)
                return;

            int objId = obj.GetInstanceID();
            _particles.Add(objId, obj);

            UpdateTarget();
        }

        public override void TriggerExit(Collider other)
        {
            GameObject obj = other.gameObject;
            int objId = obj.GetInstanceID();

            bool isRemoved;
            isRemoved = _particles.Remove(objId);

            if (!isRemoved)
                return;

            UpdateTarget();
        }

        public override void TriggerStay(Collider other) { }
        public override void CanUpdate() { }

        private void UpdateTarget()
        {
            Vector3 centroid = Vector3.zero;

            foreach (GameObject particle in _particles.Values)
            {
                Vector3 position = particle.transform.position;
                centroid += position;
            }

            _target = centroid;

            ObjectDetected?.Invoke(_target, _sense);
        }

        public Vector3? GetTargetPosition()
        {
            if (_particles.Keys.Count == 0)
                return null;

            return _target;
        }
    }
}
