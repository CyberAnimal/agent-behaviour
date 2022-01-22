using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class ObstructionChecker : MonoBehaviour
    {
        private readonly Dictionary<SideOrientation, Vector3Int> _directions =
                     new Dictionary<SideOrientation, Vector3Int>()
                     {
                         [SideOrientation.Top] = new Vector3Int(0, 1, 0),
                         [SideOrientation.Bottom] = new Vector3Int(0, -1, 0),

                         [SideOrientation.North] = new Vector3Int(0, 0, 1),
                         [SideOrientation.South] = new Vector3Int(0, 0, -1),

                         [SideOrientation.East] = new Vector3Int(1, 0, 0),
                         [SideOrientation.West] = new Vector3Int(-1, 0, 0)
                     };

        private RatioCalculator _calculator = new RatioCalculator();

        public float SetSideRatio(Node node, int nodeScale, SideOrientation side)
        {
            RaycastHit hit;
            Vector3Int centr = node.NodeCentr;

            bool boxCast = Physics.BoxCast(centr, new Vector3(nodeScale, 1, nodeScale),
                                           GetDirection(centr, side), out hit);

            if (boxCast && IsLock(hit))
                return float.Epsilon;

            else
                return _calculator.GetRatio(GetType(hit));
        }
        public float SetNodeRatio(Node node, int nodeScale)
        {
            List<SideOrientation> sides = new List<SideOrientation>(_directions.Count);
            float allRatio = default;

            foreach (var side in sides)
                allRatio += SetSideRatio(node, nodeScale, side);

            return allRatio;
        }

        private Vector3 GetDirection(Vector3Int centr, SideOrientation orientation) => centr + _directions[orientation];

        private bool IsLock(RaycastHit hit)
        {
            GameObject other = hit.collider.gameObject;
            TypeOfWall wall = other.GetComponent<TypeOfWall>();

            if (wall != null)
                return true;

            else
                return false;
        }

        private ObjectType GetType(RaycastHit hit)
        {
            GameObject other = hit.collider.gameObject;
            EntityType type = other.GetComponent<EntityType>();

            return type.Type;
        }
    }
}
