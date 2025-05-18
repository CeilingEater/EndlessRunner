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
    public float chargeSpeed = 8f;

    void FixedUpdate()
    {
        timeElapsed += Time.fixedDeltaTime;

        if (!bossActive && timeElapsed >= levelDuration/2f)
        {
            SpawnBoss();
            bossActive = true;
            ChargeBoss();
        }

        
    }

    private void SpawnBoss()
    {
        GameObject instantiatedBoss = Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity); //spawns boss in specific location
        instantiatedBoss.transform.Rotate(0, 90, 0); //pug didnt hv same rotation as prefab :(
    }
    
    private void ChargeBoss()
    {
        if (instantiatedBoss != null)
        {
            instantiatedBoss.transform.position += Vector3.right * (chargeSpeed * Time.deltaTime);
        }
              
    }
}
