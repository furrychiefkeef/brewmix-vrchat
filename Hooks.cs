using System.Text.Json;
using Brewchat;
using Brewchat.Cheat;
using Brewchat.Cheat.Modules;
using Brewchat.Game.Monobehaviours;
using Brewchat.Game.Wrappers;
using brewchat.hybridxcore.bep.Cheat.IMGUI;
using ExitGames.Client.Photon;
using HarmonyLib;
using Pasted.WTFBlaze.Utils.Managers;
using Photon.Realtime;
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
        static bool OverlayLoadedLoaded = false;
        static bool OfflineUILoaded = false;
        static bool OnlineUILoaded = false;



        #region INTERNAL_CORE

        [HarmonyPostfix, HarmonyPatch(typeof(VRCApplication), nameof(VRCApplication.Awake))]
        public static void DomainApp_Initialize_HOOK(ref VRCUiPageLoading? __instance)
        {
            if (__instance is null) return;
            __instance?.gameObject.GetOrAddComponent<CSS>();
            __instance?.gameObject.GetOrAddComponent<GifLoader>();
            __instance.gameObject.GetOrAddComponent<KeybindManager>();
            __instance.gameObject.GetOrAddComponent<Toolbox>();
            Toolbox.Instance?.LoadMyUI_Console();
            /*__instance?.gameObject.GetOrAddComponent<Syrup.IMGUI.simgui>();*/
            CSS.Offline();
        }

        #endregion



        #region INTERNAL_UI

        [HarmonyPostfix, HarmonyPatch(typeof(LoadingOverlayController), nameof(LoadingOverlayController.Start))]
        public static void FlatLoadingOverlay_Initialize_HOOK(ref LoadingOverlayController __instance)
        {
            if (__instance is null && !OverlayLoadedLoaded) return;
            var Background = __instance.gameObject.transform.Find("FlatLoadingOverlay(Clone)/Container").transform.FindChild("Canvas/Background").GetComponent<Image>();
            OverlayLoadedLoaded = true;

            if (Config.Appearance.Hiders.HideLoadingOverlay)
            {
                UnityEngine.Object.DestroyImmediate(Background, true);
                return;
            }

            if (Config.Appearance.FlatLoadingOverlay != null)
            {

                Background.color = new Color(0, 0, 0, 0.9f);
                var iso = GifLoader.CreateIsolatedImage(Background);
                GifLoader.Instance.AddAnimatedGif(iso, new Uri(Config.Appearance.FlatLoadingOverlay));

            }
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

            UnityEngine.Object.DestroyImmediate(UnityEngine.Object.FindObjectOfType<MainMenuContent>()._bannerCarousel, true);
            Widgets.QM.Initalize();
            LimeConsole.Text("Quick Menu Successfully Loaded!", 0);
            QM.Main.Apply();


        } 
        
        [HarmonyPostfix, HarmonyPatch(typeof(SelectedUserMenuQM), nameof(SelectedUserMenuQM.Start))]
        public static void QM_SelectdMenu_Initialize_HOOK(ref SelectedUserMenuQM? __instance)
        {
            if (__instance == null && !OnlineUILoaded) return;
            QM.SelectedMenu.Apply();
            Console.Clear();
        }
        
        [HarmonyPostfix, HarmonyPatch(typeof(MainMenu), nameof(MainMenu.Start))]
        public static void MainMenu_Initialize_HOOK(ref MainMenu? __instance)
        {
            if (__instance == null && !OnlineUILoaded) return;
            MM.Main.Apply();
        }
       

        [HarmonyPostfix, HarmonyPatch(typeof(VRCUIBackground), nameof(VRCUIBackground.Start))]
        public static void QuickMenuBackground_Initialize_HOOK(ref VRCUIBackground? __instance)
        {

            if (__instance == null && !OnlineUILoaded) return;
            OnlineUILoaded = true;
            CSS.Online();
        }

        #endregion

        #region VRC_API

        [HarmonyPostfix, HarmonyPatch(typeof(VRCPlusStatus), nameof(VRCPlusStatus.prop_Object1PublicTBoTUnique_1_Boolean_0), MethodType.Getter)]
        public static void ExSpoof_VRChatPlus_HOOK(ref Object1PublicTBoTUnique<bool> __result)
        {
            if (__result != null && !APIUser.CurrentUser.isSupporter)
                __result.field_Protected_T_0 = true;
        }

        #endregion

        #region UpdateHook

        /*[HarmonyPostfix, HarmonyPatch(typeof(VRCUiManager), nameof(VRCUiManager.LateUpdate), MethodType.Getter)]
        public static void _0UpdateManager(ref VRCUiManager _instance)
        {
            if (_instance != null && !APIUser.CurrentUser.isSupporter) return;
        }
        [HarmonyPostfix, HarmonyPatch(typeof(VRCUiManager), nameof(VRCUiManager.Update), MethodType.Getter)]
        public static void _1UpdateManager(ref VRCUiManager _instance)
        {
            if (_instance != null && !APIUser.CurrentUser.isSupporter) return;
        }
        [HarmonyPostfix, HarmonyPatch(typeof(VRCApplication), nameof(VRCApplication.Update), MethodType.Getter)]
        public static void _2UpdateManager(ref VRCUiManager _instance)
        {
            if (_instance != null && !APIUser.CurrentUser.isSupporter) return;
        }*/

        #endregion

        #region NetworkManager

        [HarmonyPostfix, HarmonyPatch(typeof(NetworkManager), nameof(NetworkManager.OnPlayerJoined))]
        public static void PlayerJoined_HOOK(VRC.Player __0)
        {
            if (__0 != null && !__0.APIUser.IsSelf)
            {
                Player? player = __0;
                if (player.APIUser.allowAvatarCopying != true) player.APIUser.allowAvatarCopying = true;
                LimeConsole.Text($"Player Joined: <color=#ff0000>[{player.IsAdult()}]</color> <color=#{EngineUtils.Conversion.ColorToHex(player.GetTrustColor())}>[{player.GetTrustRank()}]</color> {player.VRCPlayerApi.displayName} : ({player.APIUser.id}) | ACTOR: {player.VRCPlayerApi.playerId}", Logging.GameServer);

            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(NetworkManager), nameof(NetworkManager.OnPlayerLeave))]
        public static void PlayerLeft_HOOK(VRC.Player __0)
        {
            if (__0 != null && !__0.APIUser.IsSelf)
            {
                Player? player = __0;
                LimeConsole.Text($"Player Left: {player.VRCPlayerApi.displayName} : ({player.APIUser.id})", Logging.GameServer);
            }
        }

        #endregion



        #region NetworkUtils

        [HarmonyPostfix, HarmonyPatch(typeof(LoadBalancingClient), nameof(LoadBalancingClient.OnEvent))]
        public static void __PhotonLogging_HOOK(EventData __0)
        {
            if (__0 is null && __0.CustomData is null) return;

            if (Config.Logging.Photon)
            {
                object Data = null;
                ParameterDictionary? Parameters = __0.Parameters;
                foreach (var data in Parameters)
                {
                    Data = SerializationManager.FromIL2CPPToManaged<object>(data.Value);
                    string? c = System.Text.Json.JsonSerializer.Serialize(Data, new JsonSerializerOptions { AllowTrailingCommas = true });
                    /*sconsole.print("hook", "/photon", $" Player: {GameUtils.PlayerModel.GetPlayerFromActor(__0.Sender)?.VRCPlayerApi.displayName} Sent! | EV {__0.Code} [Key:{__0.CustomDataKey}] Event Data: \n {c}", 0);*/
                }
            }
        }


        [HarmonyPrefix, HarmonyPatch(typeof(VRC_EventDispatcherRFC), nameof(VRC_EventDispatcherRFC.Method_Public_Void_Player_VrcEvent_VrcBroadcastType_Int32_Single_0))]
        private static bool __RFCPatch_HOOK(ref VRC.Player __0, ref VRC_EventHandler.VrcEvent __1, ref VRCSDK2.VRC_EventHandler.VrcBroadcastType __2, ref int __3, ref float __4)
        {
            if (!Config.Exploits.RPC_ForceGlobal)
            {
                if (__0?.VRCPlayerApi?.displayName == APIUser.CurrentUser?.displayName)
                {
                    __2 = VRC_EventHandler.VrcBroadcastType.AlwaysUnbuffered;
                    return true;
                }

                return true;
            }

            return true;
        }
        
     
        #endregion
    }  
    
}

      














/*#region INTERNAL_UI

[HarmonyPostfix, HarmonyPatch(typeof(VRCUIManager), nameof(VRCUIManager.Start))]
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