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
        public enum PointOfViews
        {
            none,
            first_person, 
               second_person, 
                    third_person, 
            freecam,
        }
        
        public class Movement
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
                switch (state)
                {
                    case true:
                        flyManager.enabled = true;
                        MOTION_STATE.enabled = false;
                        CHAR.enabled = false;
                        break;
                    case false: 
                        flyManager.enabled = true;
                        MOTION_STATE.enabled = true;
                        CHAR.enabled = true;
                        UnityEngine.Object.DestroyImmediate(flyManager);
                        break;
                }
                sconsole.print($"[LOCAL_PLAYER] Fly is Set to {state}",1);
                GameUtils.UI.HUD.ToastV2("Player+Movement",$"Fly is set to {state}");
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
                sconsole.print($"Jump+Impulse is set to {state}",1);
                GameUtils.UI.HUD.ToastV2("Player+Movement",$"Jump+Impulse is set to {state}");
            }
        }
        }

        public class View
        {
            public static void ChangeCamera(PointOfViews pointOfView)
            {
               
                   GameObject obj = new("Camera");
                   obj.AddComponent<AudioListener>();
                   var cam = obj.AddComponent<Camera>();
                   GameUtils.CameraUtils.camera = cam;
              
                switch (pointOfView)
                {
                    case PointOfViews.none:
                        UnityEngine.Object.DestroyImmediate(obj,true);
                        break;
                    case PointOfViews.first_person:
                        break;
                    case PointOfViews.second_person:
                        break;
                    case PointOfViews.third_person:
                        break;
                    case PointOfViews.freecam:
                        obj.AddComponent<FlyManager>();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(pointOfView), pointOfView, null);
                }
            }         
        }
    }

 



       

                
