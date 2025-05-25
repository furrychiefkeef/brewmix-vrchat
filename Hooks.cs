using System.Text.Json;
using Brewchat;
using Brewchat.Cheat;
using Brewchat.Cheat.Modules;
using Brewchat.Game.Monobehaviours;
using Brewchat.Game.Wrappers;
using ExitGames.Client.Photon;
using HarmonyLib;
using Pasted.WTFBlaze.Utils.Managers;
using Photon.Realtime;
using Syrup.IMGUI;
using UnityEngine;
using UnityEngine.UI;
using VRC.Core;
using VRC.SDKBase;
using VRC.UI.Elements;
using VRC.UI.Elements.Menus;
using Player = VRC.Player;

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
            /*__instance.gameObject.GetOrAddComponent<KeybindManager>()*/;

            /*__instance?.gameObject.GetOrAddComponent<Syrup.IMGUI.simgui>();*/
            CSS.Offline();
        }

        #endregion

        #region INTERNAL_UI

        /*[HarmonyPostfix, HarmonyPatch(typeof(VRCUIManager), nameof(VRCUIManager.Start))]
        public static void UI_Initialize_HOOK(ref VRCUIManager? __instance)
        {
            if (__instance == null && !OnlineUILoaded) return;
            #region QM
            UnityEngine.Object.DestroyImmediate(UnityEngine.Object.FindObjectOfType<MainMenuContent>()._bannerCarousel,
                true);
            #endregion
            QM._Pinkstar.Apply();
            MM._Pinkstar.Apply();

            MM.SelectedMenu.Apply();
            QM.SelectedMenu.Apply();
            Toolbox.LoadMyUI();
        }*/

        [HarmonyPostfix, HarmonyPatch(typeof(FlatLoadingOverlay), nameof(FlatLoadingOverlay.Method_Private_Void_0))]
        public static void FlatLoadingOverlay_Initialize_HOOK(ref FlatLoadingOverlay? __instance)
        {
            /*if (__instance is null && !OfflineUILoaded) return;
            var RAW = __instance._container.GetComponent<Canvas>().gameObject
            Console.Clear();
            Console.WriteLine("--- IMAGE PROPERTIES ---");
            foreach (var VARIABLE in RAW.material.shaderKeywords)
            {
                Console.WriteLine($"{VARIABLE}");
            }*/
        }
        
        [HarmonyPostfix, HarmonyPatch(typeof(VRCUiPageLoading), nameof(VRCUiPageLoading.OnEnable))]
        public static void VRCLoadingSequence_IsEnable_HOOK(ref VRCUiPageLoading? __instance)
        {
            if (__instance is null && !OfflineUILoaded) return;
            CSS.Offline();
        }

        [HarmonyPostfix, HarmonyPatch(typeof(VRCUiPageLoading), nameof(VRCUiPageLoading.Start))]
        public static void VRCLoadingSequence_Initialize_HOOK(ref VRCUiPageLoading? __instance)
        {
            if (__instance is null && !OfflineUILoaded) return;
            AudioSource? LoadingScreenAudio = __instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>();
            UnityEngine.Object.DestroyImmediate(LoadingScreenAudio);
            EngineUtils.Assets.CreateAudio(new(Config.Appearance.QueueAudio)).transform.parent = __instance.gameObject.transform;
    
        }

        [HarmonyPostfix, HarmonyPatch(typeof(QuickMenu), nameof(QuickMenu.Start))]
        public static void QuickMenu_Initialize_HOOK(ref QuickMenu? __instance)
        {
            if (__instance == null && !OnlineUILoaded) return;
            /*__instance?.gameObject.GetOrAddComponent<Syrup.IMGUI.simgui>();*/
            UnityEngine.Object.DestroyImmediate(UnityEngine.Object.FindObjectOfType<MainMenuContent>()._bannerCarousel, true);
            QM._Pinkstar.Apply();
           
            QM.SelectedMenu.Apply();
            QM.TestMenu.Apply();
            Toolbox.LoadMyUI();
            Widgets.QM.Initalize();

        }

        [HarmonyPostfix, HarmonyPatch(typeof(MainMenu), nameof(MainMenu.Start))]
        public static void MainMenu_Initialize_HOOK(ref MainMenu? __instance)
        {
            if (__instance == null && !OnlineUILoaded) return;
            MM._Pinkstar.Apply();
            MM.SelectedMenu.Apply();
            MM.AvatarAVMMenu.Apply();
            MM.WorldMenu.Apply();
            Console.Clear();
        }

        [HarmonyPostfix, HarmonyPatch(typeof(VRCUIBackground), nameof(VRCUIBackground.Start))]
        public static void StyleEngine_Initialize_HOOK(ref VRCUIBackground? __instance)
        {
            if (__instance == null && !OnlineUILoaded) return;
            OnlineUILoaded = true;
            CSS.Online();
        }

        #endregion

        #region INTERNAL_API

        [HarmonyPostfix, HarmonyPatch(typeof(VRCPlusStatus), nameof(VRCPlusStatus.prop_Object1PublicTBoTUnique_1_Boolean_0), MethodType.Getter)]
        public static void ExSpoof_VRChatPlus_HOOK(ref Object1PublicTBoTUnique<bool> __result)
        {
            if (__result != null && !APIUser.CurrentUser.isSupporter)
                __result.field_Protected_T_0 = true;
        }

        #endregion

        #region UpdateHook

        #endregion

        #region Player_Hooks

        [HarmonyPostfix, HarmonyPatch(typeof(NetworkManager), nameof(NetworkManager.OnPlayerJoined))]
        public static void PlayerJoined(VRC.Player __0)
        {
            if (__0 != null && !__0.APIUser.IsSelf)
            {
                Player? player = __0;
                if (player.APIUser.allowAvatarCopying != true) player.APIUser.allowAvatarCopying = true;
                sconsole.print($"Player Joined: <color=#ff0000>[{player.IsAdult()}]</color> <color=#{EngineUtils.Conversion.ColorToHex(player.GetTrustColor())}>[{player.GetTrustRank()}]</color> {player.VRCPlayerApi.displayName} : ({player.APIUser.id}) | ACTOR: {player.VRCPlayerApi.playerId}", 0);
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(NetworkManager), nameof(NetworkManager.OnPlayerLeave))]
        public static void PlayerLeft(VRC.Player __0)
        {
            if (__0 != null && !__0.APIUser.IsSelf)
            {
                Player? player = __0;
                sconsole.print($"Player Left: {player.VRCPlayerApi.displayName} : ({player.APIUser.id})", 0);
            }
        }

        #endregion

        #region Test

        #endregion

        [HarmonyPostfix, HarmonyPatch(typeof(LoadBalancingClient), nameof(LoadBalancingClient.OnEvent))]
        public static void PhotonHandler(EventData __0)
        {
            if (__0 is null && __0.CustomData is null) return;

            if (Config.Logging.Photon)
            {

                bool printed = false;
                object Data = null;
                ParameterDictionary? Parameters = __0.Parameters;
                foreach (var data in Parameters)
                {
                    if (!printed)
                    {
                        Data = SerializationManager.FromIL2CPPToManaged<object>(data.Value);
                        string? c = System.Text.Json.JsonSerializer.Serialize(Data, new JsonSerializerOptions { AllowTrailingCommas = true });
                        sconsole.print("hook", "/photon", $" Player: {GameUtils.PlayerModel.GetPlayerFromActor(__0.Sender)?.VRCPlayerApi.displayName} Sent! | EV {__0.Code} [Key:{__0.CustomDataKey}] Event Data: \n {c}", 0);
                        printed = true;
                    }
                    /*if (c.Contains("IfaN0fo5NgB4vU0TH7BWslpO6BqI6MN9WYOb1HJ8KdW1WqNKAVfTUdbMYn9euoSksF1SDgDwaIQ4UD/tyDz8OUIAeIXX/76O7krXmLLajE8glg7vvITRrirzBTMLKS0lQqJu00Y0tbEihvStV3AjrxbGrzahMFuYAMlgLCh1/aA6zZjg/TkvAHi9TNXCvkYFvRlQkvZ/LLcToT2oMGAPcORVe51QMTEUx4IJdDkjaXG36+n2MBm8"))
                    {
                        sconsole.internal_log("FURFAGIE UCKY!!");
                    }*/
                }

                ;

            }
        }

       
        [HarmonyPrefix, HarmonyPatch(typeof(VRC_EventDispatcherRFC), nameof(VRC_EventDispatcherRFC.Method_Public_Void_Player_VrcEvent_VrcBroadcastType_Int32_Single_0))]
        private static bool __RFCPatch(ref VRC.Player __0, ref VRC_EventHandler.VrcEvent __1, ref VRCSDK2.VRC_EventHandler.VrcBroadcastType __2, ref int __3, ref float __4)
        {
           
            if (__0?.VRCPlayerApi?.displayName == APIUser.CurrentUser?.displayName)
            {
                __2 = VRC_EventHandler.VrcBroadcastType.AlwaysUnbuffered;
                return true;
            }
            return true;
        }
    }
}
    
       
   
  
