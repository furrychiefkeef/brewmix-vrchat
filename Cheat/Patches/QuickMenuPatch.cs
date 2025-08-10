using System;
using CoreRuntime.Manager;
using VRC.UI.Elements.Menus;
using VRC.UI.Elements;
using UnityEngine;
using autumn.ml.Cheat.Modules.Local;
using Brewchat.VRCUI;

namespace HexedBase.Patches
{
    internal class QuickMenuPatch
    {
        private delegate void _QuickMenuStartDelegate(IntPtr instance);
        private static _QuickMenuStartDelegate originalMethod;
        private static bool _isQuickMenuLoaded = false;

        public static void ApplyPatch()
        {
            originalMethod = HookManager.Detour<_QuickMenuStartDelegate>(
                typeof(QuickMenu).GetMethod(nameof(QuickMenu.Start)), 
                Patch
            );
        }

        private static void Patch(IntPtr instance)
        {
            originalMethod(instance);
            
            QuickMenu quickMenu = instance == IntPtr.Zero ? null : new QuickMenu(instance);
            if (quickMenu == null || _isQuickMenuLoaded) return;
            
            UnityEngine.Object.DestroyImmediate(UnityEngine.Object.FindObjectOfType<MainMenuContent>()._bannerCarousel, true);
            _isQuickMenuLoaded = true;
            
            quickMenu.gameObject.GetOrAddComponentSafe<ESP>();
            quickMenu.gameObject.GetOrAddComponentSafe<Wallhacks>();

            QM.Main.Apply();
            Widgets.QM.Initalize();
        }
    }
}