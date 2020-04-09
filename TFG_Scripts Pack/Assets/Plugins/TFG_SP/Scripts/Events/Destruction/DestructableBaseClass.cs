using UnityEngine;

namespace TFG_SP
{
    public abstract class DestructableBaseClass : MonoBehaviour
    {

        //Abstract class for behavoirs that destory gameobjects.

        public abstract void DestroyMe(float time);

    }
}
