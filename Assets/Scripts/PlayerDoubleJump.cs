using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleJump : MonoBehaviour
{
    private UIController uiController;
    public bool hasDoubleJump = false;

    public IEnumerator ActivateDoubleJump(float duration)
    {
        hasDoubleJump = true;
        Debug.Log("double jump activated");
        uiController?.SetDoubleJumpIcon(true);
        
        yield return new WaitForSeconds(duration);
        
        hasDoubleJump = false;
        Debug.Log("double jump deactivated");
        uiController?.SetDoubleJumpIcon(false);
    }
}
