using System;
using UnityEngine;

namespace Game.Enemy
{
    [RequireComponent(typeof(BehaviourModels))]
    public class DesicionsMaker : MonoBehaviour
    {
        private Enemy _enemy;
        private IBehavioursInitialize _behavioursInitialize;
        private IBehavioursExecute _behavioursExecute;
        private ChoiceDirection _choiceDirection;

        private void Start()
        {
            _enemy = GetComponent<Enemy>();

            _behavioursInitialize = GetComponent<BehaviourModels>();
            _behavioursExecute = GetComponent<BehaviourModels>();

            _behavioursInitialize.Initialize(_enemy.AttackType, _enemy.MoveType,
                                             _enemy.SenseType, _enemy.WalkType);
        }

        public void CanEnable(Action<GameObject, Sense> subscriber) => _behavioursInitialize.SubscribeOnDetection(subscriber);
        public void CanDisable(Action<GameObject, Sense> subscriber) => _behavioursInitialize.UnsubscribeOnDetection(subscriber);

        private void OnTriggerEnter(Collider other) => _behavioursExecute.TriggerEnter(other);
        private void OnTriggerStay(Collider other) => _behavioursExecute.TriggerStay(other);
        private void OnTriggerExit(Collider other) => _behavioursExecute.TriggerExit(other);

        private void Update() => _behavioursExecute.CanUpdate();

        private void GetRoute(Vector3 direction)
        {
            Route route = _choiceDirection.GetRoute(_enemy.transform, direction);
            Vector3 rotation = _enemy.transform.rotation.AddAngular(route.Angular);

            _enemy.transform.Rotate(rotation);
            Move(route.Linear);
        }
        
        public void Move(Vector3 direction) => _behavioursExecute.Move(direction);

        public void Attack() => _behavioursExecute.CanAttack();
    }
    
    public static class Extention
    {
        public static Vector3 AddAngular(this Quaternion rotation, float angular)
        {
            float y = rotation.y;
            y += angular;
            rotation.y = y;

            Vector3 newRotation = rotation.eulerAngles;

            return newRotation;
        }
    }
}
