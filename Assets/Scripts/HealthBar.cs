using UnityEngine;
using UnityEngine.UI;
using Variables;

namespace DefaultNamespace
{
    public class HealthBar : MonoBehaviour
    {
        public IntVariable health;
        public Text healthText;

        private void Start()
        {
            healthText.text = health.Value.ToString();
        }

        private void Update()
        {
            healthText.text = health.Value.ToString();
        }
    }
}