using Brewchat.Game.Wrappers;
using MelonLoader;
using UnityEngine;
using UnityEngine.InputSystem;
using VRC.Core;
using VRC.Udon;

namespace Brewchat.Cheats.World;

public class Global
{
    public static object udonNuke; 
    private static System.Collections.IEnumerator UdonNuke() 
    {
        foreach(UdonBehaviour key in UnityEngine.Object.FindObjectsOfType<UdonBehaviour>()) 
        {
            foreach(var events in key._eventTable.Keys) 
            {
                if (GameUtils.World.WorldData.current_worldID.Contains("wrld_858dfdfc-1b48-4e1e-8a43-f0edc611e5fe")) //Murder 4 
                { 
                    if (events == "_interact" || events == "_update") continue;
                } 
                
                GameUtils.World.SDK3.Udon.LoadCustomEvent(events);
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
    public static void Start() =>  udonNuke = MelonCoroutines.Start(UdonNuke());
    public static void Stop() => MelonCoroutines.Stop(udonNuke);
}