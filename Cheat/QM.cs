using Syrup.IMGUI;
using BlanketSDK.QM;

using BlanketSDK.Utilities;
using BlanketSDK.QM.Modals;
namespace Brewchat.Cheat;

internal class QM
{
    public static void Apply()
    {
        try
        {
            sconsole.SLog("Menu Created");
            VRCModalContainer? Modal = new VRCModalContainer("Hello World");
            VRCButton? a = new VRCButton(Root._Fallback_Dashboard_Group_QM_Buttons(), "OPEN ME", "smart", () => Modal.Open());

            VRCButton? e = new VRCButton(Modal, "OPEN ME", "smart", 
                () => sconsole.SLog("OPEN ME"));
        }
        catch(Exception e)
        {
            sconsole.SError(e.Message);
        }
    }
}
