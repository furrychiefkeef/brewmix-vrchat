using Brewchat.Game.Wrappers;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using UnityEngine;

namespace Brewchat.Game.Monobehaviours;

public class CameraModule : MonoBehaviour
{
    float targetFOV;
    float CacheFOV;
    private Camera[]? Cameras { get; set; }
    public float zoomedFOV = 30f; 
    float zspeed = 5f;
    
    void Start()
    {
        if (Camera.main != null) CacheFOV = Camera.main.fieldOfView;
        targetFOV = CacheFOV;
        Cameras = Camera.allCameras;
    }

    void Update()
    {
        targetFOV = Input.GetKey(KeyCode.LeftAlt) ? zoomedFOV : CacheFOV;
        Cameras.ToList().ForEach(x => x.fieldOfView = Mathf.Lerp(GameUtils.Camera.Get.fieldOfView, targetFOV, Time.deltaTime * zspeed));
    }
}