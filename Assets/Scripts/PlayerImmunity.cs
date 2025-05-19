using System.Collections;
using UnityEngine;

public class PlayerImmunity : MonoBehaviour
{
    private UIController uiController;
    public bool isImmune = false;
    
    public void Start()
    {
        uiController = FindAnyObjectByType<UIController>();
    }
    
    // immunity coroutine
    public IEnumerator ActivateImmunity(float duration)
    {
        isImmune = true;
        uiController?.SetImmuneIcon(true);
        
        yield return new WaitForSeconds(duration);

        isImmune = false;
        uiController?.SetImmuneIcon(false);
        
    }

    /*public bool IsImmune()
    {
        return isImmune;
    }

    public void DisplayImmune()
    {
        //uiController..text = "You are immune!";
        //gameOverTextMesh.gameObject.SetActive(true);
    }*/
}
