using System;
using CoreRuntime.Manager;
using VRC.Core;

namespace HexedBase.Patches
{
    internal class AvatarCopyPatch
    {
        private delegate bool _AllowAvatarCopyingDelegate(IntPtr instance);
        private static _AllowAvatarCopyingDelegate originalMethod;

        public static void ApplyPatch()
        {
            originalMethod = HookManager.Detour<_AllowAvatarCopyingDelegate>(
                typeof(APIUser).GetProperty(nameof(APIUser.allowAvatarCopying)).GetGetMethod(), 
                Patch
            );
        }

        private static bool Patch(IntPtr instance)
        {
            // Call original method first
            originalMethod(instance);
            
            // Always return true to force allow avatar copying
            return true;
        }
    }
}