using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private UIController uiController;
    [SerializeField] private GameManager gameManager;
    public GameObject player;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle")) //obstacle prefabs are tagged with "Obstacle"
        {
            Destroy(player);
            GameManager.Instance.GameOver();
        }


        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerImmunity immunity = collision.gameObject.GetComponent<PlayerImmunity>();
            if (immunity != null && immunity.isImmune)
            {
                Debug.Log("Player is immune. No damage.");
                return; // ignore this collision
            }

            // Not immune: game over
            Destroy(collision.gameObject);
            GameManager.Instance.GameOver();
        }
    }

    private void ReturnToMainMenu()
    {
        //return to main menu
        SceneManager.LoadScene("MainMenu");
    }
}
