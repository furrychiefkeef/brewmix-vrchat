using Brewchat.Cheat.Modules;
using Brewchat.Game.Wrappers;
using UnityEngine;

namespace Brewchat.Game.Monobehaviours;

public class CameraModule : MonoBehaviour
{
    public CameraModule(IntPtr ptr) : base(ptr) {}
    float targetFOV;
    float CacheFOV;
    private List<Camera>? Cameras { get; set; }
    public float zoomedFOV = 30f; 
    float zspeed = 5f;
    
    void Start()
    {
        if (Camera.main != null) CacheFOV = Camera.main.fieldOfView;
        targetFOV = CacheFOV;
        Cameras = Camera.allCameras.ToList();
    }

    void Update()
    {
        targetFOV = Input.GetKey(KeyCode.LeftAlt) ? zoomedFOV : CacheFOV;
        Cameras.ForEach(x => x.fieldOfView = Mathf.Lerp(GameUtils.Camera.Get.fieldOfView, targetFOV, Time.deltaTime * zspeed));
    }
}