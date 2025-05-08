using UnityEngine;
using VRC.Udon;

namespace Brewchat.Cheats.World;
    
public class M4
{
    private static List<UdonBehaviour> cachedUdonBehaviours = new List<UdonBehaviour>();
    public static string? uddies;
    public static System.Collections.IEnumerator UdonNukeCoroutine()
    {
        var udonBehaviours = UnityEngine.Object.FindObjectsOfType<UdonBehaviour>();
    
        foreach (UdonBehaviour behaviour in udonBehaviours)
        {
            if (behaviour == null) continue;
        
            foreach (string eventKey in behaviour._eventTable.Keys)
            {
                behaviour.SendCustomEvent(eventKey);
            
                // Wait for next frame to prevent freezing
                yield return new WaitForSeconds(0.01f);
                yield return null;
            }
        }
    }
}
