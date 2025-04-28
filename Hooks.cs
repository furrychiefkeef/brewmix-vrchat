using Brewchat;
using Brewchat.Cheat;
using Brewchat.Cheat.Modules;
using Brewchat.Game.Monobehaviours;
using ExitGames.Client.Photon;
using HarmonyLib;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppInterop.Runtime.Runtime;
using Il2CppSystem.Collections;
using Photon.Pun;
using Photon.Realtime;
using Syrup.IMGUI;
using Unity.Collections;
using VRC.Core;
using VRC.SDKBase;
using VRC.UI.Elements;
using VRC.UI.Elements.Menus;
using Object = Il2CppSystem.Object;


namespace brewchat.hybridxcore.bep
{
    [HarmonyPatch]
    internal class Hooks
    {
        static bool OfflineUILoaded = false;
        static bool OnlineUILoaded = false;

        #region INTERNAL_CORE

        [HarmonyPostfix, HarmonyPatch(typeof(VRCApplication), nameof(VRCApplication.Awake))]
        public static void DomainApp_Initialize_HOOK(ref VRCUiPageLoading? __instance)
        {
            if (__instance is null) return;
            __instance.gameObject.GetOrAddComponent<KeybindManager>();
        }

        #endregion

        #region INTERNAL_UI

        [HarmonyPostfix, HarmonyPatch(typeof(VRCUiPageLoading), nameof(VRCUiPageLoading.OnEnable))]
        public static void VRCLoadingSequence_Initialize_HOOK(ref VRCUiPageLoading? __instance)
        {
            if (__instance is null && !OfflineUILoaded) return;
            __instance?.gameObject.GetOrAddComponent<CSS>();
            CSS.Offline();
        }


        [HarmonyPostfix, HarmonyPatch(typeof(QuickMenu), nameof(QuickMenu.Start))]
        public static void QuickMenu_Initialize_HOOK(ref QuickMenu? __instance)
        {
            if (__instance == null && !OnlineUILoaded) return;
            __instance?.gameObject.GetOrAddComponent<Syrup.IMGUI.simgui>();
            UnityEngine.Object.DestroyImmediate(UnityEngine.Object.FindObjectOfType<MainMenuContent>()._bannerCarousel,
                true);
            QM.Apply();
            Toolbox.LoadMyUI();
        }

        [HarmonyPostfix, HarmonyPatch(typeof(MainMenu), nameof(MainMenu.Start))]
        public static void QuickMenu_Initialize_HOOK(ref MainMenu? __instance)
        {
            if (__instance == null && !OnlineUILoaded) return;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(VRCUIBackground), nameof(VRCUIBackground.Start))]
        public static void BG_Initialize_HOOK(ref VRCUIBackground? __instance)
        {
            if (__instance == null && !OnlineUILoaded) return;
            OnlineUILoaded = true;
            CSS.Online();
        }

        #endregion

        #region INTERNAL_API

        [HarmonyPostfix, HarmonyPatch(typeof(VRCPlusStatus), nameof(VRCPlusStatus.prop_Object1PublicTYBoTYUnique_1_Boolean_0), MethodType.Getter)]
        public static void ExSpoof_VRChatPlus_HOOK(ref Object1PublicTYBoTYUnique<bool> __result)
        {
            if (__result != null && !APIUser.CurrentUser.isSupporter)
                __result.prop_TYPE_0 = true;
        }

        #endregion

        #region UpdateHook

        #endregion

        #region Player_Hooks

        #endregion

        #region PHOTON

        [HarmonyPrefix, HarmonyPatch(typeof(LoadBalancingClient),  nameof(LoadBalancingClient.OnEvent))]
        public static bool CatchPhotonEvents(EventData? __0)
        {
            if (__0 == null) return false;
            /*if (Config.Logging.LogPhotonData)
            {
               
            }*/
            if (__0.Code == 12)
            {
                if (Config.Protection.block_event12)
                {
                    sconsole.print("photon","/hook", " blocking [sync] packet",0);
                    return false;
                }
            }
            if (__0.Code == 43)
            {
                    sconsole.print("photon","/hook", $"{__0.CustomData.ToString() } ",0);
            }
            /*if (__0.Code == 1)
            {
                Il2CppArrayBase<byte> vd = Il2CppArrayBase<byte>.WrapNativeGenericArrayPointer(__0.CustomData.Pointer);
                string ae = Convert.ToBase64String(vd);
                sconsole.print("photon", "/hook", $"[voice] data : {ae} ", 0);
            }*/
            return true;
        }

    /*[HarmonyPostfix, HarmonyPatch(typeof(PhotonNetwork), nameof(PhotonNetwork.Method_Public_Static_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0))]
    public static void CatchOutgoingPhotonEvents(byte __0, object __1, RaiseEventOptions __2, SendOptions __3)
    {

        sconsole.print($"Photon Event Sent: Code: {__0}, Content={__1.GetType()} Send {__3.DeliveryMode} ", 0);
    }*/
        /*[HarmonyPostfix, HarmonyPatch(typeof(VRCNetworkingClient), nameof(VRCNetworkingClient.OnEvent))]
        public static void LogAllPhotonEvents(EventData __0)
        {
            // Log the basic event info
            sconsole.print($"Photon Event: Code={__0.Code}, Sender={__0.Sender}", 0);
    
            // Log the custom data
            if (__0.CustomData != null)
            {
                sconsole.print($"  Custom Data: {__0.CustomData}", 0);
            }
    
            // Log all parameters using Dictionary methods instead of Key/Value properties
            if (__0.Parameters != null)
            {
                // Try to cast to IDictionary to access the keys and values
                var paramDict = __0.Parameters as System.Collections.IDictionary;
                if (paramDict != null)
                {
                    foreach (System.Collections.DictionaryEntry entry in paramDict)
                    {
                        sconsole.print($"  Param {entry.Key}: {entry.Value}", 0);
                    }
                }
                else
                {
                    // If direct casting doesn't work, try to log the whole Parameters object
                    sconsole.print($"  Parameters: {__0.Parameters}", 0);
                }
            }*/
        /*
        [HarmonyPostfix, HarmonyPatch(typeof(VRC_EventDispatcherRFC), nameof(VRC_EventDispatcherRFC.Method_Public_Boolean_Player_VrcEvent_VrcBroadcastType_0))]
        public static void GetEventPatch(ref VRC.Player __0, ref VRC_EventHandler.VrcEvent __1, ref VRC_EventHandler.VrcBroadcastType __2)
        {
            //Need to log events for funny
            var player = __0;
            var PlayerName = player.field_Private_APIUser_0.displayName;
            var PlayerID = player.field_Private_APIUser_0.id;
            var ObjectName = __1.ParameterObject.name;
            Il2CppSystem.Object[] RawParameters = Networking.DecodeParameters(__1.ParameterBytes);
            string ParametersString = "";
            for (int i = 0; i < RawParameters.Length; i++) ParametersString += Il2CppSystem.Convert.ToString(RawParameters[i]) + "|";
            sconsole.print($"[RPC] Sender: {PlayerName} ({PlayerID}) | OBJ: {ObjectName} | RPC: {__1.ParameterString} | VALUES: [{ParametersString}] | TYPE: {__2}",0);
            */
           
            /*switch (ObjectName)
            {
                case "UdonSyncRunProgramAsRPC":
                    break;
                /*case "SyncKill":
                    return !WorldsMenu.GodMode;#1#
            }*/
        }
        
    
        /*[HarmonyPostfix, HarmonyPatch(typeof(NetworkManager), nameof(NetworkManager.Method_Public_Virtual_Final_New_Void_EventData_0))]
        public static void CatchOutgoingPhotonEvents(ref EventData? __0)
        {

            sconsole.print($"Photon Event: Sender: Sender -> {__0.sender}, Param={__0.Parameters} Code: {__0.Code} ", 0);
        }*/
        /*[HarmonyPostfix, HarmonyPatch(typeof(NetworkManager), nameof(NetworkManager.Method_Public_Void_Player_0))]
        public static void PlayerJoined(ref VRC.Player player)
        {
                sconsole.print($"Player Joined: {player}", 0);
        }
        
        [HarmonyPostfix, HarmonyPatch(typeof(NetworkManager), nameof(NetworkManager.field_Internal_Static_NetworkManager_0.Method_Public_Void_Player_PDM_1))]
        public static void PlayerLeft(VRC.Player player)
        {
            sconsole.print($"Player Left: {player}", 0);
        }*/
        #endregion
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
