using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreTextMesh;
    [SerializeField] private TextMeshProUGUI gameOverTextMesh;
    [SerializeField] private Button restartButton;

    private void Awake()
    {
        gameOverTextMesh.gameObject.SetActive(false);  
        restartButton.gameObject.SetActive(false);
        UpdateScoreDisplay(0);
    }
    
    void Start()
    {
        restartButton.onClick.AddListener(OnRestartButtonClicked);
    }
    
    private void OnRestartButtonClicked()
    {
        SceneManager.LoadScene("LevelStomp");
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

    public void DisplayGameOver()
    {
        gameOverTextMesh.text = "GAME OVER";
        gameOverTextMesh.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }
    
    
}
