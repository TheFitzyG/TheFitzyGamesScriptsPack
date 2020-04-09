using System.Collections;
using UnityEngine;

namespace TFG_SP
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController2DTopDown : MonoBehaviour
    {

        [SerializeField]
        private float MovementSpeed;

        [Tooltip("Speed that control for the character will be lost at. e.g. for knockback or external forces.")]
        [SerializeField]
        private float SpeedThreshold;

        [Header("Dashing")]
        [SerializeField] private float dashSpeed;
        [SerializeField] private float dashduration;
        [SerializeField] private float dashRechargeTime;

        private Rigidbody2D rb;

        [Header("States")]
        public State state;

        private int DashCharges = 1;

        BasicControls BC;
        private void OnEnable()
        {
            BC = new BasicControls();
            BC.Basic.Enable();
        }

        private void OnDisable()
        {
            BC.Basic.Disable();
        }


        [Space] public Direction direction;
        public enum State
        {

            walking,
            dashing

        }

        public enum Direction
        {
            North,
            South,
            East,
            West

        }


        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {

            if (state == State.walking)
            {

                CheckDash();
                Walk();


            }
            else if (state == State.dashing)
            {

                Dash();

            }

            direction = SetDirection();

        }

        Vector2 readInput()
        {
            Vector2 inputVector = BC.Basic.Move.ReadValue<Vector2>();

            if (inputVector.magnitude > 1f)
            {

                inputVector.Normalize();


            }
            else
            {

                inputVector *= 0.8f;

            }

            return inputVector;

        }


        Direction SetDirection()
        {

            Vector2 Input = readInput();

            if (Input.magnitude > 0.1f)
            {

                if (Mathf.Abs(Input.x) > Mathf.Abs(Input.y))
                {

                    if (Input.x > 0)
                    {

                        return Direction.East;

                    }
                    else
                    {

                        return Direction.West;

                    }

                }
                else
                {

                    if (Input.y > 0)
                    {

                        return Direction.North;

                    }
                    else
                    {

                        return Direction.South;

                    }

                }
            }

            return direction;

        }

        void Walk()
        {

            Vector2 movementVector = readInput() * MovementSpeed;

            //allows for external forces to take control. e.g. Knockback, boost pads etc.
            if (rb.velocity.magnitude < SpeedThreshold)
            {

                rb.velocity = movementVector;

            }

        }

        void Dash()
        {

            Vector2 movementVecotor = readInput() * dashSpeed;

            rb.velocity = movementVecotor;

        }

        void CheckDash()
        {

            if (DashCharges > 0)
            {
                if (BC.Basic.Jump.ReadValue<float>() > 0.5f)
                {

                    if (state == State.walking)
                    {
                        StartCoroutine(DashTimer());
                    }
                }
            }

        }

        IEnumerator DashTimer()
        {

            state = State.dashing;

            DashCharges--;

            yield return new WaitForSeconds(dashduration);

            state = State.walking;

            StartCoroutine(DashRecharge());

        }

        IEnumerator DashRecharge()
        {
            yield return new WaitForSeconds(dashRechargeTime);

            DashCharges++;

        }

    }
}
