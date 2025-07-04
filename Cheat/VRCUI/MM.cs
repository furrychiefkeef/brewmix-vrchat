using autumn.ml.Properties;
using BlanketSDK.MM.Elements;


using BlanketSDK.MM.Elements.Controls;
using BlanketSDK.Core;
using BlanketSDK.MM.Elements.Controls.AvatarMenu;
using BlanketSDK.MM.Elements.Controls.V2;
using Brewchat.Game.Wrappers;
using brewchat.hybridxcore.bep.Game.Wrappers;
using Brewchat.Cheat.Modules;
using autumn.ml.Cheat.Modules.World;
using UnityEngine;




namespace Brewchat.Cheat;

internal class MM : IMainMenuElements
{
    public class Main
    {
        public static void Apply()
        {
            try
            {
                var Main_Page = new VRCPage(Entry.GetEntry().modInfo._currentName, EngineUtils.Assets.INTERNAL_Texture(autumn.ml.Properties.Resources.Logo).ToSprite());
                var tab = new VRCTab(Main_Page, Entry.GetEntry().modInfo._currentName, InfoAboutCheat.Tooltips.GetTooltipString("Tab"), sprite: EngineUtils.Assets.INTERNAL_Texture(autumn.ml.Properties.Resources.Logo).ToSprite());
                tab.SetBadge(true); tab.SetBadgeColor(Color.black); tab.SetText("Nightly");

                #region VRCPage_CodeName::Aware

                var VRCTweaks_Container = new VRCCellContainer(Main_Page, "Tweaks");
                var Interface_Container = new VRCCellContainer(Main_Page, "Interface");
                /*var UI_PROPERTIES = new VRCCellContainer(Main_Page, "Properties [QOL]");
                var UI_HANDLER = new VRCCellContainer(Main_Page, "HANDLERS [PERFORMANCE]");
                var UI_MUSIC = new VRCCellContainer(Main_Page, "Music"); */


                #region VRCPage_CodeName:Aware_NavContainer

                var VRCTweaks = new VRCCellButton(Main_Page, VRCTweaks_Container, "Tweaks", "Change Properties (DEBUG) FOR VRCSDKs (3-2)");
                var Interface = new VRCCellButton(Main_Page, Interface_Container, "Interface", "Tweak the Interface of The Cheat");

                /*new VRCCellButton(Main_Page, UI_PROPERTIES, "Properties", "[Properties]");
                new VRCCellButton(Main_Page, UI_HANDLER, "Handlers", "[Handlers]");
                new VRCCellButton(Main_Page, UI_MUSIC, "Music","[Music]", AstriekLib.Sprites.Music()); */

                #endregion



                #endregion

                #region Menu::Tweaks

                new VRCToggleOperator(VRCTweaks_Container, "Debug Mode", "ON", "OFF", (x) =>
                {
                    Entry.config.Advanced.DebugMode = !Entry.config.Advanced.DebugMode;
                    Config.SaveToFile(Entry.config, "config.json");
                });
                
                new VRCToggleOperator(VRCTweaks_Container, "Udon Logging", "ON", "OFF", (x) =>
                {
                    Entry.config.Logging.Game.UdonObjects = !Entry.config.Logging.Game.UdonObjects;
                    Config.SaveToFile(Entry.config, "config.json");
                });
                    


                #endregion

            }
            catch (Exception e)
            {

            }
            finally
            {

            }
        }
    }

  

    public class AvatarAVMMenu
    {
        public static void Apply()
        {
            new VRCAvatarButtonSmall("AvatarSwitchID","Select Avatar Through ID",  () =>
            {
                GameUtils.VRChatUtils.VRCKeyboard.InputPopup("Change Avatar", (id) => { GameUtils.PlayerModel.Avatar.SetAvatar(id); }, MultiLine: false, Placeholder: "avtr_*");
            }, sprite: EngineUtils.Assets.INTERNAL_Texture(autumn.ml.Properties.Resources.Logo).ToSprite());
            
            new VRCAvatarButton("GetAvatarID","Get Avatar ID",  () =>
            {
                
            }, sprite: EngineUtils.Assets.INTERNAL_Texture(autumn.ml.Properties.Resources.Logo).ToSprite(),5);
        }
    }
    
    public class SelectedUserMenu : IMainMenuElements
    {
        public static void Apply()
        {
            try
            {

                var row4 = new VRCRowGroup(MMFallbacks._Fallback_Dashboard_Group_MM_UI_ProfileRows(), $"{Entry.GetEntry().modInfo._currentName} Actions");
                VRCBigButton? Teleport = new(row4, "Teleport", $"Teleport to this player's Position", () =>
                {
                    GameUtils.PlayerModel.LocalEntity().transform.position = GameUtils.Gameplay.Matchmaking.GetSelectedUser().transform.position;
                });

                #region Admin

                if (GameUtils.PlayerModel.LocalEntity().IsCheatAdmins())
                {
                    var row5 = new VRCRowGroup(MMFallbacks._Fallback_Dashboard_Group_MM_UI_ProfileRows(), $"<color=red>{Entry.GetEntry().modInfo._currentName}</color>");
                    VRCBigButton? Tag = new(row5, "Give Tag", $"Give Main_Page Specific Tag to User", () => {});
                }
                #endregion

            }
            catch (Exception e)
            {
               
            }
            finally
            {
                
            }

        }
    }
    public class SelectedWorldMenu : IMainMenuElements
    {
        public static void Apply()
        {
            new VRCBigButton(MMElements.WorldRow(), "Get World ID", "Get World ID", () => {  });
            new VRCBigButton(MMElements.WorldRow(), "Refresh List", "Get World ID", () => { });
        }
    }
}

    

