using autumn.ml.Properties;
using BlanketSDK.MM.Elements;


using BlanketSDK.MM.Elements.Controls;
using BlanketSDK.Core;
using BlanketSDK.MM.Elements.Controls.AvatarMenu;
using BlanketSDK.MM.Elements.Controls.V2;
using Brewchat.Game.Wrappers;
using brewchat.hybridxcore.bep.Game.Wrappers;
using Brewchat.Cheat.Modules;




namespace Brewchat.Cheat;

internal class MM : IMainMenuElements
{
    public class Main
    {
        public static void Apply()
        {
            try
            {
                var a = new VRCPage(Entry.NAME, EngineUtils.Assets.INTERNAL_Texture(Resources.Logo).ToSprite());
                new VRCTab(a, Entry.FULLNAME, "The passion project.", sprite: EngineUtils.Assets.INTERNAL_Texture(Resources.Logo).ToSprite());

                #region Header_Container

                VRCSmallButton acee2 = new(MMFallbacks._Fallback_Dashboard_Group_MM_UI_ExpandButtons(), "The passion project.", 5, () => GameUtils.UI.HUD.Alert("bitch2"), EngineUtils.Assets.INTERNAL_Texture(Resources.Logo).ToSprite());

                #endregion

                #region VRCPage_CodeName:Aware

                var VRCSdk_Container = new VRCCellContainer(a, "VRChat-SDK");
                /*var UI_PROPERTIES = new VRCCellContainer(a, "Properties [QOL]");
                var UI_HANDLER = new VRCCellContainer(a, "HANDLERS [PERFORMANCE]");
                var UI_MUSIC = new VRCCellContainer(a, "Music"); */


                #region VRCPage_CodeName:Aware_NavContainer

                var VRCsdk = new VRCCellButton(a, VRCSdk_Container, "VRChat-SDK", "Change Properties (DEBUG) FOR VRCSDKs (3-2)");

                var radial = new VRCRadialSelect(VRCSdk_Container, "Auto Role Assign", "wow");
                radial.AddChoice("Murder","Changes u to Murderer", () => { });
                radial.AddChoice("Detective","Changes u to Detective",() => { });

                var btn = new VRCButtonOperator(VRCSdk_Container, new()
                {
                    _title = "Revolver",
                    _subtitle = "weapon",
                    _text = "Revolver",
                    _tooltip = "bitch",
                    _maintooltip = "bitch 2s"
                }, () =>
                {
                   
                });
                 var btna = new VRCButtonOperator(VRCSdk_Container, new()
                {
                    _title = "Reload CSS",
                    _subtitle = "weapon",
                    _text = "Reload",
                    _tooltip = "This reloads the style for the mod",
                    _maintooltip = "This reloads the style for the mod"
                }, () =>
                {
                   CSS.Online();
                });
                /*new VRCCellButton(a, UI_PROPERTIES, "Properties", "[Properties]");
                new VRCCellButton(a, UI_HANDLER, "Handlers", "[Handlers]");
                new VRCCellButton(a, UI_MUSIC, "Music","[Music]", AstriekLib.Sprites.Music()); */

                #endregion



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
            }, sprite: EngineUtils.Assets.INTERNAL_Texture(Resources.Logo).ToSprite());
            
            new VRCAvatarButton("GetAvatarID","Get Avatar ID",  () =>
            {
                
            }, sprite: EngineUtils.Assets.INTERNAL_Texture(Resources.Logo).ToSprite(),5);
        }
    }
    
    public class SelectedUserMenu : IMainMenuElements
    {
        public static void Apply()
        {
            try
            {

                var row4 = new VRCRowGroup(MMFallbacks._Fallback_Dashboard_Group_MM_UI_ProfileRows(), $"{Entry.NAME} Actions");
                VRCBigButton? Teleport = new(row4, "Teleport", $"Teleport to this player's Position", () => { GameUtils.PlayerModel.VRC_LocalPlayer().transform.position = GameUtils.Gameplay.Matchmaking.GetSelectedUser()!.transform.position; });

                #region Admin

                if (GameUtils.PlayerModel.VRC_LocalPlayer().IsCheatAdmins())
                {
                    var row5 = new VRCRowGroup(MMFallbacks._Fallback_Dashboard_Group_MM_UI_ProfileRows(), $"<color=red>{Entry.NAME}</color>");
                    VRCBigButton? Tag = new(row5, "Give Tag", $"Give a Specific Tag to User", () => {  });
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
            new VRCBigButton(MMElements.WorldRow(), "Get World ID", "Get World ID", () => { Console.WriteLine($@"WORLD ID: {GameUtils.World.GetID()}");});
        }
    }
}

    

