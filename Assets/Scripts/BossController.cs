using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    //boss spawning variables
    public GameObject bossPrefab;
    private GameObject instantiatedBoss;
    public Transform spawnPoint;
    public GameObject bossEnd;  //obstacle end

    public float levelDuration = 500;
    public float timeElapsed;
    public bool bossActive = false;
   
    //boss charging variables -5.5 to 5.5
    public float chargeSpeed = 25f;

    void FixedUpdate()
    {
        timeElapsed += Time.fixedDeltaTime;

        if (!bossActive && timeElapsed >= levelDuration / 2f)
        {
            SpawnBoss();
            Debug.Log("Boss spawned");
            bossActive = true;
        }

        if (bossActive)
        {
            ChargeBoss();
            Debug.Log("Boss charging");
        }
        
    }

    private void SpawnBoss()
    {
        instantiatedBoss = Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity); //spawns boss in specific location
        instantiatedBoss.transform.Rotate(0, 90, 0); //pug didnt hv same rotation as prefab :(
    }
    
    private void ChargeBoss()
    {
        if (instantiatedBoss != null)
        {
            instantiatedBoss.transform.position += Vector3.right * (chargeSpeed * Time.fixedDeltaTime);
        }

        if (instantiatedBoss != null)
        {
            if (instantiatedBoss.transform.position.x >= bossEnd.transform.position.x)
            {
                Destroy(instantiatedBoss);
            }
        }

    }
}
