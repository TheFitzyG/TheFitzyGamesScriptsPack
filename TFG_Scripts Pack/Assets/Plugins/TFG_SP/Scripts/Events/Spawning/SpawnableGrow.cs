using DG.Tweening;
using UnityEngine;

namespace TFG_SP
{

    public class SpawnableGrow : SpawnableBaseClass
    {

        //Requires DoTween http://dotween.demigiant.com/


        public Vector3 InitialSize = Vector3.zero;
        public Vector3 FinalScale = Vector3.one;

        public override void SpawnMe(float t)
        {

            transform.localScale = InitialSize;
            transform.DOScale(FinalScale, t);


        }
    }
}
