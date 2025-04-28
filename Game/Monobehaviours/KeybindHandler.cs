
using Brewchat.Cheat.Modules;
using Brewchat.Game.Wrappers;
using Syrup.IMGUI;
using UnityEngine;


namespace Brewchat.Game.Monobehaviours;

internal class KeybindManager : UnityEngine.MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            GameUtils.VRChatUtils.VRCKeyboard.InputPopup("Enter a Command", (command) =>
            {
                sconsole.internal_log(command);
            });
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.F)) Config.Movement.fly = !Config.Movement.fly;
        if (Input.GetKeyDown(KeyCode.F12))  Brewchat.Cheat.Modules.Exploits.Enabled = !Brewchat.Cheat.Modules.Exploits.Enabled;
        if (Input.GetKeyDown(KeyCode.Mouse2))  PlayerModule.Movement.ClickTP();
    }

    /*void Fly()
    {
       
    }*/
}