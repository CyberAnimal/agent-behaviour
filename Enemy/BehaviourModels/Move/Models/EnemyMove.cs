using UnityEngine;

namespace Game.Enemy
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyMove : MonoBehaviour
    {
		[SerializeField] private Enemy Enemy;
		[SerializeField, Range(0f, 100f)] protected float MaxSpeed;
		[SerializeField, Range(0f, 100f)] protected float MaxAcceleration = 10f, MaxAirAcceleration = 1f;
		[SerializeField, Range(0f, 10f)] protected float JumpHeight;
		[SerializeField, Range(0, 5)] protected int MaxAirJumps = 0;
		[SerializeField, Range(0, 90)] protected float MaxGroundAngle = 25f;

		protected Rigidbody Body;
		protected Vector3 Velocity, DesiredVelocity;
		protected Vector3 ContactNormal;
		protected bool DesiredJump;
		protected int GroundContactCount;
		protected bool OnGround => GroundContactCount > 0;
		protected int JumpPhase;
		protected float MinGroundDotProduct;

		protected virtual void OnValidate()
		{
			MinGroundDotProduct = Mathf.Cos(MaxGroundAngle * Mathf.Deg2Rad);
		}

		protected virtual void Awake()
		{
			Body = GetComponent<Rigidbody>();
			OnValidate();
		}

		protected virtual void Start()
		{
			MaxSpeed = Enemy.Speed;
			JumpHeight = Enemy.JumpHeight;
		}

		public virtual void Move(Vector3 target)
		{
			DesiredVelocity = target * MaxSpeed;
			
			DesiredJump |= Input.GetButtonDown("Jump");
		}

		protected virtual void FixedUpdate()
		{
			UpdateState();
			AdjustVelocity();

			if (DesiredJump)
			{
				DesiredJump = false;
				Jump();
			}

			Body.velocity = Velocity;
			ClearState();
		}

		protected virtual void ClearState()
		{
			GroundContactCount = 0;
			ContactNormal = Vector3.zero;
		}

		protected virtual void UpdateState()
		{
			Velocity = Body.velocity;

			if (OnGround)
			{
				JumpPhase = 0;

				if (GroundContactCount > 1)
					ContactNormal.Normalize();
			}

			else
				ContactNormal = Vector3.up;
		}

		protected virtual void AdjustVelocity()
		{
			Vector3 xAxis = ProjectOnContactPlane(Vector3.right).normalized;
			Vector3 zAxis = ProjectOnContactPlane(Vector3.forward).normalized;

			float currentX = Vector3.Dot(Velocity, xAxis);
			float currentZ = Vector3.Dot(Velocity, zAxis);

			float acceleration = OnGround ? MaxAcceleration : MaxAirAcceleration;
			float maxSpeedChange = acceleration * Time.deltaTime;

			float newX =
				Mathf.MoveTowards(currentX, DesiredVelocity.x, maxSpeedChange);
			float newZ =
				Mathf.MoveTowards(currentZ, DesiredVelocity.z, maxSpeedChange);

			Velocity += xAxis * (newX - currentX) + zAxis * (newZ - currentZ);
		}

		protected virtual void Jump()
		{
			if (OnGround == false || JumpPhase >= MaxAirJumps)
				return;

			JumpPhase += 1;

			float jumpSpeed = Mathf.Sqrt(-2f * Physics.gravity.y * JumpHeight);
			float alignedSpeed = Vector3.Dot(Velocity, ContactNormal);

			if (alignedSpeed > 0f)
				jumpSpeed = Mathf.Max(jumpSpeed - alignedSpeed, 0f);

			Velocity += ContactNormal * jumpSpeed;
		}

		protected virtual void OnCollisionEnter(Collision collision)
		{
			EvaluateCollision(collision);
		}

		protected virtual void OnCollisionStay(Collision collision)
		{
			EvaluateCollision(collision);
		}

		protected virtual void EvaluateCollision(Collision collision)
		{
			for (int i = 0; i < collision.contactCount; i++)
			{
				Vector3 normal = collision.GetContact(i).normal;

				if (normal.y >= MinGroundDotProduct)
				{
					GroundContactCount += 1;
					ContactNormal += normal;
				}
			}
		}

		protected virtual Vector3 ProjectOnContactPlane(Vector3 vector) => 
			vector - ContactNormal * Vector3.Dot(vector, ContactNormal);
	}
}
