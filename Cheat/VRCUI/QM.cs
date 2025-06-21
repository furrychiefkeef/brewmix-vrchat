using autumn.ml.Cheat.Modules.World;
using BlanketSDK.Core;
using BlanketSDK.QM.Elements;

using BlanketSDK.QM.Elements.Controls;
using BlanketSDK.QM.Elements.Modals;
using BlanketSDK.Utilities;

using Brewchat.Cheat.Modules;
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
            if (IQuickMenuElements.Misc.TooltipText != null){
               return IQuickMenuElements.Misc.TooltipText.richText = true;
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
                  }, Placeholder: "https:// or C://");
                }, false);
               Group_UI.AddButton("Set Loading Audio", $"Audio is currently set to: {Entry.config.Appearance.inGame.QueueAudio} ", () =>
                {
                   GameUtils.VRChatUtils.VRCKeyboard.InputPopup("Enter a URL", (x) =>
                   {
                       Entry.config.Appearance.inGame.QueueAudio = x;
                       Config.SaveToFile(Entry.config, "config.json");
                       GameUtils.UI.HUD.SendLocalNotifcation("Config", "Saved!", null, null);
                       CSS.Refreshers.RefreshLoadingAudio();
                   }, Placeholder: "https:// or C://");
                }, false);
               Group_UI.AddButton("Set Loading Screen Background", $" is currently set to: {Entry.config.Appearance.inGame.FlatLoadingOverlay} ", () =>
                {
                   GameUtils.VRChatUtils.VRCKeyboard.InputPopup("Enter a URL", (x) =>
                   {
                       Entry.config.Appearance.inGame.FlatLoadingOverlay.Image = x;
                       Config.SaveToFile(Entry.config, "config.json");
                       GameUtils.UI.HUD.SendLocalNotifcation("Config", "Saved!", null, null);
                   }, Placeholder: "https:// or C://");
                }, false);
                }
        }
        public class NestedQMDebug { public VRCPage Page { get; set; }

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
        
        public static void Apply()
        {

            CSS.Online();
            Core.EnableRichText();
            NestedQMAppearance appearance = new(); appearance.Execute();
            NestedQMDebug debug = new(); debug.Execute();
            #region Main
            var MainPage = new VRCPage(Entry.GetEntry().modInfo._currentName, pageOptions: new() { ShowScrollWheel = false });
            VRCTab tab = new VRCTab(MainPage, "it's a luxury to even be owning this project. becareful what you show. take care <3", sprite: EngineUtils.Assets.INTERNAL_Texture(Resources.Logo).ToSprite());
            tab.SetBadge(true); tab.SetBadgeColor(Color.black); tab.SetText("Nightly");

            #region Groupbox
            VRCGroupLayout mainGroupLayout = new(MainPage);
            VRCModalContainer? PlayerMovementModal = new("Player+Movement");
            mainGroupLayout.AddButton("Player", "Modify The Player", PlayerMovementModal.Open, true);
            mainGroupLayout.AddButton("Appearance", "in Game Appearance", appearance.Page.Open, true);
            mainGroupLayout.AddButton("Debug", "Debugger", debug.Page.Open, true);
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
            #endregion
        }
    } 


    public class WorldCheats
    {
         public class NestedQMMurder4
        {
            public VRCPage Page { get; set; }

            public void Execute()
            {
               Page = new VRCPage("Murder 4", true, pageOptions: new() { ShowScrollWheel = false });
               VRCGroupLayout Group_UI = new(Page);
               VRCGroupLayout Group_Roles = new(Page, $"Currently Modifiying (<color=red>{GameUtils.Gameplay.Matchmaking.GetSelectedUser().APIUser.displayName}</color>) Roles");
               Group_UI.AddButton("Kill", "", () =>      
               {

                Murder_4.NetworkEvents.GameManager.Logic.PunishmentReceptors.Kill(GameUtils.Gameplay.Matchmaking.GetSelectedUser());

               }, false);
               Group_Roles.AddButton("Assign Murderer", "", () =>
               {

               Murder_4.NetworkEvents.GameManager.Roles.AssignMurderer(GameUtils.Gameplay.Matchmaking.GetSelectedUser());

               },false);    
               Group_Roles.AddButton("Assign Detective", "", () => {
                Murder_4.NetworkEvents.GameManager.Roles.AssignDetective(GameUtils.Gameplay.Matchmaking.GetSelectedUser());
               }, false);   
               Group_Roles.AddButton("Assign Bystander", "", () =>
               {
        
               Murder_4.NetworkEvents.GameManager.Roles.AssignBystander(GameUtils.Gameplay.Matchmaking.GetSelectedUser());

               },false);    

            }
        }
    }
    
    public class SelectedUserMenu : IQuickMenuElements
    {
        public static void Apply()
        {
             WorldCheats.NestedQMMurder4 m4 = new(); m4.Execute();
             QMElements.SelectedUser.SelectedUser_ButtonContainer().AddButton("Murder-4", "", m4.Page.Open);
             QMElements.SelectedUser.SelectedUser_ButtonContainer().AddToggle("BlockUdon", "","", (x) => 
             {
                    Entry.config.Modules.Udon.isBlocked = x;
             });

             
        }
    }
}


