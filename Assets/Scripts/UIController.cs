using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreTextMesh;
    [SerializeField] private TextMeshProUGUI gameOverTextMesh;

    private void Awake()
    {
        gameOverTextMesh.gameObject.SetActive(false);  
        UpdateScoreDisplay(0);
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
    }
}
