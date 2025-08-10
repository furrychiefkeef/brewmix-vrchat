using System;
using CoreRuntime.Manager;
using VRC.Udon.Wrapper.Modules;

namespace HexedBase.Patches
{
    internal class UdonDropPickupPatch
    {
        private delegate void _UdonDropPickupDelegate(IntPtr instance);
        private static _UdonDropPickupDelegate originalMethod;

        public static void ApplyPatch()
        {
            originalMethod = HookManager.Detour<_UdonDropPickupDelegate>(
                typeof(ExternVRCSDK3ComponentsVRCPickup).GetMethod("__Drop__SystemVoid"), 
                Patch
            );
        }

        private static void Patch(IntPtr instance)
        {
            // Block the drop by not calling original method
            ExternVRCSDK3ComponentsVRCPickup pickup = instance == IntPtr.Zero ? null : new ExternVRCSDK3ComponentsVRCPickup(instance);
            if (pickup == null) return;
            
            // Don't call originalMethod to prevent dropping
            Console.WriteLine("Blocked pickup drop");
        }
    }
}