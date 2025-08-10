using System;
using System.Reflection;
using CoreRuntime.Interfaces;
using CoreRuntime.Manager;
using Mono.CSharp;

using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.UI;


public class Entry : HexedMod
{

    internal static Entry GetEntry() => new();
    internal ModInfo? modInfo => new();
    public static Config config = new();

   
    internal class ModInfo
    {
        public Prefixes prefixes => new();
        public class Prefixes
        {
            public static string VariableName = "_VRLone";
        }
        public readonly string? Enviroment = Environment.GetEnvironmentVariable(Prefixes.VariableName, EnvironmentVariableTarget.User);
        public string? _currentName { get; set; } = "Vanity";
        public string? _currentVersion { get; set; } = "<2>.<5>.<7>.<7>";
        public string? _currentHash { get; set; } = System.Reflection.Assembly.GetExecutingAssembly().GetHashCode().ToString();
        public static Dictionary<string, string>? _custom_properties { get; set; } = new();
    }

    public override void OnEntry(string[] args = null)
    {
        foreach (Type behaviour in Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(MonoBehaviour))))
        {
            ClassInjector.RegisterTypeInIl2Cpp(behaviour, true);
            continue;
        }
        if (File.Exists("config.json")) config = Config.LoadFromFile("config.json");
        else config = new();

        HooksManager.ApplyAll();
    }
    #region ML

    public override void OnApplicationQuit() => Config.SaveToFile(config, "config.json");
 
    #endregion



    #region SceneView

    #endregion

}
