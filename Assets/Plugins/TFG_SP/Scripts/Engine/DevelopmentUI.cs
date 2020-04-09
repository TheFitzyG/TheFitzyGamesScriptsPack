using System.Collections;
using TMPro;
using UnityEngine;


namespace TFG_SP
{



    public class DevelopmentUI : MonoBehaviour
    {

        //Shows the FPS

        public TMP_Text FPSCounter;
        public int FPSChecksPerSecond;

        void OnEnable()
        {

            StartCoroutine(FPSUpdate());

        }

        private void OnDisable()
        {

            StopAllCoroutines();

        }


        IEnumerator FPSUpdate()
        {

            while (true)
            {

                yield return new WaitForSecondsRealtime(1f / FPSChecksPerSecond);
                FPSCounter.text = ((int)(1f / Time.deltaTime)) + " fps";


            }

        }


    }
}
