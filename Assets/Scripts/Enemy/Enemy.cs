using System.Collections;
using System.Collections.Generic;
using GestureRecognizer;
using UnityEngine;
using UnityEngine.Events;
using Variables;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        public ColorVariable damageColor;
        public UnityEvent landEvent;
        public UnityEvent deathEvent;
        public UnityEvent hit;

        [SerializeField] private List<string> _patterns = new List<string>();
        private Animator _animator;
        private SpriteRenderer _renderer;
        private SpriteRenderer _player;
        private Enemy2DController _controller;
        private GameObject _patternsObj;
        private Collider2D _collider;
        private bool _exploding;

        private float _topX;
        private float _spriteWidth;

        #region Animation

        private static readonly int DeathAnimation = Animator.StringToHash("death");
        private static readonly int ExplodingAnimation = Animator.StringToHash("exploding");

        #endregion

        #region Intialize

        public void SetGesturePatterns(List<GesturePattern> gesturePatterns)
        {
            for (var i = 0; i < gesturePatterns.Count; i++)
            {
                var pattern = gesturePatterns[i];
                _patterns.Add(pattern.id);

                _spriteWidth = pattern.sprite.bounds.size.x;

                var patternGO = new GameObject(pattern.id);
                var patternRenderer = patternGO.AddComponent<SpriteRenderer>();
                patternRenderer.sprite = pattern.sprite;
                patternRenderer.sortingLayerName = _renderer.sortingLayerName;
                patternRenderer.sortingOrder = _renderer.sortingOrder;
                patternGO.transform.parent = _patternsObj.transform;
                patternGO.transform.localPosition = new Vector3(i * _spriteWidth, 0f);
            }

            var center = _spriteWidth * _patterns.Count / 2f - _spriteWidth / 2f;
            _patternsObj.transform.localPosition = new Vector3((center) * -1f, .75f);
        }

        private void Awake()
        {
            _player = GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>();
            _controller = GetComponent<Enemy2DController>();
            _animator = GetComponent<Animator>();
            _renderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();

            _patternsObj = new GameObject("patterns");
            _patternsObj.transform.parent = transform;

            _topX = Camera.main.transform.position.y + Camera.main.orthographicSize;
        }

        #endregion

        private void FixedUpdate()
        {
            if (!_exploding)
            {
                Landing();
                Destroying();
            }
            else if (_exploding && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 &&
                     !_animator.IsInTransition(0))
            {
                _animator.SetBool(DeathAnimation, true);
                Destroy(gameObject);
            }
        }

        private void Landing()
        {
            if (!(transform.position.y <= _player.transform.position.y)) return;
            Exploding();
            landEvent.Invoke();
        }

        private void Destroying()
        {
            if (_patterns.Count > 0) return;
            Exploding();
            deathEvent.Invoke();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Exploding();
            }
        }

        public void RecognizedPattern(RecognitionResult result)
        {
            Debug.Log("[Recognition] Gesture: " + result.gesture.id + " Score: " + result.score.score);

            var hit = HitEnemy(result.gesture.id);
            if (!hit)
            {
                return;
            }

            StartCoroutine(nameof(EnemyDamage));
            StartCoroutine(nameof(RelocatePatternSprites));
        }

        private bool HitEnemy(string gestureId)
        {
            if (_patterns.Count <= 0)
            {
                return false;
            }

            if (IsOutOfBounds())
            {
                return false;
            }

            for (var i = 0; i < _patterns.Count; i++)
            {
                if (_patterns[i] != gestureId) continue;

                var patternTransform = _patternsObj.transform.GetChild(i);
                patternTransform.parent = null;
                Destroy(patternTransform.gameObject);
                
                if (_patterns.Count > 1) hit.Invoke();
                _patterns.RemoveAt(i);
             
                return true;
            }

            return false;
        }

        private bool IsOutOfBounds()
        {
            return transform.position.y + _renderer.size.y >= _topX;
        }

        private void Exploding()
        {
            if (_exploding)
            {
                return;
            }

            Destroy(_patternsObj);
            _collider.enabled = false;

            _exploding = true;
            _controller.Stop();
            _animator.SetBool(ExplodingAnimation, true);
        }

        private void RelocatePatternSprites()
        {
            for (var i = 0; i < _patterns.Count; i++)
            {
                var patternGO = _patternsObj.transform.GetChild(i).gameObject;
                patternGO.transform.localPosition = new Vector3(i * _spriteWidth, 0f);
            }

            var center = _spriteWidth * _patterns.Count / 2f - _spriteWidth / 2f;
            _patternsObj.transform.localPosition = new Vector3(center * -1f, .75f);
        }

        private IEnumerator EnemyDamage()
        {
            _renderer.color = damageColor.Value;
            yield return new WaitForSeconds(.1f);
            _renderer.color = Color.white;
        }
    }
}