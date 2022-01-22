using UnityEngine;

namespace Game.Enemy
{
    public class ChoiceDirection : MonoBehaviour
    {
        public virtual Route GetRoute(Transform enemyTransform, Vector3 targetPosition)
        {
            return new Route(enemyTransform.rotation.x, enemyTransform.position);
        }
    }
}

