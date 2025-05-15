using System.Text.Json;
using Brewchat;
using Brewchat.Cheat;
using Brewchat.Cheat.Modules;
using Brewchat.Game.Monobehaviours;
using Brewchat.Game.Wrappers;
using ExitGames.Client.Photon;
using HarmonyLib;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Pasted.WTFBlaze.Utils.Managers;
using Photon.Realtime;
using Syrup.IMGUI;
using UnityEngine;
using VRC.Core;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;
using VRC.UI.Client;
using VRC.UI.Core.Styles;
using VRC.UI.Elements;
using VRC.UI.Elements.Menus;
using Object = Il2CppSystem.Object;
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
            __instance.gameObject.GetOrAddComponent<KeybindManager>();

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
            __instance?.gameObject.GetOrAddComponent<Syrup.IMGUI.simgui>();
            UnityEngine.Object.DestroyImmediate(UnityEngine.Object.FindObjectOfType<MainMenuContent>()._bannerCarousel, true);
            QM._Pinkstar.Apply();
            QM.SelectedMenu.Apply();
            Toolbox.LoadMyUI();

        }

        [HarmonyPostfix, HarmonyPatch(typeof(MainMenu), nameof(MainMenu.Start))]
        public static void MainMenu_Initialize_HOOK(ref MainMenu? __instance)
        {
            if (__instance == null && !OnlineUILoaded) return;
            MM._Pinkstar.Apply();
            MM.SelectedMenu.Apply();
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
              
                object Data = null;
                ParameterDictionary? Parameters = __0.Parameters;
                foreach (var data in __0.Parameters)
                {
                    Data = SerializationManager.FromIL2CPPToManaged<object>(data.Value);
                    if (__0.Code == 1)
                    {
                        string? c = System.Text.Json.JsonSerializer.Serialize(Data, new JsonSerializerOptions { AllowTrailingCommas = true });
                        sconsole.print("hook", "/photon", $" Player: {GameUtils.PlayerModel.GetPlayerFromActor(__0.Sender)?.VRCPlayerApi.displayName} Sent! | EV {__0.Code} [Key:{__0.CustomDataKey}] Event Data: \n {c}", 0);
                        if (c.Contains("IfaN0fo5NgB4vU0TH7BWslpO6BqI6MN9WYOb1HJ8KdW1WqNKAVfTUdbMYn9euoSksF1SDgDwaIQ4UD/tyDz8OUIAeIXX/76O7krXmLLajE8glg7vvITRrirzBTMLKS0lQqJu00Y0tbEihvStV3AjrxbGrzahMFuYAMlgLCh1/aA6zZjg/TkvAHi9TNXCvkYFvRlQkvZ/LLcToT2oMGAPcORVe51QMTEUx4IJdDkjaXG36+n2MBm8"))
                        {
                            sconsole.internal_log("FURFAGIE UCKY!!");
                        }
                    }
                    
                };
                
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(UdonBehaviour), nameof(UdonBehaviour.SendCustomNetworkEvent), typeof(NetworkEventTarget), typeof(string) )]
        public static void UdonEventLog(ref UdonBehaviour __instance, ref NetworkEventTarget __0, string __1)
        {

            if (Config.Logging.Udon)
            {
                if (__1.Contains("_"))
                {
                    sconsole.print("hook", "/udon", $"Network Event: {__1}", 0);
                }

                sconsole.print("hook", "/udon", $"Node Event: {__1}", 0);


                var eventTableField = typeof(UdonBehaviour).GetField("_eventTable", 
                    
                    System.Reflection.BindingFlags.NonPublic 
                    | System.Reflection.BindingFlags.Instance);
                if (eventTableField != null)
                {
                    var eventTable = eventTableField.GetValue(__instance) as Dictionary<string, uint>;
                    if (eventTable != null)
                    {
                        foreach (var entry in eventTable)
                        {
                            sconsole.internal_log($"global: {entry.Key} | VAL >> [{entry.Value}]");
                        }
                    }
                }
            }
        }
        [HarmonyPatch(typeof(VRC_EventDispatcherRFC), nameof(VRC_EventDispatcherRFC.Method_Public_Void_Player_VrcEvent_VrcBroadcastType_Int32_Single_0))]
        public static void RFC(ref VRC.Player __0, ref VRC_EventHandler.VrcEvent __1, ref VRC_EventHandler.VrcBroadcastType __3, ref int __4, ref float ___5)
        {
            __3 = VRC_EventHandler.VrcBroadcastType.Always;
            sconsole.internal_log($"{__0.APIUser.displayName},{__1.ParameterString},{__3},{__4},{___5}");
        }
    }
}



  
