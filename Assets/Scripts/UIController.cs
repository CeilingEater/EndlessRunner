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
    
    public Image flyIcon;
    public Image immuneIcon;
    public Image doubleJumpIcon;
    private int score = 0;

    private void Awake()
    {
        gameOverTextMesh.gameObject.SetActive(false);  
        restartButton.gameObject.SetActive(false);
        FinalScoreTextMesh.gameObject.SetActive(false);
        MainMenuButton.gameObject.SetActive(false);
        GamePauseTextMesh.gameObject.SetActive(false);
        
        flyIcon.enabled = false;
        immuneIcon.enabled = false;
        doubleJumpIcon.enabled = false;
        
        UpdateScoreDisplay(0);
    }
    
    void Start()
    {
        restartButton.onClick.AddListener(OnRestartButtonClicked);
        MainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
    }
    
    private void OnRestartButtonClicked()
    {
        SceneManager.LoadScene("LevelStomp");
        Time.timeScale = 1f;
    }
    
    private void OnMainMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    
    public void UpdateScoreDisplay(int score)
    {
        Debug.Log("[UIController] Updating score UI: " + score);
        if (scoreTextMesh != null)
            scoreTextMesh.text = score.ToString("00");
        else
            Debug.LogError("scoreTextMesh is not assigned in UIController");
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
        GameEvents.OnImmunityPickedUp += EnableImmuneIcon;
        GameEvents.OnFlightPickedUp += EnableFlyIcon;
        GameEvents.OnDoubleJumpPickedUp += EnableDoubleJumpIcon;
        GameEvents.OnScoreIncremented += HandleScoreIncremented;
    }

    private void OnDisable()
    {
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
        score += amount;
        UpdateScoreDisplay(score);
    }
    
}
