using BlanketSDK.MM.Elements;
using Syrup.IMGUI;

using BlanketSDK.MM.Elements.Controls;
using BlanketSDK.Core;
using BlanketSDK.Utilities;
using Brewchat.Game.Wrappers;
using FragLabs.Audio.Codecs.Opus;
using Visionary;
using VRC.Core;
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
                new VRCWingButton(MMFallbacks._Fallback_Dashboard_Group_MM_UI_WingButtons("Left"), SDKUtils.Wing.Position.Left, 
                "Test", "Test", () =>
                {
                    
                });
                VRCSmallButton acee2 = new(MMFallbacks._Fallback_Dashboard_Group_MM_UI_ExpandButtons(), "The passion project.", 5, () => GameUtils.UI.HUD.Alert("bitch2", GameUtils.UI.HUD.AlertType.MainMenu), EngineUtils.Assets.INTERNAL_Texture(Resources.Logo).ToSprite());
                var aware = new VRCPage(Entry.NAME, EngineUtils.Assets.INTERNAL_Texture(Resources.Logo).ToSprite());
                new VRCTab(aware, Entry.FULLNAME, "The passion project.", sprite: EngineUtils.Assets.INTERNAL_Texture(Resources.Logo).ToSprite());
                
         
                
                var ace = new VRCCellContainer(aware, "The passion project.", 1);
                VRCCellButton? AE = new(aware, ace, "Passion", "Fruit");
                VRCCellButton? AC = new(aware.navigation_buttons, "Fruit", "Passion", () =>
                {
                    sconsole.print("passion_fruit", 1);
                });
                
                
            }
            catch (Exception e) { sconsole.print(e.Message + "\n" + e.Source ,2); }
            finally{sconsole.print("Created The MainMenu+BlanketSDK.UI",1);}
        }
    }

    public class SelectedMenu
    {
        public static void Apply()
        {
            try
            {
               
                var row4 = new VRCProfileRow(IMainMenuElements.MMFallbacks._Fallback_Dashboard_Group_MM_UI_ProfileRows(),$"{Entry.NAME} Actions");
                VRCProfileButton? Teleport = new(row4, "Teleport", $"Teleport to this player's Position", () =>
                {
                    GameUtils.PlayerModel.Get().transform.position = GameUtils.PlayerModel.Matchmaking.GetSelectedUser().transform.position;
                });
                VRCProfileButton? Info = new(row4,"Info", $"Grab Players Info", () =>
                {
                    Entry.Console.LogMessage(GameUtils.PlayerModel.Matchmaking.GetSelectedUser().VRCPlayerApi.displayName);
                    Entry.Console.LogMessage(GameUtils.PlayerModel.Matchmaking.GetSelectedUser().APIUser.id);
                });

                #region Admin
                var row5 = new VRCProfileRow(IMainMenuElements.MMFallbacks._Fallback_Dashboard_Group_MM_UI_ProfileRows(),$"<color=red>{Entry.NAME}</color>");
                VRCProfileButton? Tag = new(row5,"Give Tag", $"Give a Specific Tag to User", () =>
                {
                            Vision.API.GiveTag();
                });
                #endregion
             
            }
            catch (Exception e) { sconsole.print(e.Message + "\n" + e.Source ,2); }
          finally{sconsole.print("Created The Selected_MainMenu+BlanketSDK.UI",1);}
            
        }
    }
}
