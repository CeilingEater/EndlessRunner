using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    [SerializeField] private float xPlayerPosition = 0f;
    
    [SerializeField] private float yPrefabPosition = 0f;
    [SerializeField] private float zPrefabPosition = 0f;
    [SerializeField] private float xPrefabPosition = 0f;
    
    List<GameObject[,,] > Obstacles = new List<GameObject[,,]>(); //rows first, columns second
        
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(SpawnPrefabs), 1.0f, 10f);  //spawns prefabs in intervals
        InvokeRepeating(nameof(DeletePrefabs), 1.0f, 12f);  //deletes prefabs after a certain num of secs
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(200f * Time.deltaTime, 0, 0);  //constant mvoing
    }

    private void SpawnPrefabs()
    {
        //randomly spawns a plain obstacle
        //has a chance of spawning an obstacle on top of the basic plat form
        for (int i = 0; i < Obstacles.Count; i++)  //row
        {
            for (int j = 0; j < Obstacles[i].Length; j++) //column
            {
                Resources.Load<GameObject>("Prefabs/Level1/BasicPlat");
                j++;
            }

            i++;
        }
        Resources.Load<GameObject>("Prefabs/Level1/BasicPlat");
    }

    private void DeletePrefabs()
    {
        //deletes prefabs after they pass the player
        Resources.UnloadUnusedAssets();
    }
}
