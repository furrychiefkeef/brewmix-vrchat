using Syrup.IMGUI;
using BlanketSDK.QM;

using BlanketSDK.Utilities;
using BlanketSDK.QM.Modals;
using Brewchat.Cheat.Modules;

namespace Brewchat.Cheat;

internal class QM
{
    public static void Apply()
    {
        try
        {
            sconsole.SLog("Created The QuickMenu+BlanketSDK.UI");
            VRCModalContainer? Modal = new VRCModalContainer("Player+Movement");
            VRCButton? a = new VRCButton(Root._Fallback_Dashboard_Group_QM_Buttons(), "Player", "Modify The Player", () => Modal.Open());
            VRCToggle? Fly_Toggle = new VRCToggle(Modal, "Fly", "Fly On", "Fly Off", (x) =>  PlayerMovement.Fly(x));
                
        }
        catch(Exception e)
        {
            sconsole.SError(e.Message);
        }
    }
}
