using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PickUpManager : MonoBehaviour
{
    [SerializeField] private float pickupMoveSpeed = 30f;
    [SerializeField] private float xPickupPosition = 50f;
    [SerializeField] private float zPickupMin = -5f, zPickupMax = 5f;
    
    [SerializeField] private GameObject[] pickups;
    private List<GameObject> _instantiatedPickups = new List<GameObject>(); //list to keep spawned pickups in
    private Material _material;
    private Renderer _playerRenderer;

    public GameObject pickupSpawner;
    public GameObject pickupEnd;

    public static bool IsPickupDestroyed;

    public bool isSame;
    void Start()
    {
        InvokeRepeating(nameof(SpawnPrefabs), 1.0f, 2f);  //spawns prefabs in intervals
        InvokeRepeating(nameof(DeletePrefabs), 1.0f, 4f);  //deletes prefabs after a certain num of secs
    }

    void Update()
    {
        MovePrefabs();
    }
    
    private void MovePrefabs()
    {
        for (int i = 0; i < _instantiatedPickups.Count; i++)
        {
            if (_instantiatedPickups[i] != null)
            {
                _instantiatedPickups[i].transform.position = Vector3.MoveTowards(_instantiatedPickups[i].transform.position, pickupEnd.transform.position, pickupMoveSpeed * Time.deltaTime);
            }
        }
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))  //if it isnt the player, ignore
            return;
        
        _playerRenderer = other.gameObject.GetComponent<Renderer>();  //gets the player's renderer
        _playerRenderer.material.color = _material.color;
        //GameManager.Instance.IncrementScore(_renderer);  score wont be increased with pickups
        
        Destroy(gameObject);
        
    }
    
    private void SpawnPrefabs()
    {

        if (pickups.Length == 0) return;
        
        GameObject spawnPrefab = pickups[Random.Range(0, pickups.Length)]; //spawn random prefab from array
        float randomZ = Random.Range(zPickupMin, zPickupMax); //to spawn obstacle randomly in front of player
        GameObject newObstacle = Instantiate(spawnPrefab, new Vector3(xPickupPosition, 0.0f, randomZ), Quaternion.identity); //autofilled LOL i hope its right

        _instantiatedPickups.Add(newObstacle);
        

    }

    private void DeletePrefabs()
    {
        for (int i = _instantiatedPickups.Count - 1; i >= 0; i--) 
        {
            if (_instantiatedPickups[i] != null)
            {
                if (_instantiatedPickups[i].transform.position.z <= pickupEnd.transform.position.z) //delete prefabs if they go past obstacleEnd
                {
                    Destroy(_instantiatedPickups[i]);
                    _instantiatedPickups.RemoveAt(i);
                }
                
            }
        }
        IsPickupDestroyed = true;
        GameManager.Instance.IncrementScore(IsPickupDestroyed);
    }

    private void Immune(Collider other)
    {
        other.gameObject.GetComponent<Collider>().enabled = false; 
    }
}
