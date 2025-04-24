using Brewview;
using ExitGames.Client.Photon;
using Il2CppInterop.Runtime;
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
using Object = Il2CppSystem.Object;

namespace Brewchat.Game.Wrappers;

internal class GameUtils
{
    public class Gameplay
    {
        public struct  CallbackEvents
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
        public struct  PhotonUtils
        {
            public static void SendRaiseEvent(byte eventCode, object parameter, SendOptions sendOptions)
            {
                Il2CppSystem.Object il2cpp_object = SerializationManager.FromManagedToIL2CPP<Il2CppSystem.Object>(parameter);
                VRCNetworkingClient.field_Internal_Static_VRCNetworkingClient_0.Method_Public_Virtual_New_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0(eventCode, il2cpp_object ,new()
                {
                    field_Public_ReceiverGroup_0 = ReceiverGroup.All,
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
    }

    public class CameraUtils
    {   
        public static Camera? camera { get; set; } = Camera.main;
        public static GameObject CameraObj => GameObject.Find("Camera (eye)");
    }

    public class PlayerModel
    {
        public static VRC.Player Get() => VRC.Player.prop_Player_0;

        public static class Motion
        {
            public static VRCPlayerApi Run_Speed(float v = 2f)
            {
                VRCPlayerApi? player = Get().field_Private_VRCPlayerApi_0;
                player.SetRunSpeed(v);
                return player;
            }

            public static VRCPlayerApi Walk_Speed(float v = 2f)
            {
                VRCPlayerApi? player = Get().field_Private_VRCPlayerApi_0;
                player.SetWalkSpeed(v);
                return player;
            }

            public static VRCPlayerApi Jump_Impulse(float v = 3f)
            {
                VRCPlayerApi? player = Get().field_Private_VRCPlayerApi_0;
                player.SetJumpImpulse(v);
                return player;
            }

            public static VRCPlayerApi Velocity(Vector3 v)
            {
                VRCPlayerApi? player = Get().field_Private_VRCPlayerApi_0;
                player.SetVelocity(v);
                return player;

            }

            public static VRCPlayerApi Gravity(float v = 1f)
            {
                VRCPlayerApi? player = Get().field_Private_VRCPlayerApi_0;
                player.SetGravityStrength(v);
                return player;
            }
        }

        public static class Avatar
        {
            public static void AvatarScale(float v = 4f) =>
                Get().field_Private_VRCPlayerApi_0.SetAvatarEyeHeightByMeters(v);

            public static void AllowAvatarScaling(bool v = true) =>
                Get().field_Private_VRCPlayerApi_0.SetManualAvatarScalingAllowed(v);

            public static void AvatarScaleMax(float v) =>
                Get().field_Private_VRCPlayerApi_0.SetAvatarEyeHeightMaximumByMeters(v);

            public static void AvatarScaleMin(float v) =>
                Get().field_Private_VRCPlayerApi_0.SetAvatarEyeHeightMinimumByMeters(v);
        }

        public static class Matchmaking
        {
            public static Il2CppSystem.Collections.Generic.List<VRC.Player> GetPlayers =>
                PlayerManager.prop_PlayerManager_0.field_Private_List_1_Player_0;
        }


    }

    public class World
    {
        public static bool IsWorldLoaded() => RoomManager.prop_ApiWorldInstance_0 != null ||
RoomManager.field_Internal_Static_ApiWorld_0 != null;
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
                    ?.Method_Public_Void_Sprite_LocalizableString_LocalizableString_Single_Object1PublicTYBoTYUnique_1_Boolean_0
                        (sprite, title.Localize(), content.Localize(), duration);

                #region HUD_V1

            }

            #endregion

            public static void PopUp(string? title = null, string? content = null, float duration = 10f)
            {
                VRCUiManager.field_Private_Static_VRCUiManager_0.prop_VRCUiPopupManager_0
                    .Method_Public_Void_LocalizableString_LocalizableString_Single_0
                        (title.Localize(), content.Localize(), duration);
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

            public static VRCInput GetInput(Enum name) =>
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
            { Standard, Numeric, Search }

            public static void InputPopup(string Title, Action<string> EndString, Action<string>? RealTimeString = null, Action? OnClose = null, KeyBoardType keyBoardType = KeyBoardType.Standard, string Placeholder = "Enter Text", string OkButton = "OK", string CancelButton = "Cancel", bool MultiLine = true, int CharLimit = 0, bool KeepOpen = false, bool ReadOnly = false)
            {
                if (inputfield == null)
                {
                    Object = new GameObject("[Saturn] Keyboard");
                    UnityEngine.Object.DontDestroyOnLoad(Object);
                    inputfield = Object.AddComponent<VRCInputField>();
                }

                KeyboardData keyboardData = new();
                KeyboardData keyboardData2 = keyboardData.Method_Public_KeyboardData_LocalizableString_LocalizableString_String_LocalizableString_LocalizableString_0(LocalizableStringExtensions.Localize(Title), LocalizableStringExtensions.Localize(Placeholder), "", LocalizableStringExtensions.Localize(OkButton), LocalizableStringExtensions.Localize(CancelButton));
                KeyboardData keyboardData3 = keyboardData2.Method_Public_KeyboardData_Action_1_String_Action_1_String_Action_Boolean_PDM_0(RealTimeString, EndString, OnClose, KeepOpen);
                KeyboardData keyboardData4 = keyboardData3.Method_Public_KeyboardData_InputPopupType_Boolean_PDM_0((InputPopupType)keyBoardType, param_2: true);
                KeyboardData keyboardData5 = keyboardData4.Method_Public_KeyboardData_InputType_ContentType_Int32_Boolean_Boolean_InterfacePublicAbstractBoStVoAc1VoAcSt1BoUnique_PDM_0(TMP_InputField.InputType.Standard, TMP_InputField.ContentType.Standard, CharLimit, MultiLine, ReadOnly);
                keyboardData5._isWorldKeyboard = true;
                inputfield._keyboardData = keyboardData5;
                inputfield.Method_Private_Void_0();
            }
        }
    }
    
}