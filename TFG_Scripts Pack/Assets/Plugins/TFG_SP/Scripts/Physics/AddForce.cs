using UnityEngine;

namespace TFG_SP
{
    [RequireComponent(typeof(Rigidbody))]
    public class AddForce : MonoBehaviour
    {
        private Rigidbody rb;

        [Tooltip("Impulse force to add")]
        public Vector3 Force;

        public void ApplyForce()
        {

            rb = GetComponent<Rigidbody>();

            rb.AddForce(Force, ForceMode.Impulse);

        }


    }
}
