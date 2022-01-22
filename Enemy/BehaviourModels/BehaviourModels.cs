using System;
using UnityEngine;

namespace Game.Enemy
{
    [RequireComponent(typeof(AttackModels), typeof(MoveModels))]
    [RequireComponent(typeof(SenseModels), typeof(WalkModels))]
    public class BehaviourModels : MonoBehaviour, IBehavioursInitialize, IBehavioursExecute
    {
        [SerializeField] private AttackModels AttackModel;
        [SerializeField] private MoveModels MoveModel;
        [SerializeField] private SenseModels SenseModel;
        [SerializeField] private WalkModels WalkModel;

        public void Initialize(AttackType attackType, MoveType moveType, SenseType senseType, WalkType walkType)
        {
            SetAttackModel(attackType);
            SetMoveModel(moveType);
            SetSenseModel(senseType);
            SetWalkModel(walkType);
        }

        public void SetAttackModel(AttackType attackType) => AttackModel.Set(attackType);
        public void SetMoveModel(MoveType moveType) => MoveModel.Set(moveType);
        public void SetSenseModel(SenseType senseType) => SenseModel.Set(senseType);
        public void SetWalkModel(WalkType walkType) => WalkModel.Set(walkType);

        public void SubscribeOnDetection(Action<GameObject, Sense> subscriber) => SenseModel.Subscribe(subscriber);
        public void UnsubscribeOnDetection(Action<GameObject, Sense> subscriber) => SenseModel.Unsubscribe(subscriber);

        public void TriggerEnter(Collider other) => SenseModel.TriggerEnter(other);
        public void TriggerStay(Collider other) => SenseModel.TriggerStay(other);
        public void TriggerExit(Collider other) => SenseModel.TriggerExit(other);

        public void CanUpdate() => SenseModel.CanUpdate();
        public void CanAttack() => AttackModel.Attack();
        public void Move(Vector3 direction) => MoveModel.Move(direction);
    }

    public interface IBehavioursInitialize
    {
        public void Initialize(AttackType attackType, MoveType moveType, SenseType senseType, WalkType walkType);

        public void SubscribeOnDetection(Action<GameObject, Sense> subscriber);
        public void UnsubscribeOnDetection(Action<GameObject, Sense> subscriber);
    }

    public interface IBehavioursExecute
    {
        public void TriggerEnter(Collider other);
        public void TriggerStay(Collider other);
        public void TriggerExit(Collider other);

        public void CanUpdate();
        public void CanAttack();
        public void Move(Vector3 direction);
    }
}
