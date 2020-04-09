using DG.Tweening;
using UnityEngine;

namespace TFG_SP
{
    public class DestructableSinkIntoGround : DestructableBaseClass
    {

        //Requires DoTween http://dotween.demigiant.com/

        [Tooltip("How deep should I travel before dissapearing?")]
        public float depth = 1;

        public override void DestroyMe(float time)
        {

            //Setting every rigidbody to kinematic to avoid physics bugs
            Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();

            foreach (Rigidbody r in rbs)
            {

                r.isKinematic = true;

            }


            transform.DOMoveY(transform.position.y - depth, time);
            Destroy(gameObject, time);
        }

    }
}
