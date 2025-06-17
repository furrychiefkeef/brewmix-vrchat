using BlanketSDK.Core;
using BlanketSDK.QM.Elements;

using BlanketSDK.QM.Elements.Controls;
using BlanketSDK.QM.Elements.Modals;
using BlanketSDK.Utilities;

using Brewchat.Cheat.Modules;
using Brewchat.Game.Monobehaviours;
using Brewchat.Game.Wrappers;
using MelonLoader;
using VRC.Networking;
using static Brewchat.Cheat.QM;
using Resources = autumn.ml.Properties.Resources;

namespace Brewchat.Cheat;

internal class QM
{
    public class Core
    {
        public static VRCPage? QMAppearance { get; set; }
        public static Action EnableRichText = () =>
        {
            if (IQuickMenuElements.Misc.TooltipText != null)  
            IQuickMenuElements.Misc.TooltipText.richText = true;
        };
    }
    public class Main
    {
        public class NestedQMAppearance
        {
            public static VRCGroupLayout Main_Groupbox { get; }
            public static VRCPage Page { get; }

            public static void Execute()
            {

            }
        }

        public static void Apply()
        {

            #region Begin
            var MainPage = new VRCPage(Entry.entry.NAME, pageOptions: new() { ShowScrollWheel = false });
            var Page = new VRCPage("Appearance", true, pageOptions: new() { ShowScrollWheel = false });
            new VRCTab(MainPage, "The passion project.", sprite: EngineUtils.Assets.INTERNAL_Texture(Resources.Logo).ToSprite());
            #endregion

            VRCGroupLayout Main_Groupbox = new(MainPage, "Main");
            VRCGroupLayout _Nau = new(Page, "Appearance");



            _Nau.AddButton("Set Background", $"Background is currently set to: {Entry.config.Appearance.inGame.Qm_Background} ", () =>
            {
                GameUtils.VRChatUtils.VRCKeyboard.InputPopup("Enter a URL", (x) =>
                {
                    Entry.config.Appearance.inGame.Qm_Background = x;
                    Config.SaveToFile(Entry.config, "config.json");
                    GameUtils.UI.HUD.SendLocalNotifcation("Config", "Saved!", null, null);
                    CSS.ApplyAdvancedMenuBackgrounds();
                }, Placeholder: "https:// or C://");
            }, false);
            _Nau.AddButton("Set Loading Audio", $"Audio is currently set to: {Entry.config.Appearance.inGame.QueueAudio} ", () =>
            {
                GameUtils.VRChatUtils.VRCKeyboard.InputPopup("Enter a URL", (x) =>
                {
                    Entry.config.Appearance.inGame.QueueAudio = x;
                    Config.SaveToFile(Entry.config, "config.json");
                    GameUtils.UI.HUD.SendLocalNotifcation("Config", "Saved!", null, null);
                    CSS.RefreshLoadingAudio();
                }, Placeholder: "https:// or C://");
            }, false);
             _Nau.AddButton("Set Loading Screen Background", $" is currently set to: {Entry.config.Appearance.inGame.FlatLoadingOverlay} ", () =>
            {
                GameUtils.VRChatUtils.VRCKeyboard.InputPopup("Enter a URL", (x) =>
                {
                    Entry.config.Appearance.inGame.FlatLoadingOverlay.Image = x;
                    Config.SaveToFile(Entry.config, "config.json");
                    GameUtils.UI.HUD.SendLocalNotifcation("Config", "Saved!", null, null);
                }, Placeholder: "https:// or C://");
            }, false);
            
            // INITIALIZE THE SUBMENU FIRST

            #region Groupbox
            VRCModalContainer? PlayerMovementModal = new("Player+Movement");
            Main_Groupbox.AddButton("Player", "Modify The Player", PlayerMovementModal.Open, true);
            Main_Groupbox.AddButton("Apperance", "Modify The Player", Page.Open, true);
            #endregion


            PlayerMovementModal.AddToggle("Flight", "Enable Flight", "Disable Flight", (x) =>
            {
                Entry.config.Player.Airborn.Flight.Enabled = x;
                PlayerModule.Movement.Fly();
                Config.SaveToFile(Entry.config, "config.json");
            });

            PlayerMovementModal.AddToggle("Force Jump", "Enable Force Jump", "Disable Force Jump", (x) =>
            {
                PlayerModule.Movement.ForceJump(x);
                Config.SaveToFile(Entry.config, "config.json");
            });
        }
    } 


      
    
    public class SelectedUserMenu : IQuickMenuElements
    {
        public static void Apply()
        {
                VRCToggle toggle = new(QMElements.SelectedUser.SelectedUser_ButtonContainer(), "FREEZE!", "FREEZE CRIMINAL SCUM, ", "UNFREEZE NICE INDIVIUAL!", (x) =>
                {
                    var selecteduser = GameUtils.Gameplay.Matchmaking.GetSelectedUser();
                    if (selecteduser != null)
                    {
                        selecteduser.GetComponent<FlatBufferNetworkSerializer>().enabled = !x;
                    }
                }, false);
        }
    }
}

