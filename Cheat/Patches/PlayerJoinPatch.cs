using System;
using CoreRuntime.Manager;
using VRC.Core;
using Brewchat.Game.Monobehaviours;
using autumn.ml.Cheat.Modules.Local;
using Brewchat.VRCUI;
using VRRecords;
using autumn.ml.Game.Monobehaviours;
using Brewchat.Cheat.Modules;
using Brewchat.Game.Wrappers;
using brewchat.hybridxcore.bep.Game.Monobehaviours;

namespace HexedBase.Patches
{
    internal class PlayerJoinPatch
    {
        private delegate void _PlayerJoinDelegate(IntPtr instance, IntPtr player);
        private static _PlayerJoinDelegate originalMethod;

        public static void ApplyPatch()
        {
            var methodInfo = typeof(NetworkManager).GetMethod("Method_Internal_Void_Player_0");
            originalMethod = HookManager.Detour<_PlayerJoinDelegate>(methodInfo.MethodHandle.GetFunctionPointer(), Patch);
        }

        private static void Patch(IntPtr instance, IntPtr playerPtr)
        {
            // Call original first
            originalMethod(instance, playerPtr);

            // Cast pointer to VRC.Player
            VRC.Player player = playerPtr == IntPtr.Zero ? null : new VRC.Player(playerPtr);
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