using BlanketSDK.Core;
using BlanketSDK.QM.Elements;

using BlanketSDK.QM.Elements.Controls;
using BlanketSDK.QM.Elements.Modals;
using BlanketSDK.Utilities;

using Brewchat.Cheat.Modules;

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
            public static VRCGroupLayout Main_Groupbox { get; set; }
            public static VRCPage Page { get; set; }

            public static void Execute()
            {
               Page = new VRCPage("Appearance", true);
               Main_Groupbox = new(Page, "Appearance");
            }
        }

        public static void Apply()
        {
         
                #region Begin
                var MainPage = new VRCPage(Entry.entry.NAME, pageOptions: new(){ShowScrollWheel = false});
                new VRCTab(MainPage, "The passion project.", sprite: EngineUtils.Assets.INTERNAL_Texture(Resources.Logo).ToSprite());
                #endregion

                VRCGroupLayout Main_Groupbox = new(MainPage, "Main");

                // INITIALIZE THE SUBMENU FIRST

                #region Groupbox
                VRCModalContainer? PlayerMovementModal = new("Player+Movement");
                Main_Groupbox.AddButton("Player", "Modify The Player", PlayerMovementModal.Open, true);
            #endregion
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

