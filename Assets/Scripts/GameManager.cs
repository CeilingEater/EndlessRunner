using System;
using System.Collections;
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
    public Material material;
    
    //for level switching
    private int _currentLevel = 1;
    private int _scoreThreshold = 30;
    private bool _hasSwitchedLevels = false;
    private bool _hasFinishedFirstCycle = false;
    public int CurrentScore => _score;
    private int _levelsBeaten = 0;
    
    public GameObject player;
    
    //Singleton
    public static GameManager Instance { get; private set; }

    private int _score;
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

    private void Start()
    {
        _hasSwitchedLevels = false;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }
    
    private void OnEnable()
    {
        GameEvents.OnLevelBeaten += UpdateLevelsBeaten;
        GameEvents.OnScoreIncremented += IncrementScore;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        GameEvents.OnLevelBeaten -= UpdateLevelsBeaten;
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
            SwitchLevel();
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
    
    public void ResetGameState()
    {
        _score = 0;
        _isGameOver = false;
        _hasSwitchedLevels = false;
        _hasFinishedFirstCycle = false;
        _scoreThreshold = 30;
        _currentLevel = 1;
        _levelsBeaten = 0;

        uiController?.UpdateScoreDisplay(_score);
        uiController?.UpdateLevelsBeaten(_levelsBeaten);
    }
    
    // level switching
    private void SwitchLevel()
    {
        _hasSwitchedLevels = true;
        _scoreThreshold += 30;
        string nextScene;

        if (!_hasFinishedFirstCycle)
        {
            // First time switchin always go to level2
            _hasFinishedFirstCycle = true;
            _currentLevel = 2;
            nextScene = "Level2";
        }
        else
        {
            
            _currentLevel = Random.Range(1, 3);   // 1 or 2
            nextScene = _currentLevel == 1 ? "Level1" : "Level2";
        }

        Debug.Log("Switching to: " + nextScene);
        SceneManager.LoadScene(nextScene);

        _levelsBeaten++;
        GameEvents.RaiseLevelBeaten(_levelsBeaten);
        
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(AssignSceneReferencesAfterDelay());
    }
    
    private IEnumerator AssignSceneReferencesAfterDelay() // delay after scene initialises
    {
        yield return null; // wait 

        uiController = FindFirstObjectByType<UIController>();
        if (uiController == null)
        {
            Debug.LogError("no ui controller or in main menu");
        }
        else
        {
            uiController.UpdateScoreDisplay(_score);
            uiController.UpdateLevelsBeaten(_levelsBeaten);
        }

        GameObject foundPlayer = GameObject.FindWithTag("Player");
        if (foundPlayer != null)
        {
            player = foundPlayer;
            playerRenderer = player.GetComponent<Renderer>();
        }

        _hasSwitchedLevels = false;
    }
    
    // levels beaten

    private void UpdateLevelsBeaten(int newValue)
    {
        _levelsBeaten = newValue;
        uiController?.UpdateLevelsBeaten(_levelsBeaten);
        Debug.Log("updated levels beaten");
    }
}
