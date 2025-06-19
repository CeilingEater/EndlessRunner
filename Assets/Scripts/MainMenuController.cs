using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button quitGameButton;
    [SerializeField] private Button leaderboardButton;
    void Start()
    {
        startGameButton.onClick.AddListener(OnStartButtonClicked);
    }

    private void OnStartButtonClicked()
    {
        SceneManager.LoadScene("LevelStomp");
    }
    
    private void OnQuitButtonClicked()
    {
       
    }
    
    private void leaderboardButtonClicked()
    {
       
    }
}
