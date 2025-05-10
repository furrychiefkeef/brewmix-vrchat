using Syrup.IMGUI;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

namespace Brewchat.Cheats.World;
    
public class M4
{
    /*private static List<UdonBehaviour> cachedUdonBehaviours = new List<UdonBehaviour>();*/
    public static string? uddies;
    public static System.Collections.IEnumerator UdonNukeCoroutine()
    {
        var  cachedUdonBehaviours = UnityEngine.Object.FindObjectsOfType<UdonBehaviour>();
    
        foreach (UdonBehaviour behaviour in cachedUdonBehaviours)
        {
          
            foreach (string eventKey in behaviour._eventTable.Keys)
            {
                behaviour.SendCustomEvent(eventKey);
                yield return null;
            }
        }
    }
}
