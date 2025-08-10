using System;
using CoreRuntime.Manager;
using VRC;
using VRC.Core;

namespace HexedBase.Patches
{
    internal class RFCEventPatch
    {
        private delegate void _RFCEventDelegate(IntPtr instance, IntPtr player, IntPtr vrcEvent, IntPtr broadcastType, int param3, float param4);
        private static _RFCEventDelegate originalMethod;

        public static void ApplyPatch()
        {
            originalMethod = HookManager.Detour<_RFCEventDelegate>(
                typeof(VRC_EventDispatcherRFC).GetMethod("Method_Public_Void_Player_VrcEvent_VrcBroadcastType_Int32_Single_0"), 
                Patch
            );
        }

        private static void Patch(IntPtr instance, IntPtr playerPtr, IntPtr vrcEventPtr, IntPtr broadcastTypePtr, int param3, float param4)
        {
            Player player = playerPtr == IntPtr.Zero ? null : new Player(playerPtr);
            VRC_EventHandler.VrcEvent vrcEvent = vrcEventPtr == IntPtr.Zero ? null : new VRC_EventHandler.VrcEvent(vrcEventPtr);
            VRC_EventHandler.VrcBroadcastType broadcastType = broadcastTypePtr == IntPtr.Zero ? VRC_EventHandler.VrcBroadcastType.AlwaysUnbuffered : new VRC_EventHandler.VrcBroadcastType(broadcastTypePtr);
            
            if (Entry.config.Modules.RFCTampering.SDK2.EnforceToAll)
            {
                if (player?.VRCPlayerApi?.displayName == APIUser.CurrentUser?.displayName)
                {
                    broadcastType = VRC_EventHandler.VrcBroadcastType.AlwaysUnbuffered;
                }
            }
            
            originalMethod(instance, playerPtr, vrcEventPtr, broadcastType.Pointer, param3, param4);
        }
    }
}