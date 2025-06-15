using System.Reflection;
using Brewchat.Game.Monobehaviours;
using HarmonyLib;

using MelonLoader;
using UnhollowerRuntimeLib;
using UnityEngine;
using VRC.SDKBase;
using Brewchat.Game.Wrappers;

[assembly: MelonInfo(typeof(Entry), "Autumn", "7.0", "Dog")]
public class Entry : MelonMod
{
    public const string NAME = "Autumn";
    public const string FULLNAME = "Autumn";
    public static Config config = new();
    public static HarmonyLib.Harmony h = new(Guid.NewGuid().ToString());
    public override void OnInitializeMelon()
    {
        Config.SaveToFile(config, "config.json");
        config._Appearance._inGame._FlatLoadingOverlay.Tint.ChangeColor(new(0, 0, 0, 0.9f));
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
}

