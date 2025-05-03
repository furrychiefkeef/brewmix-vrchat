
using Brewchat.Cheat.Modules;
using Brewchat.Game.Wrappers;
using Sentry.Unity.NativeUtils;
using Syrup.IMGUI;
using UnityEngine;


namespace Brewchat.Game.Monobehaviours;

internal class KeybindManager : UnityEngine.MonoBehaviour
{
    private void Awake()
    {
        sconsole.print("Monobehaviours", "/KeybindManager", "KeybindManager", 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            GameUtils.VRChatUtils.VRCKeyboard.InputPopup("Enter a Command", (command) =>
            {
                
                var a = GameUtils.PlayerModel.GrabPlayerFromDisplayName(command);
                GameUtils.PlayerModel.Get().transform.position = a.gameObject.transform.position;

            });
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.F)) Config.Movement.fly = !Config.Movement.fly;
        if (Input.GetKeyDown(KeyCode.Mouse2))  PlayerModule.Movement.ClickTP();
    }

    /*void Fly()
    {
       
    }*/
}