using UnityEngine;

namespace Player
{
    public class Player2DController : MonoBehaviour
    {
        public float speed = 350f;

        private float _horizontalMove = 0f;
        private Rigidbody2D _player;
        private Vector2 _screenBounds;
        private float _characterWidth;

        // Start is called before the first frame update
        private void Start()
        {
            _player = GetComponent<Rigidbody2D>();
            _screenBounds =
                Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,
                    Camera.main.transform.position.z));
            _characterWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        }

        // Update is called once per frame
        private void Update()
        {
            _horizontalMove = Input.GetAxis("Horizontal");
        }

        private void FixedUpdate()
        {
            var velocity = _horizontalMove * Time.fixedDeltaTime * speed;
            _player.velocity = new Vector3(velocity, 0, 0);
        }

        private void LateUpdate()
        {
            var viewPos = transform.position;

            viewPos.x = Mathf.Clamp(viewPos.x, (_screenBounds.x * -1) + _characterWidth,
                _screenBounds.x - _characterWidth);
            transform.position = viewPos;
        }
    }
}
