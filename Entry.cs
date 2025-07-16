using System;
using System.Reflection;
using Harmony;
using MelonLoader;
using Mono.CSharp;
using MonoMod.Utils;
using UnhollowerRuntimeLib;
using UnityEngine;

[assembly: MelonInfo(typeof(Entry), "SwiftVRC", "Nightly", "Vlone")]
public class Entry: MelonMod
{
    
     internal static Entry GetEntry() => new();
     internal ModInfo modInfo  => new();
     public static Config config  = new();

     public HarmonyLib.Harmony Hooks =>  new("[2577]");
    internal class ModInfo
    {
        public Prefixes prefixes => new();
        public class Prefixes 
        {
            public  static string VariableName = "_VRLone";
        }
        public readonly string? Enviroment = Environment.GetEnvironmentVariable(Prefixes.VariableName, EnvironmentVariableTarget.User);
        public string? _currentName { get;set;} = "Swift";
        public string? _currentVersion { get; set;} = "<2>.<5>.<7>.<7>";
        public string? _currentHash {get; set;} = System.Reflection.Assembly.GetExecutingAssembly().GetHashCode().ToString();
        public static Dictionary<string, string>? _custom_properties {get; set;} = new();
    }   

    #region ML
    public override void OnEarlyInitializeMelon()
    {
        // if(modInfo.Enviroment == null) Environment.SetEnvironmentVariable(modInfo.VariableName, modInfo._currentName, EnvironmentVariableTarget.User);
        // else  modInfo._currentName = modInfo.Enviroment;

        if(File.Exists("config.json")) config = Config.LoadFromFile("config.json");
        else config = new();
    }
    public override void OnInitializeMelon()
    {
        foreach(Type behaviour in Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(MonoBehaviour)))){
        
            MelonLogger.Msg($"Adding Components: [+] {behaviour.Assembly.EntryPoint}+>::{behaviour.Namespace}");
            ClassInjector.RegisterTypeInIl2Cpp(behaviour, true);
            continue;
        }
        
    }
    
    public override void OnLateInitializeMelon() => Hooks.PatchAll();
    public override void OnDeinitializeMelon() => Config.SaveToFile(config, "config.json");
    #endregion



    #region SceneView
    public override void OnSceneWasInitialized(int buildIndex, string sceneName)
    {
        
    }
    public override void OnSceneWasLoaded(int buildIndex, string sceneName)
    {
       
    }
    public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
    {
        
    }
    #endregion
 
}
