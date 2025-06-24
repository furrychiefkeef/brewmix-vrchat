using autumn.ml.Cheat.Modules.World;
using BlanketSDK.Core;
using BlanketSDK.QM.Elements;

using BlanketSDK.QM.Elements.Controls;
using BlanketSDK.QM.Elements.Modals;
using BlanketSDK.Utilities;

using Brewchat.Cheat.Modules;
using Brewchat.Cheats.World;
using Brewchat.Game.Monobehaviours;
using Brewchat.Game.Wrappers;
using MelonLoader;
using UnityEngine;
using Valve.VR.InteractionSystem;
using VRC.Core;
using VRC.Networking;
using VRC.SDKBase;
using static Brewchat.Cheat.QM;
using Resources = autumn.ml.Properties.Resources;

namespace Brewchat.Cheat;

internal class QM
{
    public class Core
    {
        public static bool EnableRichText()
        {
            if (IQuickMenuElements.Misc.TooltipText != null && IMainMenuElements.Misc.TooltipText != null)
            {
                 IQuickMenuElements.Misc.TooltipText.richText = true;
                 IMainMenuElements.Misc.TooltipText.richText = true;
                 return true;
            }
            return false;
        }
    }
    public class Main
    {
        public class NestedQMAppearance
        {
            public VRCPage Page { get; set; }

            public void Execute()
            {
               Page = new VRCPage("Appearance", true, pageOptions: new() { ShowScrollWheel = false });
               VRCGroupLayout Group_UI = new(Page);
               Group_UI.AddButton("Set Background", $"Background is currently set to: {Entry.config.Appearance.inGame.Qm_Background} ", () =>      
                {
                  GameUtils.VRChatUtils.VRCKeyboard.InputPopup("Enter a URL", (x) =>
                  {
                      Entry.config.Appearance.inGame.Qm_Background = x;
                      Config.SaveToFile(Entry.config, "config.json");
                      GameUtils.UI.HUD.SendLocalNotifcation("Config", "Saved!", null, null);
                      CSS.ApplyAdvancedMenuBackgrounds();
                  }, Placeholder: "https:// or C://", MultiLine: false);
                }, false);
               Group_UI.AddButton("Set Loading Audio", $"Audio is currently set to: {Entry.config.Appearance.inGame.QueueAudio} ", () =>
                {
                   GameUtils.VRChatUtils.VRCKeyboard.InputPopup("Enter a URL", (x) =>
                   {
                       Entry.config.Appearance.inGame.QueueAudio = x;
                       Config.SaveToFile(Entry.config, "config.json");
                       GameUtils.UI.HUD.SendLocalNotifcation("Config", "Saved!", null, null);
                       CSS.Refreshers.RefreshLoadingAudio();
                   }, Placeholder: "https:// or C://", MultiLine: false);
                }, false);
               Group_UI.AddButton("Set Loading Screen Background", $" is currently set to: {Entry.config.Appearance.inGame.FlatLoadingOverlay} ", () =>
                {
                   GameUtils.VRChatUtils.VRCKeyboard.InputPopup("Enter a URL", (x) =>
                   {
                       Entry.config.Appearance.inGame.FlatLoadingOverlay.Image = x;
                       Config.SaveToFile(Entry.config, "config.json");
                       GameUtils.UI.HUD.SendLocalNotifcation("Config", "Saved!", null, null);
                   }, Placeholder: "https:// or C://", MultiLine: false);
                }, false);
                }
        }
        public class NestedQMDebug 
        { 
            public VRCPage Page { get; set; }
          
            public void Execute()
            {
               Page = new VRCPage("Debug", true, pageOptions: new() { ShowScrollWheel = false });
               VRCGroupLayout Group_UI = new(Page);
               Group_UI.AddButton("SendUdonEvent", $"Send a Udon Event <color=red>(Custom Only NOT NETWORKING)</color>", () =>      
                {
                  GameUtils.VRChatUtils.VRCKeyboard.InputPopup("Enter Event Name", (x) => 
                  {
                     GameUtils.World.SDK3.Udon.LoadCustomEvent(x);
                  }, Placeholder: "Such as: SyncKill", MultiLine: false);
                  
                }, false);
             }
         }
         public class NestedQMVisuals 
         { 
            public VRCPage Page { get; set; }
        
            public void Execute()
            {
               Page = new VRCPage("Visuals", true, pageOptions: new() { ShowScrollWheel = false });
               VRCGroupLayout Group_UI = new(Page, isCollapsible: false);
               VRCModalContainer? Visuals_RenderModal = new("Visuals+Render");
               Group_UI.AddButton("Renderer", "", Visuals_RenderModal.Open, true);

               #region ModalRenderer
               Visuals_RenderModal.AddToggle("<color=#C278A7>[</color><color=#B23E7B>W</color><color=#A30550>I</color><color=#C23E6D>P</color><color=#E1778A>]</color> ESP Player","","", (x) => 
               {
                  ESP.Instance.FX_Entities.enabled = !ESP.Instance.FX_Entities.enabled;
               });

                Visuals_RenderModal.AddToggle("<color=#C278A7>[</color><color=#B23E7B>W</color><color=#A30550>I</color><color=#C23E6D>P</color><color=#E1778A>]</color> ESP Pickups","","", (x) => 
               {
                  ESP.Instance.FX_Pickups.enabled = !ESP.Instance.FX_Pickups.enabled;
               });

               Visuals_RenderModal.AddToggle("<b><color=#843E6B>[</color><color=#91346A>U</color><color=#9F2B6A>n</color><color=#AD216A>f</color><color=#BB1869>i</color><color=#C90F69>n</color><color=#D70569>i</color><color=#BE0F62>s</color><color=#A5195B>h</color><color=#8C2355>e</color><color=#732D4E>d</color><color=#5A3747>]</color></b> Wallhacks","","", (x) => 
               {
                // wallhacks.Instance.FX.enabled = !ESP.Instance.FX.enabled;
               });
               #endregion
            }
        }
        public static void Apply()
        {
            Core.EnableRichText();

            #region Submenus
            NestedQMAppearance appearance = new(); appearance.Execute();
            NestedQMDebug debug = new(); debug.Execute();
            WorldCheats.NestedQMWorld world = new(); world.Execute();
            NestedQMVisuals visuals = new(); visuals.Execute();
            #endregion


            #region Main
            var MainPage = new VRCPage(Entry.GetEntry().modInfo._currentName, pageOptions: new() { ShowScrollWheel = false });
            VRCTab tab = new VRCTab(MainPage, InfoAboutCheat.Tooltips.GetTooltipString("Tab"), sprite: EngineUtils.Assets.INTERNAL_Texture(Resources.Logo).ToSprite());
            tab.SetBadge(true); tab.SetBadgeColor(Color.black); tab.SetText("Nightly");
            #endregion

            #region Groupbox

            VRCGroupLayout mainGroupLayout = new(MainPage, isCollapsible: false);
          
            VRCModalContainer? PlayerMovementModal = new("Player+Movement");
            mainGroupLayout.AddButton("Player", "Modify The Player", PlayerMovementModal.Open, true);
            
            mainGroupLayout.AddButton("Appearance", "in Game Appearance", appearance.Page.Open, true);
            mainGroupLayout.AddButton("Debug", "Debugger", debug.Page.Open, true);
            mainGroupLayout.AddButton("World", "Debugger", world.Page.Open, true);
            mainGroupLayout.AddButton("Visuals", "Visuals", visuals.Page.Open, true);
            #endregion  

            #region Movement_Modal
            PlayerMovementModal.AddToggle("Flight", "Enable Flight", "Disable Flight", (x) =>
            {
                Entry.config.Player.Airborn.Flight.Enabled = x;
                PlayerModule.Movement.Fly();
                Config.SaveToFile(Entry.config, "config.json");
            }, Entry.config.Player.Airborn.Flight.Enabled);

            PlayerMovementModal.AddToggle("Force Jump", "Enable Force Jump", "Disable Force Jump", (x) =>
            {
                PlayerModule.Movement.ForceJump(x);
                Config.SaveToFile(Entry.config, "config.json");
            });
            #endregion

            
        }
    } 


    public class WorldCheats
    {
         public class NestedQMSelectedMurder4
        {
            public VRCPage Page { get; set; }

            public void Execute()
            {
               Page = new VRCPage("Murder 4", true, pageOptions: new() { ShowScrollWheel = false });
               VRCGroupLayout Group_UI = new(Page);
               var selectedPlayer = GameUtils.Gameplay.Matchmaking.GetSelectedUser();
               VRCGroupLayout Group_Roles = new(Page, $"Currently Modifiying (<color=red>{selectedPlayer.APIUser.displayName}</color>) Roles");
               Group_UI.AddButton("Kill", "", () =>      
               {
                    var selectedPlayer = GameUtils.Gameplay.Matchmaking.GetSelectedUser();
                Murder_4.NetworkEvents.GameManager.Logic.PunishmentReceptors.Kill(selectedPlayer);

               }, false);
               Group_Roles.AddButton("Assign Murderer", "", () =>
               {
                var selectedPlayer = GameUtils.Gameplay.Matchmaking.GetSelectedUser();
               Murder_4.NetworkEvents.GameManager.Roles.AssignMurderer(selectedPlayer);

               },false);    
               Group_Roles.AddButton("Assign Detective", "", () => {
                var selectedPlayer = GameUtils.Gameplay.Matchmaking.GetSelectedUser();
                Murder_4.NetworkEvents.GameManager.Roles.AssignDetective(selectedPlayer);
               }, false);   
               Group_Roles.AddButton("Assign Bystander", "", () =>
               {
                var selectedPlayer = GameUtils.Gameplay.Matchmaking.GetSelectedUser();
               Murder_4.NetworkEvents.GameManager.Roles.AssignBystander(selectedPlayer);

               },false);    

            }
        }
         public class NestedQMWorld 
         { 
            public VRCPage Page { get; set;   }

            public void Execute()
            {
               Page = new VRCPage("World", true, pageOptions: new() { ShowScrollWheel = false });
               VRCGroupLayout Group_UI = new(Page, isCollapsible: false);
               Group_UI.AddToggle("Udon Nuker", $"Rapes Udon Games  <color=grey>(Sometimes)</color> <color=red>On</color>", "Rapes Udon Games  <color=red>(Sometimes)</color> <color=green>Off</color>", (x) =>      
                {
                    switch(x) 
                    {
                        case true:
                        Global.Start();
                        break;
                        case false:
                        Global.Stop();
                        break;
                    }
                  
                });
                }
            }
    }
    
    public class SelectedUserMenu : IQuickMenuElements
    {
        public static void Apply()
        {
             WorldCheats.NestedQMSelectedMurder4 m4 = new(); m4.Execute();
             QMElements.SelectedUser.SelectedUser_ButtonContainer().AddButton("<color=red>M4</color>", "", m4.Page.Open);
        }
    }
}