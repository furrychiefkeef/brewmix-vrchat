using BlanketSDK.MM.Elements;
using Syrup.IMGUI;

using BlanketSDK.MM.Elements.Controls;
using BlanketSDK.Utilities;
using Brewchat.Game.Wrappers;
using Resources = brewchat.hybridxcore.bep.Properties.Resources;

namespace Brewchat.Cheat;

internal class MM 
{
    public class _Pinkstar 
    { 
        public static void Apply()
        {
            try
            { 
                VRCSmallButton acee2 = new(Templates._Fallback_Dashboard_Group_MM_UI_ExpandButtons(), "The passion project.", 5, () => sconsole.print("passion_fruit", 0), EngineUtils.Assets.INTERNAL_Texture(Resources.Logo).ToSprite());
                var aware = new VRCPage(Entry.NAME);
                new VRCTab(aware, Entry.FULLNAME, "The passion project.", sprite: EngineUtils.Assets.INTERNAL_Texture(Resources.Logo).ToSprite());
                
                VRCButton? AE = new(aware, "Passion", "Fruit");
                VRCButton? AD = new(aware, "Fruit", "Passion");
                
               
                
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
               
                var row4 = new VRCProfileRow(Templates._Fallback_Dashboard_Group_MM_UI_ProfileRows(),$"{Entry.NAME} Actions");
                VRCProfileButton? Teleport = new(row4, "Teleport", $"Teleport to this player's Position", () =>
                {
                    GameUtils.PlayerModel.Get().transform.position = GameUtils.PlayerModel.Matchmaking.GetSelectedUser.transform.position;
                });
                VRCProfileButton? Info = new(row4,"Info", $"Grab Players Info", () =>
                {
                    Entry.Console.LogMessage(GameUtils.PlayerModel.Matchmaking.GetSelectedUser.VRCPlayerApi.displayName);
                    Entry.Console.LogMessage(GameUtils.PlayerModel.Matchmaking.GetSelectedUser.APIUser.id);
                });

                #region Admin
                var row5 = new VRCProfileRow(Templates._Fallback_Dashboard_Group_MM_UI_ProfileRows(),$"<color=red>{Entry.NAME}</color>");
                VRCProfileButton? Tag = new(row5,"Give Tag", $"Give a Specific Tag to User", () =>
                {
                            /*Pinkstar.Utils().GiveTag(player)*/
                });
                #endregion
             
            }
            catch (Exception e) { sconsole.print(e.Message + "\n" + e.Source ,2); }
          finally{sconsole.print("Created The MainMenu+BlanketSDK.UI",1);}
            
        }
    }
}
