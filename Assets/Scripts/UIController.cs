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
    
    public Image flyIcon;
    public Image immuneIcon;
    public Image doubleJumpIcon;

    private void Awake()
    {
        gameOverTextMesh.gameObject.SetActive(false);  
        restartButton.gameObject.SetActive(false);
        FinalScoreTextMesh.gameObject.SetActive(false);
        
        flyIcon.enabled = false;
        immuneIcon.enabled = false;
        doubleJumpIcon.enabled = false;
        
        UpdateScoreDisplay(0);
    }
    
    void Start()
    {
        restartButton.onClick.AddListener(OnRestartButtonClicked);
    }
    
    private void OnRestartButtonClicked()
    {
        SceneManager.LoadScene("LevelStomp");
        Time.timeScale = 1f;
    }
    
    
    public void UpdateScoreDisplay(int score)
    {
        scoreTextMesh.text = score.ToString("00"); //update displayed score
    }

    public void DisplayYouWin()
    {
        gameOverTextMesh.text = "YOU WIN";
        gameOverTextMesh.gameObject.SetActive(true);
    }

    public void DisplayGameOver(int score)
    {
        gameOverTextMesh.text = "GAME OVER";
        gameOverTextMesh.gameObject.SetActive(true);
        
        restartButton.gameObject.SetActive(true);
        
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

    
    
}
