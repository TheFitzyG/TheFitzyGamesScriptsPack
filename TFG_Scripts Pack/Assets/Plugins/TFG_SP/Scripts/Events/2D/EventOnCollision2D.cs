using UnityEngine;
using UnityEngine.Events;

namespace TFG_SP
{
    public class EventOnCollision2D : MonoBehaviour
    {

        public LayerMask layerMask;

        [Tooltip("Trigger only once?")]
        public bool OneShot;

        public UnityEvent OnCollisionEvent;

        private bool shot;

        void OnCollisionEnter2D(Collision2D other)
        {

            if (!shot)
            {

                if (Checks.CompareLayers(layerMask, other.gameObject))
                {

                    OnCollisionEvent?.Invoke();

                    if (OneShot)
                    {
                        shot = true;
                    }
                }
            }
        }
    }
}
