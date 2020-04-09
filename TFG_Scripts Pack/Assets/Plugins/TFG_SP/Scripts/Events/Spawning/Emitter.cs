using UnityEngine;


namespace TFG_SP
{
    public class Emitter : MonoBehaviour
    {
        [System.Serializable]
        public struct Emitable
        {

            [Tooltip("Just used to change name in list")]
            public string EditorDisplayName;

            [Tooltip("Desired Prefab to emit")]
            public GameObject GameObjectToEmit;
            [Tooltip("How many should be emiitted at once")]
            public int EmissionCount;

            [Tooltip("Offset to emitter position")]
            public Vector3 LocalEmissionPoint;

            [Tooltip("Inherit the rotation of the emitter or just have default rotation")]
            public bool InheritRotation;

            [Space]
            [Tooltip("Attempt to trigger A 'Spawnable' behavour on emitted objects")]
            public bool TriggerSpawnable;
            [Tooltip("If Triggering Spawnable, how long should it take to spawn?")]
            public float SpawnTime;

            [Space]
            [Tooltip("Only applies if emmitted object contains a rigidbody")]
            [Header("Rigidbody Forces")]
            public Vector3 ForceToApply;
            [Tooltip("Randomises force applied between (-x and x) ....")]
            public Vector3 ForceVariation;

            public Vector3 TorqueToApply;

            [Tooltip("Randomises force applied between (-x and x) ....")]
            public Vector3 TorqueVariation;

        }

        public Emitable[] ToEmit;


        public void EmitAll()
        {

            foreach (var E in ToEmit)
            {

                Emit(E);

            }

        }

        private void Emit(Emitable E)
        {

            for (int i = 0; i < E.EmissionCount; i++)
            {

                GameObject temp = Instantiate(E.GameObjectToEmit);
                temp.transform.position = transform.position + E.LocalEmissionPoint;

                if (E.InheritRotation)
                {
                    temp.transform.rotation = transform.rotation;
                }
                else
                {
                    temp.transform.rotation = Quaternion.Euler(Vector3.zero);
                }

                if (E.TriggerSpawnable)
                {

                    try
                    {

                        temp.GetComponent<SpawnableBaseClass>().SpawnMe(E.SpawnTime);

                    }
                    catch
                    {

                        Debug.Log("Failed to Find Spawnable on: " + temp.name);

                    }

                }

                if (temp.GetComponent<Rigidbody>())
                {


                    Vector3 NewForce = new Vector3(E.ForceToApply.x + Random.Range(-E.ForceVariation.x, E.ForceVariation.x), E.ForceToApply.y + Random.Range(-E.ForceVariation.y, E.ForceVariation.y), E.ForceToApply.z + Random.Range(-E.ForceVariation.z, E.ForceVariation.z));

                    temp.GetComponent<Rigidbody>().AddForce(NewForce, ForceMode.Impulse);

                    Vector3 NewTorque = new Vector3(E.TorqueToApply.x + Random.Range(-E.TorqueVariation.x, E.TorqueVariation.x), E.TorqueToApply.y + Random.Range(-E.TorqueVariation.y, E.TorqueVariation.y), E.TorqueToApply.z + Random.Range(-E.TorqueVariation.z, E.TorqueVariation.z));

                    temp.GetComponent<Rigidbody>().AddTorque(NewTorque, ForceMode.Force);


                }

            }

        }


        public void EmitIndex(int index)
        {

            try
            {

                Emit(ToEmit[index]);

            }
            catch
            {

                Debug.Log("Failed to Emit @ index: " + index);

            }


        }


        private void OnDrawGizmos()
        {

            foreach (Emitable e in ToEmit)
            {

                Vector3 LocalPosition = transform.position + e.LocalEmissionPoint;

                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(LocalPosition, 0.1f);
                Gizmos.DrawLine(LocalPosition, LocalPosition + (e.ForceToApply.normalized * 0.5f));

            }

        }


    }
}


//NGL, I'm very proud of this script