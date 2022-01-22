using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    [Serializable]
    public class SenseModels : MonoBehaviour
    {
        private readonly Dictionary<SenseType, SenseModel> _models = new Dictionary<SenseType, SenseModel>()
        {
            [SenseType.Vision] = new Visor(),
            [SenseType.Hearing] = new SoundEmitter()
        };

        private SenseType _type;

        public void Set(SenseType type)
        {
            _type = type;
            AddComponent(type);
        }

        public void Subscribe(Action<GameObject, Sense> subscriber) => _models[_type].ObjectDetected += subscriber;

        public void Unsubscribe(Action<GameObject, Sense> subscriber) => _models[_type].ObjectDetected -= subscriber;

        public void CanUpdate() => _models[_type].CanUpdate();

        public void TriggerEnter(Collider other) => _models[_type].TriggerEnter(other);

        public void TriggerStay(Collider other) => _models[_type].TriggerStay(other);

        public void TriggerExit(Collider other) => _models[_type].TriggerExit(other);

        private void AddComponent(SenseType type)
        {
            if (type == SenseType.Vision)
                gameObject.AddComponent<Visor>();

            if (type == SenseType.Hearing)
                gameObject.AddComponent<SoundEmitter>();
        }
    }
}