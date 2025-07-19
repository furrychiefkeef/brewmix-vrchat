using BlanketSDK.Utilities.VRC.QM.Widgets;
using Brewchat.Game.Monobehaviours;
using Brewchat.Game.Wrappers;

using UnityEngine;

using VRC;
using VRC.Animation;
using VRC.Core;
using VRC.SDKBase;

namespace Brewchat.Cheat.Modules;

internal class PlayerModule
{
    public class Movement
    {

        static readonly List<string> _disabledColliders = new();
        static float _cacheJumpData { get; set; }
        static float _cacheWalkData { get; set; }
        static float _cacheRunData { get; set; }
        static float _cacheStrafeData  { get; set; }
        public static void Fly()
        {
            Player player = GameUtils.PlayerModel.LocalEntity();
            if (!player && !GameUtils.World.IsLoaded()) return;
            else
            {
                #region REQUIRED_COMPONENTS

                VRCMotionState? motion_state = player.GetComponent<VRCMotionState>();
                FlyManager? fly_component = player.gameObject.GetOrAddComponentSafe<FlyManager>();
                CharacterController? characterController = player.GetComponent<CharacterController>();

                #endregion

                #region LOGIC

                switch (Entry.config.Player.Airborn.Flight.Enabled)
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
                GameUtils.UI.HUD.ToastV1($"Fly {(Entry.config.Player.Airborn.Flight.Enabled ? "enabled" : "disabled")}.");

                #endregion

            }

        }

        public static void ForceJump(bool state)
        {
            #region LOGIC
            Player player = GameUtils.PlayerModel.LocalEntity();
            if (_cacheJumpData == null)
            {
                _cacheJumpData = player.VRCPlayerApi.GetJumpImpulse();
                return;
            }
        
            if (!player && !GameUtils.World.IsLoaded()) return;
            {

                switch (state)
                {
                    case true:
                        GameUtils.PlayerModel.Motion.Networking.Jump_Impulse();
                        break;
                    case false:
                        GameUtils.PlayerModel.Motion.Networking.Jump_Impulse(_cacheJumpData);
                        break;
                }
               GameUtils.UI.HUD.ToastV1($"ForceJump {(state ? "enabled" : "disabled")}.");

                #endregion

            }
        }
        public static void SpeedHack(bool state)
        {
            #region LOGIC
            Player player = GameUtils.PlayerModel.LocalEntity();
            if (_cacheStrafeData == null && _cacheRunData == null)
            {

                _cacheStrafeData = player.VRCPlayerApi.GetStrafeSpeed();
                _cacheRunData = player.VRCPlayerApi.GetRunSpeed();
                return;
            }
            if (!player && !GameUtils.World.IsLoaded()) return;
            
            switch (state)
            {
                case true:
                    GameUtils.PlayerModel.Motion.Networking.Run_Speed(35f);
                    GameUtils.PlayerModel.Motion.Networking.Strafe_Speed(35f);
                    break;

                case false:
                    GameUtils.PlayerModel.Motion.Networking.Run_Speed(_cacheRunData);
                    GameUtils.PlayerModel.Motion.Networking.Strafe_Speed(_cacheStrafeData);
                    break;
            }
            #endregion
        }

        public static void ClickTP()
        {
            Player player = GameUtils.PlayerModel.LocalEntity();
            if (!player && !GameUtils.World.IsLoaded() || GameUtils.Gameplay.CallbackEvents.IsInMainMenu()) return;
            if (Physics.Raycast(GameUtils.Camera.Get.transform.position, GameUtils.Camera.Get.transform.forward, out RaycastHit hit)) player.transform.position = hit.point;
        }
    }

    public class CameraBuilder
    {
        public enum View { Front, Back, FreeCam }

       static Camera MakeCameras(View view)
        {
            GameObject cameraObject = new($"{view}_Camera");
            cameraObject.AddComponent<UniversialTags>().AddTagToObject("V-Cams");
            if (GameUtils.Camera.Get != null)
            {
                Camera? referenceCamera = GameUtils.Camera.Get;

                if (referenceCamera != null)
                {
                    cameraObject.transform.localScale = referenceCamera.transform.localScale;
                    cameraObject.transform.parent = referenceCamera.transform;
                    cameraObject.transform.rotation = referenceCamera.transform.rotation;

                    var camera = cameraObject.GetOrAddComponentSafe<UnityEngine.Camera>();
                    camera.fieldOfView = referenceCamera.fieldOfView;
                    camera.nearClipPlane /= 4f;
                    if (referenceCamera != null) camera.cullingMask = -524289;

                    switch (view)
                    {
                        case View.Front:
                            cameraObject.transform.Rotate(new Vector3(0f, 180f, 0f));
                            cameraObject.transform.position = referenceCamera.transform.position + (referenceCamera.transform.forward * 2f);
                            return camera;
                        case View.Back:
                            cameraObject.transform.Rotate(Vector3.zero);
                            cameraObject.transform.position = referenceCamera.transform.position + (-referenceCamera.transform.forward * 2f);
                            return camera;
                        case View.FreeCam:
                            cameraObject.transform.parent = null;
                            cameraObject.transform.position = referenceCamera.transform.position + (-cameraObject.transform.forward * 2f);
                            cameraObject.AddComponent<AudioListener>();
                            cameraObject.AddComponent<FlyManager>().AllowRotation = true;
                            cameraObject.transform.position = GameUtils.PlayerModel.LocalEntity().VRCPlayerApi.GetBonePosition(HumanBodyBones.Head);
                            return camera;
                    }
                }
            }

            return null;
        }
        private static int currentView = -1;
        public static Camera[] camera = new Camera[3];
        public static void CameraController()
        {
           
            if (camera == null) camera = new Camera[3];
            if (camera[0] == null && camera[1] == null && camera[2] == null)
            { 

                camera[0] = MakeCameras(View.Back);
                camera[1] = MakeCameras(View.Front);
                camera[2] = MakeCameras(View.FreeCam);
            }
            currentView = (currentView + 1) % 4;
            if (camera[0] != null && camera[1] != null && camera[2] != null)
            {
             
                Widgets.QM.cameramode_widget.SetState(true);
                switch (currentView)
                {
                    case 0:
                        Widgets.QM.cameramode_widget.SetText($"Third Person (Back)");
                        GameUtils.UI.HUD.ToastV1("Camera Position Set To: <color=#00FF00>(Back)</color>.");
                        camera[0].enabled = true;
                        camera[1].enabled = false;
                        camera[2].enabled = false;
                        break;
                    case 1:
                         Widgets.QM.cameramode_widget.SetText($"Third Person (Front)");
                        GameUtils.UI.HUD.ToastV1("Camera Position Set To:  <color=#00FF00>(Front)</color>.");
                        camera[0].enabled = false;
                        camera[1].enabled = true;
                        camera[2].enabled = false;
                        break;
                    case 2:
                        Widgets.QM.cameramode_widget.SetText($"FreeCam");
                        GameUtils.UI.HUD.ToastV1("Camera Mode Set To:  <color=#00FF00>(FreeCam)</color>  | Note: Right Click (To Move Camera).");
                        camera[0].enabled = false;
                        camera[1].enabled = false;
                        camera[2].enabled = true;
 
                        break;
                    case 3:
                        GameUtils.UI.HUD.ToastV1("Reset Back to Main Camera.");
                        DestoryCameras(); 
                        Widgets.QM.cameramode_widget.SetState(false);
                        break;
                }
            }
        }
        public static void DestoryCameras() => UniversialTags.Destory("V-Cams");
    }
}



       

                
