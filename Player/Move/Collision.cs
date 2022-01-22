using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class Collision : MonoBehaviour
    {
        [SerializeField] private List<GameObject> Turnings = new List<GameObject>();
        [SerializeField] private Camera SelfCamera;
        [SerializeField] private GameObject Player;

        private static Turning _turning;
        private static bool _isEquals;
        private static bool _isGrounded;

        public static event Action<int> Changed = default;

        private void OnCollisionEnter(Collider coll) => IsGroundedUpate(coll, true);

        private void OnCollisionExit(Collider coll) => IsGroundedUpate(coll, false);

        private void OnTriggerEnter(Collider other)
        {
            _isEquals = CheckTurnAround(other);

            ChangedTurning();
        }

        private void OnTriggerExit(Collider other)
        {
            other.gameObject.SetActive(false);
            _isEquals = false;
        }

        private bool CheckTurnAround(Collider other)
        {
            bool isEquals = false;

            Turning turning = other.gameObject.GetComponent<Turning>();
            if (turning != null)
            {
                isEquals = true;
                _turning = turning;
            }

            return isEquals;
        }

        private static void ChangedTurning()
        {
            int direction = 1;

            if (_turning.SelfDirection == Turning.Direction.Right)
                direction = 1;

            if (_turning.SelfDirection == Turning.Direction.Left)
                direction = -1;

            if (_isEquals && Changed != null)
                Changed(direction);
        }

        private Surface.Type GetType(Collider coll) => coll.GetComponent<Surface>().SelfType;

        private void IsGroundedUpate(Collider coll, bool value)
        {
            if (coll.gameObject.tag == ("Ground"))
                _isGrounded = value;
        }
    }

    public class Surface
    {
        public enum Type
        {
            Wall,
            Plane
        }

        [SerializeField] private Type Self;
        public Type SelfType => Self;
    }
}
