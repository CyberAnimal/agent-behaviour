using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class NodeCreator : MonoBehaviour
    {
        [SerializeField] private GameObject NodePrefab;

        public GameObject Create(Vector3Int nodePosition)
        {
            GameObject nodeObject = Instantiate(NodePrefab, nodePosition, Quaternion.identity);

            return nodeObject;
        }
    }
}
