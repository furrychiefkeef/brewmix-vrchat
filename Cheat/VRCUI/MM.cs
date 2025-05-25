using BlanketSDK.MM.Elements;
using Syrup.IMGUI;

using BlanketSDK.MM.Elements.Controls;
using BlanketSDK.Core;
using BlanketSDK.MM.Elements.Controls.AvatarMenu;
using BlanketSDK.MM.Elements.Controls.V2;
using BlanketSDK.Utilities;
using Brewchat.Game.Wrappers;
using Visionary;

using Resources = brewchat.hybridxcore.bep.Properties.Resources;

namespace Brewchat.Cheat;

internal class MM : IMainMenuElements
{
    public class _Pinkstar
    {
        public static void Apply()
        {
            try
            {
                new VRCWingButton(MMFallbacks._Fallback_Dashboard_Group_MM_UI_WingButtons("Left"), SDKUtils.Wing.Position.Left, "Test", "Test", () => { });
                VRCSmallButton acee2 = new(MMFallbacks._Fallback_Dashboard_Group_MM_UI_ExpandButtons(), "The passion project.", 5, () => GameUtils.UI.HUD.Alert("bitch2", GameUtils.UI.HUD.AlertType.MainMenu), EngineUtils.Assets.INTERNAL_Texture(Resources.Logo).ToSprite());
                var aware = new VRCPage(Entry.NAME, EngineUtils.Assets.INTERNAL_Texture(Resources.Logo).ToSprite());
                new VRCTab(aware, Entry.FULLNAME, "The passion project.", sprite: EngineUtils.Assets.INTERNAL_Texture(Resources.Logo).ToSprite());

            }
            catch (Exception e)
            {
                sconsole.print(e.Message + "\n" + e.Source, 2);
            }
            finally
            {
                sconsole.print("Created The MainMenu+BlanketSDK.UI", 1);
            }
        }
    }

    public class SelectedMenu
    {
        public static void Apply()
        {
            try
            {

                var row4 = new VRCRowGroup(IMainMenuElements.MMFallbacks._Fallback_Dashboard_Group_MM_UI_ProfileRows(), $"{Entry.NAME} Actions");
                VRCBigButton? Teleport = new(row4, "Teleport", $"Teleport to this player's Position", () => { GameUtils.PlayerModel.Get().transform.position = GameUtils.PlayerModel.Matchmaking.GetSelectedUser().transform.position; });

                #region Admin

                var row5 = new VRCRowGroup(IMainMenuElements.MMFallbacks._Fallback_Dashboard_Group_MM_UI_ProfileRows(), $"<color=red>{Entry.NAME}</color>");
                VRCBigButton? Tag = new(row5, "Give Tag", $"Give a Specific Tag to User", () => { Vision.API.GiveTag(); });

                #endregion

            }
            catch (Exception e)
            {
                sconsole.print(e.Message + "\n" + e.Source, 2);
            }
            finally
            {
                sconsole.print("Created The Selected_MainMenu+BlanketSDK.UI", 1);
            }

        }
    }

    public class AvatarAVMMenu
    {
        public static void Apply()
        {
            new VRCAvatarButton("Ava", "Package It!", () =>
            {
                GameUtils.VRChatUtils.VRCKeyboard.InputPopup("Change Avatar", (id) => { GameUtils.PlayerModel.Avatar.SetAvatar(id); }, MultiLine: false, Placeholder: "avtr_*");
            }, sprite: EngineUtils.Assets.INTERNAL_Texture(Resources.Logo).ToSprite());
        }
    }

    public class WorldMenu : IMainMenuElements
    {
        public static void Apply()
        {
            new VRCBigButton(MMElements.WorldRow(), "Avatar ID", "Test", () => { Console.Beep();} );
        }
    }
}

    

