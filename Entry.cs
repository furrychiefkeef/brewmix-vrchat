using BepInEx;
using BepInEx.Unity.IL2CPP;
using Brewchat.Cheat.Modules;
using Brewchat.Game.Monobehaviours;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;

[BepInPlugin("brewchat.mod.com", "brew", "4.0.0")]
public class Entry : BasePlugin
{
    Harmony h = new("brew@UZI5eGVK1BeA1ajf6dC2kWoAhPhvw5e4Dlg2nxGA.patches");
    public override void Load()
    {
        #region LIB
        ClassInjector.RegisterTypeInIl2Cpp<Syrup.IMGUI.simgui>(); 
        ClassInjector.RegisterTypeInIl2Cpp<Brewchat.Game.Monobehaviours.Toolbox>();
        ClassInjector.RegisterTypeInIl2Cpp<CSS>();
        ClassInjector.RegisterTypeInIl2Cpp<FlyManager>();
        h.PatchAll();
        #endregion
    }
}

   