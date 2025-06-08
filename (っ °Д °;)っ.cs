using System.Reflection;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using ImGuiNET;
using UnityEngine;
using UNIXEngine;
using UNIXEngine.Logging;
using VRC.SDKBase;

[UnixModDescriptor("0000000000","Autumn","0.0.0.0")]
public class Entry : UNIXEngine.IL2CPPModding.UnixMod
{
    public const string NAME = "Autumn";
    public const string FULLNAME = "Autumn";
    Harmony h = new(Guid.NewGuid().ToString());
    public static ManualLogSource Console = new("Autumn");
  
    public override void Load()
    {

        UNIXEngine.Logging.Logger.Sources.Add(Console);
       
        #region Components 
         
        foreach (Type monos in Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(MonoBehaviour))))
        {
            Console.LogMessage($"Registering Monobehaviours: {monos.Name}");
            ClassInjector.RegisterTypeInIl2Cpp(monos);
        } 
      
        h.PatchAll();
        #endregion
    } 
   
}

