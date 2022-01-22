using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collision))]
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private GameObject Player;
        [SerializeField] private Camera Camera;
        [SerializeField] private float Speed;

        private Rigidbody _rigidbody;
        private ImaginaryNumber _imaginaryNumber;
        private Collision _collision;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collision = GetComponent<Collision>();
            _imaginaryNumber = new ImaginaryNumber();
        }

        private void OnEnable()
        {
            Collision.Changed += ChangeDirection;
        }

        private void OnDisable()
        {
            Collision.Changed -= ChangeDirection;
        }

        public void Move(Vector3 movement)
        {
            movement = Vector3.ClampMagnitude(movement, Speed);
            movement = movement * Speed * Time.fixedDeltaTime;

            Vector3 local = GetLocalCoordinates(movement);
            _rigidbody.AddForce(local);

            //Debug.Log("Direction " + _imaginaryNumber.Direction.ToString());
        }

        private Vector3 GetLocalCoordinates(Vector3 movement)
        {
            Vector3 modified;
            modified = _imaginaryNumber.ModifyDirection(movement);

            return modified;
        }

        private void ChangeDirection(int value)
        {
            _imaginaryNumber.Change(value);
        }
    }

    public class ImaginaryNumber
    {
        public enum ChangeDirection
        {
            Original = 0,
            TurnInRight = 1,
            TurnAround = 2,
            TurnInLeft = 3
        }

        private ChangeDirection _changeDirection = ChangeDirection.TurnInRight;
        public ChangeDirection Direction => _changeDirection;

        public void Change(int imaginaryNumber)
        {
            _changeDirection = SetDirection(imaginaryNumber);
        }

        private ChangeDirection SetDirection(int imaginaryNumber)
        {
            ChangeDirection change = _changeDirection + imaginaryNumber;

            if ((int)change == 4)
                change = ChangeDirection.Original;

            if ((int)change == -1)
                change = ChangeDirection.TurnInLeft;

            return change;
        }

        public Vector3 ModifyDirection(Vector3 vector)
        {
            Vector3 modified = _changeDirection switch
            {
                ChangeDirection.TurnInRight => MultiplyOnNegative(vector),
                ChangeDirection.TurnAround => Squaring(vector),
                ChangeDirection.TurnInLeft => MultiplyOnPositive(vector),
                ChangeDirection.Original => vector,
                _ => vector
            };

            return modified;
        }

        private Vector3 MultiplyOnNegative(Vector3 vector)
        {
            float aroundAxisX = -vector.x;
            Vector3 transformedVector = new Vector3(vector.z, vector.y, aroundAxisX);

            return transformedVector;
        }

        private Vector3 Squaring(Vector3 vector)
        {
            float aroundAxisX = -vector.x;
            float aroundAxisZ = -vector.z;
            Vector3 transformedVector = new Vector3(aroundAxisX, vector.y, aroundAxisZ);

            return transformedVector;
        }

        private Vector3 MultiplyOnPositive(Vector3 vector)
        {
            float aroundAxisZ = -vector.z;
            Vector3 transformedVector = new Vector3(aroundAxisZ, vector.y, vector.x);

            return transformedVector;
        }
    }
}
