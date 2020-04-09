using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace TFG_SP
{
    public class TimerEvent : MonoBehaviour
    {

        [Tooltip("Duration of timer if one is not set on StartTimer()")]
        public float TimerDurationDefault = 1f;

        [Tooltip("Start timer with default duration on enable")]
        public bool BeginOnEnable;

        [Tooltip("When the timer ends, begin it again?")]
        public bool Looping;

        public UnityEvent OnTimerEnd;

        private bool TimerActive;


        private void OnEnable()
        {

            if (BeginOnEnable)
            {

                StartTimer();

            }

        }

        public void StartTimer()
        {

            StartCoroutine(Timer(TimerDurationDefault));

        }

        public void StartTimer(float duration)
        {

            StartCoroutine(Timer(duration));

        }

        IEnumerator Timer(float duration)
        {

            if (TimerActive)
            {
                yield break;
            }

            TimerActive = true;

            yield return new WaitForSeconds(duration);

            TimerActive = false;

            OnTimerEnd?.Invoke();

            if (Looping)
            {

                StartTimer();

            }

        }

    }
}
