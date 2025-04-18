using Brewchat;
using Brewchat.Cheat;
using Brewchat.Cheat.Modules;
using Brewchat.Game.Monobehaviours;
using Brewchat.Game.Wrappers;
using ExitGames.Client.Photon;
using HarmonyLib;
using Photon.Pun;
using Sentry.Unity.NativeUtils;
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
        
        #region Player_Hooks

        [HarmonyPostfix, HarmonyPatch(typeof(QuickMenu), nameof(QuickMenu.Start))]
        public static void PlayerModel_Initalize_HOOK(ref QuickMenu __instance)
        {
            if (__instance.gameObject != null && !_ui)
            {
                UnityEngine.Object.DestroyImmediate(
                    UnityEngine.Object.FindObjectOfType<MainMenuContent>()._bannerCarousel, true);
                __instance.gameObject.GetOrAddComponent<Syrup.IMGUI.simgui>();
                __instance.gameObject.GetOrAddComponent<CSS>();
                QM.Apply();
            }
        }


        #region API
        [HarmonyPostfix, HarmonyPatch(typeof(VRCPlusStatus), nameof(VRCPlusStatus.prop_Object1PublicTYBoTYUnique_1_Boolean_0), MethodType.Getter)]
        public static void ExSpoof_VRChatPlus_HOOK(ref Object1PublicTYBoTYUnique<bool> __result)
        {
            if (__result != null && !APIUser.CurrentUser.isSupporter)
                __result.prop_TYPE_0 = true;
        }  
        [HarmonyPostfix, HarmonyPatch(typeof(VRC.Print), nameof(VRC.Print.prop_FlatBufferNetworkSerializer_0), MethodType.Setter)]
        public static void BlockPrinting(ref bool __result)
        {
            __result = false;
        }
        #endregion
        #endregion
        
        #region UpdateHook
        #endregion
      
    }
   
}
/*[HarmonyPostfix, HarmonyPatch(typeof(VRCUiManager), nameof(QuickMenu.LateUpdate))]
      public static void UpdateManager()
      {

      }
      [HarmonyPostfix, HarmonyPatch(typeof(VRCUiManager), nameof(QuickMenu.FixedUpdate))]
      public static void FixedUpdateManager()
      {

      }*/
        
/*[HarmonyPostfix, HarmonyPatch(typeof(VRCUiManager), nameof(VRCUiManager.LateUpdate))]
public static void LateUpdateManager__HOOK(ref VRCUiManager __instance)
{

}*/
/*[HarmonyPostfix, HarmonyPatch(typeof(ChatBubbleRateLimiter), nameof(ChatBubbleRateLimiter.field_Private_Single_0), MethodType.Getter | MethodType.Setter)]
public static float RateLimiterBypass__HOOK(ref ChatBubbleRateLimiter __instance, ref float __result)
{

    if (__instance != null && __instance.gameObject != null)
    {
        __result = 0;
        sconsole.SLog($"RATE LIMITER BYPASSED CHECK DEBUG: {__result}");
        return __result;
    }
    return __result;
}
#endregion*/
