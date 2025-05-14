using System.Collections;
using UnityEngine;

public class PlayerFlight : MonoBehaviour
{
    public bool isFlying = false;

    public IEnumerator Fly(float duration, float flyHeight) 
    {
        isFlying = true;

        Vector3 originalPosition = transform.position;
        Vector3 targetPosition = new Vector3(originalPosition.x, flyHeight, originalPosition.z);
        
        transform.position = targetPosition;
        Debug.Log("fly");

        yield return new WaitForSeconds(duration);
        
        transform.position = new Vector3(transform.position.x, originalPosition.y, transform.position.z);
        isFlying = false;

        Debug.Log("stop fly");
    }
}
