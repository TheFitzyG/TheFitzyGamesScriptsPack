using UnityEngine;

//Tutorial for this scripts usage can be found here: https://youtu.be/1WwLgfJj7Uo

namespace TFG_SP
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController3rdPerson : MonoBehaviour
    {



        [SerializeField] private float MovementSpeed = 4f;

        private Vector3 moveDirection;


        [Tooltip("Speed that the controller rotates to face its movement direction")]
        [SerializeField] private float RotationSpeed;

        [Header("Jumping")] [SerializeField] private float JumpVelocity = 5f;

        [Tooltip("Multiplication of gravity on the controller")]
        [SerializeField] private float FallMulitplier = 2.5f;
        [Tooltip("Multiplication of Gravity on the controller when Jump has not been held")]
        [SerializeField] private float LowJumpMultiplier = 3f;

        [HideInInspector] public float XVelocity;

        private CharacterController cc;


        private float verticleForce;


        [Header("Sliding")]
        [Tooltip("Downward Force to be applied to the controller when on a slope")]
        [SerializeField] private float slopeForce;

        [Tooltip("length of slope detection ray")]
        [SerializeField] private float rayLenght;


        private float groundAngle;

        private Vector3 groundNormal;


        [Tooltip("Friction on body while Sliding")]
        public float slideFriction = 0.3f;
        [Tooltip("Lateral speed of body while sliding")]
        public float slideSpeed;

        bool hasJumped;

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




        private void Awake()
        {


            cc = GetComponent<CharacterController>();

        }


        private void Update()
        {

            XVelocity = 0f;


            Move();
            JumpCheck();
            LookForward();


            moveDirection.y = verticleForce;

            cc.Move(moveDirection * Time.deltaTime);

            SlopeMove();

        }


        private void Move()
        {

            Vector3 CameraLookDirection = Camera.main.transform.forward; //CVC.transform.forward;
            CameraLookDirection.y = 0;
            CameraLookDirection.Normalize();

            Vector3 CameraLookRight = Camera.main.transform.right; // CVC.transform.right;
            CameraLookRight.y = 0;
            CameraLookRight.Normalize();

            Vector2 rawInput = BC.Basic.Move.ReadValue<Vector2>();

            Vector3 movementInput = (rawInput.y * CameraLookDirection) + (rawInput.x * CameraLookRight);

            if (movementInput.magnitude > 1f)
            {

                movementInput.Normalize();

            }

            movementInput *= MovementSpeed;

            moveDirection = movementInput;

        }

        void SlopeMove()
        {

            if (OnSlope())
            {

                Vector3 movement = Vector3.zero;

                if (moveDirection.magnitude > 0)
                {

                    movement += Vector3.down * (cc.height / 2) * slopeForce; // * Time.deltaTime;

                }

                if (groundAngle > cc.slopeLimit && groundAngle < 90)
                {

                    movement.x += (1f - groundNormal.y) * groundNormal.x * (slideSpeed - slideFriction);
                    movement.z += (1f - groundNormal.y) * groundNormal.z * (slideSpeed - slideFriction);
                }

                cc.Move(movement * Time.deltaTime);

            }

        }

        private void JumpCheck()
        {


            if (cc.isGrounded)
            {

                verticleForce = -0.1f;
                hasJumped = false;

                if (BC.Basic.Jump.triggered)
                {

                    verticleForce = JumpVelocity;
                    hasJumped = true;

                }

            }

            if (!cc.isGrounded)
            {

                if (verticleForce < 0f)
                {

                    verticleForce += Physics.gravity.y * (FallMulitplier - 1) * Time.deltaTime;

                }
                else if (verticleForce >= 0f && BC.Basic.Jump_Held.ReadValue<float>() < 0.5)
                {

                    verticleForce += Physics.gravity.y * (LowJumpMultiplier - 1) * Time.deltaTime;

                }

            }

            verticleForce += Physics.gravity.y * Time.deltaTime;

        }


        private void LookForward()
        {

            Vector2 HorizontalVelocity = Vector2.zero;

            HorizontalVelocity = new Vector2(cc.velocity.x, cc.velocity.z);

            if (HorizontalVelocity.magnitude > cc.minMoveDistance)
            {

                HorizontalVelocity.Normalize();

                float theta = Mathf.Atan2(HorizontalVelocity.x, HorizontalVelocity.y) * 180 / Mathf.PI;


                Quaternion desired = Quaternion.Euler(0f, theta, 0f);

                Quaternion newRot = Quaternion.Lerp(transform.rotation, desired, RotationSpeed * Time.deltaTime);


                transform.rotation = newRot;

            }

        }

        bool OnSlope()
        {

            RaycastHit hit;


            if (Physics.Raycast(cc.transform.position - new Vector3(0, (cc.height / 2) - 0.1f, 0), Vector3.down, out hit, rayLenght))
            {


                if (hit.normal != Vector3.up)
                {
                    groundNormal = hit.normal;
                    groundAngle = Vector3.Angle(Vector3.up, hit.normal);


                    Debug.Log("ground Detected");



                    return true;
                }

            }

            return false;

        }

    }
}