using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private UIController uiController;
    [SerializeField] private GameManager gameManager;
    public GameObject player;
    
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            PlayerImmunity immunity = player.GetComponent<PlayerImmunity>();
            if (immunity != null && immunity.isImmune)
            {
                return;
            }
            
            Destroy(player);
            GameManager.Instance.GameOver();
            
            //adding to leaderboard after death
            Debug.Log("Player death detected. Submitting score.");
            string playerName = PlayerPrefs.GetString("PlayerName", "Unknown");
            int score = GameManager.Instance.CurrentScore;
            DatabaseManager.Instance.SubmitScore(playerName, score);
            
            return;
        }
        
    }
    
}
