using System.Collections;
using System.Reflection;
using System.Resources;
using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using Brewchat.Cheat.Modules;
using Brewchat.Game.Monobehaviours;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using Photon.Realtime;
using Syrup.IMGUI;
using Logger = Cpp2IL.Core.Logging.Logger;

[BepInPlugin("brewchat.mod.com", "brew", "4.0.0")]
public class Entry : BasePlugin
{
    Harmony h = new("brew@UZI5eGVK1BeA1ajf6dC2kWoAhPhvw5e4Dlg2nxGA.patches");
    public static ManualLogSource Log = new("PINKSTAR.RECORDS");
    
    
    public override void Load()
    {
        BepInEx.Logging.Logger.Sources.Add(Log);
        Entry.Log.LogInfo("Entry LEVEL APPLICATION");
        Entry.Log.LogMessage("Entry LEVEL APPLICATION");
        #region LIB
        ClassInjector.RegisterTypeInIl2Cpp<Syrup.IMGUI.simgui>(); 
        ClassInjector.RegisterTypeInIl2Cpp<Brewchat.Game.Monobehaviours.Toolbox>();
        ClassInjector.RegisterTypeInIl2Cpp<CSS>();
        ClassInjector.RegisterTypeInIl2Cpp<FlyManager>();
        ClassInjector.RegisterTypeInIl2Cpp<KeybindManager>();
        h.PatchAll();
        #endregion
    } 
    public static void LogAllResources()
    {
        var assembly = Assembly.GetExecutingAssembly();
        sconsole.print($"=== RESOURCE DUMP for {assembly.GetName().Name} ===", 0);
    
        foreach (var name in assembly.GetManifestResourceNames())
        {
            sconsole.print($"Resource: {name}", 0);
        
            // Try to read the resource to see what's inside
            try 
            {
                using (var stream = assembly.GetManifestResourceStream(name))
                {
                    if (stream != null)
                    {
                        sconsole.print($"  Size: {stream.Length} bytes", 0);
                    
                        // If it's a .resources file, let's peek inside
                        if (name.EndsWith(".resources"))
                        {
                            sconsole.print("  Contents of .resources file:", 0);
                            using (var reader = new ResourceReader(stream))
                            {
                                foreach (DictionaryEntry entry in reader)
                                {
                                    sconsole.print($"   - {entry.Key} ({entry.Value?.GetType().Name})", 0);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sconsole.print($"  ERROR reading: {ex.Message}", 0);
            }
        }
    }
}

   