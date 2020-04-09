using UnityEngine;

namespace TFG_SP
{
    public class Billborder : MonoBehaviour
    {

        [Tooltip("Only allow rotation on the Y 'UP' Axis")]
        public bool LockToYRotation;

        void LateUpdate()
        {

            Vector3 LookPosition = Camera.main.transform.position;

            if (LockToYRotation)
            {

                LookPosition.y = transform.position.y;

            }

            transform.LookAt(LookPosition);

        }
    }
}
