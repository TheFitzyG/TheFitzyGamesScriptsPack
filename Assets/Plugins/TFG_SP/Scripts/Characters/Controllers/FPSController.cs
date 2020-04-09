using UnityEngine;

namespace TFG_SP
{
    [RequireComponent(typeof(CharacterController))]
    public class FPSController : MonoBehaviour
    {

        //If you want to stop the player moving during cutscenes etc
        public static bool CanMove = true;

        private CharacterController cc;

        [Header("Movement")]
        [SerializeField]
        private float speed;

        [Header("Look")]
        [SerializeField] private float rotationSpeed;

        [Tooltip("A transform containing the camera, should be located inside the head")]
        [SerializeField] private Transform cameraTransform;

        [Tooltip("The Max and Min angles the camera can look. both should be positive")]
        [SerializeField] private float verticleMax, verticleMin;


        private float angle;
        private Vector3 movement;
        private float yMovement;
        private bool hasJumped;



        [Header("Jump")]
        [SerializeField]
        private float jumpVelocity;

        [Tooltip("Multiplication of gravity to be applied when jump is not pressed")]
        [SerializeField]
        private float fallMulitplier;



        private BasicControls BC;

        private void Awake()
        {
            BC = new BasicControls();

        }


        void Start()
        {


            cc = GetComponent<CharacterController>();

        }

        private void OnEnable()
        {
            BC.Basic.Enable();
        }

        private void OnDisable()
        {
            BC.Basic.Disable();
        }


        void Update()
        {

            if (CanMove)
            {

                movement = GetMovement();

                yMovement = Jump();
            }
            else
            {

                movement = Vector2.zero;
                yMovement = 0f;

            }


            SetCharacterControllerMove();
            if (CanMove) Rotate();

        }

        void Rotate()
        {

            Vector2 Input = BC.Basic.Look.ReadValue<Vector2>();

            if (Mathf.Abs(Input.x) > 0.01f)
            {

                Quaternion newQuat = Quaternion.AngleAxis((rotationSpeed) * Input.x, Vector3.up);

                transform.rotation = transform.rotation * newQuat;


            }

            if ((Mathf.Abs(Input.y) > 0.01f))
            {



                angle += rotationSpeed * -Input.y;


                angle = Mathf.Clamp(angle, -verticleMin, verticleMax);



                cameraTransform.localRotation = Quaternion.Euler(angle, 0f, 0f);
            }


        }


        Vector3 GetMovement()
        {

            Vector2 context = BC.Basic.Move.ReadValue<Vector2>();


            Vector3 movementInput = (context.x * transform.right) + (context.y * transform.forward);



            movementInput.y = 0;

            if (movementInput.magnitude > 1f)
            {

                movementInput.Normalize();

            }



            return movementInput * speed;


        }

        float Jump()
        {

            float vertForce = yMovement;

            if (cc.isGrounded)
            {

                vertForce = -0.1f;
                hasJumped = false;

                if (BC.Basic.Jump.triggered)
                {

                    vertForce = jumpVelocity;
                    hasJumped = true;

                }


            }

            vertForce += Physics.gravity.y * fallMulitplier * Time.deltaTime;

            return vertForce;

        }

        void SetCharacterControllerMove()
        {

            Vector3 toMove = new Vector3(movement.x, yMovement, movement.z);

            cc.Move(toMove * Time.deltaTime);

        }


    }
}
