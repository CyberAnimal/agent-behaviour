using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class Navigator : MonoBehaviour
    {
        private SenseModels _senseModels;
        private Action _actionType;
        public Action ActionType => _actionType;

        public void SetModelsList(SenseType type)
        {
            if (type == SenseType.Vision)
                gameObject.AddComponent<Visor>();

            if (type == SenseType.Hearing)
                gameObject.AddComponent<SoundEmitter>();
        }

        void AddComponent<T>(SenseModel senseModel) where T : Component
        {
            gameObject.AddComponent<SenseModel>();
        }







        //private void Update()
        //{
        //    if (_soundEmitter.IsEmit())
        //    {
        //        CanAction(_soundEmitter.ReceiverPosition);
        //    }
        //}

        //private void OnEnable()
        //{
        //    _visor.ObjectDetected += OnMoving;
        //    _visor.ObjectStanding += CanAction;
        //    _visor.ObjectGoned += OnWaiting;
        //}

        //private void OnDisable()
        //{
        //    _visor.ObjectDetected -= OnMoving;
        //    _visor.ObjectStanding -= CanAction;
        //    _visor.ObjectGoned -= OnWaiting;
        //}

        //private void CanAction(Vector3 playerPosition)
        //{

        //}

        //private void OnMoving()
        //{
        //    _actionType = EnemyActionType.Move;
        //}

        //private void OnWaiting()
        //{
        //    _actionType = EnemyActionType.Wait;
        //}
    }

}
