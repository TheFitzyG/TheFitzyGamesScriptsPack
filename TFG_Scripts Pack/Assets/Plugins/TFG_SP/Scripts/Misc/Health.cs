using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace TFG_SP
{
    public class Health : MonoBehaviour
    {
        [Tooltip("Maximum health")]
        public int MaxHealth;

        [HideInInspector]
        public int currentHealth { get; private set; }

        [Tooltip("How long should I be invincible for after losing health")]
        public float InvincibilityTime = 0f;

        private bool Invincible;

        [Space]
        public UnityEvent OnHealthLost;

        public UnityEvent OnHealthGained;



        [Space]
        public UnityEvent OnZeroHealth;


        [HideInInspector]
        public bool hasDied;

        private void Start()
        {
            currentHealth = MaxHealth;
        }


        public void ChangeHealth(int value)
        {

            if (!hasDied)
            {

                if (!Invincible || value > 0)
                {
                    currentHealth += value;
                }

                if (value < 0)
                {
                    OnHealthLost?.Invoke();

                    StartCoroutine(InvincibilityTimer());

                }
                else
                {

                    OnHealthGained?.Invoke();

                }

                currentHealth = Mathf.Clamp(currentHealth, 0, MaxHealth);

                if (currentHealth <= 0)
                {

                    OnZeroHealth?.Invoke();
                    hasDied = true;

                }
            }

        }

        IEnumerator InvincibilityTimer()
        {

            if (Invincible)
            {

                yield break;
            }

            Invincible = true;

            yield return new WaitForSeconds(InvincibilityTime);

            Invincible = false;

        }

    }
}