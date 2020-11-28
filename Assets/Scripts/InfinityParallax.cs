using UnityEngine;

public class InfinityParallax : MonoBehaviour
{
    public float speed = 2f;

    private Material _material;
    private Vector2 _offset;


    // Start is called before the first frame update
    private void Start()
    {
        var sr = GetComponent<SpriteRenderer>();
        _material = sr.material;
        _offset = _material.mainTextureOffset;
    }

    // Update is called once per frame
    private void Update()
    {
        _offset.y += Time.deltaTime / speed;
        _material.mainTextureOffset = _offset;
    }
}
