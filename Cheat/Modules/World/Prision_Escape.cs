using System.Collections;
using BlanketSDK.Utilities;
using Brewchat.Game.Wrappers;
using MelonLoader;
using TMPro;
using UnhollowerBaseLib.Runtime.VersionSpecific.Class;
using UnityEngine;
using VRC.Networking;
using VRC.Networking.Tween;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;

namespace autumn.ml.Cheat.Modules.World;

public class Prision_Escape
{
    public static VRC.Networking.UdonSync RevolverData { get; set; }

    public class Weapons
    {
        public static VRCPickup? SpawnRevolver()
        {
            VRCPickup? pickupObj = GameUtils
            .World
            .SDK3.Udon.Pickups.GetPickup("Revolver");
           pickupObj.SetOwnerAndGivePickupToLocalEntity();
            return pickupObj;
        }
        public static VRCPickup? SpawnKnife()
        {
            int i = 0;
            VRCPickup pickupObj = GameUtils
            .World
            .SDK3.Udon.Pickups.GetPickup($"Knife ({i++})");
           pickupObj.SetOwnerAndGivePickupToLocalEntity();
             return pickupObj;
        }
        public static VRCPickup? SpawnShotgun()
        {
            VRCPickup pickupObj = GameUtils
            .World
            .SDK3.Udon.Pickups.GetPickup($"Shotgun");
           pickupObj.SetOwnerAndGivePickupToLocalEntity();
            return pickupObj;
         }
        public static VRCPickup? SpawnSniper()
        {
            VRCPickup pickupObj = GameUtils
            .World
            .SDK3.Udon.Pickups.GetPickup($"Sniper");
            pickupObj.SetOwnerAndGivePickupToLocalEntity();
            pickupObj.gameObject.SetActive(true);
            return pickupObj;
         }

        public struct Special
        {
            public static void SpawnBaton()
            {

                VRCPickup pickupObj = GameUtils
                .World
                .SDK3.Udon.Pickups.GetPickup("Baton");
               pickupObj.SetOwnerAndGivePickupToLocalEntity();
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
                    public static void Spawn() => GameUtils.World.SDK3.Udon.LoadCustomEvent("_SpawnPlayer");
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
                        public static void AssignGuard(VRC.Player player) 
                        {
                           if(player != null)
                           {
                                PlayerNode.Wrapper.GetPlayerNode(player).SendCustomNetworkEvent(0, "SyncAssignM");
                                if(Entry.config.Advanced.MurderPlayerNode)
                                {
                                    GameUtils.UI.HUD.ToastV1($"You Enforced Murderer onto: {player.VRCPlayerApi.playerId}");
                                }
                           }                            
                            
                        }
                        public static void AssignPrisioners(VRC.Player player) 
                        {
                           if(player != null)
                           {
                                PlayerNode.Wrapper.GetPlayerNode(player).SendCustomNetworkEvent(0, "SyncAssignD");
                                if(Entry.config.Advanced.MurderPlayerNode)
                                {
                                    GameUtils.UI.HUD.ToastV1($"You Enforced Detective onto: {player.VRCPlayerApi.playerId}");
                                }
                           }                            
                            
                        }
                }
                 
            }
            public struct Config
            {         
                    // public static void VictoryBystander() => GameUtils.World.SDK3.Udon.LoadCustomEvent("SyncVictoryB");
                    // public static void Toggle_ShowKnives() => GameUtils.World.SDK3.Udon.LoadCustomEvent("ShouldShowKnives");
                    // public static void Toggle_CanUseSnakeDispensers() => GameUtils.World.SDK3.Udon.LoadCustomEvent("CanUseSnakeDispensers");
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
                            if(!isLog) GameUtils.UI.HUD.Alert($"Player not found in any node.");     
                        }
                        return null;
                    
                    }  
                }
            }
            public struct Commerence
            {
                public static void PateronPerks()
                {

                }
            }     
        }





    }

    
 





        

     
    
    

  
 