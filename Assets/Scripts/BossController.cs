using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject bossPrefab;
    public Transform spawnPoint;

    public float levelDuration = 500;
    public float timeElapsed;
    public bool bossActive = false;
   
    void FixedUpdate()
    {
        timeElapsed += Time.fixedDeltaTime;

        if (!bossActive && timeElapsed >= levelDuration/2f)
        {
            SpawnBoss();
            bossActive = true;
        }
    }

    private void SpawnBoss()
    {
        GameObject boss = Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity); //spawns boss in specific location
        boss.transform.Rotate(0, 90, 0); //pug didnt hv same rotation as prefab :(

    }
}
