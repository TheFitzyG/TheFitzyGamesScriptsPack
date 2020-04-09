using System;
using UnityEngine;


namespace TFG_SP
{
    public class Listen : BaseState
    {

        [Tooltip("Range at which things will be detected at")]
        [Space] public float ListenRange;

        public LayerMask ListenForMask;



        public override void OnActivate()
        {

        }

        public override Type Perform()
        {

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, ListenRange);

            foreach (var H in hits)
            {

                if (Checks.CompareLayers(ListenForMask, H.gameObject))
                {

                    myStateMachine.Target = H.transform;
                    return NextState.GetType();

                }

            }

            return null;

        }

        public override void OnDeactivate()
        {

        }

        void OnDrawGizmosSelected()
        {


#if UNITY_EDITOR

            UnityEditor.Handles.color = Color.green;
            UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, ListenRange);

#endif

        }


    }
}