using BlanketSDK.Utilities;
using Brewview;
using ExitGames.Client.Photon;
using Il2CppInterop.Runtime;
using Il2CppMono.Security.Interface;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.DataModel;
using VRC.Localization;
using VRC.SDKBase;
using VRC.UI.Client.HUD;
using VRC.UI.Elements;
using VRC.UI.Elements.Controls;
using VRC.UI.Elements.Menus;
using Object = Il2CppSystem.Object;
using Player = VRC.Player;

namespace Brewchat.Game.Wrappers;

internal class GameUtils
{
    public class Gameplay
    {
        public struct RenderLayers
        {
            public static LayerMask InternalUI = LayerMask.NameToLayer("InternalUI");
            public static LayerMask Default = LayerMask.NameToLayer("Default");
        }

        public struct CallbackEvents
        {
            static QuickMenu? QuickMenu_Cache;
            static MainMenu? MainMenu_Cache;

            public static bool IsInQuickMenu
            {
                get
                {
                    if (QuickMenu_Cache is null) QuickMenu_Cache = UnityEngine.Object.FindObjectOfType<QuickMenu>();
                    return QuickMenu_Cache != null && QuickMenu_Cache.enabled;
                }
            }

            public static bool IsInMainMenu
            {
                get
                {
                    if (MainMenu_Cache is null) MainMenu_Cache = UnityEngine.Object.FindObjectOfType<MainMenu>();
                    return MainMenu_Cache != null && MainMenu_Cache.enabled;
                }
            }
        }

        public struct PhotonUtils
        {
            public enum Events : Byte
            {
                VoiceData = 1,
                Chatbox = 43,
                OnPlayerJoin = 253,
                OnPlayerLeft = 254,
                OnPlayerFailedJoin = byte.MaxValue,
            }

            public static void SendRaiseEvent(byte code, object parameter, SendOptions sendOptions)
            {
                var il2 =
                    Pasted.WTFBlaze.Utils.Managers.SerializationManager.FromManagedToIL2CPP<Il2CppSystem.Object>(
                        parameter);
                PhotonNetwork.RaiseEvent(code, il2, new()
                {
                    field_Public_ReceiverGroup_0 = ReceiverGroup.Others,
                    field_Public_EventCaching_0 = EventCaching.DoNotCache
                }, sendOptions);
            }

            public static string GetPhotonEventName(byte code)
            {
                return code switch
                {
                    0 => "CustomEvent",
                    43 => "Chatbox",
                    200 => "CustomEvent",
                    201 => "RaiseEvent",
                    202 => "SerializeView",
                    203 => "Instantiate",
                    204 => "Destroy",
                    205 => "RemoveCachedRPCs",
                    206 => "SendSerializeReliable",
                    207 => "DestroyPlayerObjects",
                    208 => "DestroyAll",
                    209 => "OwnershipRequest",
                    210 => "OwnershipTransfer",
                    251 => "Leave",
                    252 => "Join",
                    253 => "JoinLobby",
                    254 => "LeaveLobby",
                    255 => "JoinRandomFailed",
                    _ => "Unknown"
                };
            }
        }

        public struct NetworkUtils
        {

            private static int _eintMValue;

            public static void SendEmojiRPC(int emoji)
            {
                Il2CppSystem.Int32 eint = default(Il2CppSystem.Int32);
                _eintMValue = eint.m_value;
                _eintMValue = emoji;
                Il2CppSystem.Object @object = @eint.BoxIl2CppObject();
                Networking.RPC(0, PlayerModel.Get().gameObject, "SendEmojiRPC", new Il2CppSystem.Object[] { emoji });
            }
        }
    }

    public class Camera
    {



        public static UnityEngine.Camera? Get { get; set; } = UnityEngine.Camera.main;
        public static GameObject CameraObj => GameObject.Find("Camera");
    }

    public class PlayerModel
    {
        public static VRC.Player Get() => Player.prop_Player_0;

        public static VRC.Player? GrabPlayerFromDisplayName(string name)
        {

            if (string.IsNullOrEmpty(name))
                return null;
        
            foreach (var player in Matchmaking.GetAllPlayers)
            {
                if (player.APIUser.IsSelf || !player._vrcplayer || !player._vrcplayer._player)
                    continue;
            
                if (string.Equals(player.APIUser.displayName, name, StringComparison.OrdinalIgnoreCase))
                    return player._vrcplayer._player;
            }
    
            return null;
        }
        public static VRC.Player? GrabPlayerFromActorId(int id)
        {

            if (id.Equals(0)) return null;
        
            foreach (var player in Matchmaking.GetAllPlayers)
            {
                if (player.APIUser.IsSelf || !player._vrcplayer || !player._vrcplayer._player)
                    continue;
            
                if (int.Equals(player.VRCPlayerApi.playerId, id))
                    return player._vrcplayer._player;
            }
    
            return null;
        }
        public static class Motion
        {
            public struct Networking
            {
                public static VRCPlayerApi Run_Speed(float v = 2f)
                {
                    VRCPlayerApi? player = Get().VRCPlayerApi;
                    player.SetRunSpeed(v);
                    return player;
                }

                public static VRCPlayerApi Walk_Speed(float v = 2f)
                {
                    VRCPlayerApi? player = Get().VRCPlayerApi;
                    player.SetWalkSpeed(v);
                    return player;
                }

                public static VRCPlayerApi Jump_Impulse(float v = 3f)
                {
                    VRCPlayerApi? player = Get().VRCPlayerApi;
                    player.SetJumpImpulse(v);
                    return player;
                }

                public static VRCPlayerApi Velocity(Vector3 v)
                {
                    VRCPlayerApi? player = Get().VRCPlayerApi;
                    player.SetVelocity(v);
                    return player;

                }

                public static VRCPlayerApi Gravity(float v = 1f)
                {
                    VRCPlayerApi? player = Get().VRCPlayerApi;
                    player.SetGravityStrength(v);
                    return player;
                }
            }
        }

        public static class Avatar
        {
            public static void AvatarScale(float v = 4f) =>
                Get().VRCPlayerApi.SetAvatarEyeHeightByMeters(v);

            public static void AllowAvatarScaling(bool v = true) =>
                Get().VRCPlayerApi.SetManualAvatarScalingAllowed(v);

            public static void AvatarScaleMax(float v) =>
                Get().VRCPlayerApi.SetAvatarEyeHeightMaximumByMeters(v);

            public static void AvatarScaleMin(float v) =>
                Get().VRCPlayerApi.SetAvatarEyeHeightMinimumByMeters(v);

            public static void AllowScaling(bool v) =>
                Get().VRCPlayerApi.SetManualAvatarScalingAllowed(v);
        }

        public static class Matchmaking
        {
            public static VRC.Player? GetSelectedUser()
            {
                    return UnityEngine.Object.FindObjectOfType<SelectedUserMenuQM>().transform
                        .GetComponent<MonoBehaviourPublicTeObSlGrImSiObUnique>()
                        .field_Private_IUser_0.prop_String_1.GetPlayer()
                        ?._vrcplayer._player;
            }
            public static Il2CppSystem.Collections.Generic.List<VRC.Player> GetAllPlayers =>
                PlayerManager.prop_PlayerManager_0.PlayerList;
        }
    }

    public class World
    {
        public static bool IsWorldLoaded() =>
            RoomManager.prop_ApiWorldInstance_0 != null || RoomManager.currentApiWorld != null;
    }

    public static class UI
    {
      
        public class HUD
        {
            #region NOTIFICATION MANAGER

            #region HUD_V2
            
            private static Hud_UserEventCarousel? _carousel;
            private static float _lastCheckTime;
            private const float CacheTimeout = 30f;
            
            private static Hud_UserEventCarousel GetCarousel()
            {
                if (_carousel != null && Time.realtimeSinceStartup - _lastCheckTime < CacheTimeout)
                    return _carousel;

                _lastCheckTime = Time.realtimeSinceStartup;

                foreach (var carousel in UnityEngine.Object.FindObjectsOfType<Hud_UserEventCarousel>())
                {
                    if (carousel?.field_Private_List_1_MonoBehaviourPublicObteCaSiImSidiCaSiUnique_0.Count != 0)
                        return _carousel = carousel;
                }

                return _carousel;
            }

            public static void ToastV1(string content, float duration = 3f, Sprite? sprite = null)
            {
                try
                {
                    GetCarousel()
                        ?.Method_Private_Void_LocalizableString_Sprite_Single_0(content.Localize(), sprite, duration);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"[NotificationManager] Message error: {ex.Message}");
                }
            }

            public static void ToastV2(string? title, string? content = null, float duration = 2f,
                Sprite? sprite = null)
            {
                VRC.UI.Client.HUD.HudController
                    .field_Public_Static_HudController_0
                    .notification
                    ?.ShowNotification(sprite, title.Localize(), content.Localize(), duration);

            }

            #region Alert

            public enum AlertType
            {
                QuickMenu,
                MainMenu,
            }

            public static string Alert(string content, AlertType Alert)
            {
                if (Alert == AlertType.QuickMenu)
                {
                    UnityEngine.Object.FindObjectOfType<QuickMenu>().field_Private_ModalAlert_0
                        .Method_Public_Void_LocalizableString_PDM_0(content.Localize());
                }
                if (Alert == AlertType.MainMenu)
                {
                    UnityEngine.Object.FindObjectOfType<MainMenu>().field_Private_ModalAlert_0
                        .Method_Public_Void_LocalizableString_PDM_0(content.Localize());
                }
                return content;
            }

            #endregion
            #endregion

            #region HUD_V1

            public static void PopUp(string? title = null, string? content = null, float duration = 10f)
            {
                VRCUiManager.field_Private_Static_VRCUiManager_0.prop_VRCUiPopupManager_0
                    .Method_Public_Void_LocalizableString_LocalizableString_Single_0
                        (title.Localize(), content.Localize(), duration);
            }
            public static void PopUp2(string? title = null, string? content = null, string? uhz = null, Action func = null)
            {
                VRCUiManager.field_Private_Static_VRCUiManager_0.prop_VRCUiPopupManager_0
                    .Method_Public_Void_LocalizableString_LocalizableString_LocalizableString_Action_Action_1_VRCUiPopup_0
                        (title.Localize(), content.Localize(), uhz.Localize(),func);
            }

            public static void LoadingPopUp2(string? title, string? content, string? button_text, System.Action func,
                System.Action<VRCUiPopup> idk, float duration = 10f)
            {
                VRCUiManager.field_Private_Static_VRCUiManager_0.prop_VRCUiPopupManager_0
                    .Method_Public_Void_LocalizableString_LocalizableString_LocalizableString_Action_LocalizableString_Action_Action_1_VRCUiPopup_0
                    (title.Localize(), content.Localize(), button_text.Localize(), func, button_text.Localize(),
                        null, null);

            }

            public static void LoadingPopUp(string? title, string? content, string? button_text, System.Action func,
                System.Action<VRCUiPopup> idk, float duration = 10f)
            {
                VRCUiManager.field_Private_Static_VRCUiManager_0.prop_VRCUiPopupManager_0
                    .Method_Public_Void_LocalizableString_LocalizableString_LocalizableString_Action_Action_1_VRCUiPopup_0
                        (title.Localize(), content.Localize(), button_text.Localize(), func, idk);

            }

            #endregion

            #endregion
        }
    }

    public class VRChatUtils
    {
        public static class InputManager
        {
            public static VRCInput GetInput(string name) =>
                VRCInputManager.field_Private_Static_Dictionary_2_String_VRCInput_0[name];

            public static VRCInput GetInput(Get name) =>
                VRCInputManager.field_Private_Static_Dictionary_2_String_VRCInput_0[name.ToString()];

            public enum Get
            {
                Horizontial,
                Vertical,
            }
        }

        public static class VRCKeyboard
        {
            //vinyl thanks <3
            private static VRCInputField? inputfield;
            private static GameObject? Object;

            public enum KeyBoardType
            {
                Standard,
                Numeric,
                Search
            }

            public static void InputPopup(string Title, Action<string> EndString, Action<string>? RealTimeString = null,
                Action? OnClose = null, KeyBoardType keyBoardType = KeyBoardType.Standard,
                string Placeholder = "Enter Text", string OkButton = "OK", string CancelButton = "Cancel",
                bool MultiLine = true, int CharLimit = 0, bool KeepOpen = false, bool ReadOnly = false)
            {
                if (inputfield == null)
                {
                    Object = new GameObject($"[{Entry.NAME}] Keyboard");
                    Object.transform.parent = Templates._MM.Parent();
                    UnityEngine.Object.DontDestroyOnLoad(Object);
                    inputfield = Object.GetOrAddComponent<VRCInputField>();
                }

                KeyboardData keyboardData = new();
                KeyboardData keyboardData2 =
                    keyboardData
                        .Method_Public_KeyboardData_LocalizableString_LocalizableString_String_LocalizableString_LocalizableString_0(
                            LocalizableStringExtensions.Localize(Title),
                            LocalizableStringExtensions.Localize(Placeholder), "",
                            LocalizableStringExtensions.Localize(OkButton),
                            LocalizableStringExtensions.Localize(CancelButton));
                KeyboardData keyboardData3 =
                    keyboardData2.Method_Public_KeyboardData_Action_1_String_Action_1_String_Action_Boolean_PDM_0(
                        RealTimeString, EndString, OnClose, KeepOpen);
                KeyboardData keyboardData4 =
                    keyboardData3.Method_Public_KeyboardData_InputPopupType_Boolean_PDM_0((InputPopupType)keyBoardType,
                        param_2: true);
                KeyboardData keyboardData5 =
                    keyboardData4
                        .Method_Public_KeyboardData_InputType_ContentType_Int32_Boolean_Boolean_InterfacePublicAbstractBoStVoAc1VoAcSt1BoUnique_PDM_0(
                            TMP_InputField.InputType.Standard, TMP_InputField.ContentType.Standard, CharLimit,
                            MultiLine, ReadOnly);
                keyboardData5._isWorldKeyboard = true;
                inputfield._keyboardData = keyboardData5;
                inputfield.Method_Private_Void_0();
            }
        }

        public static class RPCs
        {

        }

        public static class Events
        {
            public static string SendEvent43_Chatbox(string msg)
            {
                GameUtils.Gameplay.PhotonUtils.SendRaiseEvent(43, msg, default);
                return msg;
            }
        }
    }

   
}


public static class Player_Extensions
{
    public static VRC.Player? GetPlayer(this string name)
    {

        if (string.IsNullOrEmpty(name))
            return null;
        
        foreach (var player in GameUtils.PlayerModel.Matchmaking.GetAllPlayers)
        {
            if (player.APIUser.IsSelf || player._vrcplayer == null || player._vrcplayer._player == null)
                continue;
            
            if (string.Equals(player.APIUser.displayName, name, StringComparison.OrdinalIgnoreCase))
                return player._vrcplayer._player;
        }
    
        return null;
    }
}