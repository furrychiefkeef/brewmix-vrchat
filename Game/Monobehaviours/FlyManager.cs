using Brewchat.Game.Wrappers;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

namespace Brewchat.Game.Monobehaviours;

internal class FlyManager : UnityEngine.MonoBehaviour
{
    public FlyManager(IntPtr ptr) : base(ptr) {}
    public bool AllowRotation { get; set; } = false;
    public bool Player { get; set; } = false;
    void Update() => Controller();

    Vector3 velocity = Vector3.zero;

    void Controller()
    {
        
        float speed = (float)(Input.GetKey(KeyCode.LeftShift) ? (Entry.config.Player.Airborn.Flight.Speed * 5f) : Entry.config.Player.Airborn.Flight.Speed)!;
    
        if (transform is null || GameUtils.Gameplay.CallbackEvents.IsInMainMenu()) return;

        Vector3 targetDirection = Vector3.zero;
        
        if (Input.GetKey(KeyCode.Q) || Input.GetButton("Button.PrimaryThumbstick")) targetDirection += Vector3.down;
        if (Input.GetKey(KeyCode.E) || Input.GetButton("Button.SecondaryThumbstick")) targetDirection += Vector3.up;

        
        targetDirection += GameUtils.Camera.Get.transform.forward * GameUtils.VRChatUtils.InputManager.GetInput("Vertical").prop_Single_2;
        targetDirection += GameUtils.Camera.Get.transform.right * GameUtils.VRChatUtils.InputManager.GetInput("Horizontal").prop_Single_2;
        targetDirection.Normalize();

        /*if (!Config.Movement.abruptness2motion)
        {
            if (targetDirection != Vector3.zero) velocity = Vector3.Lerp(velocity, targetDirection * speed, acceleration * Time.deltaTime);
            else velocity = Vector3.Lerp(velocity, Vector3.zero, deceleration * Time.deltaTime);
        }*/
        transform.position += targetDirection * (speed * Time.deltaTime);

        if (AllowRotation) transform.rotation = GameUtils.Camera.Get.transform.rotation;
    }
}