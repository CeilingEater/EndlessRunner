using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleJump : MonoBehaviour
{
    private UIController uiController;
    public bool hasDoubleJump = false;
    
    public void Start()
    {
        uiController = FindAnyObjectByType<UIController>();
    }

    public IEnumerator ActivateDoubleJump(float duration)
    {
        hasDoubleJump = true;
        Debug.Log("double jump");
        uiController?.SetDoubleJumpIcon(true);
        
        yield return new WaitForSeconds(duration);
        
        hasDoubleJump = false;
        Debug.Log("double jump stopped");
        uiController?.SetDoubleJumpIcon(false);
    }
}
