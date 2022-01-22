using UnityEngine;

namespace Game.Enemy
{
    public class ClimbingArguments : ScriptableObject
	{
		[SerializeField, Range(0f, 100f)]
		private float _maxClimbSpeed = 4f,
														_maxClimbAcceleration = 40f;

		public float MaxClimbSpeed => _maxClimbSpeed;
		public float MaxClimbAcceleration => _maxClimbAcceleration;

		[SerializeField, Range(0, 90)] private float _maxGroundAngle = 25f, _maxStairsAngle = 50f;
		[SerializeField, Range(0f, 10f)] private float _jumpHeight;
		[SerializeField, Range(0, 5)] private int _maxAirJumps = 0;
		[SerializeField, Min(0f)] private float _probeDistance = 1f;
		[SerializeField] private LayerMask _climbMask = -1;

		public float MaxGroundAngle => _maxGroundAngle;
		public float MaxStairsAngle => _maxStairsAngle;
		public float JumpHeight => _jumpHeight;
		public float MaxAirJumps => _maxAirJumps;
		public float ProbeDistance => _probeDistance;
		public LayerMask ClimbMask => _climbMask;
		//
		[SerializeField, Range(90, 170)] private float _maxClimbAngle = 140f;
		[SerializeField] private Transform _playerInputSpace = default, _ball = default;
		[SerializeField] private float _submergenceOffset = 0.5f;
		[SerializeField, Min(0.1f)] private float _submergenceRange = 1f;
		public float MaxClimbAngle => _maxClimbAngle;
		public float SubmergenceOffset => _submergenceOffset;
		public float SubmergenceRange => _submergenceRange;
		public Transform PlayerInputSpace => _playerInputSpace;
		public Transform Ball => _ball;

		[SerializeField, Min(0f)] private float _buoyancy = 1f;
		[SerializeField] private Material _climbingMaterial = default;

		public Material ClimbingMaterial => _climbingMaterial;
		public float Buoyancy => _buoyancy;
	}
}