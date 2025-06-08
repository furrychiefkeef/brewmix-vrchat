using BlanketSDK.Core;
using BlanketSDK.QM.Elements;
using Syrup.IMGUI;
using BlanketSDK.QM.Elements.Controls;
using BlanketSDK.QM.Elements.Controls.V2;
using BlanketSDK.QM.Elements.Modals;
using BlanketSDK.QM.Elements.V2;
using BlanketSDK.Utilities;
using BlanketSDK.Utilities.VRC;
using Brewchat.Cheat.Modules;

using Brewchat.Game.Wrappers;
using brewchat.hybridxcore.bep.Cheat.IMGUI;
using VRC.Networking;
using VRC.Udon;
using Resources = brewchat.hybridxcore.bep.Properties.Resources;

namespace Brewchat.Cheat;

internal class QM
{
    public class Main
    {
        public static void Apply()
        {
            try
            {

                #region Begin

                var MainPage = new VRCPage(Entry.FULLNAME, ShowScrollWheel: true);
                new VRCTab(MainPage, "The passion project.",
                    sprite: EngineUtils.Assets.INTERNAL_Texture(Resources.Logo).ToSprite());

                #endregion

                VRCGroupLayout Main_Groupbox = new(MainPage, "Main");

                #region Groupbox

                VRCModalContainer? PlayerMovementModal = new("Player+Movement");
                Main_Groupbox.AddButton("Player", "Modify The Player", PlayerMovementModal.Open, true);

                #endregion

                #region Modal_Player+Movement

                VRCSmallButton Injection = new(PlayerMovementModal, "Injection", () => sconsole.print("passion_fruit", 1));
                UIRefs.Fly = PlayerMovementModal.AddToggle("Fly", "Turn Fly On", "Turn Fly Off", (x) =>
                {
                    Config.Movement.fly = !Config.Movement.fly;
                    PlayerModule.Movement.Fly();
                });
                
                PlayerMovementModal.AddToggle("Force Jump", "Force Jump Off", "Fly Off", (x) => { PlayerModule.Movement.ForceJump(x); });

                PlayerMovementModal.AddToggle("Ghost", "Booo I'm a ghostttt", "awh I'm solid again", (x) =>
                {
                    Widgets.QM.ghostmode_widget.SetState(x);
                    var _player = GameUtils.PlayerModel.Get();
                    var gameObject = PlayerModule.Body.Clone(_player);
                    if (gameObject != null && x == false) UnityEngine.Object.Destroy(gameObject);
                    _player.GetComponent<FlatBufferNetworkSerializer>().enabled = !x;
                });

                #endregion

                #region Modal Logging__Main

                VRCModalContainer? Logging = new("Logging");
                VRCButton? R = new(Main_Groupbox, "Logging", "Configure Logging", () => Logging.Open(), true);

                Logging.AddMiniButton("Configure Events List", () => Console.Beep());
                Logging.AddMiniButton("Configure RPCs List", () => Console.Beep());
                Logging.AddMiniButton("Configure Udon Events List", () => Console.Beep());


                Logging.AddToggle("RPC", "parse", "parse", (x) => { Config.Logging.RPC = !Config.Logging.RPC; });
                Logging.AddToggle("Photon", "parse", "parse", (x) => { Config.Logging.Photon = !Config.Logging.Photon; });
                Logging.AddToggle("Udon", "parse", "parse", (x) => { Config.Logging.Udon = !Config.Logging.Udon; });

                /*VRCModalContainer? Modal_Config_RPCs = new("RPcs");*/

                #region Modal Logging__MainPage2

                VRCModalContainer? LoggingP2 = new("Logging Page 2");
                VRCSmallButton Next = new(Logging, "Next", () => LoggingP2.Open());
                LoggingP2.AddToggle("Debug", "parse", "parse", (x) => { Config.Advanced.DebugMode = !Config.Advanced.DebugMode; });
                /*VRCToggle? Log_RPCs = new(Logging, "RPCs", "parse", "parse", (x) =>
                {

                });
                VRCToggle? Log_Events = new(Logging, "Events", "parse", "parse", (x) =>
                {

                });
                VRCToggle? Udon_Events = new(Logging, "Udon Events", "parse", "parse", (x) =>
                {

                });*/

                #endregion


                #endregion

                #region Modal_Render

                VRCModalContainer? Renders = new("Renderer");
                Main_Groupbox.AddButton("Renders", "Render Properties", () => Renders.Open(), true);
                Renders.AddToggle("ESP", "parse", "parse", (x) => { Config.Render.esp = !Config.Render.esp; });
                Renders.AddToggle("Wallhacks", "parse", "parse", (x) => { Config.Render.player_wallhack = !Config.Render.player_wallhack; });
                
                #endregion
            }
            catch (Exception xe)
            {
               
            }
            finally
            {
                
            }
        }
    }

    public class SelectedMenu : IQuickMenuElements
    {
        public static void Apply()
        {
            try
            {
                VRCToggle toggle = new(QMElements.SelectedUser.SelectedUser_ButtonContainer(), "FREEZE!", "FREEZE CRIMINAL SCUM, ", "UNFREEZE NICE INDIVIUAL!", (x) =>
                {
                    var selecteduser = GameUtils.PlayerModel.Matchmaking.GetSelectedUser();
                    if (selecteduser != null)
                    {
                       
                        selecteduser.GetComponent<FlatBufferNetworkSerializer>().enabled = !x;
                    }
                }, false);
            }
            catch (Exception e)
            {
               
            }
            finally
            {
               
            }
        }
    }
}
