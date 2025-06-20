using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIController uiController;
    [SerializeField] private Renderer playerRenderer;  //added
    [SerializeField] private TextMeshProUGUI gameOverTextMesh;
    private readonly List<Renderer> _pickupRenderers = new List<Renderer>();
    
    bool _isGameOver;
    private bool _isPaused = false;
    
    //for level switching
    private int _currentLevel = 1;
    private int _scoreThreshold = 20;
    private bool _hasSwitchedLevels = false;
    
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
        DontDestroyOnLoad(gameObject);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }
    
    //increment score event
    private void OnEnable()
    {
        GameEvents.OnScoreIncremented += IncrementScore;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        GameEvents.OnScoreIncremented -= IncrementScore;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void IncrementScore(int amount)
    {
        if (_isGameOver) return;

        _score += amount;
        uiController?.UpdateScoreDisplay(_score);

        if (_score >= _scoreThreshold && !_hasSwitchedLevels)
        {
            SwitchLevel(); //if reach score threshold and havent switched yet
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
        Time.timeScale = 0f; //pause game
        uiController.DisplayGameOver(_score);
    }

    public void PauseGame()
    {
        _isPaused = !_isPaused;
        Time.timeScale = _isPaused ? 0f : 1f;
        
        uiController?.PauseMenuVisible(_isPaused);
    }
    
    // level switching
    private void SwitchLevel()
    {
        _hasSwitchedLevels = true;
        _scoreThreshold += 20;

        string nextScene = "";

        if (_currentLevel == 1)
        {
            nextScene = "Level2";
            _currentLevel = 2;
        }
        else
        {
            _currentLevel = Random.Range(1, 3); // 1 or 2
            nextScene = _currentLevel == 1 ? "Level1" : "Level2";
        }

        Debug.Log("Loading scene: " + nextScene);
        SceneManager.LoadScene(nextScene);
    }
    
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        uiController = FindFirstObjectByType<UIController>(); // for ui not working when scene switch
    }
}
