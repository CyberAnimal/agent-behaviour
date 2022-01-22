using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class Visor : SenseModel, ISence
    {
        [SerializeField] private GameObject Player;
        [SerializeField] private GameObject Agent;

        private Sense _sense = Sense.Sight;

        public override event Action<GameObject, Sense> ObjectDetected;

        public event Action<Vector3, Vector3> PlayerAction;

        private List<Type> _triggerObjects;

        private float _timeCount = 0.0f;
        private float _period = 1.0f;

        private void Start()
        {
            if (Agent == null)
                Agent = gameObject;
        }

        public void SetTypes(List<Type> triggerObjects)
        {
            _triggerObjects = triggerObjects;
        }

        public override void CanUpdate()
        {

        }

        public override void TriggerEnter(Collider other)
        {
            if (other.gameObject != Player)
                return;

            GameObject player = other.gameObject;
            ObjectDetected?.Invoke(player, _sense);
        }

        public override void TriggerStay(Collider other)
        {
            if (other.gameObject != Player)
                return;

            GameObject player = other.gameObject;
            RaycastHit[] hits = CanRaycast(player);

            int i;
            for (i = 0; i < hits.Length; i++)
            {
                GameObject hitObject;
                hitObject = hits[i].collider.gameObject;

                if (IsWall(hitObject))
                    return;
            }

            ObjectDetected?.Invoke(player, _sense);

            // TODO
            // target is visible
            // code your behaviour below

            _timeCount += Time.deltaTime;

            if (_timeCount < _period)
                return;

            else
            {
                TransferPositions(player);
                _timeCount = 0.0f;
            }
        }

        public override void TriggerExit(Collider other)
        {
            if (other.gameObject != Player)
                return;

            GameObject player = other.gameObject;
            Vector3 playerPosition = player.transform.position;

        }

        private RaycastHit[] CanRaycast(GameObject player)
        {
            Vector3 agentPosition = Agent.transform.position;
            Vector3 targetPosition = player.transform.position;
            Vector3 direction = targetPosition - agentPosition;

            float length = direction.magnitude;
            direction.Normalize();

            Ray ray = new Ray(agentPosition, direction);
            RaycastHit[] hits;
            hits = Physics.RaycastAll(ray, length);

            return hits;
        }

        private bool IsWall(GameObject hitObject)
        {
            EntityType type = hitObject.GetComponent<EntityType>();

            return type.Type == ObjectType.Wall;
        }

        private void TransferPositions(GameObject player)
        {
            Vector3 playerPosition = player.transform.position;
            Vector3 agentPosition = Agent.transform.position;

            PlayerAction?.Invoke(playerPosition, agentPosition);
        }
    }

    public interface ISence
    {

    }
}
