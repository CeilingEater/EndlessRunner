using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
       
       // obstacle being passed (score)
       public static event Action<int> OnScoreIncremented;

       public static void RaiseScoreIncremented(int amount)
       {
              OnScoreIncremented?.Invoke(amount);
       }
       // pickups
       public static event Action OnImmunityPickedUp;
       public static event Action OnFlightPickedUp;
       public static event Action OnDoubleJumpPickedUp;
   
       public static void RaiseImmunity() => OnImmunityPickedUp?.Invoke();
       public static void RaiseFlight() => OnFlightPickedUp?.Invoke();
       public static void RaiseDoubleJump() => OnDoubleJumpPickedUp?.Invoke();
       
       //boss spawn
       public static event Action OnBossSpawned;

       public static void RaiseBossSpawned()
       {
              OnBossSpawned?.Invoke();
       }
       
       // levels beaten

       public static event Action<int> OnLevelBeaten;

       public static void RaiseLevelBeaten(int totalBeaten)
       {
              OnLevelBeaten?.Invoke(totalBeaten);
       }
}
