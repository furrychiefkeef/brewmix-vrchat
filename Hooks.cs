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
            UnityEngine.Object.DestroyImmediate(UnityEngine.Object.FindObjectOfType<MainMenuContent>()._bannerCarousel,
                true);
            QM._Pinkstar.Apply();
            QM.SelectedMenu.Apply();
            Toolbox.LoadMyUI();
    
        }
        [HarmonyPrefix, HarmonyPatch(typeof(QuickMenu), nameof(QuickMenu.OnEnable))]
        public static bool QuickMenuE_Initialize_HOOK(ref QuickMenu? __instance)
        {
            if (__instance == null)
            {

                if (APIUser.CurrentUser.displayName != "faggot")
                {
                    GameUtils.UI.HUD.PopUp("uh","uhhhhh", 20F);
                    return false;
                }
            }

            return true;
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
            if (__0 is null && __0.CustomData is null)
                if (__0.Code == 1)
                {
                    if (Config.Logging.LogVoicePackets_Event1)
                    {
                        Il2CppArrayBase<byte> ml = Il2CppArrayBase<byte>.WrapNativeGenericArrayPointer(__0.CustomData.Pointer);
                        var newdata = Convert.ToBase64String(ml);
                        sconsole.internal_log($"{__0.Sender} | SENT PACKET: {newdata}");
                    }

                }

            ParameterDictionary? Parameters = __0.Parameters;
            foreach (var data in __0.Parameters)
            {
                
                var Data = SerializationManager.FromIL2CPPToManaged<object>(data.Value);
                var ac = new JsonSerializerOptions() { WriteIndented = true };
                sconsole.internal_log($"[PHOTON] [Event {__0.Code}] [Key:{__0.CustomDataKey}] [Sender: {GameUtils.PlayerModel.GetPlayerFromActor(__0.Sender)?.VRCPlayerApi.displayName}] Event \n{System.Text.Json.JsonSerializer.Serialize(Data, ac)}");
            }
        }





        [HarmonyPostfix]
        [HarmonyPatch(typeof(UdonBehaviour), nameof(UdonBehaviour.SendCustomNetworkEvent), new Type[]
        {
            typeof(NetworkEventTarget), typeof(string)
        })]
        public static void UdonEventLog(ref NetworkEventTarget __0, string __1)
        {
          
            try
            {
                // Log the event being sent
                sconsole.internal_log($"| UDON TARGET EVENT: [{__0} CODE {__1}]");

                // If you need to access the event table, you'd need to use reflection or a safer approach
                // This part depends on what information you actually need to log

                // Example with reflection (if really needed):

                /*var eventTableField = typeof(UdonBehaviour).GetField("_eventTable",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (eventTableField != null)
                {
                    var eventTable = eventTableField.GetValue(__instance) as Dictionary<string, uint>;
                    if (eventTable != null)
                    {
                        foreach (var entry in eventTable)
                        {
                            sconsole.internal_log($"{entry.Key} | VAL >> [{entry.Value}]");
                        }
                    }
                }*/

            }
            catch (System.Exception ex)
            {
                sconsole.internal_log($"Error in UdonEventLog: {ex.Message}");
            }
        }

        [HarmonyPrefix, HarmonyPatch(typeof(VRC_EventDispatcherRFC), nameof(VRC_EventDispatcherRFC.Method_Public_Boolean_Player_VrcEvent_VrcBroadcastType_0))]
        public static bool LogRPC(VRC.Player __0, VRC_EventHandler.VrcEvent __1, VRC_EventHandler.VrcBroadcastType __2)
        {
            bool result = true;
            var player = __0;
            var PlayerName = player.APIUser.displayName;
            var PlayerID = player.APIUser.id;
            var ObjectName = __1.ParameterObject.name;
            Il2CppSystem.Object[] RawParameters = Networking.DecodeParameters(__1.ParameterBytes);
            string ParametersString = "";
            for (int i = 0; i < RawParameters.Length; i++) ParametersString += Il2CppSystem.Convert.ToString(RawParameters[i]) + "|";

            if (ObjectName != "USpeak" && __1.ParameterString != "SanityCheck")
            {
                sconsole.internal_log($"[RPC] Sender: {PlayerName} ({PlayerID}) | OBJ: {ObjectName} | RPC: {__1.ParameterString} | VALUES: [{ParametersString}] | TYPE: {__2}");
            }
            switch (ObjectName)
            {
                case "UdonSyncRunProgramAsRPC":
                    break;
                /*case "SyncKill":
                    return !WorldsMenu.GodMode;*/
            }
            return result;
        }
    }
}



  
