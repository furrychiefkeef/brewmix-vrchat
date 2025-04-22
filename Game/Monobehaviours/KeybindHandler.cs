
using UnityEngine;


namespace Brewchat.Game.Monobehaviours;

public class KeybindHandler : UnityEngine.MonoBehaviour
{
    public KeybindHandler Instance { get; private set; }
    private void Awake()
    {
        if (Instance is null)
        {
            Instance = this;
            UnityEngine.Object.DontDestroyOnLoad(this);
        }
        else UnityEngine.Object.Destroy(this);
    }

   

    public void LateUpdate()
    {
        Fly();
    }

    void Fly()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.F)) Config.Fly = !Config.Fly;
    }
}