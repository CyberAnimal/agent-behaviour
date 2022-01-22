using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class Graph<F> : MonoBehaviour 
        where F : ICalculateRatio, new()
    {
        [SerializeField] private GameObject LevelArea;
        [SerializeField] private int LayerCount;

        private ObstructionChecker _checker;

        private GraphFlat<F>[] _graphFlats;
        private int _xSize;
        private int _ySize;

        public GraphFlat<F> GetGraphLayer(int layerNumber) => _graphFlats[layerNumber];
        public void Start()
        {
            _checker = new ObstructionChecker();
            _graphFlats = new GraphFlat<F>[LayerCount];

            _xSize = (int)LevelArea.transform.localScale.x;
            _ySize = (int)LevelArea.transform.localScale.y;
        }

        public void SetGraph()
        {
            for (int i = 0; i < _graphFlats.Length; i++)
                _graphFlats[i] = new GraphFlat<F>(i, _xSize, _ySize, LayerCount, _checker.SetSideRatio);
        }
    }
}
