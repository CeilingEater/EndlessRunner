using UnityEngine;

public class Boss2Controller : MonoBehaviour
{
    public GameObject bossPrefab;
    public Transform spawnPoint;
    public float levelDuration = 500f;
    private float timer = 0f;
    private bool hasSpawnedBoss = false;

    void Update()
    {
        timer += Time.deltaTime;

        if (!hasSpawnedBoss && timer >= levelDuration / 2f)
        {
            Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);
            hasSpawnedBoss = true;
        }
    }
}
