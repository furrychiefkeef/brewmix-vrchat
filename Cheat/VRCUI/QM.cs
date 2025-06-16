using BlanketSDK.Core;
using BlanketSDK.QM.Elements;

using BlanketSDK.QM.Elements.Controls;
using BlanketSDK.QM.Elements.Modals;
using BlanketSDK.Utilities;

using Brewchat.Cheat.Modules;

using Brewchat.Game.Wrappers;
using MelonLoader;
using VRC.Networking;
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
        public static NestedQMAppearance submenu_appearance = new();

        public class NestedQMAppearance
        {
            public VRCGroupLayout Main_Groupbox { get; set; }
            public VRCPage Page { get; set; }

            public static void Execute()
            {
                submenu_appearance.Page = new VRCPage("Appearance", true);
                submenu_appearance.Main_Groupbox = new(submenu_appearance.Page, "Appearance");
            }
        }

        public static void Apply()
        {
            try
            {
                #region Begin
                var MainPage = new VRCPage(Entry.entry.NAME, pageOptions: new(){
                    ShowScrollWheel = false,
                });
                new VRCTab(MainPage, "The passion project.", sprite: EngineUtils.Assets.INTERNAL_Texture(Resources.Logo).ToSprite());
                #endregion

                VRCGroupLayout Main_Groupbox = new(MainPage, "Main");

                // INITIALIZE THE SUBMENU FIRST!
                NestedQMAppearance.Execute();

                #region Groupbox
                VRCModalContainer? PlayerMovementModal = new("Player+Movement");
                Main_Groupbox.AddButton("Player", "Modify The Player", PlayerMovementModal.Open, true);
                 Main_Groupbox.AddButton("Player", "Modify The Player", () =>
                 {
                    submenu_appearance.Page.Open();
                 },true);
                // Now add buttons to the submenu
                submenu_appearance.Main_Groupbox.AddButton("Set Background", $"Background is currently set to: {Entry.config.Appearance.inGame.Qm_Background} ", () =>
                {
                    GameUtils.VRChatUtils.VRCKeyboard.InputPopup("Enter a URL", (x) =>
                    {
                        Entry.config.Appearance.inGame.Qm_Background = x;
                        Config.SaveToFile(Entry.config, "config.json");
                        CSS.ApplyAdvancedMenuBackgrounds();
                    }, Placeholder: "https:// or C://");
                }, false);
                submenu_appearance.Main_Groupbox.AddButton("Loading Audio", $"Audio is currently set to: {Entry.config.Appearance.inGame.QueueAudio} ", () =>
                {
                    GameUtils.VRChatUtils.VRCKeyboard.InputPopup("Enter a URL", (x) =>
                    {
                        Entry.config.Appearance.inGame.QueueAudio = x;
                        Config.SaveToFile(Entry.config, "config.json");
                    }, Placeholder: "https:// or C://");
                }, false);

                #endregion
            }
            catch (Exception ex)
            {
                MelonLogger.Error($"Error in Apply(): {ex.Message}");
            }
        }
    }
    public class SelectedUserMenu : IQuickMenuElements
    {
        public static void Apply()
        {
            try
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
            catch (Exception e)
            {

            }
            finally
            {

            }
        }
    }
}

