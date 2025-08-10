using System;
using CoreRuntime.Manager;
using VRC.Core;
using VRC;
using autumn.ml.Cheat.Modules.Local;

namespace HexedBase.Patches
{
    internal class PlayerLeavePatch
    {
        private delegate void _PlayerLeaveDelegate(IntPtr instance, IntPtr player);
        private static _PlayerLeaveDelegate originalMethod;

        public static void ApplyPatch()
        {
            originalMethod = HookManager.Detour<_PlayerLeaveDelegate>(
                typeof(NetworkManager).GetMethod("Method_Internal_Void_Player_PDM_0"), 
                Patch
            );
        }

        private static void Patch(IntPtr instance, IntPtr playerPtr)
        {
            originalMethod(instance, playerPtr);
            
            Player player = playerPtr == IntPtr.Zero ? null : new Player(playerPtr);
            if (player == null) return;
            
            switch (player.APIUser.IsSelf)
            {
                case true:
                    ESP.Instance.ClearAll();
                    break;
                case false:
                    ESP.Instance.Remove(player);
                    break;
            }
        }
    }
}