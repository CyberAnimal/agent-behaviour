using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class Action : MonoBehaviour
    {
        public List<Situation> Situations = new List<Situation>();

        private float _progress = 0.0f;
        public bool IsComplete => _progress >= 100.0f;

        public void CanAction(Vector3 target) { }
    }
}
