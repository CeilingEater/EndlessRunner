using System.Collections;
using UnityEngine;

public class PlayerImmunity : MonoBehaviour
{
    public bool isImmune = false;

    public IEnumerator ActivateImmunity(float duration)
    {
        isImmune = true;
        Debug.Log("IMMUNE");

        // Optionally disable obstacle collisions or change material here
        yield return new WaitForSeconds(duration);

        isImmune = false;
        Debug.Log("NOT IMMUNE");
    }

    public bool IsImmune()
    {
        return isImmune;
    }
    
}
