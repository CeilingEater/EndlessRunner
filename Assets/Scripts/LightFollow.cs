using UnityEngine;

public class LightFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 11, 0);
    void Update()
    {
        if (player == null) return;
        transform.position = player.position + offset;
    }
}
