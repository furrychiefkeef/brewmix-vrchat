using System.Collections;
using BlanketSDK.Utilities;
using Brewchat.Game.Wrappers;
using TMPro;
using UnhollowerBaseLib.Runtime.VersionSpecific.Class;
using UnityEngine;
using VRC.Networking;
using VRC.Networking.Tween;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common;

namespace autumn.ml.Cheat.Modules.World;

public class Murder_4
{
    public static VRC.Networking.UdonSync RevolverData { get; set; }

    public class Weapons
    {
        public static void SpawnRevolver()
        {
            VRCPickup? pickupObj = GameUtils
            .World
            .SDK3.Udon.Pickups.GetPickup("Revolver");
            pickupObj?.SetOwnerAndGivePickupToLocalEntity();
        }
        public static void SpawnKnife()
        {
            int i = 0;
            VRCPickup? pickupObj = GameUtils
            .World
            .SDK3.Udon.Pickups.GetPickup($"Knife ({i++})");
            pickupObj?.SetOwnerAndGivePickupToLocalEntity();
        }
        public struct Special
        {
            public static void SpawnLuger()
            {
                VRCPickup? pickupObj = GameUtils
                .World
                .SDK3.Udon.Pickups.GetPickup("Luger");
                 pickupObj?.SetOwnerAndGivePickupToLocalEntity();
               
            }
            public static void SpawnShotgun()
            {
                VRCPickup? pickupObj = GameUtils
                .World
                .SDK3.Udon.Pickups.GetPickup("Shotgun");
                pickupObj?.SetOwnerAndGivePickupToLocalEntity();

            }  

            }
            
        }
        
    public class NetworkEvents
    {
            public struct GameManager
            {
                public struct Logic
                {
                    
                    public static void VictoryBystanderEnding() => GameUtils.World.SDK3.Udon.LoadCustomEvent("SyncVictoryB");
                    public static void VictoryMurderEnding() => GameUtils.World.SDK3.Udon.LoadCustomEvent("SyncVictoryM");
                    public static void AbortGame() => GameUtils.World.SDK3.Udon.LoadCustomEvent("SyncAbort");
                    public static void StartGame() => GameUtils.World.SDK3.Udon.LoadCustomEvent("SyncCountdown");
                    public struct PunishmentReceptors
                    {
                        public static void Kill(VRC.Player player)
                        {
                            if(player != null)
                            {
                                PlayerNode.Wrapper.GetPlayerNode(player).SendCustomNetworkEvent(0, "SyncKill");
                                if(Entry.config.Advanced.MurderPlayerNode)
                                {
                                    GameUtils.UI.HUD.ToastV1($"You Sent Unfair UdonEvent [SyncKill]: {player.VRCPlayerApi.playerId}!");
                                    
                                }
                            }      
                        }
                        public static void Blind(VRC.Player player)
                        {
                            if(player != null)
                            {
                                PlayerNode.Wrapper.GetPlayerNode(player).SendCustomNetworkEvent(0, "SyncKill");
                                if(Entry.config.Advanced.MurderPlayerNode)
                                {
                                    GameUtils.UI.HUD.ToastV1($"You Sent Unfair UdonEvent [SyncBlindPenatlity]: {player.VRCPlayerApi.playerId}!");
                                }
                            }      
                        }
                    }
                   
                }
                public struct Roles
                {
                        public static void AssignMurderer(VRC.Player player) 
                        {
                           if(player != null)
                           {
                                PlayerNode.Wrapper.GetPlayerNode(player).SendCustomNetworkEvent(0, "SyncAssignM");
                                if(Entry.config.Advanced.DebugMode)
                                {
                                    GameUtils.UI.HUD.ToastV1($"You Enforced Murderer onto: {player.VRCPlayerApi.playerId}");
                                }
                           }                            
                            
                        }
                        public static void AssignDetective(VRC.Player player) 
                        {
                           if(player != null)
                           {
                                PlayerNode.Wrapper.GetPlayerNode(player).SendCustomNetworkEvent(0, "SyncAssignD");
                                if(Entry.config.Advanced.DebugMode)
                                {
                                    GameUtils.UI.HUD.ToastV1($"You Enforced Detective onto: {player.VRCPlayerApi.playerId}");
                                }
                           }                            
                            
                        }
                        public static void AssignBystander(VRC.Player player) 
                        {
                           if(player != null)
                           {
                                PlayerNode.Wrapper.GetPlayerNode(player).SendCustomNetworkEvent(0, "SyncAssignB");
                                if(Entry.config.Advanced.DebugMode)
                                {
                                    GameUtils.UI.HUD.ToastV1($"You Enforced Bystander onto: {player.VRCPlayerApi.playerId}");
                                }
                           }                            
                        }   
                }
                 
            }
            public struct Config
            {         
                    // public static void VictoryBystander() => GameUtils.World.SDK3.Udon.LoadCustomEvent("SyncVictoryB");
                    public static void Toggle_ShowKnives() => GameUtils.World.SDK3.Udon.LoadCustomEvent("ShouldShowKnives");
                    public static void Toggle_CanUseSnakeDispensers() => GameUtils.World.SDK3.Udon.LoadCustomEvent("CanUseSnakeDispensers");
            }
            public struct PlayerNode 
            { 
                public class Wrapper
                {
                    public static int GetPlayersNodeNumber(VRC.Player player, bool isLog = false)
                    {
                        var playerList = GameObject.Find("Game Logic/Game Canvas/Game In Progress/Player List/Player List Group");
                        var playerNodes = GameObject.Find("Game Logic/Player Nodes");
                    
                        foreach(GameObject playerEntry in playerList.transform.GetChildren())
                        {
                            // Extract index from Player Entry name (e.g., "Player Entry (5)" -> 5)
                            var match = System.Text.RegularExpressions.Regex.Match(playerEntry.name, @"\((\d+)\)");
                            if(match.Success)
                            {
                                int entryIndex = int.Parse(match.Groups[1].Value);
                            
                                // Find matching Player Node with same index
                                var playerNode = playerNodes.transform.Find($"Player Node ({entryIndex})"); // Changed to Find and fixed format
                            
                                if(playerNode != null) // Just check if node exists
                                {  
                                    if (playerEntry.GetComponentInChildren<TextMeshProUGUI>()?.text == player?.APIUser.displayName)
                                    {
                                        if(isLog) GameUtils.UI.HUD.Alert($"Player: {player.VRCPlayerApi.displayName}, Actor: {player.VRCPlayerApi.playerId}, Node: {playerNode.name}");
                                        return entryIndex;
                                    }
                                }
                            }
                            if(isLog) GameUtils.UI.HUD.Alert($"Player not found in any node.");
       
                        }
                        return 0;
                    
                    }
                    public static UdonBehaviour GetPlayerNode(VRC.Player player, bool isLog = false)
                    {
                        var playerList = GameObject.Find("Game Logic/Game Canvas/Game In Progress/Player List/Player List Group");
                        var playerNodes = GameObject.Find("Game Logic/Player Nodes");
                    
                        foreach(GameObject playerEntry in playerList.transform.GetChildren())
                        {
                            // Extract index from Player Entry name (e.g., "Player Entry (5)" -> 5)
                            var match = System.Text.RegularExpressions.Regex.Match(playerEntry.name, @"\((\d+)\)");
                            if(match.Success)
                            {
                                int entryIndex = int.Parse(match.Groups[1].Value);
                            
                                // Find matching Player Node with same index
                                var playerNode = playerNodes.transform.Find($"Player Node ({entryIndex})"); // Changed to Find and fixed format
                            
                                if(playerNode != null) // Just check if node exists
                                {  
                                    if (playerEntry.GetComponentInChildren<TextMeshProUGUI>()?.text == player?.APIUser.displayName)
                                    {
                                        if(isLog) GameUtils.UI.HUD.Alert($"Player: {player.VRCPlayerApi.displayName}, Actor: {player.VRCPlayerApi.playerId}, Node: {playerNode.name}");
                                        return playerNode.gameObject.GetComponent<UdonBehaviour>();
                                    }
                                }
                            }
                            if(isLog) GameUtils.UI.HUD.Alert($"Player not found in any node.");     
                        }
                        return null;
                    
                    }  
                }
            }
            public struct Commerence
            {
                public static void PateronPerks(bool state)
                {
                    UdonBehaviour? luger = GameUtils.World.SDK3.Udon.Pickups.GetPickup("Luger")?.GetComponent<UdonBehaviour>();
                    UdonBehaviour? revolver = GameUtils.World.SDK3.Udon.Pickups.GetPickup("Revolver")?.GetComponent<UdonBehaviour>();
                    UdonBehaviour? shotgun = GameUtils.World.SDK3.Udon.Pickups.GetPickup("Shotgun")?.GetComponent<UdonBehaviour>();
                    switch (state)
                    {
                        case true:
                            luger?.SetVariable<bool>("isPatreon", true);
                            revolver?.SetVariable<bool>("isPatreon", true);
                            shotgun?.SetVariable<bool>("isPatreon", true);
                            break;
                        case false:
                            break;
                    }
                    GameUtils.UI.HUD.ToastV1($"RPateron is Set: {(state ? "enabled" : "disabled")}!");
                }
            }     
        
    }
    public class Modules
    {
        public static void RapidFire(bool state)
        {
            UdonBehaviour? luger = GameUtils.World.SDK3.Udon.Pickups.GetPickup("Luger")?.GetComponent<UdonBehaviour>();
            UdonBehaviour? revolver = GameUtils.World.SDK3.Udon.Pickups.GetPickup("Revolver")?.GetComponent<UdonBehaviour>();
            UdonBehaviour? shotgun = GameUtils.World.SDK3.Udon.Pickups.GetPickup("Shotgun")?.GetComponent<UdonBehaviour>();
            switch (state)
            {
                case true:
                    luger?.SetVariable<float>("fireInterval", 0.0f);
                    revolver?.SetVariable<float>("fireInterval", 0.0f);
                    shotgun?.SetVariable<float>("fireInterval", 0.0f);
                    break;
                case false:
                    break;
            }
            GameUtils.UI.HUD.ToastV1($"Rapid Fire: {(state ? "enabled" : "disabled")}!");
        }
    }
}
 





        

     
    
    

  
 