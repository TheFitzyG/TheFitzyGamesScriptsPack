using UnityEngine;
using UnityEngine.Events;

namespace TFG_SP
{
    public class EventOnEnable : MonoBehaviour
    {


        //why is this necissary? idk, to make these things available in inspector

        public UnityEvent OnEnableEvent;

        public UnityEvent OnDisableEvent;


        private void OnEnable()
        {

            OnEnableEvent?.Invoke();

        }

        private void OnDisable()
        {

            OnDisableEvent?.Invoke();

        }

    }
}
