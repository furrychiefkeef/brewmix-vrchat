using BlanketSDK.Core;
using ExitGames.Client.Photon;
using Photon.Pun;
using TMPro;
using UnityEngine;
using VRC;
using VRC.Animation;
using VRC.Core;
using VRC.DataModel;
using VRC.Localization;
using VRC.Networking;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Components;
using VRC.SDKBase;

using VRC.Udon;
using VRC.Udon.Common.Interfaces;
using VRC.UI;
using VRC.UI.Client;
using VRC.UI.Client.HUD;
using VRC.UI.Elements;
using VRC.UI.Elements.Controls;
using Object = Il2CppSystem.Object;

namespace Brewchat.Game.Wrappers
{
    internal class GameUtils
    {
        // =============================================================================
        // GAMEPLAY UTILITIES
        // =============================================================================
        
        public class Gameplay
        {
            public static class Matchmaking
            {
                
                public static VRC.Player GetSelectedUser() => UnityEngine.Object.FindObjectOfType<QuickMenu>().field_Private_IUser_0.prop_String_1.getPlayer()?._vrcplayer._player;
                   
                
                public static Il2CppSystem.Collections.Generic.List<VRC.Player> GetPlayers => 
                    PlayerManager.prop_PlayerManager_0.PlayerList;
            }

            public struct RenderLayers
            {
                public static LayerMask InternalUI = LayerMask.NameToLayer("InternalUI");
                public static LayerMask Default = LayerMask.NameToLayer("Default");
            }

            public struct CallbackEvents
            {
                static QuickMenu? _QuickMenu;
                static MainMenu? _MainMenu;

                public static Func<bool> IsInQuickMenu
                {
                    get
                    {
                        if (_QuickMenu is null) 
                            _QuickMenu = UnityEngine.Object.FindObjectOfType<QuickMenu>();
                        return () => _QuickMenu != null && _QuickMenu.enabled;
                    }
                }

                public static Func<bool> IsInMainMenu
                {
                    get
                    {
                        if (_MainMenu is null) 
                            _MainMenu = UnityEngine.Object.FindObjectOfType<MainMenu>();
                        return () => _MainMenu != null && _MainMenu.enabled;
                    }
                }
            }

            public struct NetworkUtils
            {
                public struct Player
                {
                    public static void SpawnEmoji(string id) { }
                    public static void SpawnPrint(string id) { }
                    public static void SpawnPortal(string id) { }
                    public static void SpawnDrone(int actor) { }
                }

                private static int _eintMValue;

                /*public static void SendEmojiRPC(int emoji)
                {
                    Il2CppSystem.Int32 eint = default(Il2CppSystem.Int32);
                    _eintMValue = eint.m_value;
                    _eintMValue = emoji;
                    Il2CppSystem.Object @object = @eint.BoxIl2CppObject();
                    Networking.RPC(0, PlayerModel.GetLocalPlayer().gameObject, "SendEmojiRPC", new Il2CppSystem.Object[] { (Object)emoji });
                }*/

                public static string Join(string worldId)
                {
                    Networking.GoToRoom(worldId);
                    return worldId;
                }
            }

            public struct PhotonUtils
            {
                public enum Events : byte
                {
                    VoiceData = 1,
                    MovementData = 12,
                    MovementData2 = 13,
                    EV = 11,
                    PlayerPrefab = 202,
                    Chatbox = 43,
                    Social = 74,
                    OnPlayerJoin = 253,
                    OnPlayerLeft = 254,
                    OnPlayerFailedJoin = byte.MaxValue
                }

                public static void SendRaiseEvent(byte code, object payload, SendOptions sendOptions)
                {
                    var IL2CPP = EngineUtils.iL2CPPWrapper.Serialization.FromManagedToIL2CPP<Il2CppSystem.Object>(payload);
                    PhotonNetwork.Method_Public_Static_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0(code, IL2CPP, new()
                    {
                        field_Public_ReceiverGroup_0 = Photon.Realtime.ReceiverGroup.Others,
                        field_Public_EventCaching_0 = Photon.Realtime.EventCaching.DoNotCache
                    }, sendOptions);
                }
            }
        }

        // =============================================================================
        // CAMERA UTILITIES
        // =============================================================================
        
        public class Camera
        {
            public static UnityEngine.Camera Get { get; set; } = UnityEngine.Camera.main;
            public static GameObject CameraObj {
                get
                {
                    if (CameraObj is null)
                        return GameObject.Find("Camera");
                    return CameraObj;
                }
            }
        }

        // =============================================================================
        // PLAYER MODEL UTILITIES
        // =============================================================================
        
        public class PlayerModel
        {
            public static VRCPlayer VRCPlayerEntity() => VRCPlayer.field_Internal_Static_VRCPlayer_0;
            public static VRC.Player LocalEntity() => VRC.Player.prop_Player_0;
            public static VRC.Player Get => VRC.Player.prop_Player_0;

            public struct _components
            {
                public static VRCMotionState _motion = LocalEntity().GetComponent<VRCMotionState>();
                public static FlatBufferNetworkSerializer _serializer = LocalEntity().GetComponent<FlatBufferNetworkSerializer>();
                /*public static SyncPhysics _SyncPhysics = VRC_LocalPlayer().GetComponent<SyncPhysics>();*/
            }

            public static class Motion
            {
                public struct Networking
                {
                    public static class World
                    {
                        public static void SetOwnerAndGivePickupLocalEntity(VRCPickup pickup)
                        {
                            var Player = LocalEntity().VRCPlayerApi;
                            Player.TakeOwnership(pickup.gameObject);
                            pickup.gameObject.transform.position = Player.gameObject.transform.position + new Vector3(0.0f, 0.1f, 0f);
                        }
                    }
                    public static VRCPlayerApi Run_Speed(float v = 2f)
                    {
                        VRCPlayerApi? player = LocalEntity().VRCPlayerApi;
                        player.SetRunSpeed(v);
                        return player;
                    }

                    public static VRCPlayerApi Walk_Speed(float v = 2f)
                    {
                        VRCPlayerApi? player = LocalEntity().VRCPlayerApi;
                        player.SetWalkSpeed(v);
                        return player;
                    }
                    public static VRCPlayerApi Strafe_Speed(float v = 3f)
                    {
                        VRCPlayerApi? player = LocalEntity().VRCPlayerApi;
                        player.SetStrafeSpeed(v);
                        return player;
                    }
                    public static VRCPlayerApi Jump_Impulse(float v = 3f)
                    {
                        VRCPlayerApi? player = LocalEntity().VRCPlayerApi;
                        player.SetJumpImpulse(v);
                        return player;
                    }

                    public static VRCPlayerApi Velocity(Vector3 v)
                    {
                        VRCPlayerApi? player = LocalEntity().VRCPlayerApi;
                        player.SetVelocity(v);
                        return player;
                    }
                   

                    public static VRCPlayerApi Gravity(float v = 1f)
                    {
                        VRCPlayerApi? player = LocalEntity().VRCPlayerApi;
                        player.SetGravityStrength(v);
                        return player;
                    }
                }
            }

            public static class Avatar
            {
                public static void CloneAvatar(VRC.Player player, bool keepParameters)
                {
        
                    Transform? head = player._vrcplayer.VRCPlayerApi.GetBoneTransform(HumanBodyBones.Head);
                    head.localScale = new Vector3(1f, 1f, 1f);
                    
                    if (player == null)
                    {
                        var obj = UnityEngine.Object.Instantiate(player._vrcplayer.field_Private_VRCAvatarManager_0.field_Private_GameObject_0);
                        if (keepParameters)
                        {
                            var sourceDescriptor = player._vrcplayer.field_Private_VRCAvatarManager_0.field_Private_GameObject_0.GetComponent<VRCAvatarDescriptor>();
                            var targetDescriptor = obj.GetComponent<VRCAvatarDescriptor>();
                            
                            if (sourceDescriptor != null && targetDescriptor != null)
                            {
                                // Copy all the avatar descriptor properties
                                targetDescriptor.expressionParameters = sourceDescriptor.expressionParameters;
                                targetDescriptor.expressionsMenu = sourceDescriptor.expressionsMenu;
                                targetDescriptor.customExpressions = sourceDescriptor.customExpressions;
                                targetDescriptor.ScaleIPD = sourceDescriptor.ScaleIPD;
                                targetDescriptor.lipSync = sourceDescriptor.lipSync;
                                targetDescriptor.lipSyncJawBone = sourceDescriptor.lipSyncJawBone;
                                targetDescriptor.VisemeSkinnedMesh = sourceDescriptor.VisemeSkinnedMesh;
                                targetDescriptor.VisemeBlendShapes = sourceDescriptor.VisemeBlendShapes;
                                targetDescriptor.portraitCameraPositionOffset = sourceDescriptor.portraitCameraPositionOffset;
                                targetDescriptor.portraitCameraRotationOffset = sourceDescriptor.portraitCameraRotationOffset;
                                // Add any other properties you need to copy
                            }
                        }
                        obj.transform.position = player.transform.position;
                    }
                }

                public static ApiAvatar SetAvatar(string id)
                {
                    ApiAvatar avatar = new() { id = id };
                    PageAvatar.Method_Public_Static_Void_ApiAvatar_String_0(avatar, "AvatarMenu");
                    return avatar;
                }

                public static ApiAvatar GetAvatar() => LocalEntity().prop_ApiAvatar_0;

                public static void AvatarScale(float v = 4f) =>
                    LocalEntity().VRCPlayerApi.SetAvatarEyeHeightByMeters(v);

                public static void AllowAvatarScaling(bool v = true) =>
                    LocalEntity().VRCPlayerApi.SetManualAvatarScalingAllowed(v);

                public static void AvatarScaleMax(float v) =>
                    LocalEntity().VRCPlayerApi.SetAvatarEyeHeightMaximumByMeters(v);

                public static void AvatarScaleMin(float v) =>
                    LocalEntity().VRCPlayerApi.SetAvatarEyeHeightMinimumByMeters(v);
            }

            public static class Matchmaking
            {
                public static Il2CppSystem.Collections.Generic.List<VRC.Player> GetPlayers => 
                    PlayerManager.prop_PlayerManager_0.PlayerList;
            }
        }

        // =============================================================================
        // WORLD UTILITIES
        // =============================================================================
        
        public class World
        {
            public static bool IsLoaded() => RoomManager.prop_ApiWorldInstance_0 != null || RoomManager.prop_ApiWorldInstance_1 != null || RoomManager.currentApiWorld != null;
            public static class WorldData
            {
                public static string current_worldID  => RoomManager.field_Private_Static_ApiWorldInstance_1.id;
                public static string current_worldName => RoomManager.field_Private_Static_ApiWorldInstance_1.name;
                public static string current_intanceID => RoomManager.field_Private_Static_ApiWorldInstance_1.instanceId;
            }
           

            public class SDK2
            {
                public static VRC_Trigger? InteractWithTrigger(string triggerobj)
                {
                    foreach (VRCSDK2.VRC_Trigger? trigger in UnityEngine.Object.FindObjectsOfType<VRCSDK2.VRC_Trigger>())
                    {
                        if (trigger.gameObject.name.Contains(triggerobj))
                        {
                            trigger.Interact();
                            if (!Entry.config.Advanced.DebugMode)
                                UI.HUD.ToastV1($"Sent Trigger {trigger.gameObject.name}");
                            return trigger;
                        }
                        return null;
                    }
                    return null;
                }
            }

            public class SDK3
            {
                public struct Udon
                {
                public static class Pickups
                {
                    static VRCPickup _lastPickup;public static VRCPickup? GetPickup(string name)
                    {
                        var pickups = UnityEngine.Object.FindObjectsOfType<VRCPickup>();
                        
                        // First pass - exact match
                        foreach (VRCPickup pickup in pickups)
                        {
                            if (string.Equals(pickup.name, name, StringComparison.OrdinalIgnoreCase))
                            {
                                _lastPickup = pickup;
                                UI.HUD.ToastV1($"Found: {_lastPickup.name}");
                                return pickup;
                            }
                        }
                        
                        // Second pass - contains match
                        foreach (VRCPickup pickup in pickups)
                        {
                            if (pickup.name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                _lastPickup = pickup;
                                UI.HUD.ToastV1($"Found: {_lastPickup.name}");
                                return pickup;
                            }
                        }
                        
                        UI.HUD.ToastV1($"Pickup Not Found");
                        return null;
                    }
                }
                public static void LoadCustomEvent(string _event)
                {
                    foreach (UdonBehaviour udon in UnityEngine.Object.FindObjectsOfType<UdonBehaviour>())
                    {
                        if (udon._eventTable.ContainsKey(_event))
                        {
                            udon.SendCustomEvent(_event);
                        }
                    }
                }
                public static void LoadCustomNetworkEvent(NetworkEventTarget target, string _event)
                {
                    foreach (UdonBehaviour udon in UnityEngine.Object.FindObjectsOfType<UdonBehaviour>())
                    {
                        if (udon._eventTable.ContainsKey(_event))
                        {
                            udon.SendCustomNetworkEvent(target, _event);
                        }
                    }
                }
              }
            }
        }

        // =============================================================================
        // GAME SERVER UTILITIES
        // =============================================================================
        
        public class GameServer
        {
            public static VRC.Player? getPlayer_displayName(string name)
            {
                if (string.IsNullOrEmpty(name)) return null;

                foreach (var player in PlayerModel.Matchmaking.GetPlayers)
                {
                    if (player.APIUser.IsSelf || !player._vrcplayer || !player._vrcplayer._player)
                        continue;

                    if (string.Equals(player.APIUser.displayName, name, StringComparison.OrdinalIgnoreCase))
                        return player._vrcplayer._player;
                }

                return null;
            }
            public static VRC.Player? getPlayer_networkId(int id)
            {
                if (id < 0 ) return null;

                foreach (var player in PlayerModel.Matchmaking.GetPlayers)
                {
                    if (player.APIUser.IsSelf || !player._vrcplayer || !player._vrcplayer._player)
                        continue;

                    if (int.Equals(player.VRCPlayerApi.playerId, id))
                        return player._vrcplayer._player;
                }

                return null;
            }
            public static VRC.Player? getPlayer_userId(string id)
            {
                if (string.IsNullOrEmpty(id)) return null;

                foreach (var player in PlayerModel.Matchmaking.GetPlayers)
                {
                    if (player.APIUser.IsSelf || !player._vrcplayer || !player._vrcplayer._player)
                        continue;

                    if (string.Equals(player.APIUser.id, id, StringComparison.OrdinalIgnoreCase))
                        return player._vrcplayer._player;
                }

                return null;
            }
            /*public static VRC.Player? GetPlayer(int id)
            {
                foreach (var player in PlayerModel.Matchmaking.GetPlayers)
                {
                    if (player.VRCPlayerApi.playerId == id)
                    {
                        return player;
                    }
                }

                return null;
            }*/
        }

        // =============================================================================
        // UI UTILITIES
        // =============================================================================
        
        public static class UI
        {
            public class HUD
            {
                #region NOTIFICATION MANAGER V2

                  
                /*private static Hud_UserEventCarousel? _carousel;
                private static float _lastCheckTime;
                private const float CacheTimeout = 30f;
            
                private static Hud_UserEventCarousel GetCarousel()
                {
                    if (_carousel != null && Time.realtimeSinceStartup - _lastCheckTime < CacheTimeout)
                        return _carousel;

                    _lastCheckTime = Time.realtimeSinceStartup;

                    foreach (var carousel in Resources.FindObjectsOfTypeAll<Hud_UserEventCarousel>())
                    {
                        if (carousel?.field_Private_List_1_MonoBehaviourPublicObteCaSiImSidiCaSiUnique_0.Count != 0)
                            return _carousel = carousel;
                    }

                    return _carousel;
                }

                public static void ToastV1(string content, float duration = 3f, Sprite? sprite = null)
                {
                    GetCarousel().Method_Private_Void_LocalizableString_Sprite_Single_1(content.Localizable(), sprite, duration);
                }*/
                public static void ToastV1(string content, float duration = 3f, bool isAudible = true, Sprite? sprite = null)
                {
                    if (isAudible)  VRCUiSoundPlayer.Method_Public_Static_Void_AudioClip_Single_0(HudController.field_Public_Static_HudController_0.notificationSound, 5f);
                    
                    HudController.field_Public_Static_HudController_0
                        .userEventCarousel
                        .Method_Private_Void_LocalizableString_Sprite_Single_0(content.Localizable(), sprite, duration);
                }

                public static void ToastV2(string? title, string? content = null, float duration = 2f, bool isAudible = true, Sprite? sprite = default)
                {
                    if (isAudible) VRCUiSoundPlayer.Method_Public_Static_Void_AudioClip_Single_0(HudController.field_Public_Static_HudController_0.notificationSound, 5f);
                    
                    HudController.field_Public_Static_HudController_0
                     .notification
                     ?.Method_Public_Void_Sprite_LocalizableString_LocalizableString_Single_Object1PublicTBoTUnique_1_Boolean_0(sprite, title.Localizable(), content.Localizable(), duration);
                }

                public static void SendLocalNotifcation(string name, string usr_id, string type, string _4 = null)
                {
                    MonoBehaviourPublicObDi2StObDi2So1NoUnique.field_Private_Static_MonoBehaviourPublicObDi2StObDi2So1NoUnique_0
                        .Method_Public_Notification_LocalizableString_LocalizableString_String_String_ArrayOf_Dictionary_2_String_Object_Boolean_Dictionary_2_String_String_0
                            (name.Localizable(), 
                                usr_id.Localizable(), 
                                type, _4);
                }
                #endregion

                #region ALERT SYSTEM

                public static string Alert(string content)
                {
                    VRCUIManager.prop_VRCUIManager_0.Method_Public_Virtual_Final_New_Void_LocalizableString_0(content.Localizable());
                    return content;
                }

                #endregion

                #region POPUP SYSTEM V1

                public static void PopUp(string title = null, string content = null, float duration = 10f)
                {
                    VRCUiManager.prop_VRCUiManager_0.prop_VRCUiPopupManager_0
                        .Method_Public_Void_LocalizableString_LocalizableString_Single_0
                            (title.Localizable(), content.Localizable(), duration);
                }

                public static void PopUp2(string title = null, string content = null, string? uhz = null, Action func = null)
                {
                    VRCUiManager.prop_VRCUiManager_0.prop_VRCUiPopupManager_0
                        .Method_Public_Void_LocalizableString_LocalizableString_LocalizableString_Action_Action_1_VRCUiPopup_0
                            (title.Localizable(), content.Localizable(), uhz.Localizable(), func);
                }

                public static void LoadingPopUp2(string? title, string? content, string? button_text, System.Action func,
                    System.Action<VRCUiPopup> idk, float duration = 10f)
                {
                    VRCUiManager.field_Private_Static_VRCUiManager_0.prop_VRCUiPopupManager_0
                        .Method_Public_Void_LocalizableString_LocalizableString_LocalizableString_Action_LocalizableString_Action_Action_1_VRCUiPopup_0
                        (title.Localizable(), content.Localizable(), button_text.Localizable(), func, button_text.Localizable(),
                            null, null);
                }

                public static void LoadingPopUp(string? title, string? content, string? button_text, System.Action func,
                    System.Action<VRCUiPopup> idk, float duration = 10f)
                {
                    VRCUiManager.field_Private_Static_VRCUiManager_0.prop_VRCUiPopupManager_0
                        .Method_Public_Void_LocalizableString_LocalizableString_LocalizableString_Action_Action_1_VRCUiPopup_0
                            (title.Localizable(), content.Localizable(), button_text.Localizable(), func, idk);
                }

                #endregion
            }
        }

        // =============================================================================
        // VRCHAT UTILITIES
        // =============================================================================
        
        public class VRChatUtils
        {
            public static class InputManager
            {
                public static VRCInput GetInput(string name) => 
                    VRCInputManager.field_Private_Static_Dictionary_2_String_VRCInput_0[name];
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
                        Object = new GameObject($"[{Entry.GetEntry().modInfo._currentName}] Keyboard");
                        Object.transform.parent = IMainMenuElements.MMParent();
                        UnityEngine.Object.DontDestroyOnLoad(Object);
                        inputfield = Object.AddComponent<VRCInputField>();
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

            public static class Events
            {
                public static string eventCode43_Chatbox(string msg)
                {
                    GameUtils.Gameplay.PhotonUtils.SendRaiseEvent((byte)Gameplay.PhotonUtils.Events.Chatbox, msg, default);
                    return msg;
                }
            }
        }
    }

    // =============================================================================
    // EXTENSION METHODS
    // =============================================================================
    
    public static class PlayerExtensions
    {
        public static Color GetTrustColor(this VRC.Player player) => VRCPlayer.Method_Public_Static_Color_APIUser_0(player.APIUser);
        public static string? GetTrustRank(this VRC.Player player) => VRCPlayer.Method_Public_Static_LocalizableString_APIUser_0(player.APIUser)._localizationKey;
        
        public static string IsFriend(this VRC.Player player)
        {
            return player.APIUser.isFriend ? "Friend" : null;
        }

        public static string IsAdult(this VRC.Player player)
        {
            return player.APIUser.ageVerified ? "+18" : "-18";
        }

        public static VRC.Player? getPlayer(this string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            foreach (var player in GameUtils.PlayerModel.Matchmaking.GetPlayers)
            {
                if (player.APIUser.IsSelf || player._vrcplayer == null || player._vrcplayer._player == null)
                    continue;

                if (string.Equals(player.APIUser.displayName, name, StringComparison.OrdinalIgnoreCase))
                    return player._vrcplayer._player;
            }

            return null;
        }
        public static VRC.Player? getPlayer(this int networkId)
        {
            foreach (var player in GameUtils.PlayerModel.Matchmaking.GetPlayers)
            {
                if (player.VRCPlayerApi.playerId == networkId) 
                    return player;
            }
            return null;
        }
    }
}