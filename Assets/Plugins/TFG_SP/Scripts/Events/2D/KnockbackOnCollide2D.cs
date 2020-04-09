using UnityEngine;

namespace TFG_SP
{

    public class KnockbackOnCollide2D : MonoBehaviour
    {

        [Tooltip("Knockback Force")]
        public float Force;

        public LayerMask KnockbackMask;

        [Tooltip("Apply force from point of contact or center of body")]
        public bool UseCollisionPoint;

        [Tooltip("Allows knockback on trigger enter")]
        public bool UseTrigger;

        private void OnCollisionEnter2D(Collision2D other)
        {

            if (Checks.CompareLayers(KnockbackMask, other.gameObject))
            {

                if (other.gameObject.GetComponent<Rigidbody2D>())
                {

                    Vector2 pos = transform.position;

                    if (UseCollisionPoint)
                    {

                        pos = other.contacts[0].point;

                    }

                    Knockback.KnockBack2D(other.rigidbody, pos, Force);

                }

            }

        }


        private void OnTriggerEnter2D(Collider2D other)
        {

            if (UseTrigger)
            {

                if (Checks.CompareLayers(KnockbackMask, other.gameObject))
                {

                    if (other.gameObject.GetComponent<Rigidbody2D>())
                    {

                        Vector2 pos = transform.position;

                        Knockback.KnockBack2D(other.gameObject.GetComponent<Rigidbody2D>(), pos, Force);

                    }

                }

            }
        }

    }
}