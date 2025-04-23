
using Brewchat.Game.Wrappers;
using Syrup.IMGUI;
using UnityEngine;


namespace Brewchat.Game.Monobehaviours;

internal class KeybindManager : UnityEngine.MonoBehaviour
{

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.F))
        {
            Config.Fly = !Config.Fly;
            sconsole.internal_log(($"{Config.Fly}"));
        }
        if (Input.GetKeyDown(KeyCode.F9))
        {
            GameUtils.VRChatUtils.VRCKeyboard.InputPopup("Enter a Command", (command) =>
            {
                sconsole.internal_log(command);
            });
            sconsole.internal_log(($"{Config.Fly}"));
        }
    }

    /*void Fly()
    {
       
    }*/
}