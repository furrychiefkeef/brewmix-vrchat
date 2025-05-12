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
                    sconsole.print("hook", "/photon", $" Player: {GameUtils.PlayerModel.GetPlayerFromActor(__0.Sender)?.VRCPlayerApi.displayName} Sent! | EV {__0.Code} [Key:{__0.CustomDataKey}] Event Data: \n {System.Text.Json.JsonSerializer.Serialize(Data, new JsonSerializerOptions { AllowTrailingCommas = true})}", 0);
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

        /*[HarmonyPrefix, HarmonyPatch(typeof(VRC_EventDispatcherRFC), nameof(Networking.RPC), 
             typeof(RPC.Destination), typeof(GameObject), typeof(string), typeof(Il2CppReferenceArray<Object>))]

        public static bool LogRPC(RPC.Destination __0, GameObject __1, string __2, Il2CppReferenceArray<Object> __3)
        {
            bool result = true;
            var player = __0;
            /*var PlayerName = player.APIUser.displayName;
            var PlayerID = player.APIUser.id;#1#
            /*Il2CppSystem.Object[] RawParameters = Networking.DecodeParameters(__3);
            string ParametersString = "";
            for (int i = 0; i < RawParameters.Length; i++) ParametersString += Il2CppSystem.Convert.ToString(RawParameters[i]) + "|";#1#

            if (!Config.Logging.RPC)
            {
                sconsole.print("hook", "/rpc", $"DEST {__0}| | RPC: {__1.gameObject.name} | VALUES: [{__2}] | TYPE: {__3}",0);
            }

            return result;
        }*/
    }
}
  

  
