using UnityEngine;

public class ResizeBg : MonoBehaviour
{
    private Vector3 _worldBounds;
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        var screenHeight = Camera.main.orthographicSize * 2f;
        var screenWidth = screenHeight / Screen.height * Screen.width;
        _worldBounds = new Vector2(screenWidth, screenHeight);

        _renderer.size = _worldBounds;
    }
}