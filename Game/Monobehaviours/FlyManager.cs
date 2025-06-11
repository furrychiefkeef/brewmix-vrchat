using Brewchat.Game.Wrappers;
using UnityEngine;
using VRC.Animation;
using VRC.SDKBase;

namespace Brewchat.Game.Monobehaviours;

internal class FlyManager : UnityEngine.MonoBehaviour
{
    public FlyManager(IntPtr ptr) : base(ptr) {}
    public bool AllowRotation { get; set; } = false;
    public bool Player { get; set; } = false;
    void Update() => Controller();

    Vector3 velocity = Vector3.zero;
    float acceleration = 10f; // How fast you reach target speed
    float deceleration = 5f; // How fast you stop

    void Controller()
    {
        
        float speed = (float)(Input.GetKey(KeyCode.LeftShift) ? (Config.Movement.speed * 5f) : Config.Movement.speed)!;
    
        if (transform is null || GameUtils.Gameplay.CallbackEvents.IsInMainMenu) return;

        Vector3 targetDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.Q)) targetDirection += Vector3.down;
        if (Input.GetKey(KeyCode.E)) targetDirection += Vector3.up;

        

        /*
        GameUtils.PlayerModel._components._motion.field_Private_Boolean_1 = true;
        GameUtils.PlayerModel._components._motion.field_Private_Boolean_2 = true;
        GameUtils.PlayerModel._components._motion.field_Private_Boolean_3 = true;
        GameUtils.PlayerModel._components._motion.field_Private_Boolean_4 = true;
        GameUtils.PlayerModel._components._motion.field_Private_Boolean_5 = true;
        GameUtils.PlayerModel._components._motion.field_Private_Boolean_5 = true;
        GameUtils.PlayerModel._components._motion.field_Private_Boolean_6 = true;
        GameUtils.PlayerModel._components._motion.field_Private_Boolean_7 = true;
        GameUtils.PlayerModel._components._motion.field_Private_Boolean_8 = true;
        GameUtils.PlayerModel._components._motion.field_Private_Boolean_9 = true;
        GameUtils.PlayerModel._components._motion.field_Private_Boolean_10 = true;
        GameUtils.PlayerModel._components._motion.field_Private_Boolean_11 = true;
        GameUtils.PlayerModel._components._motion.field_Private_Boolean_12 = true;
        GameUtils.PlayerModel._components._motion.field_Private_Boolean_13 = true;
        GameUtils.PlayerModel._components._motion.field_Private_Boolean_14 = true;
        GameUtils.PlayerModel._components._motion.field_Private_EnumNPrivateSealedvaNoPrFoAi5vUnique_0 = VRCMotionState.EnumNPrivateSealedvaNoPrFoAi5vUnique.ForceApplied;
        */
        
        
        targetDirection += GameUtils.Camera.Get.transform.forward * GameUtils.VRChatUtils.InputManager.GetInput("Vertical").prop_Single_2;
        targetDirection += GameUtils.Camera.Get.transform.right * GameUtils.VRChatUtils.InputManager.GetInput("Horizontal").prop_Single_2;
        targetDirection.Normalize();

        /*if (!Config.Movement.abruptness2motion)
        {
            if (targetDirection != Vector3.zero) velocity = Vector3.Lerp(velocity, targetDirection * speed, acceleration * Time.deltaTime);
            else velocity = Vector3.Lerp(velocity, Vector3.zero, deceleration * Time.deltaTime);
        }*/
        transform.position += targetDirection * (speed * Time.deltaTime) ;

        if (AllowRotation) transform.rotation = GameUtils.Camera.Get.transform.rotation;
    }
}