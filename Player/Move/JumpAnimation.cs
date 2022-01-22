using System.Collections;
using UnityEngine;

namespace Game.Player
{
    public class JumpAnimation : MonoBehaviour
    {
        [SerializeField] private AnimationCurve Animation;
        [SerializeField] private float Height;
        [SerializeField] private float Duration;

        public void Jump(GameObject player)
        {
            StartCoroutine(PlayAnimation(player));
        }

        private IEnumerator PlayAnimation(GameObject player)
        {
            float time = 0f;
            float value = 0f;
            float maxHeight = GetMaxValue() * Height;

            Vector3 startPosition = player.transform.position;
            Vector3 finish = startPosition + new Vector3(0f, maxHeight, 0f);

            while (value < 1)
            {
                time += Time.deltaTime;
                value = time / Duration;

                Vector3 direction = Vector3.Lerp(startPosition, finish, value);
                player.transform.Translate(direction * Time.deltaTime, Space.World);

                yield return null;
            }
        }

        private float GetMaxValue()
        {
            float maxHeight = 0f;

            for (int i = 0; i < Animation.length; i++)
            {
                float height = Animation[i].value;

                if (maxHeight < height)
                    maxHeight = height;
            }

            return maxHeight;
        }
    }
}
