using UnityEngine;


namespace TFG_SP
{
    [System.Serializable]
    public abstract class BaseState : MonoBehaviour
    {

        //Base state which other states are derived from. Does nothing.

        [Tooltip("State the should be changed to after this one")]
        public BaseState NextState;

        [HideInInspector]
        public StateMachine myStateMachine;

        private void Start()
        {

            myStateMachine = GetComponent<StateMachine>();

        }


        public abstract void OnActivate();

        public abstract System.Type Perform();

        public abstract void OnDeactivate();



    }
}
