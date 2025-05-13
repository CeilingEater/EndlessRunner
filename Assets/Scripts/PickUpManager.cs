using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PickUpManager : MonoBehaviour
{
    [SerializeField] private float pickupMoveSpeed = 30f;
    [SerializeField] private float zPickupMin = -5f, zPickupMax = 5f;
    
    [SerializeField] private GameObject[] pickups;
    private List<GameObject> _instantiatedPickups = new List<GameObject>(); //list to keep spawned pickups in
    private Material _material;
    private Renderer _playerRenderer;

    public GameObject pickupSpawner;
    public GameObject pickupEnd;

    public static bool IsPickupDestroyed;
    private bool isImmune = false;

    private float duration = 5f;
    void Start()
    {
        InvokeRepeating(nameof(SpawnPickups), 1.0f, 1f);  //spawns prefabs in intervals
        //InvokeRepeating(nameof(DeletePickups), 1.0f, 4f);  //deletes prefabs after a certain num of secs
    }

    void Update()
    {
        MovePickups();
        DeletePickups();
    }
    
    private void OnTriggerEnter(Collider other) //detect player collision when they touch a pickup
    {
        if (!other.CompareTag("Player"))
            return;

        if (gameObject.CompareTag("ImmunePickup"))
        {
            PlayerImmunity playerScript = other.GetComponent<PlayerImmunity>();
            if (playerScript != null)
            {
                float duration = 5f;
                playerScript.StartCoroutine(playerScript.ActivateImmunity(duration));
            }
        }

        Destroy(gameObject); // remove pickup after use
    }
    
    private void SpawnPickups()
    {

        if (pickups.Length == 0) return;
        
        GameObject spawnPrefab = pickups[Random.Range(0, pickups.Length)]; //spawn random prefab from array
        float randomZ = Random.Range(zPickupMin, zPickupMax); 
        GameObject newPickup = Instantiate(spawnPrefab, pickupSpawner.transform.position + new Vector3(0, 2, randomZ), Quaternion.identity);

        _instantiatedPickups.Add(newPickup);
        

    }
    

    private void DeletePickups()
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
    }
    
    
    private void MovePickups()
    {
        for (int i = 0; i < _instantiatedPickups.Count; i++)
        {
            if (_instantiatedPickups[i] != null)
            {
                _instantiatedPickups[i].transform.position = Vector3.MoveTowards(_instantiatedPickups[i].transform.position, pickupEnd.transform.position, pickupMoveSpeed * Time.deltaTime);
            }
        }
    }
}
