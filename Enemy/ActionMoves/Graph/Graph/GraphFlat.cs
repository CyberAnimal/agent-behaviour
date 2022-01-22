using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public interface ICalculateRatio
    {
        public Func<Node, int, SideOrientation, float> RatioFunction { get; }
    }

    public class CalculateRatio : ICalculateRatio
    {
        private float GetRatio(Node node, int count, SideOrientation orientation)
        {
            return UnityEngine.Random.Range(float.Epsilon, 1.0f);
        }

        public Func<Node, int, SideOrientation, float> RatioFunction => GetRatio;
    }

    public class GraphFlat<F> : MonoBehaviour
        where F : ICalculateRatio, new()
    {
        private NodeCreator _creator = new NodeCreator();

        private Node[,] _nodes;
        private int _flatLayer;
        private int _xSize;
        private int _zSize;

        public Node[,] Nodes => _nodes;

        private List<int> InvertX()
        {
            int xSizeHalf = _xSize / 2;

            List<int> listX = new List<int>(xSizeHalf);

            for (int x = xSizeHalf; x > 0; x--)
            {
                listX.Add(x);
            }

            return listX;
        }

        private List<int> InvertZ()
        {
            int zSizeHalf = _zSize / 2;

            List<int> listZ = new List<int>(zSizeHalf);

            for (int z = zSizeHalf; z > 0; z--)
            {
                listZ.Add(z);
            }

            return listZ;
        }

        public Node GetIntermediateNode(Vector3Int desirePosition, Node current)
        {
            (SideActive active, Node targetNode) = current.GetTargetNode(desirePosition);

            if (active == SideActive.Active)
                return targetNode;

            else
            {
                Vector3Int difference = desirePosition - current.NodeCentr;

                if (difference.x != 0)
                {
                    Vector3Int newDesire = current.NodeCentr +
                                           new Vector3Int(0, 0, difference.x);

                    return GetIntermediateNode(newDesire, current);
                }

                else
                {
                    Vector3Int newDesire = current.NodeCentr +
                                           new Vector3Int(difference.z, 0, 0);

                    return GetIntermediateNode(newDesire, current);
                }
            }
        }

        public Node GetStartNode(Vector3Int position) =>
                    GetNode(new Position(position.x, position.z));

        private Node GetNode(Position position) => position switch
        {
            { X: var x, Z: var z } when x >= -_xSize && x < 0 &&
                                        z >= -_zSize && z < 0 => _nodes[InvertX()[-x / 2],
                                                                        InvertZ()[-z / 2]],

            { X: var x, Z: var z } when x <= _xSize && x > 0 &&
                                        z <= _zSize && z > 0 => _nodes[x / 2,
                                                                        z / 2],

            { X: var x, Z: var z } when x <= _xSize && x > 0 &&
                                        z >= -_zSize && z < 0 => _nodes[x / 2,
                                                                        InvertZ()[-z / 2]],

            { X: var x, Z: var z } when x >= -_xSize && x < 0 &&
                                        z <= _zSize && z > 0 => _nodes[InvertX()[-x / 2],
                                                                        z / 2],
        };

        public GraphFlat(int layer, int xSize, int zSize, int layerCount,
                         Func<Node, int, SideOrientation, float> ratioFunc)
        {
            _flatLayer = layer;
            _xSize = xSize;
            _zSize = zSize;
            _nodes = new Node[_xSize, _zSize];

            SetGraphFlat(1, layerCount, ratioFunc);
        }

        private void SetGraphFlat(int minLayer, int maxLayer,
                                  Func<Node, int, SideOrientation, float> ratioFunc)
        {
            SideOrientation defaultOrientation = default;

            if (_flatLayer == minLayer)
                defaultOrientation = SideOrientation.Bottom;

            if (_flatLayer == maxLayer)
                defaultOrientation = SideOrientation.Top;

            for (int x = 0; x < _nodes.GetLength(0); x++)
                for (int z = 0; z < _nodes.GetLength(1); z++)
                    _nodes[x, z] = NodeParamsSwitch(new Position(x, z));
        }

        private readonly struct Position
        {
            public Position(int x, int z) => (X, Z) = (x, z);

            public int X { get; }
            public int Z { get; }
        }

        private Dictionary<SideOrientation, Node> GetTargets(Position position) => position switch
        {
            { X: 0, Z: 0 } => new Dictionary<SideOrientation, Node>()
            {
                [SideOrientation.West] = _nodes[1, 0],
                [SideOrientation.North] = _nodes[0, 1]
            },

            { X: var x, Z: var z } when x == _xSize &&
                                        z == _zSize => new Dictionary<SideOrientation, Node>()
                                        {
                                            [SideOrientation.East] = _nodes[x - 1, z],
                                            [SideOrientation.South] = _nodes[x, z - 1]
                                        },

            { X: 0, Z: var z } when z < _zSize => new Dictionary<SideOrientation, Node>()
            {
                [SideOrientation.West] = _nodes[1, z],
                [SideOrientation.North] = _nodes[0, z + 1],
                [SideOrientation.South] = _nodes[0, z - 1]
            },

            { X: 0, Z: var z } when z == _zSize => new Dictionary<SideOrientation, Node>()
            {
                [SideOrientation.West] = _nodes[1, z],
                [SideOrientation.South] = _nodes[0, z - 1]
            },

            { X: var x, Z: 0 } when x < _xSize => new Dictionary<SideOrientation, Node>()
            {
                [SideOrientation.East] = _nodes[x - 1, 0],
                [SideOrientation.West] = _nodes[x + 1, 0],
                [SideOrientation.North] = _nodes[0, 1]
            },

            { X: var x, Z: 0 } when x == _xSize => new Dictionary<SideOrientation, Node>()
            {
                [SideOrientation.East] = _nodes[x - 1, 0],
                [SideOrientation.North] = _nodes[x, 1]
            },

            { X: var x, Z: var z } when x < _xSize &&
                                        z < _zSize => new Dictionary<SideOrientation, Node>()
                                        {
                                            [SideOrientation.West] = _nodes[x + 1, z],
                                            [SideOrientation.East] = _nodes[x - 1, z],
                                            [SideOrientation.North] = _nodes[x, z + 1],
                                            [SideOrientation.South] = _nodes[x, z - 1]
                                        },

        };

        private Node NodeParamsSwitch(Position position, Func<Node, int, SideOrientation, float> ratioFunc, SideSurface surface) => position switch
        {
            { X: 0, Z: 0 } => SetNode(0, 0, surface,
                                      GetTargets(position),
                                      SideOrientation.East,
                                      SideOrientation.South),

            { X: var x, Z: var z } when x == _xSize &&
                                        z == _zSize => SetNode(x, z, surface,
                                                               GetTargets(position),
                                                               SideOrientation.West,
                                                               SideOrientation.North),

            { X: 0, Z: var z } when z < _zSize => SetNode(0, z, surface,
                                                           GetTargets(position),
                                                           SideOrientation.East),

            { X: 0, Z: var z } when z == _zSize => SetNode(0, z, surface,
                                                           GetTargets(position),
                                                           SideOrientation.East,
                                                           SideOrientation.North),

            { X: var x, Z: 0 } when x < _xSize => SetNode(x, 0, surface,
                                                           GetTargets(position),
                                                           SideOrientation.South),

            { X: var x, Z: 0 } when x == _xSize => SetNode(x, 0, surface,
                                                           GetTargets(position),
                                                           SideOrientation.South,
                                                           SideOrientation.West),

            { X: var x, Z: var z } when x > 0 && x < _xSize &&
                                        z > 0 && z < _zSize => SetNode(x, z, surface,
                                                                       GetTargets(position)),

        };

        private Node SetNode(int x, int z, SideSurface surface,
                             Dictionary<SideOrientation, Node> targets,
                             params SideOrientation[] orientations)
        {
            Node node = new Node();
            int coordY = _flatLayer == 1 ?
                         _flatLayer :
                         _flatLayer * 2 - 1;

            Vector3Int position = new Vector3Int(x * 2 + 1, coordY, z * 2 + 1);

            node.SetNode(position, new Locks(orientations), new F().RatioFunction,
                         _creator.Create(position), surface, targets);

            return node;
        }
    }
}
