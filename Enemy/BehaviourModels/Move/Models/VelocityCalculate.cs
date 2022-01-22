using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class VelocityCalculate
	{
		private class MoveData
		{
			public int GroundContactCount = 0;
			public int SteepContactCount = 0;
			public int JumpPhase = 0;
			public int MaxAirJumps = 0;
			public int ClimbContactCount = 0;
			public int StepsSinceLastJump = 0;
			public int StepsSinceLastGrounded = 0;
			public int Submergence = 0;
		}
		private MoveData _data = new MoveData();

		private SurfaceArguments _arguments = new SurfaceArguments();

		private readonly Rigidbody _body;
		private Rigidbody _connectedBody, _previousConnectedBody;
		private Vector3 _velocity, _desiredVelocity;
		private Vector3 _contactNormal, _steepNormal;
		private Vector3 _upAxis, _rightAxis, _forwardAxis;
		private Vector3 _climbNormal, _lastClimbNormal;
		private Vector3 _lastContactNormal, _lastSteepNormal, _lastConnectionVelocity;
		private Vector3 _playerInput, _connectionVelocity;
		private Vector3 _connectionWorldPosition, _connectionLocalPosition;
		private MoveState _state;

		private bool _isDesiredJump, _isDesiresClimbing;
		private int _groundContactCount;
		private int _jumpPhase;
		private float _minGroundDotProduct, _minStairsDotProduct, _minClimbDotProduct;
		private float _submergence;

		public Vector3 Velocity => _velocity;

		public VelocityCalculate(Rigidbody body) => _body = body;

		private bool CheckClimbing()
		{
			if (!StateClimbing())
				return false;

			if (IsManyClimbContact())
				if (ClimbDone())
					_climbNormal = _lastClimbNormal;

			_data.GroundContactCount = 1;
			_contactNormal = _climbNormal;

			return true;
		}

		private bool CheckSwimming()
		{
			if (!StateSwiming())
				return false;

			_data.GroundContactCount = 0;
			_contactNormal = _upAxis;

			return true;
		}

		private bool CheckSteepContacts()
		{
			if (OneSteepContact() && SmallGroundDot())
				return false;

			_data.SteepContactCount = 0;
			_data.GroundContactCount = 1;
			_contactNormal = _steepNormal;

			return true;
		}

		private bool SnapToGround()
		{
			if (SnapSteps() && SpeedExceed() &&
				ExceedProbeDistance() && MinimalDot())
				return false;

			if (IsClimb())
			{
				float dot = Vector3.Dot(Velocity, DownAxisHit().normal);
				float speed = Velocity.magnitude;

				_velocity = (Velocity - (DownAxisHit().normal * dot)).normalized * speed;
			}

			_data.GroundContactCount = 1;
			_connectedBody = DownAxisHit().rigidbody;

			return true;
		}

		private bool StateClimbing() => _state == MoveState.Climbing;

		private bool StateSwiming() => _state == MoveState.Swimming;

		private bool InWater() => _state == MoveState.InWater;

		private bool OnGround() => _state == MoveState.OnGround;

		private bool OnSteep() => _state == MoveState.OnSteep;

		private bool IsManyClimbContact() => _data.ClimbContactCount > 1;

		private bool SnapSteps() => _data.StepsSinceLastGrounded > 1 ||
									_data.StepsSinceLastJump <= 2 ||
									_state != MoveState.InWater;

		private bool SpeedExceed() => Velocity.magnitude > _arguments.MaxSnapSpeed;

		private RaycastHit DownAxisHit()
		{
			Physics.Raycast(_body.position, -_upAxis, out RaycastHit hit,
							_arguments.ProbeDistance, _arguments.ProbeMask, QueryTriggerInteraction.Ignore);

			return hit;
		}

		private bool ExceedProbeDistance() => !Physics.Raycast(_body.position, -_upAxis, out RaycastHit hit,
															   _arguments.ProbeDistance, _arguments.ProbeMask,
															   QueryTriggerInteraction.Ignore);

		private bool MinimalDot() => Vector3.Dot(_upAxis, DownAxisHit().normal) <
									 GetMinDot(DownAxisHit().collider.gameObject.layer);

		protected virtual float GetMinDot(int layer) => (_arguments.StairsMask & (1 << layer)) == 0 ?
														 _minGroundDotProduct : _minStairsDotProduct;

		private bool OneSteepContact() => _data.SteepContactCount <= 1;

		private bool ManyGroundContact() => _data.GroundContactCount > 1;

		private bool ManyStepsJumpSince() => _data.StepsSinceLastJump > 1;

		private bool SmallGroundDot()
		{
			if (VectorIsNull(_steepNormal))
				return false;

			return
				Vector3.Dot(_upAxis, _steepNormal.normalized) <
				_minGroundDotProduct ? true : false;
		}

		private bool ClimbDone()
		{
			if (VectorIsNull(_climbNormal))
				return false;

			return
				Vector3.Dot(_upAxis, _climbNormal.normalized) >=
				_minGroundDotProduct ? true : false;
		}

		private bool VectorIsNull(Vector3 vector) => vector.magnitude < float.Epsilon;

		private bool IsClimb() => Vector3.Dot(Velocity, DownAxisHit().normal) > 0f;

		private bool InJump() => _arguments.MaxAirJumps > 0 &&
								 _data.JumpPhase <= _arguments.MaxAirJumps;

		public VelocityCalculate CheckState()
		{
			if (_data.GroundContactCount > 0)
				_state = MoveState.OnGround;

			if (_data.SteepContactCount > 0)
				_state = MoveState.OnSteep;

			if (_state == MoveState.OnGround ||
				_data.JumpPhase >= _data.MaxAirJumps)
				_state = MoveState.OnGround;

			if (_data.ClimbContactCount > 0 &&
				_data.StepsSinceLastJump > 2)
				_state = MoveState.Climbing;

			if (_data.Submergence > 0f)
				_state = MoveState.InWater;

			if (_data.Submergence >= _arguments.SwimThreshold)
				_state = MoveState.Swimming;

			return this;
		}

		private Vector3 ProjectOnContactPlane(Vector3 vector) =>
			vector - _contactNormal * Vector3.Dot(vector, _contactNormal);

		private void UpdateState()
		{
			_data.StepsSinceLastGrounded += 1;
			_data.StepsSinceLastJump += 1;
			_velocity = _body.velocity;

			if (CheckClimbing() || CheckSwimming() ||
				_state == MoveState.OnGround ||
				SnapToGround() || CheckSteepContacts())
			{
				_data.StepsSinceLastGrounded = 0;

				if (ManyStepsJumpSince())
					_data.JumpPhase = 0;

				if (ManyGroundContact())
					_contactNormal.Normalize();
			}

			else
				_contactNormal = _upAxis;

			if (_connectedBody)
				if (_connectedBody.isKinematic || _connectedBody.mass >= _body.mass)
					UpdateConnectionState();
		}

		public VelocityCalculate AdjustVelocity()
		{
			Vector3 xAxis = ProjectOnContactPlane(Vector3.right).normalized;
			Vector3 zAxis = ProjectOnContactPlane(Vector3.forward).normalized;

			float currentX = Vector3.Dot(_velocity, xAxis);
			float currentZ = Vector3.Dot(_velocity, zAxis);

			float acceleration = OnGround() ? _arguments.MaxAcceleration : _arguments.MaxAirAcceleration;
			float maxSpeedChange = acceleration * Time.deltaTime;

			float newX =
				Mathf.MoveTowards(currentX, _desiredVelocity.x, maxSpeedChange);
			float newZ =
				Mathf.MoveTowards(currentZ, _desiredVelocity.z, maxSpeedChange);

			_velocity += (xAxis * (newX - currentX)) + (zAxis * (newZ - currentZ));

			return this;
		}

		public VelocityCalculate Jump(Vector3 gravity)
		{
			Vector3 jumpDirection;

			if (OnGround())
				jumpDirection = _contactNormal;

			else if (OnSteep())
			{
				jumpDirection = _steepNormal;
				_data.JumpPhase = 0;
			}

			else if (InJump())
			{
				if (_data.JumpPhase == 0)
					_data.JumpPhase = 1;

				jumpDirection = _contactNormal;
			}

			else return this;

			_data.StepsSinceLastJump = 0;
			_data.JumpPhase += 1;
			float jumpSpeed = Mathf.Sqrt(2f * gravity.magnitude * _arguments.JumpHeight);

			if (InWater())
				jumpSpeed *= Mathf.Max(0f, 1f - _submergence / _arguments.SwimThreshold);

			jumpDirection = (jumpDirection + _upAxis).normalized;
			float alignedSpeed = Vector3.Dot(Velocity, jumpDirection);

			if (alignedSpeed > 0f)
				jumpSpeed = Mathf.Max(jumpSpeed - alignedSpeed, 0f);

			_velocity += jumpDirection * jumpSpeed;

			return this;
		}

		private void EvaluateCollision(Collision collision)
		{
			if (StateSwiming()) return;

			int layer = collision.gameObject.layer;
			float minDot = GetMinDot(layer);

			for (int i = 0; i < collision.contactCount; i++)
			{
				Vector3 normal = collision.GetContact(i).normal;
				float upDot = Vector3.Dot(_upAxis, normal);

				if (upDot >= minDot)
				{
					_data.GroundContactCount += 1;
					_contactNormal += normal;
					_connectedBody = collision.rigidbody;
				}

				else
				{
					if (upDot > -0.01f)
					{
						_data.SteepContactCount += 1;
						_steepNormal += normal;

						if (_data.GroundContactCount == 0)
							_connectedBody = collision.rigidbody;
					}

					if (_isDesiresClimbing &&
						upDot >= _minClimbDotProduct &&
						(_arguments.ClimbMask & (1 << layer)) != 0)
					{
						_data.ClimbContactCount += 1;
						_climbNormal += normal;
						_lastClimbNormal = normal;
						_connectedBody = collision.rigidbody;
					}
				}
			}
		}

		private void UpdateConnectionState()
		{
			if (_connectedBody == _previousConnectedBody)
			{
				Vector3 connectionMovement =
					_connectedBody.transform.TransformPoint(_connectionLocalPosition) -
					_connectionWorldPosition;
				_connectionVelocity = connectionMovement / Time.deltaTime;
			}

			_connectionWorldPosition = _body.position;
			_connectionLocalPosition =
				_connectedBody.transform.InverseTransformPoint(_connectionWorldPosition);
		}

		private void ClearState()
		{
			_lastContactNormal = _contactNormal;
			_lastSteepNormal = _steepNormal;
			_lastConnectionVelocity = _connectionVelocity;

			_data.GroundContactCount = _data.SteepContactCount = _data.ClimbContactCount = 0;
			_contactNormal = _steepNormal = _climbNormal = Vector3.zero;

			_connectionVelocity = Vector3.zero;
			_previousConnectedBody = _connectedBody;
			_connectedBody = null;
			_submergence = 0f;
		}

		private VelocityCalculate FixedUpdate()
		{
			Vector3 gravity = CustomGravity.GetGravity(_body.position, out _upAxis);

			UpdateState();

			if (InWater())
				_velocity *= 1f - _arguments.WaterDrag * _submergence * Time.deltaTime;

			AdjustVelocity();

			if (_isDesiredJump)
			{
				_isDesiredJump = false;
				Jump(gravity);
			}

			SetVelocity(gravity);
			ClearState();

			return this;
		}

		private Vector3 StateHandling(Vector3 gravity, params Func<bool>[] checkStates)
		{
			foreach (var check in checkStates)
				if (check())
					return GetVelocity(check, gravity);

			return Move(gravity);
		}

		private Vector3 GetVelocity(Func<bool> checkState, Vector3 gravity) => _stateChecks[checkState].Invoke(gravity);

		private Dictionary<Func<bool>, Func<Vector3, Vector3>> _stateChecks =
			new Dictionary<Func<bool>, Func<Vector3, Vector3>>();

		private void SetChecks()
		{
			_stateChecks.Add(StateClimbing, MoveIsClimbimg);
			_stateChecks.Add(InWater, MoveInWater);
			_stateChecks.Add(RestOnGround, MoveStartOnGround);
			_stateChecks.Add(ClimbingOnGround, ClimbMoveOnGround);
			_stateChecks.Add(StateClimbing, MoveIsClimbimg);
		}

		private Vector3 MoveIsClimbimg(Vector3 gravity) =>
			_velocity -= _contactNormal * (_arguments.MaxClimbAcceleration * 0.9f * Time.deltaTime);

		private Vector3 MoveInWater(Vector3 gravity) =>
			_velocity += gravity * ((1f - _arguments.Buoyancy * _submergence) * Time.deltaTime);

		private Vector3 MoveStartOnGround(Vector3 gravity) =>
			_velocity += _contactNormal * (Vector3.Dot(gravity, _contactNormal) * Time.deltaTime);

		private Vector3 ClimbMoveOnGround(Vector3 gravity) =>
			_velocity += (gravity - _contactNormal * (_arguments.MaxClimbAcceleration * 0.9f)) * Time.deltaTime;

		private Vector3 Move(Vector3 gravity) =>
			_velocity += gravity * Time.deltaTime;

		private bool RestOnGround() => OnGround() && Velocity.sqrMagnitude < 0.01f;

		private bool ClimbingOnGround() => _isDesiresClimbing && OnGround();

		private Vector3 SetVelocity(Vector3 gravity)
		{
			if (StateClimbing())
				_velocity -= _contactNormal * (_arguments.MaxClimbAcceleration * 0.9f * Time.deltaTime);

			else if (InWater())
				_velocity += gravity * ((1f - _arguments.Buoyancy * _submergence) * Time.deltaTime);

			else if (OnGround() && Velocity.sqrMagnitude < 0.01f)
				_velocity += _contactNormal * (Vector3.Dot(gravity, _contactNormal) * Time.deltaTime);

			else if (_isDesiresClimbing && OnGround())
				_velocity += (gravity - _contactNormal * (_arguments.MaxClimbAcceleration * 0.9f)) * Time.deltaTime;

			else _velocity += gravity * Time.deltaTime;

			return _velocity;
		}
	}
}
