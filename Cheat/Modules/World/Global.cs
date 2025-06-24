using Brewchat.Game.Wrappers;
using MelonLoader;
using UnityEngine;
using UnityEngine.InputSystem;
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
                GameUtils.World.SDK3.Udon.LoadCustomEvent(events);
                yield return new WaitForSeconds(5f);
            }
        }
    }
    public static void Start() =>  udonNuke = MelonCoroutines.Start(UdonNuke());
    public static void Stop() => MelonCoroutines.Stop(udonNuke);
}