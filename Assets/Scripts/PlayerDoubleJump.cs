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
        uiController?.SetDoubleJumpIcon(true);
        
        yield return new WaitForSeconds(duration);
        
        hasDoubleJump = false;
        uiController?.SetDoubleJumpIcon(false);
    }
}
