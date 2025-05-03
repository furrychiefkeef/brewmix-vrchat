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
                var aware = new VRCPage(Entry.NAME);
                new VRCTab(aware, Entry.FULLNAME, "The passion project.", sprite: EngineUtils.Assets.INTERNAL_Texture(Resources.Logo).ToSprite());
                
                VRCButton? AE = new(aware, "Passion", "Fruit");
                VRCButton? AD = new(aware, "Fruit", "Passion");
                
                VRCSmallButton acee2 = new(Templates._Fallback_Dashboard_Group_MM_UI_ExpandButtons(), "Injection2", () => sconsole.print("passion_fruit", 0), EngineUtils.Assets.INTERNAL_Texture(Resources.Logo).ToSprite());
                
            }
            catch (Exception e) { sconsole.print(e.Message + "\n" + e.Source ,2); }
            finally{sconsole.print("Created The MainMenu+BlanketSDK.UI",1);}
        }
    }

    public class SelectedMenu
    {
        public static void Apply()
        {
            if (!GameUtils.World.IsWorldLoaded()) return;
            VRCProfileButton? EA = new(new VRCProfileRow(Templates._Fallback_Dashboard_Group_MM_UI_ProfileRows(),"Player"), "Teleport", $"Teleport to this player's Position", () =>
            {
                GameUtils.PlayerModel.Get().transform.position = GameUtils.PlayerModel.Matchmaking.GetSelectedUser.transform.position;
            });
        }
    }
  
}
