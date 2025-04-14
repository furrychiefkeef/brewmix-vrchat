using BepInEx;
using BepInEx.Logging;

using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;

[BepInPlugin("brewchat.mod.com", "brew", "2.0.0")]
public class Entry : BasePlugin
{
    Harmony h = new("brew@UZI5eGVK1BeA1ajf6dC2kWoAhPhvw5e4Dlg2nxGA.patches");
    public static ManualLogSource Log;
    public override void Load()
    {
       
        #region LIB
        ClassInjector.RegisterTypeInIl2Cpp<Syrup.IMGUI.simgui>();
        h.PatchAll();
        #endregion

    }
}

   