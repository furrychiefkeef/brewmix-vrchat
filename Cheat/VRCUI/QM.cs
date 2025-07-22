using autumn.ml.Cheat.Modules.Global;
using autumn.ml.Cheat.Modules.World;
using BlanketSDK.Core;
using BlanketSDK.QM.Elements;

using BlanketSDK.QM.Elements.Controls;
using BlanketSDK.QM.Elements.Controls.V2;
using BlanketSDK.QM.Elements.Modals;
using BlanketSDK.QM.Elements.V2;
using BlanketSDK.Utilities;
using brewchat.hybridxcore.bep.Game.Monobehaviours;
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
using VRC.Udon.Common;
using static Brewchat.VRCUI.QM;
using Resources = autumn.ml.Properties.Resources;

namespace Brewchat.VRCUI;

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
        public static void CurrentStates()
        {
            Main.Toggles.Player.Fly.SetState(Entry.config.Player.Airborn.Flight.Enabled);
        }
    }
    public class Main
    {
        public class Toggles
        {
            public struct Player
            {
                public static VRCToggle? Fly;    
            }
            
        }
        public class NestedQMAppearance
        {
            public VRCPage? Page { get; set; }

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
                        CSS.ApplyAdvancedMenuBackgrounds();
                    }, Placeholder: "https:// or C://", MultiLine: false);
                 }, false);
                Group_UI.AddButton("Set Loading Audio", $"Audio is currently set to: {Entry.config.Appearance.inGame.QueueAudio} ", () =>
                 {
                     GameUtils.VRChatUtils.VRCKeyboard.InputPopup("Enter a URL", (x) =>
                    {
                        Entry.config.Appearance.inGame.QueueAudio = x;
                        Config.SaveToFile(Entry.config, "config.json");
                        CSS.Refreshers.RefreshLoadingAudio();
                    }, Placeholder: "https:// or C://", MultiLine: false);
                 }, false);
                Group_UI.AddButton("Set Loading Screen Background", $" is currently set to: {Entry.config.Appearance.inGame.FlatLoadingOverlay} ", () =>
                 {
                     GameUtils.VRChatUtils.VRCKeyboard.InputPopup("Enter a URL", (x) =>
                    {
                        Entry.config.Appearance.inGame.FlatLoadingOverlay.Image = x;
                        Config.SaveToFile(Entry.config, "config.json");
                    }, Placeholder: "https:// or C://", MultiLine: false);
                 }, false);
            }
        }
        public class NestedQMDebug
        {
            public VRCPage? Page { get; set; }

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

                Group_UI.AddButton("Fake Player Name (Local) [Phase 1]", "Phase 1", () =>
                {
                    GameUtils.VRChatUtils.VRCKeyboard.InputPopup("Enter A Name", (x) =>
                    {
                        GameUtils.PlayerModel.LocalEntity().GetComponent<Nameplates>().SetNameText(x);
                    }, Placeholder: "Player", MultiLine: false);

                }, false);
                Group_UI.AddButton("Fake Player Name (Local) [Phase 2]", "Phase 1", () =>
                {
                    GameUtils.VRChatUtils.VRCKeyboard.InputPopup("Enter A Name", (x) =>
                    {
                        GameUtils.PlayerModel.LocalEntity().APIUser.displayName = x;
                        GameUtils.PlayerModel.LocalEntity().APIUser.username = x;
                        GameUtils.PlayerModel.LocalEntity().VRCPlayerApi.displayName = x;
                    }, Placeholder: "Player", MultiLine: false);

                }, false);
                Group_UI.AddButton("Give Pickup", $"Send a Udon Event <color=red>(Custom Only NOT NETWORKING)</color>", () =>
                {
                    GameUtils.VRChatUtils.VRCKeyboard.InputPopup("Enter a Pickup Name", (x) =>
                    {
                        var pickup = GameUtils.World.SDK3.Udon.Pickups.GetPickup(x);
                        pickup.SetOwnerAndGivePickupToLocalEntity();
                    }, Placeholder: "Such as: Revolver", MultiLine: false);

                }, false);

                Group_UI.AddToggle("Block Udon", "<green>ON</color>", "<red>OFF</color>", (x) =>
                {
                    Entry.config.Protection.Udon.isBlocked = !Entry.config.Protection.Udon.isBlocked;
                    Config.SaveToFile(Entry.config, "config.json");
                });

            }
        }
        public class NestedQMExploits
        {
            public VRCPage? Page { get; set; }

            public void Execute()
            {
                Page = new VRCPage("Exploits", true, pageOptions: new() { ShowScrollWheel = false });
                VRCGroupLayout Group_UI = new(Page, "Crash");
                Group_UI.AddToggle("Questie (Crash) <color=red>(Mediocre)</color>", "On", "Off", (x) =>
                {
                    Sploits.playerQuestCrashInternalFunction(x);
                });
            }
        }
        public class NestedQMVisuals
        {
            public VRCPage? Page { get; set; }

            public void Execute()
            {
                Page = new VRCPage("Visuals", true, pageOptions: new() { ShowScrollWheel = false });
                VRCGroupLayout Group_UI = new(Page, isCollapsible: false);
                VRCModalContainer? Visuals_RenderModal = new("Visuals+Render");

                VRCModalContainer? VisualsEspPropertiesModal = new("ESP+Properties");
                VRCModalContainer? VisualsWallhacksPropertiesModal = new("Wallhacks+Properties");
                VRCModalContainer? VisualsChamsPropertiesModal = new("Chams+Properties");
                Group_UI.AddButton("Renderer", "", Visuals_RenderModal.Open, true);

                #region ModalRenderer


                Visuals_RenderModal.AddMiniButton("ESP Properties", VisualsEspPropertiesModal.Open);
                Visuals_RenderModal.AddMiniButton("Wallhacks Properties", VisualsWallhacksPropertiesModal.Open);
                Visuals_RenderModal.AddMiniButton("Chams Properties", VisualsChamsPropertiesModal.Open);



                #region Modal_Main
                Visuals_RenderModal.AddToggle("ESP", "", "", (x) =>
                {
                    Entry.config.Render.Overlays.ESP.Enabled = !Entry.config.Render.Overlays.ESP.Enabled;
                });

                //     Visuals_RenderModal.AddToggle("<color=#C278A7>[</color><color=#B23E7B>W</color><color=#A30550>I</color><color=#C23E6D>P</color><color=#E1778A>]</color> ESP Pickups", "", "", (x) =>
                //    {
                //        ESP.Instance.FX_Pickups.enabled = !Entry.config.Render.Overlays.ESP.Enabled;
                //    });

                #endregion

                #endregion

                #region Modal_Properties-ESP
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
            NestedQMExploits exploits = new(); exploits.Execute();

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
            mainGroupLayout.AddButton("Exploits", "Visuals", exploits.Page.Open, true);
            VRCCarouselButton vcb = new(mainGroupLayout, "Fly", "ice", null);

            #endregion

            #region Movement_Modal
            Toggles.Player.Fly = PlayerMovementModal.AddToggle("Flight", "Enable Flight", "Disable Flight", (x) =>
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


            VRCPanelGroupLayout vRCPanelGroupLayout = new(MainPage, 0, "Overhaul");
            vRCPanelGroupLayout.AddTitle("Test Title");
            VRCButtonOperator vRCButtonOperator = new(vRCPanelGroupLayout, new()
            {
                _title = "Title",
                _maintooltip = "Main_Tooltip",
                _text = "Text",
                _subtitle = "Subtitle",
                _tooltip = "tooltip"

            }, null);

            var radial = new BlanketSDK.QM.Elements.Controls.V2.VRCRadialSelect(vRCPanelGroupLayout, "Test");

            radial.AddChoice("Example 1", "This does jack 1", null);
            radial.AddChoice("Example 2", "This does jack 2", null);
            radial.AddChoice("Example 3", "This does jack 3", null);
            vRCPanelGroupLayout.AddSeperator();

            new BlanketSDK.QM.Elements.Controls.V2.VRCToggleOperator(vRCPanelGroupLayout._verticalLayoutGroup, "elements", "on", "off", (x) => MelonLogger.Msg($"Bool Set: {x} "));
            VRCPanelFoldout vRCPanelFoldout = new(vRCPanelGroupLayout._verticalLayoutGroup, "Earth Elements", "Earth Elements");

            new BlanketSDK.QM.Elements.Controls.V2.VRCToggleOperator(vRCPanelFoldout._verticalLayoutGroup, "water", "on", "off", (x) => MelonLogger.Msg($"Bool Set: {x} "));
            vRCPanelFoldout.AddSeperator();
            new BlanketSDK.QM.Elements.Controls.V2.VRCToggleOperator(vRCPanelFoldout._verticalLayoutGroup, "fire", "on", "off", (x) => MelonLogger.Msg($"Bool Set: {x} "));

        }
    }


    public class WorldCheats
    {
        public class NestedQMSelectedMurder4
        {
            public VRCPage? Page { get; set; }

            public void Execute()
            {
                Page = new VRCPage("M4", true, pageOptions: new() { ShowScrollWheel = false });
                VRCGroupLayout Group_UI = new(Page);
                VRCGroupLayout Group_Roles = new(Page, $"Events");
                Group_UI.AddButton("Kill", "", () =>
                {
                    var selectedPlayer = GameUtils.Gameplay.Matchmaking.GetSelectedUser();
                    Murder_4.NetworkEvents.GameManager.Logic.PunishmentReceptors.Kill(selectedPlayer);

                }, false);
                Group_Roles.AddButton("Assign Murderer", "", () =>
                {
                    var selectedPlayer = GameUtils.Gameplay.Matchmaking.GetSelectedUser();
                    Murder_4.NetworkEvents.GameManager.Roles.AssignMurderer(selectedPlayer);

                }, false);
                Group_Roles.AddButton("Assign Detective", "", () =>
                {
                    var selectedPlayer = GameUtils.Gameplay.Matchmaking.GetSelectedUser();
                    Murder_4.NetworkEvents.GameManager.Roles.AssignDetective(selectedPlayer);
                }, false);
                Group_Roles.AddButton("Assign Bystander", "", () =>
                {
                    var selectedPlayer = GameUtils.Gameplay.Matchmaking.GetSelectedUser();
                    Murder_4.NetworkEvents.GameManager.Roles.AssignBystander(selectedPlayer);

                }, false);

            }
        }
        public class NestedQMWorld
        {
            public VRCPage Page { get; set; }

            public void Execute()
            {
                var Murder4_Page = new VRCPage("Murder 4", true, pageOptions: new() { ShowScrollWheel = false });
                Page = new VRCPage("World", true, pageOptions: new() { ShowScrollWheel = false });

                VRCGroupLayout Group_UI = new(Page, isCollapsible: false);
                Group_UI.AddToggle("Udon Nuker", $"<color=#grey>(Sometimes)</color> Rapes Udon Games... <color=green>On</color>", "  <color=grey>(Sometimes) Rapes Udon Games...</color> <color=red>Off</color>", (x) =>
                     {
                         switch (x)
                         {

                             case true:
                                 Global.Start();
                                 break;
                             case false:
                                 Global.Stop();
                                 break;
                         }

                     });
                Group_UI.AddButton("Murder 4", "<color=red>Murder 4 Utilties</color>", Murder4_Page.Open, true);

                VRCGroupLayout Group_UI_Murder4 = new(Murder4_Page, isCollapsible: false);
                Group_UI_Murder4.AddToggle("RapidFire", "Enable RapidFire", "Disable RapidFire", (x) =>
                {
                    Murder_4.Modules.RapidFire(x);

                });
                Group_UI_Murder4.AddButton("Spawn Revolver", "Spawns Revolver", () =>
                {
                    Murder_4.Weapons.SpawnRevolver();

                },false);
                Group_UI_Murder4.AddButton("Spawn Knife", "Spawns Knife", () =>
                {
                    Murder_4.Weapons.SpawnKnife();

                }, false);
                Group_UI_Murder4.AddButton("Spawn Luger", "Spawns Luger", () =>
                {
                    Murder_4.Weapons.Special.SpawnLuger();

                }, false);
                Group_UI_Murder4.AddButton("Spawn Shotgun", "Spawns Shotgun", () =>
                {
                    Murder_4.Weapons.Special.SpawnShotgun();

                }, false);
            }
        }
    }
    public class SelectedUserMenu : IQuickMenuElements
    {
            public static void Apply()
            {
                WorldCheats.NestedQMSelectedMurder4 m4 = new(); m4.Execute();
                QMElements.SelectedUser.SelectedUser_ButtonContainer().AddButton("<color=#B80000>Murder 4</color>", "000", m4.Page.Open);
            }
        }
    }