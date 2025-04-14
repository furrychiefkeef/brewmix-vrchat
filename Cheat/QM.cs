using Syrup.IMGUI;
using BlanketSDK.QM;

using Brewchat.Game.Wrappers;
using BlanketSDK.Utilities;
using BlanketSDK.QM.Modals;
using VRC.UI.Core.Styles;
namespace Brewchat.Cheat;

internal class QM
{
    public static void Apply()
    {
        try
        {
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
        finally
        {
            sconsole.SLog("Menu Created");
        }
    }
}
