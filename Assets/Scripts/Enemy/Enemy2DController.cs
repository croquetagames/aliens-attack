using UnityEngine;

namespace Enemy
{
    public class Enemy2DController : MonoBehaviour
    {
        public float speed = 350f;

        private Rigidbody2D _enemy;


        // Start is called before the first frame update
        private void Start()
        {
            _enemy = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            var velocity = Time.fixedDeltaTime * speed * -1f;
            _enemy.velocity = new Vector3(0, velocity, 0);
        }

        public void Stop()
        {
            _enemy.velocity *= 0;
            enabled = false;
        }
    }
}