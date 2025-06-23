using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button quitGameButton;
    [SerializeField] private Button leaderboardButton;
    [SerializeField] private TMP_InputField playerNameInputField;
    void Start()
    {
        startGameButton.onClick.AddListener(OnStartButtonClicked);
        quitGameButton.onClick.AddListener(OnQuitButtonClicked);
        leaderboardButton.onClick.AddListener(OnLeaderboardButtonClicked);
    }

    private void OnStartButtonClicked()
    {
        if (playerNameInputField.text != null)
        {
            SavePlayerName();
            SceneManager.LoadScene("Level1");
        }
        else
        {
            Debug.LogError("Player name field is empty");
        }
    }
    
    private void OnQuitButtonClicked()
    {
       Application.Quit();
    }
    
    private void OnLeaderboardButtonClicked()
    {
       SceneManager.LoadScene("Leaderboard");
    }
    
    public void SavePlayerName()
    {
        string playerName = playerNameInputField.text;
        PlayerPrefs.SetString("PlayerName", playerName);
        Debug.Log("Player name saved: " + playerName);
    }
}
