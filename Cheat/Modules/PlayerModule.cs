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
                        motion_state.field_Private_EnumNPrivateSealedvaNoPrFoAi5vUnique_0 = VRCMotionState.EnumNPrivateSealedvaNoPrFoAi5vUnique.None;
                        fly_component.enabled = true;
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
            if (!player && !GameUtils.World.IsWorldLoaded() || GameUtils.Gameplay.CallbackEvents.IsInMainMenu) return;
            if (Physics.Raycast(GameUtils.Camera.Get.transform.position, GameUtils.Camera.Get.transform.forward, out RaycastHit hit)) player.transform.position = hit.point;
        }

        private static Coroutine _spinbotCoroutine;

        public static void Spinbot(bool state, float spinSpeed = 1f)
        {
            /*Player player = GameUtils.PlayerModel.Get();
            if (!player) return;

            if(state && _spinbotCoroutine == null)
            {
                _spinbotCoroutine = player.StartCoroutine(SpinbotRoutine(player, spinSpeed).WrapToIl2Cpp());
            }
            else if(!state && _spinbotCoroutine != null)
            {
                player.StopCoroutine(_spinbotCoroutine);
                _spinbotCoroutine = null;
            }*/
            sconsole.print($"Spinbot is set to {state}", 1);
            GameUtils.UI.HUD.ToastV2("Player+Movement", $"Spinbot is set to {state}");
            GameUtils.UI.HUD.Alert($"Spinbot is set to {state}", GameUtils.UI.HUD.AlertType.QuickMenu);

        }

    }

    public class CCamera
    {
        public enum View
        {
            second_person,
            third_person,
            freecam,
        }

        private static UnityEngine.Camera MakeCameras(View view)
        {
            GameObject cameraObject = new($"{view}_Camera");
            cameraObject.AddComponent<UniversialTags>().AddTagToObject("V-Cams");
            if (GameUtils.Camera.Get != null)
            {
                UnityEngine.Camera? referenceCamera = GameUtils.Camera.Get;

                if (referenceCamera != null)
                {
                    cameraObject.transform.localScale = referenceCamera.transform.localScale;
                    cameraObject.transform.parent = referenceCamera.transform;
                    cameraObject.transform.rotation = referenceCamera.transform.rotation;

                    var camera = cameraObject.AddComponent<UnityEngine.Camera>();
                    camera.fieldOfView = referenceCamera.fieldOfView;
                    camera.nearClipPlane /= 4f;
                    if (referenceCamera != null) camera.cullingMask = -524289;

                    switch (view)
                    {
                        case View.second_person:
                            cameraObject.transform.Rotate(new Vector3(0f, 180f, 0f));
                            cameraObject.transform.position = referenceCamera.transform.position + (referenceCamera.transform.forward * 2f);
                            return camera;
                        case View.third_person:
                            cameraObject.transform.Rotate(Vector3.zero);
                            cameraObject.transform.position = referenceCamera.transform.position + (-referenceCamera.transform.forward * 2f);
                            return camera;
                        case View.freecam:
                            cameraObject.transform.parent = null;
                            cameraObject.transform.position = referenceCamera.transform.position + (-cameraObject.transform.forward * 2f);
                            cameraObject.AddComponent<AudioListener>();
                            cameraObject.AddComponent<FlyManager>().AllowRotation = true;
                            return camera;
                    }
                }
            }

            return null;
        }

        private static int currentView = -1;
        public static UnityEngine.Camera[]? camera = new UnityEngine.Camera[3];

        public static void CameraController()
        {
            if (camera == null) camera = new UnityEngine.Camera[3];
            if (camera[0] == null && camera[1] == null && camera[2] == null)
            {
                camera[0] = MakeCameras(View.third_person);
                camera[1] = MakeCameras(View.second_person);
                camera[2] = MakeCameras(View.freecam);
            }

            currentView = (currentView + 1) % 4;
            if (camera[0] != null && camera[1] != null && camera[2] != null)
            {
                switch (currentView)
                {
                    case 0:
                        GameUtils.UI.HUD.ToastV1("Third Person Activated!");
                        camera[0].enabled = true;
                        camera[1].enabled = false;
                        camera[2].enabled = false;
                        break;
                    case 1:
                        GameUtils.UI.HUD.ToastV1("Second Person Activated!");
                        camera[0].enabled = false;
                        camera[1].enabled = true;
                        camera[2].enabled = false;
                        break;
                    case 2:
                        GameUtils.UI.HUD.ToastV1("FreeCam Activated! Right Click (To Move Camera)");
                        camera[0].enabled = false;
                        camera[1].enabled = false;
                        camera[2].enabled = true;
                        break;
                    case 3:
                        GameUtils.UI.HUD.ToastV1("Cameras Destoryed");
                        DestoryCameras();
                        break;
                }
            }
        }

        public static void DestoryCameras() => UniversialTags.Destory("V-Cams");
    }

    public static class Body
    {
        static GameObject AvatarObj;

        public static GameObject Clone(VRC.Player player)
        {
            Transform? head = player._vrcplayer.VRCPlayerApi.GetBoneTransform(HumanBodyBones.Head);
            head.localScale = new Vector3(1f, 1f, 1f);
            if (AvatarObj == null)
            {
                AvatarObj = UnityEngine.Object.Instantiate(player._vrcplayer.field_Private_VRCAvatarManager_0.field_Private_GameObject_0);
                AvatarObj.transform.position = player.transform.position;
            }

            return AvatarObj;
        }
    }
    
    
    
    
    
    
}



       

                
