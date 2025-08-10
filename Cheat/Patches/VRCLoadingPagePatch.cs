using System;
using CoreRuntime.Manager;
using VRC.UI.Elements;
using Brewchat.VRCUI;
using Brewchat.Cheat.Modules;

namespace HexedBase.Patches
{
    internal class VRCLoadingPagePatch
    {
        private delegate void _VRCUiPageLoadingStartDelegate(IntPtr instance);
        private static _VRCUiPageLoadingStartDelegate originalMethod;

        public static void ApplyPatch()
        {
            originalMethod = HookManager.Detour<_VRCUiPageLoadingStartDelegate>(
                typeof(VRCUiPageLoading).GetMethod(nameof(VRCUiPageLoading.Start)), 
                Patch
            );
        }

        private static void Patch(IntPtr instance)
        {
            originalMethod(instance);
            
            VRCUiPageLoading loadingPage = instance == IntPtr.Zero ? null : new VRCUiPageLoading(instance);
            if (loadingPage == null) return;
            
            CSS.SetLoadingAudio();
            CSS.Offline();
            AppearanceManager.Custom.SetLoadingBundle();
        }
    }
}