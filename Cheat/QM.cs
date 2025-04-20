using System.Diagnostics;
using Syrup.IMGUI;
using BlanketSDK.QM;

using BlanketSDK.Utilities;
using BlanketSDK.QM.Modals;
using Brewchat.Cheat.Modules;
using Brewchat.Cheats.World;
using VRC.SDK3.Components;

namespace Brewchat.Cheat;

internal class QM
{
    public static void Apply()
    {
        try
        {
            
            VRCModalContainer? Modal = new("Player+Movement");
            VRCSmallButton acee = new(Modal, "Injection", () => sconsole.print("passion_fruit",1) );

            #region Modal_1
            VRCButton? a = new (Root._Fallback_Dashboard_Group_QM_UI_Buttons(), "Player", "Modify The Player", () => Modal.Open());
            
            VRCToggle? Fly_Toggle = new(Modal, "Fly", "Fly On", "Fly Off", (x) => PlayerMovement.Fly(x));
            VRCToggle? Force_Jump = new(Modal,"Force Jump On", "Force Jump Off", "Fly Off", (x) => PlayerMovement.ForceJump(x));
            #endregion
       

        }
        catch (Exception e) { sconsole.print(e.StackTrace.ToString(),2); }
        finally{sconsole.print("Created The QuickMenu+BlanketSDK.UI",1);}
    }
}
