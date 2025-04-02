using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    [SerializeField] private float xPlayerPosition = 0f;
    
    [SerializeField] private float prefabMoveSpeed = 5f;
    [SerializeField] private float xPrefabPosition = 50f;
    [SerializeField] private float zPrefabMin = -10f, zPrefabMax = 10f;
    
    [SerializeField] private GameObject[] obstacles;
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
    
    void MovePrefabs()
    {
        //transform.Translate(200f * Time.deltaTime, 0, 0);  //constant mvoing
        
        for (int i = 0; i < obstacles.Length; i++)
        {
            if (obstacles[i] != null)
            {
                obstacles[i].transform.Translate(-prefabMoveSpeed, 0, 0 * Time.deltaTime);
            }
        }
    }

    private void SpawnPrefabs()
    {
        //randomly spawns a plain obstacle
        //has a chance of spawning an obstacle on top of the basic plat form
        /*for (int i = 0; i < Obstacles.Count; i++)  //row
        {
            for (int j = 0; j < Obstacles[i].Length; j++) //column
            {
                GameObject jumpBar = Instantiate(Resources.Load<GameObject>("Prefabs/Level1/BasicPlat"), new Vector3(xPrefabPosition, yPrefabPosition, zPrefabPosition), Quaternion.identity); //idk if this is right but it autofilled so
                j++;
            }
            i++;
        }*/

        if (obstacles.Length == 0) return;
        
        GameObject spawnPrefab = obstacles[Random.Range(0, obstacles.Length)]; //spawn random prefab from array
        
        float randomZ = Random.Range(zPrefabMin, zPrefabMax); //to spawn obstacle randomly in front of player
        GameObject newPrefab = Instantiate(spawnPrefab, new Vector3(xPrefabPosition, 0.0f, randomZ), Quaternion.identity); //autofilled LOL i hope its right
        
    }

    private void DeletePrefabs()
    {
        //IncrementScore();
        //deletes prefabs after they pass the player
        Resources.UnloadUnusedAssets();
    }
}
