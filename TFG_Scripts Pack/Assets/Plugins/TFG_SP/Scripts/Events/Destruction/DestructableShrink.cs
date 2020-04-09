using DG.Tweening;
using UnityEngine;


namespace TFG_SP
{

    //Requires DoTween http://dotween.demigiant.com/

    public class DestructableShrink : DestructableBaseClass
    {


        public override void DestroyMe(float t)
        {

            Destroy(gameObject, t);
            transform.DOScale(Vector3.zero, t);

        }

    }
}