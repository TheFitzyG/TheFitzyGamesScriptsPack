using UnityEngine;

namespace TFG_SP
{
    public abstract class SpawnableBaseClass : MonoBehaviour
    {

        //Abstract class for behaviour on spawn. e.g. growing to full size

        public abstract void SpawnMe(float time);

    }
}