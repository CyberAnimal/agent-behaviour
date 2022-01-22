using UnityEngine;

namespace Game.Enemy
{
    public class SurfaceArguments : ScriptableObject
	{
		[SerializeField, Range(0f, 100f)]
		private float _maxSpeed, _maxAcceleration = 10f,
					  _maxAirAcceleration = 1f, _maxSnapSpeed = 100f;

		public float MaxSpeed => _maxSpeed;
		public float MaxAcceleration => _maxAcceleration;
		public float MaxAirAcceleration => _maxAirAcceleration;
		public float MaxSnapSpeed => _maxSnapSpeed;

		[SerializeField, Range(0, 90)] private float _maxGroundAngle = 25f, _maxStairsAngle = 50f;
		[SerializeField, Range(0f, 10f)] private float _jumpHeight;
		[SerializeField, Range(0, 5)] private int _maxAirJumps = 0;
		[SerializeField, Min(0f)] private float _probeDistance = 1f;
		[SerializeField]
		private LayerMask _probeMask = -1, _stairsMask = -1;

		public float MaxGroundAngle => _maxGroundAngle;
		public float MaxStairsAngle => _maxStairsAngle;
		public float JumpHeight => _jumpHeight;
		public float MaxAirJumps => _maxAirJumps;
		public float ProbeDistance => _probeDistance;
		public LayerMask ProbeMask => _probeMask;
		public LayerMask StairsMask => _stairsMask;
	}
}