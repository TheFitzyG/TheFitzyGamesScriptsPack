using UnityEngine;
using UnityEngine.Events;

namespace TFG_SP
{

    [RequireComponent(typeof(Collider))]
    public class EventOnTrigger : MonoBehaviour
    {

        [Tooltip("Trigger only once")]
        public bool OneShotEnter;

        private bool triggered;

        public LayerMask LayerMask;

        public UnityEvent EventOnTriggerEnter;
        public UnityEvent EventOnTriggerStay;
        public UnityEvent EventOnTriggerExit;


        private void OnTriggerEnter(Collider other)
        {

            if (!triggered)
            {

                if (Checks.CompareLayers(LayerMask, other.gameObject))
                {

                    EventOnTriggerEnter?.Invoke();

                    if (OneShotEnter)
                    {

                        triggered = true;

                    }

                }
            }




        }

        private void OnTriggerStay(Collider other)
        {

            if (Checks.CompareLayers(LayerMask, other.gameObject))
            {

                EventOnTriggerStay?.Invoke();

            }


        }

        private void OnTriggerExit(Collider other)
        {

            if (Checks.CompareLayers(LayerMask, other.gameObject))
            {

                EventOnTriggerExit?.Invoke();

            }

        }

    }
}