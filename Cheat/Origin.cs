using BlanketSDK.Origin.Elements.Controls;

namespace Brewchat.Cheat;

public class Origin
{
    public static void Apply()
    {
        new OButton(VRCUiManager.field_Private_Static_VRCUiManager_0.transform.FindChild("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress"),1.5f,1.5f,"End Mission", () =>
        {
            Entry.Console.LogMessage("Disposable Waste");
        });
    }
}