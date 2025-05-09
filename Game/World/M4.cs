using Syrup.IMGUI;
using UnityEngine;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

namespace Brewchat.Cheats.World;
    
public class M4
{
    private static List<UdonBehaviour> cachedUdonBehaviours = new List<UdonBehaviour>();
    public static string? uddies;
    public static System.Collections.IEnumerator UdonNukeCoroutine()
    {
        cachedUdonBehaviours = UnityEngine.Object.FindObjectsOfType<UdonBehaviour>().ToList();
    
        foreach (UdonBehaviour behaviour in cachedUdonBehaviours)
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
