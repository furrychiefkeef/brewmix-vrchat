using UnityEngine;
using VRC.Localization;

namespace brewchat.hybridxcore.bep.Game.Wrappers;

internal class GameWrapper
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
