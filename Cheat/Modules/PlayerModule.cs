using Brewchat.Game.Monobehaviours;
using Brewchat.Game.Wrappers;
using Syrup.IMGUI;
using UnityEngine;
using VRC;
using VRC.Animation;
using VRC.Core;


namespace Brewchat.Cheat.Modules;

internal class PlayerModule
{
    public class Movement
    {
        static readonly List<string> _disabledColliders = new();
        public static void Fly()
        {
            bool? fly = Config.Get<bool>(Config.Properties.Fly);
            Player player = GameUtils.PlayerModel.Get();
            if (!player && !GameUtils.World.IsWorldLoaded()) return;
            else
            {
                #region REQUIRED_COMPONENTS

                VRCMotionState? motion_state = player.GetComponent<VRC.Animation.VRCMotionState>();
                FlyManager? fly_component = player.gameObject.GetOrAddComponent<FlyManager>();
                CharacterController? characterController = player.GetComponent<CharacterController>();

                #endregion

                #region LOGIC

                switch (fly)
                {
                    case true:
                        fly_component.enabled = true;
                        motion_state.enabled = false;
                        characterController.enabled = false;

                        List<Collider> body_colliders = player.GetComponents<Collider>().ToList();
                        foreach (Collider colliderbox in body_colliders)
                        {
                            if (!_disabledColliders.Contains(colliderbox.gameObject.name))
                                _disabledColliders.Add(colliderbox.gameObject.name);
                        }

                        break;
                    case false:
                        fly_component.enabled = true;
                        motion_state.enabled = true;
                        characterController.enabled = true;
                        motion_state.Reset();
                        UnityEngine.Object.DestroyImmediate(fly_component);
                        break;
                }

                sconsole.print($"[LOCAL_PLAYER] Fly is Set to {fly}", 1);
                GameUtils.UI.HUD.ToastV2("Player+Movement", $"Fly is set to {fly}");

                #endregion

            }

        }
        public static void ForceJump(bool state)
        {
            #region REQUIRED_COMPONENTS

            Player player = GameUtils.PlayerModel.Get();
            var player_motion = GameUtils.PlayerModel.Motion.Jump_Impulse();
            var cache_player_jump_ = player_motion.GetJumpImpulse();

            #endregion

            #region LOGIC

            if (!player && !GameUtils.World.IsWorldLoaded()) return;
            {
                switch (state)
                {
                    case true:
                        player_motion.SetJumpImpulse();
                        break;
                    case false:
                        player_motion.SetJumpImpulse(cache_player_jump_);
                        break;
                }

                sconsole.print($"Jump+Impulse is set to {state}", 1);
                GameUtils.UI.HUD.ToastV2("Player+Movement", $"Jump+Impulse is set to {state}");


                #endregion

            }
        }
       
  
    }
    public class Camera
    {
        public enum P0V
        {
          none,
            first_person, 
                 second_person, 
                     third_person,
          freecam,
        }
        public class View
        {
            public static void ChangeCamera(P0V POV)
            {
                GameObject obj = new("Free_Camera");
                switch (POV)
                {
                    case P0V.first_person:
                        break;
                    case P0V.second_person:
                        break;
                    case P0V.third_person:
                        break;
                    case P0V.freecam:
                        obj.AddComponent<AudioListener>();
                        obj.AddComponent<FlyManager>().Rotation = true;
                        obj.AddComponent<UnityEngine.Camera>();
                        break;
                    case P0V.none:
                    default:
                        UnityEngine.Object.Destroy(obj);
                        break;
                }
            }
        }
    }
}


 



       

                
