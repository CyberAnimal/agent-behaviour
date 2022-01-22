using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public abstract class SenseModel : MonoBehaviour
    {
        public abstract event Action<GameObject, Sense> ObjectDetected;

        public abstract void CanUpdate();
        public abstract void TriggerEnter(Collider other);
        public abstract void TriggerStay(Collider other);
        public abstract void TriggerExit(Collider other);
    }
}