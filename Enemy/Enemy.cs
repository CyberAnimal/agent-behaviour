using UnityEngine;

namespace Game.Enemy
{
    public class Enemy : MonoBehaviour
	{
		[SerializeField] private Transform Model = default;

		private Gun _gun;
		public Gun Gun => _gun;

		#region Parameters
		private float _damage;
		private float _armor;
		private float _health;

		private float _speed;
		private float _jumpHeight;
		private float _senceRadius;

		public float Damage => _damage;
		public float Armor => _armor;
		public float Health => _health;

		public float Speed => _speed;
		public float JumpHeight => _jumpHeight;
		public float SenceRadius => _senceRadius;
		#endregion

		#region Type
		private AttackType _attackType;
		private SenseType _senseType;
		private MoveType _moveType;
		private WalkType _walkType;

		public AttackType AttackType => _attackType;
		public SenseType SenseType => _senseType;
		public MoveType MoveType => _moveType;
		public WalkType WalkType => _walkType;
		#endregion

		private BehaviourType _behaviour;
		public BehaviourType BehaviourType => _behaviour;

		private CalculateInterest _calculate;
		public CalculateInterest CalculateType => _calculate;

		private EnemyFactory _originFactory;
		public EnemyFactory OriginFactory
		{
			get => _originFactory;

			set
			{
				Debug.Assert(_originFactory == null, "Redefined origin factory!");
				_originFactory = value;
			}
		}

		public bool IsGameUpdate()
		{
			if (Health <= 0f)
			{
				Remove();
				return false;
			}

			return true;
		}

		public void Remove()
		{
			OriginFactory.Destroy(this);
		}

		public void InitializeSelfParameters(float damage, float armor, float health)
		{
			_damage = damage;	
			_armor = armor;
			_health = health;
		}

		public void InitializeActionParameters(float speed, float jumpHeight, float senceRadius)
		{
			_speed = speed;
			_jumpHeight = jumpHeight;
			_senceRadius = senceRadius;
		}

		public void InitializeType(AttackType attackType, SenseType senseType, 
								   MoveType moveType, WalkType walkType)
		{
			_attackType = attackType;
			_senseType = senseType;
			_moveType = moveType;
			_walkType = walkType;
		}

		public void InitializeBehaviour(BehaviourType behaviour, CalculateInterest calculate)
		{
			_behaviour = behaviour;
			_calculate = calculate;
		}

		public void ApplyDamage(float damage)
		{
			Debug.Assert(damage >= 0f, "Negative damage applied.");

			_health -= damage;
		}
	}
}

