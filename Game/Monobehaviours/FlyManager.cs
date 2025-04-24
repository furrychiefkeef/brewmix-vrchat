using Brewchat.Game.Wrappers;
using UnityEngine;

namespace Brewchat.Game.Monobehaviours;

internal class FlyManager : UnityEngine.MonoBehaviour
{
    void LateUpdate() => Controller();

    void Controller()
    {
        float speed = (float)(Input.GetKey(KeyCode.LeftShift) ? (Config.FlySpeed * 5f) : Config.FlySpeed)!;

        if (transform is null || GameUtils.Gameplay.CallbackEvents.IsInMainMenu) return;

        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.Q)) direction += Vector3.down;
        if (Input.GetKey(KeyCode.E)) direction += Vector3.up;

        direction += GameUtils.CameraUtils.camera.transform.forward* 
                     GameUtils.VRChatUtils.InputManager.GetInput("Vertical").prop_Single_2;
        direction += GameUtils.CameraUtils.camera.transform.right* 
                     GameUtils.VRChatUtils.InputManager.GetInput("Horizontal").prop_Single_2;
        direction.Normalize();

        transform.position += direction* (speed * Time.deltaTime);
        transform.rotation = GameUtils.CameraUtils.camera.transform.rotation;
    }
}

