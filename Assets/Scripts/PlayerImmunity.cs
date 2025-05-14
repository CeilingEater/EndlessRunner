using System.Collections;
using UnityEngine;

public class PlayerImmunity : MonoBehaviour
{
    public bool isImmune = false;

    
    // immunity coroutine
    public IEnumerator ActivateImmunity(float duration)
    {
        isImmune = true;
        yield return new WaitForSeconds(duration);

        isImmune = false;
        
    }

    public bool IsImmune()
    {
        return isImmune;
    }
    
}
