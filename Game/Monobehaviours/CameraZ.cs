using Brewchat.Game.Wrappers;
using UnityEngine;

namespace Brewchat.Game.Monobehaviours;

public class CameraZoom : MonoBehaviour
{
    float targetFOV;
    float CacheFOV;
    
    public float zoomedFOV = 30f; 
    float zspeed = 5f;
    
    private void Start()
    {
        if (Camera.main != null) CacheFOV = Camera.main.fieldOfView;
        targetFOV = CacheFOV;
    }

    private void Update()
    {
        targetFOV = Input.GetKey(KeyCode.LeftAlt) ? zoomedFOV : CacheFOV;
        Camera.allCameras.ToList().ForEach(x => x.fieldOfView = Mathf.Lerp(GameUtils.Camera.Get.fieldOfView, targetFOV, Time.deltaTime * zspeed));
    }
}