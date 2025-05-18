using System.Collections;
using UnityEngine;

public class PlayerFlight : MonoBehaviour
{
    private UIController uiController;
    public bool isFlying = false;

    public IEnumerator Fly(float duration, float flyHeight) 
    {
        isFlying = true;

        Vector3 originalPosition = transform.position;
        Vector3 targetPosition = new Vector3(originalPosition.x, flyHeight, originalPosition.z);
        
        transform.position = targetPosition;
        
        uiController?.SetFlyIcon(true);
        
        //freeze y position and stop gravity
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            
            rb.linearVelocity = Vector3.zero; // stop movement
        }
        Debug.Log("fly");

        yield return new WaitForSeconds(duration);
        
        uiController?.SetFlyIcon(false);
        
        // put back 2 normal
        if (rb != null)
        {
            rb.useGravity = true;
        }
        
        transform.position = new Vector3(transform.position.x, originalPosition.y, transform.position.z);
        isFlying = false;

        Debug.Log("stop fly");
    }
}
