using UnityEngine;
using UnityEngine.Events;

namespace TFG_SP
{
    [RequireComponent(typeof(Collider2D))]
    public class EventOnTrigger2D : MonoBehaviour
    {

        [Tooltip("Trigger enter only once")]
        public bool OneShot;
        private bool triggered;

        public LayerMask LayerMask;

        public UnityEvent OnTriggerEnterEvent;
        public UnityEvent OnTriggerExitEvent;

        private void OnTriggerEnter2D(Collider2D other)
        {

            if (!OneShot || !triggered)
            {

                if (Checks.CompareLayers(LayerMask, other.gameObject))
                {

                    OnTriggerEnterEvent?.Invoke();

                    if (OneShot)
                    {

                        triggered = true;

                    }

                }

            }

        }

        private void OnTriggerExit2D(Collider2D other)
        {

            if (!OneShot || !triggered)
            {

                if (Checks.CompareLayers(LayerMask, other.gameObject))
                {

                    OnTriggerExitEvent?.Invoke();

                }

            }

        }

    }
}
