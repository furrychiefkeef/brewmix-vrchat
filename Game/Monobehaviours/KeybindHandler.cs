
using Brewchat.Cheat;
using Brewchat.Cheat.Modules;
using Brewchat.Cheats.World;
using Brewchat.Game.Wrappers;
using Brewchat.Wrappers;
using Syrup.IMGUI;
using UnityEngine;
using UNIXEngine.IL2CPPModding.Utils.Collections;


namespace Brewchat.Game.Monobehaviours;
internal class KeybindManager : UnityEngine.MonoBehaviour
{
    private void Awake()
    {
        sconsole.print(" Monobehaviours", "/KeybindManager", "KeybindManager", 0);
    }

    void LateUpdate()
    {
        if (!GameUtils.World.IsLoaded()) return;
        {
            CommandHandler();
            if(GameUtils.Gameplay.CallbackEvents.IsInMainMenu || GameUtils.Gameplay.CallbackEvents.IsInQuickMenu) return;
            
            #region Keybinds

            if (Input.GetKeyDown(KeyCode.F5)) PlayerModule.CCamera.CameraController();
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Mouse2)) PlayerModule.Movement.ClickTP();
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.F))
            {
                Config.Movement.fly = !Config.Movement.fly;
                UIRefs.Fly.SetState(Config.Movement.fly);
            }
            #endregion
        }
    }

    void CommandHandler()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            GameUtils.VRChatUtils.VRCKeyboard.InputPopup("Enter a Command ", (id) =>
            {
                
            }, MultiLine: false, Placeholder: "type *help");
        }
    }

    /*void Fly()
    {
       
    }*/
}