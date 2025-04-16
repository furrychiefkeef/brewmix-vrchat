using Brewchat.Game.Monobehaviours;
using Brewchat.Game.Wrappers;
using UnityEngine;
using VRC;
using VRC.Animation;
using VRC.Core;
using VRC.UI.Core.Styles;

namespace Brewchat.Cheat.Modules
{
    internal class PlayerMovement
    {
        public static void Fly(bool s)
        {
            Player player = GameUtils.PlayerModel.Get();
            if (!player && !GameUtils.World.IsWorldLoaded()) return;
            else
            {
                CharacterController? CHAR = player.gameObject.GetComponent<UnityEngine.CharacterController>();
                VRCMotionState? MOTION_STATE = player.gameObject.GetComponent<VRC.Animation.VRCMotionState>();
                FlyManager? flyManager = player.gameObject.GetOrAddComponent<FlyManager>();
                if (s)
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
    }
}
