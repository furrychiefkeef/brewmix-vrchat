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
        internal static VRCWidgets global_broadcaster { get; set; }
        public static void Initalize()
        {
             cameramode_widget = VRCWidgets.Create(AstriekLib.Sprites.Video(), "External Camera Mode", color: Color.white);
             ghostmode_widget = VRCWidgets.Create(AstriekLib.Sprites.Video(false), "Ghost Mode", color: Color.white);
             global_broadcaster = VRCWidgets.Create(AstriekLib.Sprites.Video(false), "Global Broadcaster", color: Color.red);
        }
    }
}