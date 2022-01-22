using UnityEngine;

namespace Game.Enemy
{
    public class GameObjectFactory : ScriptableObject
	{
		protected T CreateGameObjectInstance<T>(T prefab, Vector3 position) where T : MonoBehaviour
		{
			Quaternion rotation = new Quaternion(0.0f, Random.Range(0.0f, 360.0f), 0.0f, 0.0f);

			T instance = Instantiate(prefab, position, rotation);

			return instance;
		}
	}
}
