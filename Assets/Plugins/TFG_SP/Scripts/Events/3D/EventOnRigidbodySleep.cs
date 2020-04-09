using UnityEngine;
using UnityEngine.Events;


namespace TFG_SP
{
    [RequireComponent(typeof(Rigidbody))]
    public class EventOnRigidbodySleep : MonoBehaviour
    {

        //Calls event when the attached rigidbody sleeps

        Rigidbody rb;
        bool triggered;

        public UnityEvent OnSleepEvent;

        private void OnEnable()
        {

            rb = GetComponent<Rigidbody>();

        }

        private void FixedUpdate()
        {

            if (!triggered)
            {

                if (rb.IsSleeping())
                {

                    OnSleepEvent?.Invoke();

                    triggered = true;

                }
            }

        }

        public void Reset()
        {

            triggered = false;

        }


    }
}
