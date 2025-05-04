using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls;
using Brewchat;
using Brewchat.Cheat;
using Brewchat.Cheat.Modules;
using Brewchat.Game.Monobehaviours;
using Brewchat.Game.Wrappers;
using ExitGames.Client.Photon;
using HarmonyLib;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Photon.Pun;
using Photon.Realtime;
using Syrup.IMGUI;
using UnityEngine;
using UnityEngine.XR;
using Valve.Newtonsoft.Json.Utilities;
using VRC.Core;
using VRC.SDKBase;
using VRC.UI.Elements;
using VRC.UI.Elements.Menus;

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
            __instance?.gameObject.GetOrAddComponent<CSS>();
            __instance.gameObject.GetOrAddComponent<KeybindManager>();
          
               
            /*__instance?.gameObject.GetOrAddComponent<Syrup.IMGUI.simgui>();*/
            CSS.Offline();
        }

        #endregion

        #region INTERNAL_UI

        /*[HarmonyPostfix, HarmonyPatch(typeof(VRCUIManager), nameof(VRCUIManager.Start))]
               public static void UI_Initialize_HOOK(ref QuickMenu? __instance)
               {
                   if (__instance == null && !OnlineUILoaded) return;
                   #region QM
                   UnityEngine.Object.DestroyImmediate(UnityEngine.Object.FindObjectOfType<MainMenuContent>()._bannerCarousel,
                       true);
                   #endregion
                   QM.Apply();
                   Toolbox.LoadMyUI();
               }*/
        [HarmonyPostfix, HarmonyPatch(typeof(VRCUiPageLoading), nameof(VRCUiPageLoading.OnEnable))]
        public static void VRCLoadingSequence_IsEnable_HOOK(ref VRCUiPageLoading? __instance)
        {
            if (__instance is null && !OfflineUILoaded) return;
            CSS.Offline();
            Origin.Apply();
        }

        [HarmonyPostfix, HarmonyPatch(typeof(VRCUiPageLoading), nameof(VRCUiPageLoading.Start))]
        public static void VRCLoadingSequence_Initialize_HOOK(ref VRCUiPageLoading? __instance)
        {
            if (__instance is null && !OfflineUILoaded) return;
            AudioSource? LoadingScreenAudio = __instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>();
            UnityEngine.Object.DestroyImmediate(LoadingScreenAudio);
            var ui = EngineUtils.Assets.CreateAudio(new(Config.Appearance.QueueAudio)).transform.parent =
                __instance.gameObject.transform;
           
        }



        [HarmonyPostfix, HarmonyPatch(typeof(QuickMenu), nameof(QuickMenu.Start))]
        public static void QuickMenu_Initialize_HOOK(ref QuickMenu? __instance)
        {
            if (__instance == null && !OnlineUILoaded) return;
            __instance?.gameObject.GetOrAddComponent<Syrup.IMGUI.simgui>();
            UnityEngine.Object.DestroyImmediate(UnityEngine.Object.FindObjectOfType<MainMenuContent>()._bannerCarousel,
                true);
            QM._Pinkstar.Apply();
            QM.SelectedMenu.Apply();
        }

        [HarmonyPostfix, HarmonyPatch(typeof(MainMenu), nameof(MainMenu.Start))]
        public static void MainMenu_Initialize_HOOK(ref MainMenu? __instance)
        {
            if (__instance == null && !OnlineUILoaded) return;
            MM._Pinkstar.Apply();
            MM.SelectedMenu.Apply();
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

        
        /*[HarmonyPostfix, HarmonyPatch(typeof(VRC.Player), nameof(VRC.Player.Start))]
        public static void PlayerInt(VRC.Player __instance)
        {
        }*/
        
        [HarmonyPostfix, HarmonyPatch(typeof(NetworkManager), nameof(NetworkManager.OnPlayerJoined))]
        public static void PlayerJoined(VRC.Player __0)
        {
            if (__0 != null && !__0.APIUser.IsSelf)
            {
                __0.gameObject.AddComponent<wallhacks>();
                sconsole.print($"Player Joined: {__0.VRCPlayerApi.displayName} | Actor: {__0.VRCPlayerApi.playerId}", 0);
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(NetworkManager), nameof(NetworkManager.OnPlayerLeave))]
        public static void PlayerLeft(VRC.Player __0)
        {
            if (__0 != null && !__0.APIUser.IsSelf)
            {
                sconsole.print($"Player Left: {__0.VRCPlayerApi.displayName}", 0);
            }
        }
        #endregion

        #region Test
        
        #endregion

        [HarmonyPostfix, HarmonyPatch(typeof(LoadBalancingClient), nameof(LoadBalancingClient.OnEvent))]
        public static void PhotonHandler(EventData __0)
        {
            if (__0.Code == 1)
            {
                if (Config.Logging.LogVoicePackets_Event1)
                {
                    Il2CppArrayBase<byte> ml = Il2CppArrayBase<byte>.WrapNativeGenericArrayPointer(__0.CustomData.Pointer);
                    var newdata = Convert.ToBase64String(ml);
                    sconsole.internal_log($"{__0.Sender} | SENT PACKET: {newdata}");
                }
               
            }
        }
    }
}



  
