using System;
using CoreRuntime.Manager;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Core;

namespace HexedBase.Patches
{
    internal class LoadingOverlayPatch
    {
        private delegate void _LoadingOverlayStartDelegate(IntPtr instance);
        private static _LoadingOverlayStartDelegate originalMethod;
        private static bool _isFlatOverlayLoaded = false;

        public static void ApplyPatch()
        {
            originalMethod = HookManager.Detour<_LoadingOverlayStartDelegate>(
                typeof(LoadingOverlayController).GetMethod(nameof(LoadingOverlayController.Start)), 
                Patch
            );
        }

        private static void Patch(IntPtr instance)
        {
            originalMethod(instance);
            
            LoadingOverlayController controller = instance == IntPtr.Zero ? null : new LoadingOverlayController(instance);
            if (controller == null || _isFlatOverlayLoaded) return;
            
            _isFlatOverlayLoaded = true;
            
            Image Background = controller.gameObject
                .transform.Find("FlatLoadingOverlay(Clone)/Container")
                .transform.FindChild("Canvas/Background").GetComponent<Image>();

            if (Entry.config.Appearance.inGame.HideLoadingOverlay)
            {
                UnityEngine.Object.DestroyImmediate(Background, true);
                return;
            }

            if (Entry.config.Appearance.inGame.FlatLoadingOverlay.Image != null)
            {
                Background.color = Entry.config.Appearance.inGame.FlatLoadingOverlay.Base;
                GameObject Custom_Background_Overlay_Obj = Background.gameObject.CreateIsolatedObject(true);
                Image Custom_Background_Overlay = Custom_Background_Overlay_Obj.GetComponent<Image>();
                
                Custom_Background_Overlay.sprite = EngineUtils.Assets.Sprite(new Uri(Entry.config.Appearance.inGame.FlatLoadingOverlay.Image));
                Custom_Background_Overlay.color = Entry.config.Appearance.inGame.FlatLoadingOverlay.Tint;
            }
        }
    }
}