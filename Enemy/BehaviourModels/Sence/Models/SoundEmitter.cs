using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class SoundEmitter : SenseModel, ISence
    {
        [SerializeField] private float _soundAttenuation;
        [SerializeField] private GameObject _emitterObject;

        private Sense _sense = Sense.Sound;

        public override event Action<GameObject, Sense> ObjectDetected;

        private Vector3 _receiverPosition;
        public Vector3 ReceiverPosition => _receiverPosition;

        private Dictionary<int, SoundReceiver> _receivers;
        private Dictionary<WallType, float> _wallTypes;

        void Start()
        {
            _receivers = new Dictionary<int, SoundReceiver>();

            if (_emitterObject == null)
                _emitterObject = gameObject;
        }

        public override void CanUpdate() { }

        public override void TriggerEnter(Collider other)
        {
            SoundReceiver receiver;
            receiver = other.gameObject.GetComponent<SoundReceiver>();

            if (receiver == null)
                return;

            int objId = other.gameObject.GetInstanceID();
            _receivers.Add(objId, receiver);
        }

        public override void TriggerStay(Collider other)
        {
            SoundReceiver receiver;
            receiver = other.gameObject.GetComponent<SoundReceiver>();

            if (receiver == null)
                return;

            if (IsEmit())
                ObjectDetected?.Invoke(receiver.gameObject, _sense);
        }

        public override void TriggerExit(Collider other)
        {
            SoundReceiver receiver;
            receiver = other.gameObject.GetComponent<SoundReceiver>();

            if (receiver == null)
                return;

            int objId = other.gameObject.GetInstanceID();
            _receivers.Remove(objId);
        }

        public bool IsEmit()
        {
            GameObject receiverObject;
            Vector3 receiverPosition;
            float intensity;
            float distance;
            Vector3 emitterPosition = _emitterObject.transform.position;

            foreach (SoundReceiver receiver in _receivers.Values)
            {
                receiverObject = receiver.gameObject;
                receiverPosition = receiverObject.transform.position;
                distance = Vector3.Distance(receiverPosition, emitterPosition);

                intensity = receiver.Intensity;
                intensity -= _soundAttenuation * distance;
                intensity -= GetWallAttenuation(emitterPosition, receiverPosition);

                if (intensity < receiver.Threshold)
                    continue;

                else
                {
                    _receiverPosition = receiverPosition;
                    return true;
                }

                //receiver.Receive(intensity, emitterPosition);
            }

            return false;
        }

        private float GetWallAttenuation(Vector3 emitterPosition, Vector3 receiverPosition)
        {
            float attenuation = 0f;
            Vector3 direction = receiverPosition - emitterPosition;
            float distance = direction.magnitude;
            direction.Normalize();

            Ray ray = new Ray(emitterPosition, direction);
            RaycastHit[] hits = Physics.RaycastAll(ray, distance);
            int i;

            for (i = 0; i < hits.Length; i++)
            {
                GameObject obj;

                obj = hits[i].collider.gameObject;
                TypeOfWall objectType = obj.GetComponent<TypeOfWall>();

                if (objectType != null)
                {
                    WallType wallType = objectType.WallType;
                    attenuation += _wallTypes[wallType];
                }
            }

            return attenuation;
        }
    }
}
