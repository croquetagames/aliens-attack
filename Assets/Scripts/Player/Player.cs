using System.Collections;
using UnityEngine;
using Variables;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public ColorVariable damageColor;
        private SpriteRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }
        
        public void AddDamage()
        {
            StartCoroutine(nameof(PlayerDamage));
        }

        public void Death()
        {
            Debug.Log("player is dead");
        }

        private IEnumerator PlayerDamage()
        {
            _renderer.color = damageColor.Value;
            yield return new WaitForSeconds(.1f);
            _renderer.color = Color.white;
        }
    }
}