using UnityEngine;

namespace TFG_SP
{
    public static class Knockback
    {

        //gives knockback to things

        public static void KnockBack2D(Rigidbody2D target, Vector2 from, float force)
        {

            Vector2 direction = (Vector2)target.transform.position - from;

            target.AddForce(direction * force, ForceMode2D.Impulse);

        }

        public static void KnockBack3D(Rigidbody target, Vector3 from, float force)
        {

            Vector3 direction = target.transform.position - from;

            target.AddForce(direction * force, ForceMode.Impulse);

        }

    }
}
