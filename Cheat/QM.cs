using System.Diagnostics;
using Syrup.IMGUI;
using BlanketSDK.QM;

using BlanketSDK.Utilities;
using BlanketSDK.QM.Modals;
using Brewchat.Cheat.Modules;
using VRC.SDK3.Components;

namespace Brewchat.Cheat;

internal class QM
{
    public static void Apply()
    {
        try
        {
            
            VRCModalContainer? Modal = new VRCModalContainer("Player+Movement");
            var acee = new VRCSmallButton(Modal, "Injectiion", () => sconsole.SLog("passion_fruit") );

            VRCButton? a = new VRCButton(Root._Fallback_Dashboard_Group_QM_UI_Buttons(), "Player", "Modify The Player",
                () => Modal.Open());
            VRCToggle? Fly_Toggle = new VRCToggle(Modal, "Fly", "Fly On", "Fly Off", (x) => PlayerMovement.Fly(x));

        }
        catch (Exception e)
        {
            sconsole.SError(e.Message);
        }
        finally
        {
            sconsole.SLog("Created The QuickMenu+BlanketSDK.UI");
        }
    }
}
