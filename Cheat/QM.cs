using BlanketSDK.Core;
using BlanketSDK.QM.Elements;
using Syrup.IMGUI;
using BlanketSDK.QM.Elements.Controls;
using BlanketSDK.QM.Elements.Modals;
using BlanketSDK.Utilities;
using Brewchat.Cheat.Flaws;
using Brewchat.Cheat.Modules;
using Brewchat.Cheats.World;
using Brewchat.Game.Wrappers;
using UnityEngine.UI;
using UNIXEngine.IL2CPPModding.Utils.Collections;
using VRC.Core;
using VRC.Networking;
using Resources = brewchat.hybridxcore.bep.Properties.Resources;

namespace Brewchat.Cheat;

internal class QM
{
    public class _Pinkstar
    {
        public static void Apply()
        {
            try
            {
     
                #region Begin

                var MainPage = new VRCPage(Entry.FULLNAME, ShowScrollWheel: false);
                new VRCTab(MainPage, "The passion project.",
                    sprite: EngineUtils.Assets.INTERNAL_Texture(Resources.Logo).ToSprite());

                VRCWingButton WING_A = new(null,
                    SDKUtils.Wing.Position.L, Entry.NAME, "The passion project.",
                    () => sconsole.print("passion_fruit", 1));
                
                VRCWingButton WING_B = new(null,
                    SDKUtils.Wing.Position.R, Entry.NAME, "The passion project.",
                    () => sconsole.print("passion_fruit", 1));
                
                #endregion
    
                VRCGroupLayout Main_Groupbox = new(MainPage, "Main");

                #region Groupbox

                VRCModalContainer? PlayerMovementModal = new("Player+Movement");
                VRCButton? T = new(Main_Groupbox, "Player", "Modify The Player", () => PlayerMovementModal.Open());
                
                
                #endregion
                
                #region Modal_Player+Movement

                VRCSmallButton Injection = new(PlayerMovementModal, "Injection",
                    () => sconsole.print("passion_fruit", 1));
                VRCToggle? Fly = new(PlayerMovementModal, "Fly", "Turn Fly On", "Turn Fly Off", (x) =>
                {

                    Config.Movement.fly = !Config.Movement.fly;
                    PlayerModule.Movement.Fly();
                });
                VRCToggle? Spinbot = new(PlayerMovementModal, "Spinbot", "Spinbot On", "Spinbot Off",
                    (x) => { PlayerModule.Movement.Spinbot(x); });
                VRCToggle? ForceJump = new(PlayerMovementModal, "Force Jump", "Force Jump Off", "Fly Off",
                    (x) => { PlayerModule.Movement.ForceJump(x); });
                VRCToggle Ghost = new(PlayerMovementModal, "Ghost", "Booo I'm a ghostttt", "awh I'm solid again", (x) =>
                {
                    var _player = GameUtils.PlayerModel.Get();
                    var gameObject = PlayerModule.Body.Clone(_player);
                    if (gameObject != null && x == false)
                    {
                        UnityEngine.Object.Destroy(gameObject);
                    }
                    _player.GetComponent<FlatBufferNetworkSerializer>().enabled = !x;
                    
                    
                });
                #endregion

                #region Modal Logging__Main

                VRCModalContainer? Logging = new("Logging");

                VRCSmallButton CEL = new(Logging, "Configure Events List", () => Console.Beep());
                VRCSmallButton CRL = new(Logging, "Configure RPCs List", () => Console.Beep());
                VRCSmallButton CUEL = new(Logging, "Configure Udon Events List", () => Console.Beep());

                VRCButton? R = new(Main_Groupbox, "Logging", "Configure Logging", () => Logging.Open());


                VRCToggle? Log_RPCs = new(Logging, "RPC", "parse", "parse", (x) => { Config.Logging.RPC = !Config.Logging.RPC; });
                VRCToggle? Log_Events = new(Logging, "Photon", "parse", "parse", (x) => { Config.Logging.Photon = !Config.Logging.Photon; });
                VRCToggle? Udon_Events = new(Logging, "Udon", "parse", "parse", (x) => { Config.Logging.Udon = !Config.Logging.Udon;});

                /*VRCModalContainer? Modal_Config_RPCs = new("RPcs");*/
                #region Modal Logging__MainPage2

                VRCModalContainer? LoggingP2 = new("Logging Page 2");
                VRCSmallButton Next = new(Logging, "Next", () => LoggingP2.Open());
                VRCToggle? Debug_Log = new(LoggingP2, "Debug", "parse", "parse", (x) =>
                {
                        Config.Advanced.DebugMode = !Config.Advanced.DebugMode;
                });
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
                VRCButton? AC = new(Main_Groupbox, "Renders", "Render Properties", () => Renders.Open());

                VRCToggle? ESP = new(Renders, "ESP", "parse", "parse", (x) => { Config.Render.esp = !Config.Render.esp; });
                VRCToggle? WALLHACKS = new(Renders, "Wallhacks", "parse", "parse", (x) =>
                {
                    Config.Render.player_wallhack = !Config.Render.player_wallhack;
                });

                #endregion
            }
            catch (Exception e) { sconsole.print(e.Message + "\n" + e.Source, 2); }
            finally { sconsole.print("Created The QuickMenu+BlanketSDK.UI", 1); }
        }

        
    }
    public class SelectedMenu : IQuickMenuElements
    {
        public static void Apply()
        {
            try
            {
                VRCToggle toggle = new(QMElements.SelectedUser.SelectedUser_ButtonContainer(), "FREEZE!", "FREEZE CRIMINAL SCUM, ","UNFREEZE NICE INDIVIUAL!", (x) =>
                {
                    var selecteduser= GameUtils.PlayerModel.Matchmaking.GetSelectedUser();
                    if (selecteduser != null)
                    {
                        bool isFrozen = false;
                       
                        sconsole.print(
                            isFrozen 
                                ? $"<color=red>[Frozen]</color>: {selecteduser.APIUser.displayName}" 
                                : $"<color=red>[UnFrozen]</color>: {selecteduser.APIUser.displayName}", 
                            0
                        );
                        selecteduser.GetComponent<FlatBufferNetworkSerializer>().enabled = !x;
                    }
                }, false);
            }
            catch (Exception e) { sconsole.print(e.Message + "\n" + e.Source, 2); }
            finally { sconsole.print("Created The Selected_QuickMenu+BlanketSDK.UI", 1); }
        }
    }
}


