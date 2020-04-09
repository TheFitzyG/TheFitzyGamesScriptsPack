using UnityEngine;
using UnityEngine.Events;


namespace TFG_SP
{

    //causes object to stick into surface of hit object. e.g. for like shot arrows sticking into walls. 

    [RequireComponent(typeof(Rigidbody))]
    public class StickIntoSurface : MonoBehaviour
    {

        public LayerMask StickySurfaceMask;

        public UnityEvent OnStick;


        private void OnCollisionEnter(Collision collision)
        {



            if (TFG_SP.Checks.CompareLayers(StickySurfaceMask, collision.gameObject))
            {

                Stick(collision.transform);

            }

        }


        void Stick(Transform Parent)
        {

            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Collider>().enabled = false;
            transform.SetParent(Parent);


            OnStick?.Invoke();

        }

    }
}
