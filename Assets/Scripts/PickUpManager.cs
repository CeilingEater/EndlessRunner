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
        _renderer = GetComponentInChildren<Renderer>();
        material = _renderer.material;
        material.color = colours[Random.Range(0, colours.Length)];   
    }

    void FixedUpdate()
    {
        //spawns pickups
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
        }
        if (material.color == colours[2])  //health is green
        {
            if (PlayerController.Health == 5)
                return;
            PlayerController.Health++;
        }
        /*if (playerRenderer.material.color == material.color)   //gameobject colour == pickup color
        {
            isSame = true;
            return;  //return = exit 
        }*/
        playerRenderer.material.color = material.color;
        //GameManager.Instance.IncrementScore(_renderer);  score wont be increased with pickups
        
        Destroy(gameObject);
        
    }
}
