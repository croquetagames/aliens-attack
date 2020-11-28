using UnityEngine;
using UnityEngine.Events;
using Variables;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public IntReference maxHealth;
        public IntVariable health;
        public UnityEvent damageEvent;
        public UnityEvent deathEvent;

        private void Start()
        {
            // Reset life
            health.SetValue(maxHealth);
        }

        public void AddDamage()
        {
            health.Value -= 1;
            damageEvent.Invoke();

            if (health.Value <= 0)
            {
                deathEvent.Invoke();
            }
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
           AddDamage();
        }
    }
}