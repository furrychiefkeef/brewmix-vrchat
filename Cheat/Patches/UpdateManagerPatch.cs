using System;
using CoreRuntime.Manager;
using VRC.UI.Core;
using Brewchat.Game.Monobehaviours;
using autumn.ml.Cheat.Modules.Local;
using Brewchat.VRCUI;

namespace HexedBase.Patches
{
    internal class UpdateManagerPatch
    {
        private delegate void _VRCUiManagerUpdateDelegate(IntPtr instance);
        private static _VRCUiManagerUpdateDelegate originalMethod;

        public static void ApplyPatch()
        {
            originalMethod = HookManager.Detour<_VRCUiManagerUpdateDelegate>(
                typeof(VRCUiManager).GetMethod(nameof(VRCUiManager.Update)), 
                Patch
            );
        }

        private static void Patch(IntPtr instance)
        {
            originalMethod(instance);
            
            VRCUiManager uiManager = instance == IntPtr.Zero ? null : new VRCUiManager(instance);
            if (uiManager == null) return;
            
            if (GameUtils.PlayerModel.VRCPlayerEntity() != null && GameUtils.World.IsLoaded())
            {
                ESP.Instance.enabled = Entry.config.Render.Overlays.ESP.Enabled;
                Nameplates.Overrender = Entry.config.Render.Overlays.Nameplate_Wallhack;

                QM.Main.Toggles.Player.Fly.isEnable(Entry.config.Player.Airborn.Flight.Enabled);

                GameUtils.PlayerModel.Motion.Networking.Run_Speed(Entry.config.Player.Grounded.RunSpeed);
                GameUtils.PlayerModel.Motion.Networking.Walk_Speed(Entry.config.Player.Grounded.WalkSpeed);
                GameUtils.PlayerModel.Motion.Networking.Strafe_Speed(Entry.config.Player.Grounded.StrafeSpeed);
            }
        }
    }
}