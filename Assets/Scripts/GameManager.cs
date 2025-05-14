using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIController uiController;
    [SerializeField] private Renderer playerRenderer;  //added
    [SerializeField] private TextMeshProUGUI gameOverTextMesh;
    private readonly List<Renderer> _pickupRenderers = new List<Renderer>();
    bool _isGameOver;
    
    public GameObject player;
    
    //Singleton
    public static GameManager Instance { get; private set; }  //property
    //static bc it belongs to class
    //get can be accessed from outside the class, but not set

    private int _score = 0;
    //private int _winscore = 0;
    private GameObject _player;
    private GameObject[] _pickups;

    void Awake()  //runs before start
    {
        //an instance has already been assigned
        if (Instance != null)  //if NOT empty
        {
            Destroy(this);   //destroy current instance in favor of pre-existing one
            return; //exit code
        }
        
        Instance = this;  //instance is equal to original instance
        
        //my little pony, my little pony,aaaahahahahahahaha my little pony, i used to wonder what friendship could be! (My little pony) And to you all shared its magic with me!
        //Great adventure, tons of fun! a beautiful heart faithful and strong, sharing kindness, its an easy feat!
        //and magic makes it all complete!
    }

    public void IncrementScore(int amount)
    {
        if (_isGameOver)
            return;
        
        _score += amount;
        uiController.UpdateScoreDisplay(_score);
    }

    public void GameOver()
    {
        /*if (_isGameOver)
        {
            return;
        }*/
        _isGameOver = true;
        Time.timeScale = 0f; //pause game
        uiController.DisplayGameOver(_score);
        Invoke(nameof(ReturnToMainMenu), 3f);
    }

    private void ReturnToMainMenu()
    {
        //wait 3 secs
        //return to main menu
        SceneManager.LoadScene("MainMenu");
    }
    
    // material offset moving

    public Material material;
    public float speed = 30f;

    private float timeOffset;

    private void Start()
    {
        if (!material)
        {
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null && renderer.material != null)
            {
                material = renderer.material;
            }
            else
            {
                Debug.LogError("No material assigned and no Renderer component found.");
                enabled = false;
            }
        }
    }

    private void Update()
    {
        if (material)
        {
            timeOffset += Time.deltaTime * speed;

            // Calculate the offset (example: a scroll animation)
            Vector2 offset = new Vector2(timeOffset * 0.1f, 0f); // Scroll horizontally

            // Set the material's texture offset
            material.SetTextureOffset("_MainTex", offset);
        }
    }
}
