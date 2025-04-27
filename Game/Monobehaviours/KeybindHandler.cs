
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
            bool Fly = Config.Get<bool>(Config.Properties.Fly);
            sconsole.internal_log(($"{Fly}"));
        }
        if (Input.GetKeyDown(KeyCode.F9))
        {
            GameUtils.VRChatUtils.VRCKeyboard.InputPopup("Enter a Command", (command) =>
            {
                sconsole.internal_log(command);
            });
        }
        if (Input.GetKeyDown(KeyCode.F12))  Brewchat.Cheat.Modules.Exploits.Enabled = !Brewchat.Cheat.Modules.Exploits.Enabled;
    }

    /*void Fly()
    {
       
    }*/
}