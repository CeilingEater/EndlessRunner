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
    
    void Start()
    {
        InvokeRepeating(nameof(SpawnPickups), 1.0f, 1f);  //spawns prefabs in intervals
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
            PlayerImmunity playerImmunity = other.GetComponent<PlayerImmunity>();
            if (playerImmunity != null)
            {
                playerImmunity.StartCoroutine(playerImmunity.ActivateImmunity(5f)); //5 second immunity
            }
        }
        
        
        else if (gameObject.CompareTag("FlyPickup"))
        {
            PlayerController controller = other.GetComponent<PlayerController>();
            if (controller != null)
            {
                controller.ActivateFlight(5f, 8f);
            }
            
        }
        
        else if (gameObject.CompareTag("DoubleJumpPickup"))
        {
            PlayerDoubleJump playerDoubleJump = other.GetComponent<PlayerDoubleJump>();
            if (playerDoubleJump != null)
            {
                playerDoubleJump.StartCoroutine(playerDoubleJump.ActivateDoubleJump(5f));
            }
        }

        Destroy(gameObject);
    }
    
    private void SpawnPickups()
    {

        if (pickups.Length == 0) return;
        
        GameObject spawnPickup = pickups[Random.Range(0, pickups.Length)]; //spawn random prefab from array
        float randomZ = Random.Range(zPickupMin, zPickupMax); 
        GameObject newPickup = Instantiate(spawnPickup, pickupSpawner.transform.position + new Vector3(0, 2, randomZ), Quaternion.identity);

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
                _instantiatedPickups[i].transform.position += Vector3.right * pickupMoveSpeed * Time.deltaTime;
            }
        }
    }
}
