using System.Collections;
using System.Reflection;
using System.Resources;
using Brewchat.Cheat.Modules;
using Brewchat.Game.Monobehaviours;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using Syrup.IMGUI;
using UNIXEngine;
using UNIXEngine.Logging;

[UNIxMod("com.pinkstar.records", "pinkstar*", "0.0.0.0")]
public class Entry : UNIXEngine.IL2CPPModding.UnixMod
{
    Harmony h = new("pinkstar@UZI5eRCE1BBA1ajf6dC2kWoAhPhvw5e4Dlg2nxGA.patches");
    public static ManualLogSource Console = new("PINKSTAR X .RECORDS");
    
    
    public override void Load()
    {
        UNIXEngine.Logging.Logger.Sources.Add(Console);
        #region LIB
        ClassInjector.RegisterTypeInIl2Cpp<Syrup.IMGUI.simgui>(); 
        ClassInjector.RegisterTypeInIl2Cpp<Brewchat.Game.Monobehaviours.Toolbox>();
        ClassInjector.RegisterTypeInIl2Cpp<CSS>();
        ClassInjector.RegisterTypeInIl2Cpp<FlyManager>();
        ClassInjector.RegisterTypeInIl2Cpp<KeybindManager>();
        ClassInjector.RegisterTypeInIl2Cpp<Exploits>();
        ClassInjector.RegisterTypeInIl2Cpp<esp>();
        ClassInjector.RegisterTypeInIl2Cpp<wallhacks>();
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

