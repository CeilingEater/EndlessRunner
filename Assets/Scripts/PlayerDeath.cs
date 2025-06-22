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
            string playerName = PlayerPrefs.GetString("PlayerName", "Unknown");
            //int score = _score; //im gonna brown
            //DatabaseManager.Instance.SubmitScore(playerName, score);
            return;
        }
        
    }
    
}
