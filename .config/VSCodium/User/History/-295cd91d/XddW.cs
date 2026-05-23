using System;
using System.Collections;
using UnityEngine;

namespace Metroidvania
{
    /// <summary>
    /// Class when the health of the entity is Managed
    /// </summary>
    public class HealthHandler : MonoBehaviour
    {
        /// <summary>
        /// Event Calls when the Entiy dies
        /// </summary>
        public static event Action onEntityDeath;

        /// <summary>
        /// Event Calls when the Entity heals
        /// </summary>
        public static event Action onEntityHealth;

        /// <summary>
        /// Event calls when the entity takesDamage
        /// </summary>
        public static event Action onEntityTakesDamage;

        /// <summary>
        /// Represents the current health of the entity
        /// </summary>
        [SerializeField] public float currentHealth = 1.0f;

        /// <summary>
        /// Represents the MaximumHealth that the Entity must have
        /// </summary>
        [SerializeField] private float maxHealth = 100.0f;

        /// <summary>
        /// Checks if the Entity is already death
        /// </summary>
        private bool isDead = false;

        /// <summary>
        /// Method called when the Entity dies, always waits a second after the DeathInvoke
        /// </summary>
        /// <returns></returns>
        private IEnumerator DeathEntity()
        {
            isDead = true;
            yield return new WaitForSeconds(1.0f);
            onEntityDeath?.Invoke();
            Destroy(gameObject);
        }

        /// <summary>
        /// Corrects the currentValue if it is greater than MaxHealth and lower than 0 
        /// </summary>
        private void UpdateHealth()
        {
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            else if (currentHealth < 0)
            {
                currentHealth = 0;
                if (!isDead)
                {
                    StartCoroutine(DeathEntity()); 
                }
            }
        }

        /// <summary>
        /// Method called when the Entity takes damage
        /// </summary>
        /// <param name="damage"></param> Total damage that the entity will recive
        public void TakeDamage(int damage)
        {
            if (isDead) return;
            currentHealth -= damage;
            onEntityTakesDamage?.Invoke();
            UpdateHealth();
        }

        /// <summary>
        /// Method called when the Entity heals
        /// </summary>
        /// <param name="health"></param> Total health that the Entity heals
        public void HealthEntity(int health)
        {
            if (isDead) return;
            currentHealth += health;
            onEntityHealth?.Invoke();
            UpdateHealth();
        }

        /// <summary>
        /// Returns the CurrentHealth of the Entity
        /// </summary>
        /// <returns></returns>
        public float getCurrentHealth()
        {
            return currentHealth;
        }

        void Start()
        {
            currentHealth = maxHealth;
        }

    }
}
