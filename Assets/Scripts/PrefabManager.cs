using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    
    [SerializeField] private float prefabMoveSpeed = 30f;
    [SerializeField] private float xPrefabPosition = 50f;
    [SerializeField] private float zPrefabMin = -2.5f, zPrefabMax = 2.5f; //was 4.5f
    [SerializeField] private float yRotationMin = -90, yRotationMax = 90f;
    
    [SerializeField] private GameObject[] obstacles;
    private List<GameObject> instantiatedPrefabs = new List<GameObject>(); //list to keep spawned prefabs in

    public GameObject obstacleSpawner;
    public GameObject obstacleEnd;
    
    public static bool isObstacleDestroyed;
    
    
    void Start()
    {
        InvokeRepeating(nameof(SpawnPrefabs), 1.0f, 1f);  //spawns prefabs in intervals
    }

    void Update()
    {
        MovePrefabs();
        DeletePrefabs();
    }
    
    private void MovePrefabs()
    {
        for (int i = 0; i < instantiatedPrefabs.Count; i++)
        {
            if (instantiatedPrefabs[i] != null)
            {
                instantiatedPrefabs[i].transform.position += Vector3.right * prefabMoveSpeed * Time.deltaTime;
            }
        }       
    }
    

    private void SpawnPrefabs()
    {
        
        /*if (obstacles.Length == 0 || player == null) return;

        GameObject spawnPrefab = obstacles[Random.Range(0, obstacles.Length)];

        float randomZ = player.transform.position.z + Random.Range(spawnDistanceMin, spawnDistanceMax);
        float xOffset = Random.Range(-spawnDistanceMin, spawnDistanceMin);

        Vector3 spawnPosition = new Vector3(player.transform.position.x + xOffset, 0.0f, randomZ);
        GameObject newObstacle = Instantiate(spawnPrefab, spawnPosition, Quaternion.identity);*/
        

        if (obstacles.Length == 0) return;
        
        GameObject spawnPrefab = obstacles[Random.Range(0, obstacles.Length)]; //spawn random prefab from array
        float randomZ = Random.Range(zPrefabMin, zPrefabMax); //to spawn obstacle randomly in front of player
        
        float randomYRotation = Random.Range(yRotationMin, yRotationMax); // rotation

        Quaternion randomRotation = Quaternion.Euler(0f, randomYRotation, 0f);
        
        GameObject newObstacle = Instantiate(spawnPrefab, new Vector3(xPrefabPosition, 0.0f, randomZ), randomRotation); //autofilled LOL i hope its right

        instantiatedPrefabs.Add(newObstacle);
        
        //Debug.Log("obstacles spawned");

    }

    private void DeletePrefabs()
    {
        int obstaclesDestroyed = 0;
        
        for (int i = instantiatedPrefabs.Count - 1; i >= 0; i--) 
        {
            if (instantiatedPrefabs[i] != null)
            {
                if (instantiatedPrefabs[i].transform.position.x >= obstacleEnd.transform.position.x) //delete prefabs if they go past obstacleEnd
                {
                    Destroy(instantiatedPrefabs[i]);
                    instantiatedPrefabs.RemoveAt(i);
                    //Debug.Log("obstacles deleted");
                    obstaclesDestroyed++; //increment for each counter
                }          
            }
        }
        
        
        
        if (obstaclesDestroyed > 0)
        {
            GameManager.Instance.IncrementScore(obstaclesDestroyed);
        }
    }
}
