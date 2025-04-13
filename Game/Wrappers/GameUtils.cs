using UnityEngine;
using VRC.Localization;

namespace brewchat.hybridxcore.bep.Game.Wrappers;

internal class GameUtils
{
    public class PlayerModel
    {
        public static VRC.Player Get() => VRC.Player.prop_Player_0;

        public static class Motion
        {
            public static void Run_Speed(float v = 4) => Get().field_Private_VRCPlayerApi_0.SetRunSpeed(v);
            public static void Walk_Speed(float v = 2) => Get().field_Private_VRCPlayerApi_0.SetWalkSpeed(v);
            public static void Jump_Impulse(float v = 4) => Get().field_Private_VRCPlayerApi_0.SetJumpImpulse(v);
            public static void Velocity(Vector3 v) => Get().field_Private_VRCPlayerApi_0.SetVelocity(v);
        }
        public static class Avatar
        {
            public static void AvatarScale(float v = 4) => Get().field_Private_VRCPlayerApi_0.SetAvatarEyeHeightByMeters(v);
            public static void AllowAvatarScaling(bool v = true) => Get().field_Private_VRCPlayerApi_0.SetManualAvatarScalingAllowed(v);
            public static void AvatarScaleMax(float v) => Get().field_Private_VRCPlayerApi_0.SetAvatarEyeHeightMaximumByMeters(v);
            public static void AvatarScaleMin(float v) => Get().field_Private_VRCPlayerApi_0.SetAvatarEyeHeightMinimumByMeters(v);
        }
        public static class Matchmaking
        {
            public static Il2CppSystem.Collections.Generic.List<VRC.Player> GetPlayers => PlayerManager.prop_PlayerManager_0.field_Private_List_1_Player_0;
        }


    }
    public class HUD
    {
        public static void Notifcation(string? name, string? desc, Sprite? sprite)
        {
            VRC.UI.Client.HUD.HudController
                .field_Public_Static_HudController_0
                .notification
                .Method_Public_Void_Sprite_LocalizableString_LocalizableString_Single_Object1PublicTYBoTYUnique_1_Boolean_0
                (default, name.Localize(), desc.Localize());
        }

    }
}



