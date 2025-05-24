using BlanketSDK.Utilities.VRC.QM.Widgets;
using Brewchat.Wrappers;
using UnityEngine;

namespace Brewchat.Cheat;

public class Widgets
{
    public static class QM
    {
        internal static VRCWidgets cameramode_widget { get; set; }
        internal static VRCWidgets ghostmode_widget { get; set; }
        public static void Initalize()
        {
            cameramode_widget = VRCWidgets.Create(UICore.Sprites.Video(), "External Camera Mode", color: Color.white);
            ghostmode_widget = VRCWidgets.Create(UICore.Sprites.Ghost(), "Ghost Mode", color: Color.white);
        }
    }
}