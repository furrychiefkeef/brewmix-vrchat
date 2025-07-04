using System;
using UnityEngine;
using VRC.SDKBase;

namespace autumn.ml.Cheat.Modules.UsersOnly;

public class VPC
{
    public string UserName { get; set; }
    public static GameObject? RPCManager_Visionary { get; set; }

    public static void SendRPC(RPC.Destination destination, string eventName, object[] parameters)
    {
        if (RPCManager_Visionary == null)
        {
            RPCManager_Visionary = new GameObject("RPCManager_Visionary");
            return;
        }
        Networking.RPC(destination, RPCManager_Visionary, "VisonaryTELEPORT", parameters.Cast<Il2CppSystem.Object>().ToArray());
    }
    public static void PutLinkOnPlayer()
    {
       
    }
}
