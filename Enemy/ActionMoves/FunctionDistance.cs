using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class FunctionDistance : MonoBehaviour
    {
        public readonly struct Element
        {
            private readonly float _distance;
            private readonly float _time;

            public readonly float Distance => _distance;
            public readonly float Time => _time;

            public Element(float distance, float time)
            {
                _distance = distance;
                _time = time;
            }
        }

        private float _time;
        private List<Element> _elements;
        public List<Element> Elements => _elements;

        public FunctionDistance()
        {
            _time = 0;
            _elements = new List<Element>();
            _elements.Capacity = 100;
        }

        public void SetElement(GameObject agent, GameObject target)
        {
            Vector3 distance = target.transform.position - agent.transform.position;
            float distanceValue = distance.magnitude;
            _time += Time.deltaTime;

            if (_elements.Count >= 100)
            {
                _elements = new List<Element>();
                _time = 0;
            }

            _elements.Add(new Element(distanceValue, _time));
        }
    }
}