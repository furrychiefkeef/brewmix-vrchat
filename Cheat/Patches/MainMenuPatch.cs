using System;
using CoreRuntime.Manager;
using VRC.UI.Elements.Menus;
using Brewchat.VRCUI;

namespace HexedBase.Patches
{
    internal class MainMenuPatch
    {
        private delegate void _MainMenuStartDelegate(IntPtr instance);
        private static _MainMenuStartDelegate originalMethod;
        private static bool _isMainMenuLoaded = false;

        public static void ApplyPatch()
        {
            originalMethod = HookManager.Detour<_MainMenuStartDelegate>(
                typeof(MainMenu).GetMethod(nameof(MainMenu.Start)), 
                Patch
            );
        }

        private static void Patch(IntPtr instance)
        {
            originalMethod(instance);
            
            MainMenu mainMenu = instance == IntPtr.Zero ? null : new MainMenu(instance);
            if (mainMenu == null || _isMainMenuLoaded) return;
            
            _isMainMenuLoaded = true;
            
            MM.Main.Apply();
            MM.AvatarAVMMenu.Apply();
            QM.SelectedUserMenu.Apply();
            MM.SelectedWorldMenu.Apply();
            MM.SelectedUserMenu.Apply();
        }
    }
}