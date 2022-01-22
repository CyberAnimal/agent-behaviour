using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class NodeSide : MonoBehaviour
    {
        private readonly Vector3Int _sidePosition;
        private readonly float _sideRatio;
        private readonly Node _target;

        private readonly SideOrientation _orientation;
        private readonly SideActive _activeState;
        private readonly SideSurface _surface;

        public Vector3Int SidePosition => _sidePosition;
        public float SideRatio => _sideRatio;
        public bool Active => _activeState == SideActive.Active;
        public Node TargetNode => _target;

        public SideOrientation Orientation => _orientation;
        public SideSurface Surface => _surface;

        public NodeSide(Vector3Int position, float sideRatio, Node target, SideOrientation orientation,
                        SideActive activeState, SideSurface surface)
        {
            _sidePosition = position;
            _sideRatio = sideRatio;
            _target = target;

            _orientation = orientation;
            _activeState = activeState;
            _surface = surface;
        }
    }
}
