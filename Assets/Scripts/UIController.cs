using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreTextMesh;
    [SerializeField] private TextMeshProUGUI gameOverTextMesh;
    [SerializeField] private TextMeshProUGUI FinalScoreTextMesh;
    [SerializeField] private TextMeshProUGUI pickupText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button MainMenuButton;
    [SerializeField] private TextMeshProUGUI GamePauseTextMesh;
    [SerializeField] private TextMeshProUGUI LevelsBeatenTextMesh;
    
    public Image flyIcon;
    public Image immuneIcon;
    public Image doubleJumpIcon;
    public GameObject pausePanel;
    //private int score = 0;

    private void Awake()
    {
        gameOverTextMesh.gameObject.SetActive(false);  
        restartButton.gameObject.SetActive(false);
        FinalScoreTextMesh.gameObject.SetActive(false);
        MainMenuButton.gameObject.SetActive(false);
        pausePanel.SetActive(false);
        
        
        flyIcon.enabled = false;
        immuneIcon.enabled = false;
        doubleJumpIcon.enabled = false;
        
        UpdateScoreDisplay(0);
    }
    
    void Start()
    {
        UIController uiController = FindObjectOfType<UIController>();

        if (uiController != null)
        {
            //testing if ui reloads when going back to menu
            GameManager.Instance.RegisterUI(this);
            Debug.Log("Welcome back to the menu!");
        }
        else
        {
            Debug.LogWarning("No UIController found in this scene.");
        } 
        
        restartButton.onClick.AddListener(OnRestartButtonClicked);
        MainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
    }
    
    private void OnRestartButtonClicked()
    {
        GameManager.Instance?.ResetGameState();
        SceneManager.LoadScene("Level1");
        Time.timeScale = 1f;
    }
    
    private void OnMainMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseMenuVisible(bool state)
    {
        if (pausePanel != null)
            pausePanel.SetActive(state);
    }
    
    public void UpdateScoreDisplay(int score)
    {
        if (scoreTextMesh != null)
            scoreTextMesh.text = score.ToString("00");
        else
            Debug.LogError("not assigned");
    }
    

    public void DisplayGameOver(int score)
    {
        gameOverTextMesh.text = "GAME OVER";
        gameOverTextMesh.gameObject.SetActive(true);
        
        restartButton.gameObject.SetActive(true);
        MainMenuButton.gameObject.SetActive(true);
        
        FinalScoreTextMesh.text = "Score: " + score;
        FinalScoreTextMesh.gameObject.SetActive(true);
        
    }
    
    public void SetFlyIcon(bool state)
    {
        flyIcon.enabled = state;
    }

    public void SetImmuneIcon(bool state)
    {
        immuneIcon.enabled = state;
    }

    public void SetDoubleJumpIcon(bool state)
    {
        doubleJumpIcon.enabled = state;
    }

    private void OnEnable()
    {
        GameEvents.OnLevelBeaten += UpdateLevelsBeaten;
        GameEvents.OnImmunityPickedUp += EnableImmuneIcon;
        GameEvents.OnFlightPickedUp += EnableFlyIcon;
        GameEvents.OnDoubleJumpPickedUp += EnableDoubleJumpIcon;
        GameEvents.OnScoreIncremented += HandleScoreIncremented;
    }

    private void OnDisable()
    {
        GameEvents.OnLevelBeaten -= UpdateLevelsBeaten;
        GameEvents.OnImmunityPickedUp -= EnableImmuneIcon;
        GameEvents.OnFlightPickedUp -= EnableFlyIcon;
        GameEvents.OnDoubleJumpPickedUp -= EnableDoubleJumpIcon;
        GameEvents.OnScoreIncremented -= HandleScoreIncremented;
    }

    private void EnableImmuneIcon() => SetImmuneIcon(true);
    private void EnableFlyIcon() => SetFlyIcon(true);
    private void EnableDoubleJumpIcon() => SetDoubleJumpIcon(true);
    
    private void HandleScoreIncremented(int amount)
    {
        if (GameManager.Instance != null)
        {
            UpdateScoreDisplay(GameManager.Instance.CurrentScore); // Add a public getter in GameManager
        }
    }
    
    //track levels beaten

    public void UpdateLevelsBeaten(int levelsBeaten)
    {
        if (LevelsBeatenTextMesh != null)
            LevelsBeatenTextMesh.text = "Level: " + levelsBeaten;
        else
            Debug.LogError("LevelsBeatenText not assigned in inspector!");
    }
    
}
