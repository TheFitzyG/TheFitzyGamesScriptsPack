using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Script for managing and perfoming states.
//Based on and Modified from Jason Weimann : https://youtu.be/YdERlPfwUb0

namespace TFG_SP
{
    public class StateMachine : MonoBehaviour
    {

        private Dictionary<Type, BaseState> AvailableStates;

        public BaseState CurrentState { get; private set; }

        [Tooltip("Target for states to interact with. E.g. the player to be chased by the chase state.")]
        public Transform Target;

        public event Action<BaseState> OnStateChanged;

        private void Start()
        {

            setup();
        }

        void setup()
        {
            AvailableStates = new Dictionary<Type, BaseState>();

            BaseState[] states = GetComponents<BaseState>();

            Debug.Log(states.Length);

            foreach (BaseState BS in states)
            {

                AvailableStates.Add(BS.GetType(), BS);

            }

        }


        private void Update()
        {

            if (CurrentState == null)
            {

                CurrentState = AvailableStates.Values.First();

            }

            var nextState = CurrentState?.Perform();

            if (nextState != null && nextState != CurrentState.GetType())
            {

                SwitchToNewState(nextState);

            }

        }

        public void SwitchToNewState(Type nextState)
        {

            CurrentState.OnDeactivate();

            CurrentState = AvailableStates[nextState];
            OnStateChanged?.Invoke(CurrentState);

            CurrentState.OnActivate();

        }



    }
}