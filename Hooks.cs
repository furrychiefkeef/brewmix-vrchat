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
        public static Il2CppSystem.Collections.Generic.KeyValuePair<byte, Object>? xdata;
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

        [HarmonyPostfix, HarmonyPatch(typeof(StyleEngine), nameof(StyleEngine.Start))]
        public static void StyleEngine_Initialize_HOOK(ref StyleEngine? __instance)
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
                if (__0.APIUser.allowAvatarCopying != true) __0.APIUser.allowAvatarCopying = true;

                /*__0.gameObject.AddComponent<wallhacks>();*/
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

        [HarmonyPostfix, HarmonyPatch(typeof(UdonBehaviour), nameof(UdonBehaviour.SendCustomNetworkEvent), new Type[] { typeof(NetworkEventTarget), typeof(string) })]
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

        [HarmonyPostfix]
        [HarmonyPatch(typeof(VRC_EventDispatcherRFC), nameof(VRC_EventDispatcherRFC.TriggerEvent), 
          typeof(VRC_EventHandler) ,
          typeof(VRC_EventHandler.VrcEvent),
          typeof(VRC_EventHandler.VrcBroadcastType),
          typeof(int) ,
          typeof(float))]
        public static void SD2(VRC_EventDispatcherRFC __instance, VRC_EventHandler __0, VRC_EventHandler.VrcEvent __1, VRC_EventHandler.VrcBroadcastType __2, int __3, float __4)
        {
            // Additional null checks and logging
            if (__instance == null)
            {
                return;
            }

            try
            {
                Networking.SetOwner(GameUtils.PlayerModel.Get().VRCPlayerApi, __0.gameObject);
                __2 = VRC_EventHandler.VrcBroadcastType.Owner;
                sconsole.internal_log($"SDK2: " +
                                      $"Handler: {__0.name ?? "null"} | " +
                                      $"Event: {__1.ParameterString ?? "null"} | " +
                                      $"KAST: {__2} | " +
                                      $"Instagator Value: {__3}" +
                                      $"FastForward Value: {__4}");
                
            }
            catch (System.Exception ex)
            {
                sconsole.internal_log($"Error in SD2 patch: {ex.Message}");
            }
        }
    }
}
  

  
