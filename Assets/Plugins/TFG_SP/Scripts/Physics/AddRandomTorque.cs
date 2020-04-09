using UnityEngine;

namespace TFG_SP
{
    [RequireComponent(typeof(Rigidbody))]
    public class AddRandomTorque : MonoBehaviour
    {

        [Tooltip("How strong should the torque be")]
        public float TorqueMultiplier;

        private Rigidbody rb;

        public void ApplyTorque()
        {

            rb = GetComponent<Rigidbody>();

            Vector3 T = Random.insideUnitSphere * TorqueMultiplier;

            rb.AddTorque(T, ForceMode.Impulse);


        }

    }
}
