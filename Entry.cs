using System.Collections;

using System.Reflection;
using System.Resources;

using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using Syrup.IMGUI;
using UNIXEngine;

using UNIXEngine.Logging;


[UnixModDescriptor("com.pinkstar.records", "pinkstar*", "0.0.0.0")]
public class Entry : UNIXEngine.IL2CPPModding.UnixMod
{
    public static string NAME => "PINKSTAR";
    public static string FULLNAME => "PINKSTAR X .RECORDS";
    public static string VERSION => "0.0.0.0";
    Harmony h = new("pinkstar@UZI5eRCE1BBA1ajf6dC2kWoAhPhvw5e4Dlg2nxGA.patches");
    public static ManualLogSource Console = new("PINKSTAR X .RECORDS");
    
    
    public override void Load()
    {
        UNIXEngine.Logging.Logger.Sources.Add(Console);
        h.PatchAll();
        #region Components
        #region CORE
        foreach (var type in Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(UnityEngine.MonoBehaviour))))
        {
            Log.LogInfo($"[Setup] Registering Mono: {type.Name}");
            ClassInjector.RegisterTypeInIl2Cpp(type);
        }
        #endregion

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

