using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TFG_SP
{

    // Tracks the health of several objects and calls an event when they ahve all died
    public class EventOnEnemiesDead : MonoBehaviour
    {


        public List<Health> EnemiesToKill = new List<Health>();

        public UnityEvent OnAllEnemiesDead;

        private bool triggered;


        private void Update()
        {

            if (!triggered)
            {



                foreach (Health H in EnemiesToKill)
                {

                    if (!H.hasDied)
                    {
                        return;
                    }

                }

                triggered = true;

                OnAllEnemiesDead?.Invoke();




            }



        }


    }
}
