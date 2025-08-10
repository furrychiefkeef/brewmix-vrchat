using System;
using CoreRuntime.Manager;
using VRC.Udon;
using MelonLoader;

namespace HexedBase.Patches
{
    internal class UdonEventPatch
    {
        private delegate void _UdonSendCustomEventDelegate(IntPtr instance, IntPtr eventName);
        private static _UdonSendCustomEventDelegate originalMethod;

        public static void ApplyPatch()
        {
            originalMethod = HookManager.Detour<_UdonSendCustomEventDelegate>(
                typeof(UdonBehaviour).GetMethod(nameof(UdonBehaviour.SendCustomEvent)), 
                Patch
            );
        }

        private static void Patch(IntPtr instance, IntPtr eventName)
        {
            UdonBehaviour udonBehaviour = instance == IntPtr.Zero ? null : new UdonBehaviour(instance);
            if (udonBehaviour == null)
            {
                originalMethod(instance, eventName);
                return;
            }
            
            string eventNameStr = IL2CPP.Il2CppStringToManaged(eventName);
            
            if (Entry.config.Logging.Game.UdonObjects)
            {
                MelonLogger.Msg($"[Udon]: [{udonBehaviour.name}] -> {eventNameStr}");
            }
            
            originalMethod(instance, eventName);
        }
    }
}