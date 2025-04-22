
using UnityEngine;


namespace Brewchat.Game.Monobehaviours;

internal class KeybindHandler : UnityEngine.MonoBehaviour
{
    public static KeybindHandler Instance { get; private set; }
    void Awake()
    {
        if (Instance is null)
        {
            Instance = this;
           DontDestroyOnLoad(this);
        }
        else Destroy(this);
    }

   

    void LateUpdate()
    {
        Fly();
    }

    void Fly()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.F)) Config.Fly = !Config.Fly;
    }
}