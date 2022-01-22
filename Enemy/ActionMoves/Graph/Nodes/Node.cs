using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class Node : MonoBehaviour
    {
        private GameObject _nodeObject;
        [Range(0.01f, 0.99f)]
        private float _moveRatio;
        private Vector3Int _nodeCentr;

        public float MoveRatio => _moveRatio;
        public Vector3Int NodeCentr => _nodeCentr;

        private Dictionary<SideOrientation, NodeSide> _sides;
        private SideOrientation[] _orientations = new SideOrientation[]
        {
            SideOrientation.Top,
            SideOrientation.Bottom,

            SideOrientation.North,
            SideOrientation.South,

            SideOrientation.East,
            SideOrientation.West
        };

        public (SideActive active, Node target) GetTargetNode(Vector3Int desirePosition)
        {
            (bool isActive, NodeSide side) = GetSide(desirePosition);

            return isActive ?
                  (SideActive.Active, side.TargetNode) :
                  (SideActive.Stop, this);
        }
        private (bool isActive, NodeSide side) GetSide(Vector3Int desirePosition)
        {
            NodeSide side = _sides[SideOrientation.South];

            if (desirePosition.z > _nodeCentr.z)
            {
                side = _sides[SideOrientation.West];

                return side.Active ?
                      (true, side) :
                      (false, default);
            }

            else if (desirePosition.x > _nodeCentr.x)
            {
                side = _sides[SideOrientation.North];

                return side.Active ?
                      (true, side) :
                      (false, default);
            }

            else if (desirePosition.z < _nodeCentr.z)
            {
                side = _sides[SideOrientation.East];

                return side.Active ?
                      (true, side) :
                      (false, default);
            }

            else return side.Active ?
                       (true, side) :
                       (false, default);
        }

        private void Start()
        {
            _sides = new Dictionary<SideOrientation, NodeSide>();
        }

        public void SetNode(Vector3Int nodeCentr, Locks locks,
                            Func<Node, int, SideOrientation, float> ratioFunction, GameObject node,
                            SideSurface surface, Dictionary<SideOrientation, Node> targets)
        {
            if (_sides.Count > 1)
                return;

            for (int i = 0; i < 6; i++)
            {
                float ratio = ratioFunction.Invoke(this, 2, _orientations[i]);
                AddNodeSide(nodeCentr, locks, i, ratio, surface, targets);
            }

            _nodeObject = node;
        }

        private void AddNodeSide(Vector3Int nodeCentr, Locks locks,
                                 int count, float ratio, SideSurface surface,
                                 Dictionary<SideOrientation, Node> targets)
        {
            SideOrientation orientation = _orientations[count];
            Node target = default;

            foreach (var side in locks.Sides)
                if (side == _orientations[count])
                {
                    _sides.Add(orientation, GetNodeSide(orientation, ratio,
                               target, SideActive.Stop, nodeCentr, surface));

                    return;
                }

            foreach (var element in targets)
                if (element.Key == orientation)
                    target = element.Value;

            _sides.Add(orientation, GetNodeSide(orientation, ratio,
                       target, SideActive.Active, nodeCentr, surface));
        }

        private NodeSide GetNodeSide(SideOrientation orientation, float ratio, Node target,
                                     SideActive active, Vector3Int nodeCentr, SideSurface surface) =>
            new NodeSide(GetSidePosition(orientation, nodeCentr),
                         ratio, target, orientation, active, surface);

        private Vector3Int GetSidePosition(SideOrientation orientation, Vector3Int nodeCentr) => orientation switch
        {
            SideOrientation.Top => new Vector3Int(nodeCentr.x, nodeCentr.y + 1, nodeCentr.z),
            SideOrientation.Bottom => new Vector3Int(nodeCentr.x, nodeCentr.y - 1, nodeCentr.z),

            SideOrientation.North => new Vector3Int(nodeCentr.x - 1, nodeCentr.y, nodeCentr.z),
            SideOrientation.South => new Vector3Int(nodeCentr.x + 1, nodeCentr.y, nodeCentr.z),

            SideOrientation.East => new Vector3Int(nodeCentr.x, nodeCentr.y, nodeCentr.z - 1),
            SideOrientation.West => new Vector3Int(nodeCentr.x, nodeCentr.y, nodeCentr.z + 1)
        };
    }
}
