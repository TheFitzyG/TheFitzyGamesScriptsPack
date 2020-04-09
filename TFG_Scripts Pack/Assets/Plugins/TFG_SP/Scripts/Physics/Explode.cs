
using UnityEngine;
using UnityEngine.Events;


namespace TFG_SP
{
    public class Explode : MonoBehaviour
    {
        [Tooltip("Radius of Blast")]
        public float Range;

        [Tooltip("Damage dealt to hit objects with a 'Health' script")]
        public int Damage;

        [Tooltip("Force applied to hit objects with a rigidbody")]
        public float Knockback;

        [Tooltip("Upwards modifier to knockback force. bigger number more things go up")]
        public float UpwardsModifyer = 0f;

        [Tooltip("Is line of sight required for the blast to take effect")]
        public bool RequireLineOfSight;

        [Tooltip("Objects that arer effected by blast")]
        public LayerMask ExplosionDamageMask;

        [Tooltip("Mask of objects that will block line of sight")]
        public LayerMask BlockingMask;

        public UnityEvent OnExplode;


        public void ExplodeMe()
        {

            OnExplode?.Invoke();

            Collider[] hits = Physics.OverlapSphere(transform.position, Range, ExplosionDamageMask);

            foreach (Collider C in hits)
            {

                if (RequireLineOfSight)
                {

                    Vector3 Direction = C.transform.position - transform.position;
                    Direction.Normalize();

                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, Direction, out hit, Range, BlockingMask))
                    {
                        if (hit.collider != C)
                        {

                            Debug.Log("Not Myself");
                            continue;
                        }
                    }
                    else
                    {
                        Debug.Log("nothing hit");
                    }

                }

                try
                {
                    C.GetComponent<Health>().ChangeHealth(-Damage);
                }
                catch
                {
                    Debug.Log("No Health on Object: " + C.gameObject);
                }

                try
                {
                    C.GetComponent<Rigidbody>().AddExplosionForce(Knockback, transform.position, Range, UpwardsModifyer, ForceMode.Impulse);
                }
                catch
                {
                    Debug.Log("No Rigidbody on Object: " + C.gameObject);
                }

            }

        }

        private void OnDrawGizmosSelected()
        {

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, Range);

        }

    }
}