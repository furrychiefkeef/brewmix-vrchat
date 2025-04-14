using Brewchat.Game.Wrappers;
using VRC.UI.Core.Styles;

namespace Brewchat.Cheat.Modules
{
    internal class PlayerMovement
    {
        static bool isVR;
        public static void Fly(bool s)
        {
            var player = GameUtils.PlayerModel.Get();
            if (!player && !RoomManager.field_Internal_Static_ApiWorld_0.Method_Public_Static_Boolean_Object_PDM_0()) return;
            else
            {

                switch (player.field_Private_Player_0.field_Public_Player_0.prop_VRCPlayerApi_0.IsUserInVR())
                {
                    case true:

                    case false:
                        break;
                }
            }
        }
    }
}
