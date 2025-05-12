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
                GameUtils.UI.HUD.Alert($"Fly is set to {Config.Movement.fly}", GameUtils.UI.HUD.AlertType.QuickMenu);
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
                GameUtils.UI.HUD.Alert($"Jump+Impulse is set to {state}", GameUtils.UI.HUD.AlertType.QuickMenu);


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
        public static void Spinbot(bool state, float spinSpeed = 1f)
        {   
            Player player = GameUtils.PlayerModel.Get();
            if (!player) return;
            
            if(state && _spinbotCoroutine == null)
            {
                _spinbotCoroutine = player.StartCoroutine(SpinbotRoutine(player, spinSpeed).WrapToIl2Cpp());
            }
            else if(!state && _spinbotCoroutine != null)
            {
                player.StopCoroutine(_spinbotCoroutine);
                _spinbotCoroutine = null;
            }
            sconsole.print($"Spinbot is set to {state}", 1);
            GameUtils.UI.HUD.ToastV2("Player+Movement", $"Spinbot is set to {state}");
            GameUtils.UI.HUD.Alert($"Spinbot is set to {state}", GameUtils.UI.HUD.AlertType.QuickMenu);

        }
        
        private static System.Collections.IEnumerator SpinbotRoutine(Player player, float spinSpeed)
        {
            float rotationAngle = 0f;
            MonoBehaviourPublicLa1StVeQuVeBoInVeBoUnique? localanim;
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
        public enum CameraView
        {
            second_person, 
            third_person,
            freecam,
        }
        public class View
        {
            public static GameObject cache;
            public static void ChangeCamera(CameraView view)
            {
                
                if (GameUtils.Camera.Get != null)
                { 
                    UnityEngine.Camera? reference = GameUtils.Camera.Get;
                    GameObject obj = new($"{view}_Camera");   obj.AddComponent<UTag>().SetTag("V-Cams");
                    UnityEngine.Camera? camera = obj.AddComponent<UnityEngine.Camera>();
                    
                    if(reference != null)
                    {
                        obj.transform.localScale = reference.transform.localScale;
                        obj.transform.parent = reference.transform;
                        obj.transform.rotation = reference.transform.rotation;
                        
                        obj.transform.position = reference.transform.position;
                        obj.transform.position = reference.transform.position + (-camera.transform.forward * 2f);
                    }
                    
                    camera.fieldOfView = reference.fieldOfView;
                    camera.nearClipPlane /= 4f;
                    if (reference != null) camera.cullingMask = -524289;
                    
                    switch (view)
                    {
                        case CameraView.second_person:
                            obj.transform.Rotate(new Vector3(0f, 180f, 0f));
                            break;
                        case CameraView.third_person:
                            obj.transform.Rotate(Vector3.zero);
                            break;
                        case CameraView.freecam:
                            cache = obj;
                            obj.AddComponent<AudioListener>();
                            obj.AddComponent<FlyManager>().AnyObject = true;
                            break;
                    }
                    
                }
               
               
            } 
            public static void DestoryCameras()
            {
                foreach (var VARIABLE in UnityEngine.Object.FindObjectsOfType<UTag>().Where(x => x.identifer.Contains("V-Cams")))
                {
                    UnityEngine.Object.DestroyImmediate(VARIABLE.gameObject);
                }
            }
        }
    }

    public static class Body
    {
        static GameObject AvatarObj;
        public static GameObject Clone(VRC.Player player)
        {
            Transform? head = player._vrcplayer.VRCPlayerApi.GetBoneTransform(HumanBodyBones.Head);
            head.localScale = new Vector3(1f, 1f, 1f);
            if (AvatarObj ==null)
            {
                AvatarObj = UnityEngine.Object.Instantiate(player._vrcplayer.field_Private_VRCAvatarManager_0.field_Private_GameObject_0);
                AvatarObj.transform.position = player.transform.position;
            }
            return AvatarObj;
        }
    }
}


 



       

                
