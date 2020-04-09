using UnityEngine;

namespace TFG_SP
{
    public class Chase : BaseState
    {

        [Space]
        public float Speed;
        [Tooltip("Speed that control for the character will be lost at. e.g. for knockback or external forces.")]
        public float ThresholdSpeed;

        //  public float TurningSpeed;

        [Tooltip("Distance from Statemachine Target in which chasing will be given up")]
        [Space] public float GiveUpRange;

        private Rigidbody2D rb;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();

            myStateMachine = GetComponent<StateMachine>();


        }



        public override void OnActivate()
        {

        }

        System.Type StopChase()
        {

            rb.velocity = Vector2.zero;

            return NextState.GetType();

        }

        public override System.Type Perform()
        {

            if (myStateMachine.Target == null)
            {

                return StopChase();

            }


            if (Vector2.Distance(transform.position, myStateMachine.Target.position) > GiveUpRange)
            {

                return StopChase();

            }


            Vector2 direction = (Vector2)(myStateMachine.Target.position - transform.position);
            direction.Normalize();


            if (rb.velocity.magnitude < ThresholdSpeed)
            {


                rb.velocity = direction * Speed;

            }



            // float rotateAmount = Vector3.Cross(direction, transform.right).z;

            //rotateAmount = (float) System.Math.Round(rotateAmount, 4);

            // float scaledRotateAmount = (float) (rotateAmount * TurningSpeed);



            return null;

        }

        public override void OnDeactivate()
        {

        }


        void OnDrawGizmosSelected()
        {


#if UNITY_EDITOR

            UnityEditor.Handles.color = Color.red;
            UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, GiveUpRange);

#endif

        }


    }
}