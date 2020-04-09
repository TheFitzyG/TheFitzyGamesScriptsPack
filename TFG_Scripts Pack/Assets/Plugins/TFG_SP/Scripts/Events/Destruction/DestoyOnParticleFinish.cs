using UnityEngine;

namespace TFG_SP
{
    [RequireComponent(typeof(ParticleSystem))]
    public class DestoyOnParticleFinish : MonoBehaviour
    {

        //Destroys the gameobject when its particle system has stopped. ye know, to destroy VFX things that are spawned and whatnot.

        private ParticleSystem myParticleSystem;

        void Start()
        {
            myParticleSystem = GetComponent<ParticleSystem>();
        }


        void Update()
        {

            if (myParticleSystem.isStopped)
            {

                Destroy(gameObject);

            }

        }
    }
}
