using UnityEngine;
using UnityEngine.Serialization;

public class PickUpManager : MonoBehaviour
{
    [SerializeField] private Color[] colours;
    
    public Renderer playerRenderer;
    
    public Material material;
    private Renderer _renderer;

    public bool isSame;
    private void Start()
    {
        InvokeRepeating(nameof(SpawnPickups), 2.0f, 10f);
        _renderer = GetComponentInChildren<Renderer>();
        material = _renderer.material;
        //material.color = colours[Random.Range(0, colours.Length)];   
    }

    void FixedUpdate()
    {
        //spawns pickups
        //SpawnPickups();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))  //if it isnt the player, ignore
            return;
        
        playerRenderer = other.gameObject.GetComponent<Renderer>();  //gets the player's renderer
        if (material.color == colours[0])  //rock is red
        {
            //damages boss/obstacles in the current 'lane'
        }
        if (material.color == colours[1])  //immunity is blue
        {
            //immune for few secs
            Invoke(nameof(Immune), 10f);
        }
        if (material.color == colours[2])  //health is green
        {
            if (PlayerController.Health == 5)
                return;
            PlayerController.Health++;
        }
        playerRenderer.material.color = material.color;
        //GameManager.Instance.IncrementScore(_renderer);  score wont be increased with pickups
        
        Destroy(gameObject);
        
    }
    
    private void SpawnPickups()
    {
        //choose random colour [0,1,2]
        //assign pickup to number
        //spawn it Resources.Load<GameObject>("Prefabs/HealthPickup");
        //spawn in a specific location
        material.color = colours[Random.Range(0, colours.Length)];
        if (material.color == colours[0])  //rock is red
        {
            //damages boss/obstacles in the current 'lane'
            Resources.Load<GameObject>("Prefabs/RockPickup");
        }
        if (material.color == colours[1])  //immunity is blue
        {
            //immune for few secs
            Resources.Load<GameObject>("Prefabs/ImmunePickup");
        }
        if (material.color == colours[2])  //health is green
        {
            Resources.Load<GameObject>("Prefabs/HealthPickup");
        }
    }

    private void Immune(Collider other)
    {
        other.gameObject.GetComponent<Collider>().enabled = false; 
    }
}
