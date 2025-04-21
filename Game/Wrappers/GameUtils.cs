using UnityEngine;
using VRC;
using VRC.Localization;
using VRC.SDKBase;
using VRC.UI.Client;
using VRC.UI.Client.HUD;
using VRC.UI.Elements;

namespace Brewchat.Game.Wrappers;

internal class GameUtils
{
    public class Gameplay
    {
    public static class Events
    {
        static Component? cache;
        public static bool IsInMainMenu
        {
            get
            {
                if (cache is null) cache = UnityEngine.Object.FindObjectOfType<MainMenu>();
                return cache != null && cacheenabled;
            }
        }
        public static bool IsInQuickMenu
        {
            get
            {
                if (cache is null) cache = UnityEngine.Object.FindObjectOfType<QuickMenu>();
                return cache != null && cache.enabled;
            }
        }
    }
    public class GameCamera
    {
        public static Camera camera => Camera.main;
    }
    public class PlayerModel
    {
        public static VRC.Player Get() => VRC.Player.prop_Player_0;

        public static class Motion
        {
            public static VRCPlayerApi Run_Speed(float v = 2f)
            {
                VRCPlayerApi? player = Get().field_Private_VRCPlayerApi_0;
                player.SetRunSpeed(v); return player;
            }
            public static VRCPlayerApi Walk_Speed(float v = 2f)
            {
                VRCPlayerApi? player = Get().field_Private_VRCPlayerApi_0;
                player.SetWalkSpeed(v); return player;
            }

            public static VRCPlayerApi Jump_Impulse(float v = 3f)
            {
                VRCPlayerApi? player = Get().field_Private_VRCPlayerApi_0;
                player.SetJumpImpulse(v); return player;
            }

            public static VRCPlayerApi Velocity(Vector3 v)
            {
                VRCPlayerApi? player = Get().field_Private_VRCPlayerApi_0;
                player.SetVelocity(v); return player;
            }
        }
        public static class Avatar
        {
            public static void AvatarScale(float v = 4f) => Get().field_Private_VRCPlayerApi_0.SetAvatarEyeHeightByMeters(v);
            public static void AllowAvatarScaling(bool v = true) => Get().field_Private_VRCPlayerApi_0.SetManualAvatarScalingAllowed(v);
            public static void AvatarScaleMax(float v) => Get().field_Private_VRCPlayerApi_0.SetAvatarEyeHeightMaximumByMeters(v);
            public static void AvatarScaleMin(float v) => Get().field_Private_VRCPlayerApi_0.SetAvatarEyeHeightMinimumByMeters(v);
        }
        public static class Matchmaking
        {
            public static Il2CppSystem.Collections.Generic.List<VRC.Player> GetPlayers => PlayerManager.prop_PlayerManager_0.field_Private_List_1_Player_0;
        }


    }
    public class World  
    {
        public static bool IsWorldLoaded() => RoomManager.prop_ApiWorldInstance_0 != null || RoomManager.field_Internal_Static_ApiWorld_0 != null;
    }

    public class UI
    {
        public class Shaders
        {
            public class Parallax
            {
                /*public static Material? ThreeLayer(this Texture texture)
                {
                    Material material
                    rm_m.SetTexture("_ParallaxBackground", texture);
                    rm_m.SetTexture("_ParallaxMidground", texture);
                    rm_m.SetTexture("_ParallaxForeground", texture);
                }*/
            }
        }
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
                GetCarousel()?.Method_Private_Void_LocalizableString_Sprite_Single_0(content.Localize(), sprite, duration);
            }
            catch (Exception ex) { Debug.LogError($"[NotificationManager] Message error: {ex.Message}"); }
        }
        public static void ToastV2(string? title, string? content = null, float duration = 2f, Sprite? sprite = null)
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
       
        public static void LoadingPopUp2(string? title, string? content, string? button_text, System.Action func, System.Action<VRCUiPopup> idk, float duration = 10f)
        {
            VRCUiManager.field_Private_Static_VRCUiManager_0.prop_VRCUiPopupManager_0
                .Method_Public_Void_LocalizableString_LocalizableString_LocalizableString_Action_LocalizableString_Action_Action_1_VRCUiPopup_0
                    (title.Localize(), content.Localize(), button_text.Localize(), func, button_text.Localize(), null,null);

        }

        public static void LoadingPopUp(string? title, string? content, string? button_text, System.Action func, System.Action<VRCUiPopup> idk, float duration = 10f)
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
            public static VRCInput GetInput(string name) => VRCInputManager.field_Private_Static_Dictionary_2_String_VRCInput_0[name];
            public static VRCInput GetInput(Enum name) => VRCInputManager.field_Private_Static_Dictionary_2_String_VRCInput_0[name.ToString()];
            
            public enum Get
            {
                Horizontial,
                Vertical,
            }
        } 
    }
}



