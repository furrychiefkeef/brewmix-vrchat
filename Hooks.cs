using Brewchat.Cheat;
using Brewchat.Cheat.Modules;
using Brewchat.Game.Monobehaviours;
using Brewchat.Game.Wrappers;
using ExitGames.Client.Photon;
using HarmonyLib;
using MelonLoader;
using MonoMod.RuntimeDetour;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using VRC.Core;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;
using VRC.UI.Core.Styles;
using VRC.UI.Elements;
using VRC.UI.Elements.Menus;
using Player = VRC.Player;

namespace brewchat.hybridxcore.bep
{
    [HarmonyPatch]
    internal class Hooks
    {
        static bool _isVRCApplicationLoaded = false;
        static bool _isFlatOverlayLoaded = false;
        static bool _isSelectedMenusLoaded = false;
        static bool _isMainMenuLoaded = false;  
        static bool _isQuickMenuLoaded = false;

        #region INTERNAL_CORE
        [HarmonyPostfix, HarmonyPatch(typeof(VRCApplication), nameof(VRCApplication.Awake))]
        public static void DomainApp_Initialize_HOOK(ref VRCApplication? __instance)
        {
            if (__instance is null || _isVRCApplicationLoaded) return;
            _isVRCApplicationLoaded = true;
            autumn.ml.Initalizers.ConfigSystem.SetConfig();
            autumn.ml.Initalizers.Cheat.Execute();

            __instance?.gameObject.GetOrAddComponentSafe<CSS>();
            __instance?.gameObject.GetOrAddComponentSafe<KeybindManager>();
        }

        #endregion

        #region INTERNAL_UI

        [HarmonyPostfix, HarmonyPatch(typeof(LoadingOverlayController), nameof(LoadingOverlayController.Start))]
        public static void FlatLoadingOverlay_Initialize_HOOK(ref LoadingOverlayController? __instance)
        {
            if (__instance is null || _isFlatOverlayLoaded) return;
             _isFlatOverlayLoaded = true;
            Image Background = __instance.gameObject
            .transform.Find("FlatLoadingOverlay(Clone)/Container")
            .transform.FindChild("Canvas/Background").GetComponent<Image>();
           
            if (Entry.config.Appearance.inGame.HideLoadingOverlay)
            {
                UnityEngine.Object.DestroyImmediate(Background, true);
                return;
            }

            Background.color = Entry.config.Appearance.inGame.FlatLoadingOverlay.Base;
            if (Entry.config.Appearance.inGame.FlatLoadingOverlay.Image != null)
            {
                GameObject Custom_Background_Overlay_Obj = Background.gameObject.CreateIsolatedObject(true);
                Image Custom_Background_Overlay = Custom_Background_Overlay_Obj.GetComponent<Image>();

                Custom_Background_Overlay.sprite = EngineUtils.Assets.Sprite(new Uri(Entry.config.Appearance.inGame.FlatLoadingOverlay.Image));
                Custom_Background_Overlay.color = Entry.config.Appearance.inGame.FlatLoadingOverlay.Tint;
            }
            
        }

        // [HarmonyPostfix, HarmonyPatch(typeof(VRCUiPageLoading), nameof(VRCUiPageLoading.OnEnable))]
        // public static void VRCLoadingSequence_IsEnable_HOOK(ref VRCUiPageLoading? __instance)
        // {
        //     if (__instance is null) return;
            
        // }

        [HarmonyPostfix, HarmonyPatch(typeof(VRCUiPageLoading), nameof(VRCUiPageLoading.Start))]
        public static void VRCLoadingSequence_Initialize_HOOK(ref VRCUiPageLoading? __instance)
        {
            if (__instance is null) return;
            CSS.SetLoadingAudio();
            // CSS.Offline();
        }

        [HarmonyPostfix, HarmonyPatch(typeof(QuickMenu), nameof(QuickMenu.Start))]
        public static void QuickMenu_Initialize_HOOK(ref QuickMenu? __instance)
        {
            if (__instance is null || _isQuickMenuLoaded) return ; 
            UnityEngine.Object.DestroyImmediate(UnityEngine.Object.FindObjectOfType<MainMenuContent>()._bannerCarousel, true);
            _isQuickMenuLoaded = true;

            QM.Core.EnableRichText();
            QM.Main.Apply();
            Widgets.QM.Initalize();
        } 
        
        [HarmonyPostfix, HarmonyPatch(typeof(SelectedUserMenuQM), nameof(SelectedUserMenuQM.Start))]
        public static void SelectedUserMenu_Initialize_HOOK(ref SelectedUserMenuQM? __instance)
        {
            if (__instance is null || _isSelectedMenusLoaded) return;
            _isSelectedMenusLoaded = true;
            QM.SelectedUserMenu.Apply();
            MM.SelectedUserMenu.Apply();
          
        }

        [HarmonyPrefix, HarmonyPatch(typeof(VRC_Pickup), nameof(VRC_Pickup.Awake))]
        public static bool __0Pickups(ref VRC_Pickup? __instance)
        {
            if ( __instance is null) return false;
            __instance.DisallowTheft = false;
            __instance.proximity = float.MaxValue;
            return true;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(MainMenu), nameof(MainMenu.Start))]
        public static void MainMenu_Initialize_HOOK(ref MainMenu? __instance)
        {
            if (__instance is null || _isMainMenuLoaded) return;
            _isMainMenuLoaded = true;
            MM.Main.Apply();
            MM.AvatarAVMMenu.Apply();
            MM.SelectedWorldMenu.Apply();
           
        }
        #endregion

        #region VRC_API

        [HarmonyPostfix, HarmonyPatch(typeof(VRCPlusStatus), nameof(VRCPlusStatus.prop_Object1PublicTBoTUnique_1_Boolean_0), MethodType.Getter)]
        public static void ExSpoof_VRChatPlus_HOOK(ref Object1PublicTBoTUnique<bool>? __result)
        {
            if (__result != null && !APIUser.CurrentUser.isSupporter)
                __result.field_Protected_T_0 = true;
        }
        // [HarmonyPrefix, HarmonyPatch(typeof(UdonManager), nameof(UdonManager.Awake))]
        // public static bool __0Udon(ref UdonBehaviour __instance)
        // {
        //     if(__instance == null) return false;
        //     if(Entry.config.Modules.Udon.isBlocked) {
        //                 return false;
        //     }
            
        //     return true;
        // }
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

        [HarmonyPostfix, HarmonyPatch(typeof(NetworkManager), nameof(NetworkManager.Method_Internal_Void_Player_0))]
        public static void PlayerJoined_HOOK(VRC.Player __0)
        {
            if (__0 != null && !__0.APIUser.IsSelf)
            {
                Player? player = __0;
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(NetworkManager), nameof(NetworkManager.Method_Internal_Void_Player_PDM_0))]
        public static void PlayerLeft_HOOK(VRC.Player __0)
        {   
            if (__0 != null && !__0.APIUser.IsSelf)
            {
                Player? player = __0;
            }
        }

        #endregion

        #region NetworkUtils

        [HarmonyPostfix, HarmonyPatch(typeof(LoadBalancingClient), nameof(LoadBalancingClient.OnEvent))]
        public static void __PhotonLogging_HOOK(EventData __0)
        {
            /*if (__0 is null && __0.CustomData is null) return;

            if (Config.Logging.Photon)
            {
                object Data = null;
                ParameterDictionary? Parameters = __0.Parameters;
                foreach (var data in Parameters)
                {
                    Data = SerializationManager.FromIL2CPPToManaged<object>(data);
                    string? c = System.Text.Json.JsonSerializer.Serialize(Data, new JsonSerializerOptions { AllowTrailingCommas = true });
                    /*sconsole.print("hook", "/photon", $" Player: {GameUtils.PlayerModel.GetPlayerFromActor(__0.Sender)?.VRCPlayerApi.displayName} Sent! | EV {__0.Code} [Key:{__0.CustomDataKey}] Event Data: \n {c}", 0);#1#
                }
            }*/
        }

        
        [HarmonyPrefix, HarmonyPatch(typeof(VRC_EventDispatcherRFC), nameof(VRC_EventDispatcherRFC.Method_Public_Void_Player_VrcEvent_VrcBroadcastType_Int32_Single_0))]
        private static bool __RFCPatch_HOOK(ref VRC.Player __0, ref VRC_EventHandler.VrcEvent __1, ref VRC_EventHandler.VrcBroadcastType __2, ref int __3, ref float __4)
        {

            if (!Entry.config.Modules.RFCTampering.SDK2.EnforceToAll)
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