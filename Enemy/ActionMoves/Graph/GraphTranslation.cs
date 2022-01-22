using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class GraphTranslation<F> : MonoBehaviour
        where F : ICalculateRatio, new()
    {
        private Graph<F> _graph;

        private void Start() => _graph.SetGraph();

        private void GraphPass(Vector3Int start, Vector3Int target)
        {
            GraphFlat<F> flat = _graph.GetGraphLayer(start.y / 2);

            Node startNode = flat.GetStartNode(start);
            Node targetNode = flat.GetStartNode(target);

            Search(flat, startNode, targetNode);
        }

        private float Search(GraphFlat<F> flat, Node start, Node target)
        {
            (int absoluteX, int absoluteZ) = GetAbsolute(start.NodeCentr, target.NodeCentr);

            float ratio = 0;
            int capacity = GetCapacity(absoluteX, absoluteZ);
            Queue<Node> nodes = new Queue<Node>(capacity);
            nodes.Enqueue(start);
            int count = 1;

            while (count < capacity)
            {
                Node newNode = GetNode(flat, nodes, target,
                                       absoluteX, absoluteZ);

                ratio *= newNode.MoveRatio;
                nodes.Enqueue(newNode);

                count++;
            }

            return ratio;
        }

        private Node GetNode(GraphFlat<F> flat, Queue<Node> nodes,
                             Node target, int absoluteX, int absoluteZ)
        {
            Node current = nodes.Peek();
            Node newNode = GetNextNode(absoluteX, absoluteZ, current.NodeCentr,
                                       target.NodeCentr, flat, current);
            return newNode;
        }

        private int GetCapacity(int absoluteX, int absoluteZ) => (absoluteX + absoluteZ) / 2;

        private Node GetNextNode(int absoluteX, int absoluteZ, Vector3Int current,
                                 Vector3Int target, GraphFlat<F> flat, Node currentNode)
        {
            Vector3Int vectorForAdd;

            if (absoluteX > absoluteZ)
                vectorForAdd = new Vector3Int(current.x + StepX(current, target),
                                              current.y, current.z);

            else
                vectorForAdd = new Vector3Int(current.x, current.y,
                                              current.z + StepZ(current, target));

            current += vectorForAdd;

            return flat.GetIntermediateNode(current, currentNode);
        }

        private (int absoluteX, int absoluteZ) GetAbsolute(Vector3Int start, Vector3Int target)
        {
            int distanceX = target.x - start.x;
            int distanceZ = target.z - start.z;

            int absoluteX = Mathf.Abs(distanceX);
            int absoluteZ = Mathf.Abs(distanceZ);

            return (absoluteX, absoluteZ);
        }

        private int StepX(Vector3Int start, Vector3Int target)
        {
            if (target.x < start.x)
                return -1;

            else return 1;
        }

        private int StepZ(Vector3Int start, Vector3Int target)
        {
            if (target.z < start.z)
                return -1;

            else return 1;
        }
    }
}
