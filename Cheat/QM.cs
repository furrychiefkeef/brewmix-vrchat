using BlanketSDK.Core;
using BlanketSDK.QM.Elements;
using Syrup.IMGUI;
using BlanketSDK.QM.Elements.Controls;
using BlanketSDK.QM.Elements.Modals;
using BlanketSDK.Utilities;
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

                VRCWingButton WING_F = new(null, SDKUtils.Wing.Position.R,"Send GlobalUdon", "Send GlobalUdonEvent", () =>
                {
                    GameUtils.PlayerModel.Get().StartCoroutine(M4.UdonNukeCoroutine().WrapToIl2Cpp());
                });
                #endregion
    
                VRCGroupBox Main_Groupbox = new(MainPage, "Main");

                #region Groupbox

                VRCModalContainer? PlayerMovementModal = new("Player+Movement");
                VRCButton? T = new(Main_Groupbox, "Player", "Modify The Player", () => PlayerMovementModal.Open());
                VRCToggle E = new(Main_Groupbox, "Enable", "Enable The Player", "Disable The Player", (x) =>
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
                VRCGroupBox _Groupbox = new(MainPage, "Main");
                
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

                #endregion

                #region Modal Logging__Main

                VRCModalContainer? Logging = new("Logging");

                VRCSmallButton CEL = new(Logging, "Configure Events List", () => Console.Beep());
                VRCSmallButton CRL = new(Logging, "Configure RPCs List", () => Console.Beep());
                VRCSmallButton CUEL = new(Logging, "Configure Udon Events List", () => Console.Beep());

                VRCButton? R = new(Main_Groupbox, "Logging", "Configure Logging", () => Logging.Open());


                VRCToggle? Log_RPCs = new(Logging, "RPCs", "parse", "parse", (x) => { });
                VRCToggle? Log_Events = new(Logging, "Events", "parse", "parse",
                    (x) => { Config.Logging.LogVoicePackets_Event1 = !Config.Logging.LogVoicePackets_Event1; });
                VRCToggle? Udon_Events = new(Logging, "Udon Events", "parse", "parse", (x) => { });


                #region Modal Logging__MainPage2

                VRCModalContainer? LoggingP2 = new("Logging Page 2");
                VRCSmallButton Next = new(Logging, "Next", () => LoggingP2.Open());
                /*VRCToggle? Debug_Log = new(LoggingP2, "Debug", "parse", "parse", (x) =>
                {
                        Config.Advanced.DebugMode = !Config.Advanced.DebugMode;
                });
                VRCToggle? Log_RPCs = new(Logging, "RPCs", "parse", "parse", (x) =>
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
                VRCToggle toggle = new(QMMenus.QMSubmenus.SelectedUser_Local().GetComponentInChildren<VerticalLayoutGroup>().gameObject.transform, "FREEZE!", "FREEZE CRIMINAL SCUM, ","UNFREEZE NICE INDIVIUAL!", (x) =>
                {
                    var selecteduser= GameUtils.PlayerModel.Matchmaking.GetSelectedUser();
                    if (selecteduser != null)
                    {
                       
                        sconsole.print($"<color=red>[frozen]</color>: {selecteduser.APIUser.displayName }",0);
                        selecteduser.GetComponent<FlatBufferNetworkSerializer>().enabled = !x;
                    }
                }, false);
            }
            catch (Exception e) { sconsole.print(e.Message + "\n" + e.Source, 2); }
            finally { sconsole.print("Created The Selected_QuickMenu+BlanketSDK.UI", 1); }
        }
    }
}


