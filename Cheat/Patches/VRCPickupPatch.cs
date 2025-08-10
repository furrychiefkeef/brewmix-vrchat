using System;
using CoreRuntime.Manager;
using VRC.SDK3.Components;
using autumn.ml.Cheat.Modules.Local;

namespace HexedBase.Patches
{
    internal class VRCPickupPatch
    {
        private delegate void _VRCPickupAwakeDelegate(IntPtr instance);
        private static _VRCPickupAwakeDelegate originalMethod;

        public static void ApplyPatch()
        {
            originalMethod = HookManager.Detour<_VRCPickupAwakeDelegate>(
                typeof(VRC_Pickup).GetMethod(nameof(VRC_Pickup.Awake)), 
                Patch
            );
        }

        private static void Patch(IntPtr instance)
        {
            // Call original first in this case since we're modifying the pickup behavior
            originalMethod(instance);
            
            VRC_Pickup pickup = instance == IntPtr.Zero ? null : new VRC_Pickup(instance);
            if (pickup == null) return;
            
            pickup.DisallowTheft = false;
            pickup.proximity = float.MaxValue;
            pickup.allowManipulationWhenEquipped = true;
            ESP.Instance?.Add(pickup);
        }
    }
}