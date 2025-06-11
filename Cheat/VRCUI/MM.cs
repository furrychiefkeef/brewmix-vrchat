using autumn.ml.Properties;
using BlanketSDK.MM.Elements;


using BlanketSDK.MM.Elements.Controls;
using BlanketSDK.Core;
using BlanketSDK.MM.Elements.Controls.AvatarMenu;

using Brewchat.Game.Wrappers;
using brewchat.hybridxcore.bep.Game.Wrappers;
using Brewchat.Wrappers;




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

                var UI_PROPERTIES = new VRCCellContainer(a, "Properties [QOL]"); 
                var UI_HANDLER = new VRCCellContainer(a, "HANDLERS [PERFORMANCE]"); 
                var UI_MUSIC = new VRCCellContainer(a, "Music"); 
               
                
                #region VRCPage_CodeName:Aware_NavContainer

                new VRCCellButton(a, UI_PROPERTIES, "Properties", "[Properties]");
                new VRCCellButton(a, UI_HANDLER, "Handlers", "[Handlers]");
                new VRCCellButton(a, UI_MUSIC, "Music","[Music]", AstriekLib.Sprites.Music()); 
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
                VRCBigButton? Teleport = new(row4, "Teleport", $"Teleport to this player's Position", () => { GameUtils.PlayerModel.GetLocalPlayer().transform.position = GameUtils.Gameplay.Matchmaking.GetSelectedUser()!.transform.position; });

                #region Admin

                if (GameUtils.PlayerModel.GetLocalPlayer().IsCheatAdmins())
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

    

