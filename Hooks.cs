using Brewchat;
using Brewchat.Cheat;
using Brewchat.Cheat.Modules;
using Brewchat.Game.Monobehaviours;
using Brewchat.Game.Wrappers;
using HarmonyLib;
using Syrup.IMGUI;
using VRC.Core;
using VRC.UI.Elements;
using VRC.UI.Elements.Menus;


namespace brewchat.hybridxcore.bep
{
    [HarmonyPatch]
    internal class Hooks
    {

        static bool _ui = false;
        [HarmonyPostfix, HarmonyPatch(typeof(QuickMenu), nameof(QuickMenu.Start))]
        public static void PlayerModel_Initalized(ref QuickMenu __instance)
        {
            if (__instance.gameObject != null && !_ui)
            {
                UnityEngine.Object.DestroyImmediate(UnityEngine.Object.FindObjectOfType<MainMenuContent>()._bannerCarousel,true);
                __instance.gameObject.GetOrAddComponent<Syrup.IMGUI.simgui>();
                QM.Apply();
                
            }
        }

        #region UpdateHook

        /*[HarmonyPostfix, HarmonyPatch(typeof(VRCUiManager), nameof(QuickMenu.LateUpdate))]
        public static void UpdateManager()
        {
            
        }
        [HarmonyPostfix, HarmonyPatch(typeof(VRCUiManager), nameof(QuickMenu.FixedUpdate))]
        public static void FixedUpdateManager()
        {
            
        }*/
        
        [HarmonyPostfix, HarmonyPatch(typeof(VRCUiManager), nameof(VRCUiManager.LateUpdate))]
        public static void LateUpdateManager__HOOK(ref VRCUiManager __instance)
        {
            
        }
        #endregion
       
    }
}
