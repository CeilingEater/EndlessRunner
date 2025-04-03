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
    

    public bool isSame;
    void Start()
    {
        InvokeRepeating(nameof(SpawnPickups), 1.0f, 2f);  //spawns prefabs in intervals
        InvokeRepeating(nameof(DeletePickups), 1.0f, 4f);  //deletes prefabs after a certain num of secs
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
        
        
        if (gameObject.CompareTag("ImmunePickup")) //immunity pickup
        {
            StartCoroutine(ActivateImmunity());
        }
       
        /*else if (gameObject.CompareTag("SpeedPickup"))
        {
            
        }*/
        
        _instantiatedPickups.Remove(gameObject);
        Destroy(gameObject);
    }
    
    private void SpawnPickups()
    {

        if (pickups.Length == 0) return;
        
        GameObject spawnPrefab = pickups[Random.Range(0, pickups.Length)]; //spawn random prefab from array
        float randomZ = Random.Range(zPickupMin, zPickupMax); 
        GameObject newPickup = Instantiate(spawnPrefab, pickupSpawner.transform.position + new Vector3(0, 2, randomZ), Quaternion.identity);

        _instantiatedPickups.Add(newPickup);
        

    }
    
    private IEnumerator ActivateImmunity() //not working im going to kil myseldf
    {
        isImmune = true;

        Debug.Log("immune");
        Collider playerCollider = GetComponent<Collider>();
        
        if (playerCollider != null)
        {
            playerCollider.enabled = false;
        }
        //turn off collisions

        yield return new WaitForSeconds(5f); //https://docs.unity3d.com/6000.0/Documentation/ScriptReference/WaitForSeconds.html
        
        if (playerCollider != null)
        {
            playerCollider.enabled = true;
        }
        
        isImmune = false;
        
        Debug.Log("not immune");
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

    private void Immune(Collider other)
    {
        other.gameObject.GetComponent<Collider>().enabled = false; 
    }
}
