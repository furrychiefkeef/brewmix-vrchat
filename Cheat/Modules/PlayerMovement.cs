using Brewchat.Game.Monobehaviours;
using Brewchat.Game.Wrappers;
using Syrup.IMGUI;
using UnityEngine;
using VRC;
using VRC.Animation;
using VRC.Core;
using VRC.UI.Core.Styles;

namespace Brewchat.Cheat.Modules;

    internal class PlayerMovement
    {
        public static void Fly(bool state)
        {
            Player player = GameUtils.PlayerModel.Get();
            if (!player && !GameUtils.World.IsWorldLoaded()) return;
            else
            {
                CharacterController? CHAR = player.gameObject.GetComponent<UnityEngine.CharacterController>();
                VRCMotionState? MOTION_STATE = player.gameObject.GetComponent<VRC.Animation.VRCMotionState>();
                FlyManager? flyManager = player.gameObject.GetOrAddComponent<FlyManager>();
                if (state)
                {
                    flyManager.enabled = true;
                    MOTION_STATE.enabled = false;
                    CHAR.enabled = false;
                }
                else
                {
                    flyManager.enabled = true;
                    MOTION_STATE.enabled = true;
                    CHAR.enabled = true;
                    UnityEngine.Object.DestroyImmediate(flyManager);
                }
            }
        }

        public static void ForceJump(bool state)
        {
            Player player = GameUtils.PlayerModel.Get();
            var player_motion = GameUtils.PlayerModel.Motion.Jump_Impulse();
            if (!player && !GameUtils.World.IsWorldLoaded()) return;
            {
                switch (state)
                {
                    case true:
                        player_motion.SetJumpImpulse();
                        break;
                    case false:
                        player_motion.SetJumpImpulse(0);
                        break;
                }
                sconsole.SLog($"Jump Impluse Set to {state}");
            }
        }
    }



       

                
