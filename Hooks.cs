using Brewchat.Cheat;
using Brewchat.Cheat.Modules;
using Brewchat.Game.Monobehaviours;
using HarmonyLib;
using sysPuller;
using VRC.Core;
using VRC.Udon;
using VRC.UI.Core;
using VRC.UI.Elements;
using VRC.UI.Elements.Menus;


namespace brewchat.hybridxcore.bep
{
    [HarmonyPatch]
    internal class Hooks
    {   
        static bool OfflineUILoaded = false;
        static bool OnlineUILoaded = false;
        
        [HarmonyPostfix, HarmonyPatch(typeof(VRCUiPageLoading), nameof(VRCUiPageLoading.OnEnable))]
        public static void ApplicationDomain_Initalize_HOOK(ref VRCUiPageLoading? __instance)
        {
            if (__instance != null && !OfflineUILoaded)
            {
                __instance.gameObject.GetOrAddComponent<CSS>();
                __instance.gameObject.GetOrAddComponent<KeybindHandler>();
                CSS.Offline();
            }
        }

        #region Player_Hooks

        [HarmonyPostfix, HarmonyPatch(typeof(QuickMenu), nameof(QuickMenu.Start))]
        public static void PlayerModel_Initalize_HOOK(ref QuickMenu? __instance)
        {
            if (__instance is null) return;
            UnityEngine.Object.DestroyImmediate(UnityEngine.Object.FindObjectOfType<MainMenuContent>()._bannerCarousel, true);
            __instance.gameObject.GetOrAddComponent<Syrup.IMGUI.simgui>();

            QM.Apply();
            Thread.Sleep(1000);
            CSS.Online();
        }
        
        #region API
        [HarmonyPostfix, HarmonyPatch(typeof(VRCPlusStatus), nameof(VRCPlusStatus.prop_Object1PublicTYBoTYUnique_1_Boolean_0), MethodType.Getter)]
        public static void ExSpoof_VRChatPlus_HOOK(ref Object1PublicTYBoTYUnique<bool> __result)
        {
            if (__result != null && !APIUser.CurrentUser.isSupporter)
                        __result.prop_TYPE_0 = true;
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
