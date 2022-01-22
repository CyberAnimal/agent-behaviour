using System;
using UnityEngine;

namespace Game.Enemy
{
    [CreateAssetMenu]
	public class EnemyFactory : GameObjectFactory
	{
		[Serializable]
		private class EnemyConfig
		{
			public Enemy Prefab = default;
			public Transform Transform = default;

			public ConfigEnemyType EnemyType = default;
			public CalculateType Calculate = default;
			public ConfigParameterValue ParameterValue = default;

			public class ConfigEnemyType
			{
				public readonly AttackType AttackType;
				public readonly SenseType SenseType;
				public readonly MoveType MoveType;
				public readonly WalkType WalkType;
			}

			public class CalculateType
			{
				public readonly BehaviourType Behaviour;
				public readonly CalculateInterest Calculate;
			}

			public class ConfigParameterValue
			{
				[FloatRangeSlider(5f, 50f)]
				public readonly FloatRange Damage = new FloatRange(25f);

				[FloatRangeSlider(5f, 50f)]
				public readonly FloatRange Armor = new FloatRange(25f);

				[FloatRangeSlider(10f, 100f)]
				public readonly FloatRange Health = new FloatRange(50f);


				[FloatRangeSlider(0.1f, 2.0f)]
				public readonly FloatRange Speed = new FloatRange(1.0f);

				[FloatRangeSlider(0.1f, 2.0f)]
				public readonly FloatRange JumpHeight = new FloatRange(1.0f);

				[FloatRangeSlider(1f, 10f)]
				public readonly FloatRange SenceRadius = new FloatRange(5f);
			}

        }

		[SerializeField] private EnemyConfig A = default,
											 B = default,
											 C = default,
											 D = default,
											 E = default;

		public Enemy Get(EnemyType type = EnemyType.C)
		{
			EnemyConfig config = GetConfig(type);
			Enemy instance = CreateGameObjectInstance(config.Prefab, config.Transform.position);

			instance.OriginFactory = this;

			instance = SetTypes(instance, config);
			instance = SetParameters(instance, config);
			instance = SetBehaviour(instance, config);

			return instance;
		}

		private EnemyConfig GetConfig(EnemyType type)
		{
			switch (type)
			{
				case EnemyType.A: return A;
				case EnemyType.B: return B;
				case EnemyType.C: return C;
				case EnemyType.D: return D;
				case EnemyType.E: return E;
			}

			Debug.Assert(false, "Unsupported enemy type!");

			return null;
		}

		private Enemy SetTypes(Enemy instance, EnemyConfig config)
        {
			instance.InitializeType(config.EnemyType.AttackType,
									config.EnemyType.SenseType,
									config.EnemyType.MoveType,
									config.EnemyType.WalkType);

			return instance;
		}

		private Enemy SetParameters(Enemy instance, EnemyConfig config)
        {
			instance.InitializeSelfParameters(config.ParameterValue.Damage.RandomValueInRange,
											  config.ParameterValue.Armor.RandomValueInRange,
											  config.ParameterValue.Health.RandomValueInRange);

			instance.InitializeActionParameters(config.ParameterValue.Speed.RandomValueInRange,
										 	    config.ParameterValue.JumpHeight.RandomValueInRange,
											    config.ParameterValue.SenceRadius.RandomValueInRange);

			return instance;
		}

		private Enemy SetBehaviour(Enemy instance, EnemyConfig config)
        {
			instance.InitializeBehaviour(config.Calculate.Behaviour, 
										 config.Calculate.Calculate);

			return instance;

		}

		public void Destroy(Enemy enemy)
		{
			Debug.Assert(enemy.OriginFactory == this, "Wrong factory reclaimed!");

			Destroy(enemy.gameObject);
		}
	}
}
