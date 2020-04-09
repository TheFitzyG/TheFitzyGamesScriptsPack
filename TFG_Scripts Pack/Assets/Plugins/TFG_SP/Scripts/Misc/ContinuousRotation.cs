using UnityEngine;

namespace TFG_SP
{
    public class ContinuousRotation : MonoBehaviour
    {

        [Tooltip("Degrees per second")]
        public float Speed;

        [Tooltip("Axis of rotation, normalised in calculations")]
        public Vector3 Axis = Vector3.right;


        [Tooltip("Preview Changes")]
        public bool Preview;
        [Tooltip("Point in cycle to preview changes")]
        [Range(0, 1f)]
        public float PreviewPhase = 0;



        private float theta;


        void Update()
        {
            //LocalRoatation to allow for compounding rotations from parents
            transform.localRotation = Quaternion.AngleAxis(theta, Axis.normalized);
            theta += Speed * Time.deltaTime;
        }


        private void OnValidate()
        {

            if (Preview)
            {

                transform.localRotation = Quaternion.AngleAxis(360f * PreviewPhase, Axis.normalized);

            }
            else
            {

                transform.localRotation = Quaternion.Euler(Vector3.zero);

            }

        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;


            Vector3 NewAxis = transform.rotation * Axis.normalized;

            Vector3 PointA = transform.position - NewAxis;
            Vector3 PointB = transform.position + NewAxis;



            Gizmos.DrawLine(PointA, PointB);

        }

    }
}
