
using Brewchat.Cheat.Modules;
using Brewchat.Game.Wrappers;
using Syrup.IMGUI;
using UnityEngine;


namespace Brewchat.Game.Monobehaviours;

internal class KeybindManager : UnityEngine.MonoBehaviour
{
    private void Awake()
    {
        sconsole.print(" Monobehaviours", "/KeybindManager", "KeybindManager", 0);
    }

    void Update()
    {
        if (GameUtils.World.IsWorldLoaded())
        {
            if(GameUtils.Gameplay.CallbackEvents.IsInMainMenu || GameUtils.Gameplay.CallbackEvents.IsInQuickMenu) return;
            CommandHandler();
        
            #region Keybinds

            if (Input.GetKeyDown(KeyCode.F5)) PlayerModule.Camera.View.ChangeCamera(PlayerModule.Camera.CameraView.third_person);
            if (Input.GetKeyDown(KeyCode.F6)) PlayerModule.Camera.View.ChangeCamera(PlayerModule.Camera.CameraView.second_person);
            if (Input.GetKeyDown(KeyCode.Backspace)) PlayerModule.Camera.View.DestoryCameras();
            if (Input.GetKeyDown(KeyCode.Mouse2))  PlayerModule.Movement.ClickTP();
        }
        #endregion
       
            
       
       
        
    }

    void CommandHandler()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            GameUtils.VRChatUtils.VRCKeyboard.InputPopup("Enter a Command", (command) =>
            {
                if (command == "*help")
                {
                    sconsole.internal_log("*debug: {1,2,3}");
                    sconsole.internal_log("*player:teleport: {player}");
                }
            }, MultiLine: false);
        }

        return; 
    }

    /*void Fly()
    {
       
    }*/
}