using BlanketSDK.Utilities.VRC.QM.Widgets;
using Brewchat.Game.Monobehaviours;
using Brewchat.Game.Wrappers;

using Brewchat.Wrappers;

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
            Player player = GameUtils.PlayerModel.GetLocalPlayer();
            if (!player && !GameUtils.World.IsLoaded()) return;
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


                #endregion

            }

        }

        public static void ForceJump(bool state)
        {
            #region REQUIRED_COMPONENTS

            Player player = GameUtils.PlayerModel.GetLocalPlayer();
            var player_motion = GameUtils.PlayerModel.Motion.Networking.Jump_Impulse();
            var cache_player_jump_ = player_motion.GetJumpImpulse();

            #endregion

            #region LOGIC

            if (!player && !GameUtils.World.IsLoaded()) return;
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

                #endregion

            }
        }

        public static void ClickTP()
        {
            Player player = GameUtils.PlayerModel.GetLocalPlayer();
            if (!player && !GameUtils.World.IsLoaded() || GameUtils.Gameplay.CallbackEvents.IsInMainMenu) return;
            if (Physics.Raycast(GameUtils.Camera.Get.transform.position, GameUtils.Camera.Get.transform.forward, out RaycastHit hit)) player.transform.position = hit.point;
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
                            cameraObject.transform.position = GameUtils.PlayerModel.GetLocalPlayer().VRCPlayerApi.GetBonePosition(HumanBodyBones.Head);
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
            Widgets.QM.cameramode_widget.SetState(true);
            currentView = (currentView + 1) % 4;
            if (camera[0] != null && camera[1] != null && camera[2] != null)
            {
                Sprite? video = AstriekLib.Sprites.Video();
                Sprite? video_slash = AstriekLib.Sprites.Video(false);
                switch (currentView)
                {
                    case 0:
                        Widgets.QM.cameramode_widget.SetText($"Third Person");
                        GameUtils.UI.HUD.ToastV1("Third Person Activated!", sprite: video );
                        camera[0].enabled = true;
                        camera[1].enabled = false;
                        camera[2].enabled = false;
                        break;
                    case 1:
                        Widgets.QM.cameramode_widget.SetText($"Second Person");
                        GameUtils.UI.HUD.ToastV1("Second Person Activated!", sprite: video);
                        camera[0].enabled = false;
                        camera[1].enabled = true;
                        camera[2].enabled = false;
                        break;
                    case 2:
                        Widgets.QM.cameramode_widget.SetText($"FreeCam");
                        GameUtils.UI.HUD.ToastV1("FreeCam Activated! Right Click (To Move Camera)", sprite: video);
                        camera[0].enabled = false;
                        camera[1].enabled = false;
                        camera[2].enabled = true;
                        break;
                    case 3:
                        GameUtils.UI.HUD.ToastV1("Cameras Destoryed", sprite: video_slash);
                        DestoryCameras(); 
                        Widgets.QM.cameramode_widget.SetState(false);
                        break;
                }
            }
        }
        public static void DestoryCameras() => UniversialTags.Destory("V-Cams");
    }
}



       

                
