using System.Collections;
using System.Reflection;
using System.Resources;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using Syrup.IMGUI;
using UnityEngine;
using UNIXEngine;
using UNIXEngine.Logging;

[UNIxMod("0000000000","Visionary","0.0.0.0")]
public class Entry : UNIXEngine.IL2CPPModding.UnixMod
{
    public const string NAME = "Visionary";
    public const string FULLNAME = "Visionary";
    Harmony h = new(Guid.NewGuid().ToString());
    public static ManualLogSource Console = new("Visionary");
    
    
    public override void Load()
    {
        UNIXEngine.Logging.Logger.Sources.Add(Console);
       
        #region Components 

        foreach (Type monos in Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(MonoBehaviour))))
        {
            Console.LogDebug($"Registering Monobehaviours: {monos.Name}");
            ClassInjector.RegisterTypeInIl2Cpp(monos);
        } 
        Console.LogDebug($"Setting Hooks...");
        h.PatchAll();
        #endregion
    } 
    
}

