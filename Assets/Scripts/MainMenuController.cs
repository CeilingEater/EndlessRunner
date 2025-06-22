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
    }

    private void OnStartButtonClicked()
    {
        if (playerNameInputField.text != null)
        {
            PlayerPrefs.SetString("PlayerName", name);
            PlayerPrefs.Save(); 
            Debug.Log("Player name saved: " + name);
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
    
    private void leaderboardButtonClicked()
    {
       SceneManager.LoadScene("Leaderboard");
    }
}
