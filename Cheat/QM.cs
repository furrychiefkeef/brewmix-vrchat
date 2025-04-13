using Syrup.IMGUI;
using BlanketSDK.QM;

using Brewchat.Game.Wrappers;
namespace Brewchat.Cheat;

internal class QM
{
    public static void Apply()
    {
        new VRCButton(null, "test", "smart", () =>
        {
            GameUtils.HUD.ToastV1("TEST");
        }); 
        
        new VRCButton(null, "test2", "smart", () =>
        {
            GameUtils.HUD.LoadingPopUp("funny bitch", "bitch", "bitcah", () => { GameUtils.HUD.ToastV1("TEST"); },null);

        }); 
        
        new VRCButton(null, "test2", "smart", () =>
        {
            GameUtils.HUD.LoadingPopUp2("funny bitch", "bitch", "bitcah", () => { GameUtils.HUD.ToastV1("TEST"); }, null);
        });

    }
}
