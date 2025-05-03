using BlanketSDK.QM.Elements;
using Syrup.IMGUI;
using BlanketSDK.QM.Elements.Controls;
using BlanketSDK.Utilities;
using BlanketSDK.QM.Elements.Modals;
using Brewchat.Cheat.Modules;
using Brewchat.Game.Wrappers;
using MS.Internal.Xml.XPath;
using VRC.Core;
using Resources = brewchat.hybridxcore.bep.Properties.Resources;

namespace Brewchat.Cheat;

internal class QM 
{
    public class _Pinkstar {
        public static void Apply()
        {
            try
            {
                #region Begin

                var MainPage = new VRCPage(Entry.FULLNAME);
                new VRCTab(MainPage,"The passion project.", sprite: EngineUtils.Assets.INTERNAL_Texture(Resources.Logo).ToSprite());
                
                VRCWingButton WING_L = new(Templates._Fallback_Dashboard_Group_QM_UI_WingButtons(), Ropie.Wing.Position.L,Entry.NAME, "The passion project.", () => sconsole.print("passion_fruit",1));
                VRCWingButton WING_R = new(Templates._Fallback_Dashboard_Group_QM_UI_WingButtons(), Ropie.Wing.Position.R,Entry.NAME, "The passion project.", () => sconsole.print("passion_fruit",1));
                #endregion
              
                VRCGroupBox Main_Groupbox =  new (MainPage);
                
                #region Groupbox
                
                VRCModalContainer? PlayerMovementModal = new("Player+Movement");
                VRCButton? T = new (Main_Groupbox, "Player", "Modify The Player", () => PlayerMovementModal.Open());
                
                #endregion
                
                #region Modal_Player+Movement
           
                
                VRCSmallButton Injection = new(PlayerMovementModal, "Injection", () => sconsole.print("passion_fruit",1) );
                VRCToggle? Fly = new(PlayerMovementModal, "Fly", "Turn Fly On", "Turn Fly Off", (x) =>
                {
               
                    Config.Movement.fly = !Config.Movement.fly;
                    PlayerModule.Movement.Fly(); 
                });
                VRCToggle? Spinbot = new(PlayerMovementModal, "Spinbot", "Force Jump Off", "Fly Off", (x) =>
                {
                    PlayerModule.Movement.Spinbot(x);
                });
                #endregion
            }
            catch (Exception e) { sconsole.print(e.Message + "\n" + e.Source ,2); }
            finally{sconsole.print("Created The QuickMenu+BlanketSDK.UI",1);}
        }
    }

    public class SelectedMenu
    {
        public static void Apply()
        {   
            if (!GameUtils.World.IsWorldLoaded()) return;
            VRCButton? Fly = new(Templates._QM.SelectedUser_LocalCon(), "Teleport", $"Teleport to this player's Position", () =>
            {
                GameUtils.PlayerModel.Get().transform.position = GameUtils.PlayerModel.Matchmaking.GetSelectedUser.transform.position;
            });
        }
    }
}
