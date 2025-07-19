using BlanketSDK.Utilities.VRC.QM.Widgets;
using Brewchat.Game.Wrappers;
using UnityEngine;

namespace Brewchat.Cheat;

public class Widgets
{
    public static class QM
    {
        internal static VRCWidgets? cameramode_widget { get; set; }
        internal static VRCWidgets? ghostmode_widget { get; set; }
        internal static VRCWidgets? global_broadcaster { get; set; }
        public static void Initalize()
        {
            var current =  EngineUtils.Assets.INTERNAL_Texture(autumn.ml.Properties.Resources.action_menu_joystick).ToSprite();
            cameramode_widget = VRCWidgets.Create(current , "External Camera Mode", color: Color.red);
            // ghostmode_widget = VRCWidgets.Create( null,"Ghost Mode", color: Color.white);
            // global_broadcaster = VRCWidgets.Create(null, "Global Broadcaster", color: Color.red);
        }
    }}