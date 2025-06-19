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
    public static GameManager Instance { get; private set; }

    private int _score = 0;
    private GameObject _player;
    private GameObject[] _pickups;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
    
    //increment score event
    private void OnEnable()
    {
        Debug.Log("GameManager OnEnable - subscribing to OnScoreIncremented");
        GameEvents.OnScoreIncremented += IncrementScore;
    }

    private void OnDisable()
    {
        Debug.Log("GameManager OnDisable - unsubscribing from OnScoreIncremented");
        GameEvents.OnScoreIncremented -= IncrementScore;
    }

    public void IncrementScore(int amount)
    {
        
        if (_isGameOver) return;

        _score += amount;
        Debug.Log("Incremented score by: " + amount);
        uiController?.UpdateScoreDisplay(_score);
        
    }

    public void GameOver()
    {
        _isGameOver = true;
        Time.timeScale = 0f; //pause game
        uiController.DisplayGameOver(_score);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
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
                enabled = false;
            }
        }
    }

    private void Update()
    {
        if (material)
        {
            timeOffset += Time.deltaTime * speed;
            
            Vector2 offset = new Vector2(timeOffset * 0.1f, 0f);
            
            material.SetTextureOffset("_MainTex", offset);
        }
    }
    
   
    
}
