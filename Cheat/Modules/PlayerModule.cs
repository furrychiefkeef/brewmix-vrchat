using Brewchat.Game.Monobehaviours;
using Brewchat.Game.Wrappers;
using Syrup.IMGUI;
using UnityEngine;
using UNIXEngine.IL2CPPModding.Utils.Collections;
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

                switch (Config.Movement.fly)
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

                sconsole.print($"[LOCAL_PLAYER] Fly is Set to {Config.Movement.fly}", 1);
                GameUtils.UI.HUD.ToastV2("Player+Movement", $"Fly is set to {Config.Movement.fly}");

                #endregion

            }

        }
        public static void ForceJump(bool state)
        {
            #region REQUIRED_COMPONENTS

            Player player = GameUtils.PlayerModel.Get();
            var player_motion = GameUtils.PlayerModel.Motion.Networking.Jump_Impulse();
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

        public static void ClickTP()
        {
            Player player = GameUtils.PlayerModel.Get();
            if(!player && !GameUtils.World.IsWorldLoaded() || GameUtils.Gameplay.CallbackEvents.IsInMainMenu) return;
            if(Physics.Raycast(GameUtils.Camera.Get.transform.position, GameUtils.Camera.Get.transform.forward, out RaycastHit hit)) player.transform.position = hit.point;
        }

        private static Coroutine _spinbotCoroutine;
        
        public static void Spinbot(bool status, float spinSpeed = 1f)
        {   
            Player player = GameUtils.PlayerModel.Get();
            if (!player) return;
            
            if(status && _spinbotCoroutine == null)
            {
                _spinbotCoroutine = player.StartCoroutine(SpinbotRoutine(player, spinSpeed).WrapToIl2Cpp());
            }
            else if(!status && _spinbotCoroutine != null)
            {
                player.StopCoroutine(_spinbotCoroutine);
                _spinbotCoroutine = null;
            }
        }
        
        private static System.Collections.IEnumerator SpinbotRoutine(Player player, float spinSpeed)
        {
            float rotationAngle = 0f;     MonoBehaviourPublicLa1StVeQuVeBoInVeBoUnique? localanim;
            while(true)
            {
                rotationAngle += spinSpeed * Time.deltaTime * 360f;
                localanim =  player.GetComponentInChildren<MonoBehaviourPublicLa1StVeQuVeBoInVeBoUnique>();
                if(player._vrcplayer?.field_Private_VRCAvatarManager_0?.field_Private_GameObject_0 != null)
                {
                    player._vrcplayer.field_Private_VRCAvatarManager_0.field_Private_GameObject_0.transform.localRotation = Quaternion.Euler(0f, rotationAngle, 0f);
                    if ( localanim != null) localanim.enabled = false;
                }
                yield return new WaitForEndOfFrame();
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
                        UnityEngine.Object.Destroy(obj);
                        break;
                    default:
                        UnityEngine.Object.Destroy(obj);
                        break;
                }
            }
        }
    }

    public static class Body
    {
        public static GameObject Clone(VRC.Player pl)
        {
            return pl._vrcplayer
                .field_Private_VRCAvatarManager_0
                .field_Private_GameObject_0;
        }
    }
}


 



       

                
