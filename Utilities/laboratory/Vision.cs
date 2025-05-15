using System.Runtime.InteropServices;
using Brewchat.Game.Wrappers;

namespace Visionary;

public static class Vision
{
    public struct API
    {
        public static void GiveTag()
        {
            GameUtils.VRChatUtils.VRCKeyboard.InputPopup("Enter Tag", (x) =>
            {
                
            }, Placeholder: "This sets the players tag", MultiLine: false );
        }
    }


    
}