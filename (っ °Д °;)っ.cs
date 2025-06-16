using System.Reflection;
using Brewchat.Game.Monobehaviours;
using HarmonyLib;

using MelonLoader;
using UnhollowerRuntimeLib;
using UnityEngine;
using VRC.SDKBase;
using Brewchat.Game.Wrappers;
using autumn.ml;
using autumn;

[assembly: MelonInfo(typeof(Entry), "Autumn", "7.0", "Dog")]
public class Entry : MelonMod
{
    public string? NAME;

    public static Entry entry = new();
    public static Config config = new();
    public static HarmonyLib.Harmony h = new(Guid.NewGuid().ToString());
    public override void OnInitializeMelon()
    {

        if (File.Exists("config.json")) config = Config.LoadFromFile("config.json");
        else config = new Config();
        Config.SaveToFile(config, "config.json");

        #region Components
        try
        {
            
            foreach (Type monos in Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(MonoBehaviour))))
            {
                MelonLogger.Msg($"Registering Monobehaviours: {monos.Name}");
                ClassInjector.RegisterTypeInIl2Cpp(monos,true);
            }
        }
        catch (Exception ex){ MelonLogger.Msg(ex); }
      
      
        
        #endregion
        
        #region ASSEMBLY_CORE_STUFF
        
        h.PatchAll();
        #endregion
       
    }
    public override void OnEarlyInitializeMelon()
    {

        if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("Visionary.Mod.ML.Name")) && string.IsNullOrEmpty(Environment.GetEnvironmentVariable("Visionary.Mod.ML.Icon")))
        {
            Environment.SetEnvironmentVariable("Visionary.Mod.ML.Name", "Visionary", EnvironmentVariableTarget.User);
            Environment.SetEnvironmentVariable("Visionary.Mod.ML.Icon", "https://", EnvironmentVariableTarget.User);
        }
        else
        {
            MelonLogger.Msg("Debug Mode Disabled");
            string? name = Environment.GetEnvironmentVariable("Visionary.Mod.ML.Name", EnvironmentVariableTarget.User);
            entry.NAME = name;
        }
    }
    public override void OnSceneWasLoaded(int buildIndex, string sceneName)
    {
        base.OnSceneWasLoaded(buildIndex, sceneName);
    }
    public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
    {
        base.OnSceneWasUnloaded(buildIndex, sceneName);
    }
    public override void OnDeinitializeMelon()
    {
        Config.SaveToFile(config, "config.json");
    }
}

