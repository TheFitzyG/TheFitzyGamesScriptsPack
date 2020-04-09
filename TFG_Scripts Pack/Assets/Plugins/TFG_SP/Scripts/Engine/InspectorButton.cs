using UnityEngine;
using UnityEngine.Events;


namespace TFG_SP
{

    //No, they're not a police inspector. Just a sort of button to use in the inspector window. 
    public class InspectorButton : MonoBehaviour
    {

        [Tooltip("Click me to trigger the 'OnTrigger' event")]
        public bool Trigger;

        public UnityEvent OnTrigger;

        private void Update()
        {

            if (Trigger)
            {

                Trigger = false;
                OnTrigger.Invoke();


            }
        }


    }
}

