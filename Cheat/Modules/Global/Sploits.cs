using System;
using brewchat.hybridxcore.bep.Game.Wrappers;
using Brewchat.Game.Wrappers;
using ExitGames.Client.Photon;
using VRC.SDKBase;

namespace autumn.ml.Cheat.Modules.Global;

public class Sploits
{
    public static void event1_voiceData(string packet)
    {
        byte[] payload = Convert.FromBase64String(packet);
        byte[] serverTime = BitConverter.GetBytes(Networking.GetServerTimeInMilliseconds());
        Buffer.BlockCopy(serverTime, 0, payload, 0, 4);
        GameUtils.Gameplay.PhotonUtils.SendRaiseEvent((byte)GameUtils.Gameplay.PhotonUtils.Events.VoiceData, payload, SendOptions.SendUnreliable);
    }
}
