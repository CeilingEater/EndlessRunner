using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    
    [SerializeField] private float prefabMoveSpeed = 30f;
    [SerializeField] private float xPrefabPosition = 50f;
    [SerializeField] private float zPrefabMin = -5f, zPrefabMax = 5f;
    
    [SerializeField] private GameObject[] obstacles;
    private List<GameObject> instantiatedPrefabs = new List<GameObject>(); //list to keep spawned prefabs in

    public GameObject obstacleSpawner;
    public GameObject obstacleEnd;
    
    public static bool isObstacleDestroyed;
    
    //List<GameObject> Obstacles = new List<GameObject>(); //rows first, columns second
    
    void Start()
    {
        InvokeRepeating(nameof(SpawnPrefabs), 1.0f, 2f);  //spawns prefabs in intervals
        InvokeRepeating(nameof(DeletePrefabs), 1.0f, 12f);  //deletes prefabs after a certain num of secs
    }

    void Update()
    {
        MovePrefabs();
    }
    
    private void MovePrefabs()
    {
        for (int i = 0; i < instantiatedPrefabs.Count; i++)
        {
            if (instantiatedPrefabs[i] != null)
            {
                instantiatedPrefabs[i].transform.position = Vector3.MoveTowards(instantiatedPrefabs[i].transform.position, obstacleEnd.transform.position, prefabMoveSpeed * Time.deltaTime);
            }
        }
        
        
    }

    private void SpawnPrefabs()
    {

        if (obstacles.Length == 0) return;
        
        GameObject spawnPrefab = obstacles[Random.Range(0, obstacles.Length)]; //spawn random prefab from array
        float randomZ = Random.Range(zPrefabMin, zPrefabMax); //to spawn obstacle randomly in front of player
        GameObject newObstacle = Instantiate(spawnPrefab, new Vector3(xPrefabPosition, 0.0f, randomZ), Quaternion.identity); //autofilled LOL i hope its right

        instantiatedPrefabs.Add(newObstacle);
        

    }

    private void DeletePrefabs()
    {
        for (int i = instantiatedPrefabs.Count - 1; i >= 0; i--) 
        {
            if (instantiatedPrefabs[i] != null)
            {
                if (instantiatedPrefabs[i].transform.position.z <= obstacleEnd.transform.position.z) //delete prefabs if they go past obstacleEnd
                {
                    Destroy(instantiatedPrefabs[i]);
                    instantiatedPrefabs.RemoveAt(i);
                }
                
            }
        }
        isObstacleDestroyed = true;
    }
}
