using System;
using CoreRuntime.Manager;
using VRC.Core;
using autumn.ml.Initalizers;
using autumn.ml.Game.Monobehaviours;
using Brewchat.VRCUI;

namespace HexedBase.Patches
{
    internal class VRCApplicationAwakePatch
    {
        private delegate void _VRCApplicationAwakeDelegate(IntPtr instance);
        private static _VRCApplicationAwakeDelegate originalMethod;
        private static bool _isVRCApplicationLoaded = false;

        public static void ApplyPatch()
        {
            originalMethod = HookManager.Detour<_VRCApplicationAwakeDelegate>(
                typeof(VRCApplication).GetMethod(nameof(VRCApplication.Awake)), 
                Patch
            );
        }

        private static void Patch(IntPtr instance)
        {
            originalMethod(instance);
            
            VRCApplication vrcApp = instance == IntPtr.Zero ? null : new VRCApplication(instance);
            if (vrcApp == null || _isVRCApplicationLoaded) return;
            
            _isVRCApplicationLoaded = true;

            ConfigSystem.SetConfig();
            Cheat.Execute();
            vrcApp.gameObject.GetOrAddComponentSafe<CSS>();
            vrcApp.gameObject.GetOrAddComponentSafe<KeybindManager>();
        }
    }
}