using Brewchat.Game.Wrappers;
using UnityEngine;
using VRC.SDKBase;

namespace Brewchat.Game.Monobehaviours;

internal class FlyManager : UnityEngine.MonoBehaviour
{
    public bool AllowRotation { get; set; } = false;
    public bool Player { get; set; } = false;
    void LateUpdate() => Controller();

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

        if (Player)
        {
            
        }
        
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