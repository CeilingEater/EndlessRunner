using UnityEngine;

public class TextureMover : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    private Renderer rend;
    private Vector2 offset;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        offset = new Vector2(Time.time * scrollSpeed, 0);
        rend.material.mainTextureOffset = offset;
    }
}
