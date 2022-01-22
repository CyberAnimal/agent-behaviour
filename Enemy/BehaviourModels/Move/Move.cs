using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(ChoiceDirection))]
    public class Move : MonoBehaviour
    {
        [SerializeField] private GameObject Enemy;
        [SerializeField] private float Speed;

        private Rigidbody _rigidbody;
        private ChoiceDirection _direction;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _direction = GetComponent<ChoiceDirection>();
        }

        public void Movement(Vector3 movement)
        {
            movement = Vector3.ClampMagnitude(movement, Speed);
            movement = movement * Speed * Time.deltaTime;

            _rigidbody.AddForce(movement);
        }
    }
}
