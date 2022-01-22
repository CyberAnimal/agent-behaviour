using UnityEngine;

namespace Game.Enemy
{
    public class SwimingArguments : ScriptableObject
	{
		[SerializeField, Range(0f, 100f)]
		private float _maxSwimSpeed = 5f, _maxSwimAcceleration = 5f;

		public float MaxSwimSpeed => _maxSwimSpeed;
		public float MaxSwimAcceleration => _maxSwimAcceleration;

		[SerializeField] private LayerMask _waterMask = 0;

		public LayerMask WaterMask => _waterMask;
		//
		[SerializeField] private Transform _playerInputSpace = default, _ball = default;
		[SerializeField] private float _submergenceOffset = 0.5f;
		[SerializeField, Min(0.1f)] private float _submergenceRange = 1f;

		public float SubmergenceOffset => _submergenceOffset;
		public float SubmergenceRange => _submergenceRange;
		public Transform PlayerInputSpace => _playerInputSpace;
		public Transform Ball => _ball;

		[SerializeField, Min(0f)] private float _buoyancy = 1f;
		[SerializeField, Range(0f, 10f)] private float _waterDrag = 1f;
		[SerializeField, Range(0.01f, 1f)] private float _swimThreshold = 0.5f;
		[SerializeField]
		private Material _normalMaterial = default,
										  _swimmingMaterial = default;
		public float Buoyancy => _buoyancy;
		public float WaterDrag => _waterDrag;
		public float SwimThreshold => _swimThreshold;

		[SerializeField, Min(0.1f)] private float _ballRadius = 0.5f;
		[SerializeField, Min(0f)] private float _ballAlignSpeed = 180f;
		[SerializeField, Min(0f)]
		private float _ballAirRotation = 0.5f,
												_ballSwimRotation = 2f;
		public float BallRadius => _ballRadius;
		public float BallAlignSpeed => _ballAlignSpeed;
		public float BallAirRotation => _ballAirRotation;
		public float BallSwimRotation => _ballSwimRotation;
	}
}