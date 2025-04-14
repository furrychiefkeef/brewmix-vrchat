using Syrup.IMGUI;
using BlanketSDK.QM;

using Brewchat.Game.Wrappers;
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
            var modal = new VRCModalContainer("Hello World");
            var a = new VRCButton(Root._Fallback_Dashboard_Group_QM_Buttons(), "OPEN ME", "smart", () => modal.Open());

            var e = new VRCButton(modal, "OPEN ME", "smart", () =>
            {
                sconsole.SLog("it works");
            });
        }
        catch(Exception e)
        {
            sconsole.SError(e.Message);
        }
    }
}
