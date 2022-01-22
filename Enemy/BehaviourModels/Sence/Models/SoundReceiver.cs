using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class SoundReceiver : MonoBehaviour
    {
        [SerializeField] private Collider Collider;
        [SerializeField] private float SoundIntensity;
        [SerializeField] private float SoundThreshold;

        public float Intensity => SoundIntensity;
        public float Threshold => SoundThreshold;

        private void Start()
        {
            if (Collider != null)
                Collider = GetComponent<Collider>();
        }

        public virtual void Receive(float intensity, Vector3 position)
        {
            // TODO
            // code your own behaviour here

            Vector3 center = Collider.bounds.center;
            Vector3 size = Collider.bounds.size;


        }
    }

}
