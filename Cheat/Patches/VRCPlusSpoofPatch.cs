using System;
using CoreRuntime.Manager;
using VRC.Core;

namespace HexedBase.Patches
{
    internal class VRCPlusSpoofPatch
    {
        private delegate IntPtr _VRCPlusStatusDelegate(IntPtr instance);
        private static _VRCPlusStatusDelegate originalMethod;

        public static void ApplyPatch()
        {
            originalMethod = HookManager.Detour<_VRCPlusStatusDelegate>(
                typeof(VRCPlusStatus).GetProperty("prop_Object1PublicTBoTUnique_1_Boolean_0").GetGetMethod(), 
                Patch
            );
        }

        private static IntPtr Patch(IntPtr instance)
        {
            IntPtr result = originalMethod(instance);
            
            if (result != IntPtr.Zero && !APIUser.CurrentUser.isSupporter)
            {
                Object1PublicTBoTUnique<bool> statusObject = new Object1PublicTBoTUnique<bool>(result);
                statusObject.field_Protected_T_0 = true;
                return statusObject.Pointer;
            }
            
            return result;
        }
    }
}