using System;
using CoreRuntime.Manager;
using VRC.Core;
using VRC;
using Brewchat.Game.Monobehaviours;
using autumn.ml.Cheat.Modules.Local;
using Brewchat.VRCUI;
using VRRecords;

namespace HexedBase.Patches
{
    internal class PlayerJoinPatch
    {
        private delegate void _PlayerJoinDelegate(IntPtr instance, IntPtr player);
        private static _PlayerJoinDelegate originalMethod;

        public static void ApplyPatch()
        {
            originalMethod = HookManager.Detour<_PlayerJoinDelegate>(
                typeof(NetworkManager).GetMethod("Method_Internal_Void_Player_0"), 
                Patch
            );
        }

        private static void Patch(IntPtr instance, IntPtr playerPtr)
        {
            originalMethod(instance, playerPtr);
            
            Player player = playerPtr == IntPtr.Zero ? null : new Player(playerPtr);
            if (player == null) return;
            
            ExternPlayerModule externModule = player.gameObject.GetOrAddComponentSafe<ExternPlayerModule>();
            Nameplates nameplate = player.gameObject.GetOrAddComponentSafe<Nameplates>();
            externModule.Player = player;
            externModule.Nameplate = nameplate;
            
            switch (player.APIUser.IsSelf)
            {
                case true:
                    CSS.Online();
                    break;
                case false:
                    ESP.Instance?.Add(player);
                    break;
            }
            
            var fake = VRRecordsAPI.ReadPlayersData(externModule);
        }
    }
}