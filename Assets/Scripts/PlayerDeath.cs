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
                Debug.Log("immune so dont die");
                return;
            }

            Debug.Log("not immune so die");
            Destroy(player);
            GameManager.Instance.GameOver();
            return;
        }
        
    }

    private void ReturnToMainMenu()
    {
        //return to main menu
        SceneManager.LoadScene("MainMenu");
    }
}
