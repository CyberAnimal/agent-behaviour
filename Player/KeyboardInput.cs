using System;
using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(PlayerMove))]
    [RequireComponent(typeof(Collision))]
    public class KeyboardInput : MonoBehaviour
    {
        [SerializeField] private GameObject Player;

        private Player _player;
        private PlayerMove _movement;
        private JumpAnimation _jump;
        private Collision _collision;

        public event System.Action Shoot;

        private void Start()
        {
            if (Player == null)
                Player = gameObject;

            _player = Player.GetComponent<Player>();
            _movement = GetComponent<PlayerMove>();
            _jump = GetComponent<JumpAnimation>();
            _collision = GetComponent<Collision>();
        }

        private void FixedUpdate()
        {
            InputMove();
            InputJump();
            CanShoot();
        }

        private void InputMove()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveX, 0f, moveZ);

            _movement.Move(movement);
        }

        private void InputJump()
        {
            if (Input.GetAxis("Jump") > 0)
            {

            }
        }

        private void CanShoot()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
                Shoot?.Invoke();
        }
    }
}
