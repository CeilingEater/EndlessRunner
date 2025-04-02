using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private UIController uiController;
    public GameObject player;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle")) //obstacle prefabs are tagged with "Obstacle"
        {
            Destroy(player);
            uiController?.DisplayGameOver();
            
             
            Invoke(nameof(ReturnToMainMenu), 3f);
        }
    }
    
    private void ReturnToMainMenu()
    {
        //return to main menu
        SceneManager.LoadScene("MainMenu");
    }
}
