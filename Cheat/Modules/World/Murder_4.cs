using Brewchat.Game.Wrappers;
using UnhollowerBaseLib.Runtime.VersionSpecific.Class;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;

namespace autumn.ml.Cheat.Modules.World;

public class Murder_4
{
    public static VRC.Networking.UdonSync RevolverData { get; set; }

    public class Weapons
    {
        public static void SpawnRevolver()
        {
            VRCPickup? pickupObj = GameUtils.World.SDK3.Pickups.GetPickup("Revolver");
            GameUtils.PlayerModel.Motion.Networking.World.SetOwnerAndGivePickupLocalEntity(pickupObj);
        }
        public static void SpawnKnife()
        {
            VRCPickup pickupObj = GameUtils.World.SDK3.Pickups.GetPickup("Knife");
            GameUtils.PlayerModel.Motion.Networking.World.SetOwnerAndGivePickupLocalEntity(pickupObj);
        }
    }
    public class NetworkEvents
    {

    }
    
}