using UnityEngine;
using UnityEngine.Events;

namespace TFG_SP
{
    public class EventOnCollision : MonoBehaviour
    {

        public LayerMask LayerMask;

        [Tooltip("Trigger only once?")]
        public bool OneShot;

        public UnityEvent OnCollisionEvent;

        bool triggered;
        private void OnCollisionEnter(Collision other)
        {

            if (!triggered)
            {

                if (Checks.CompareLayers(LayerMask, other.gameObject))
                {

                    OnCollisionEvent?.Invoke();

                    if (OneShot)
                    {

                        triggered = true;

                    }

                }
            }



        }
    }
}
