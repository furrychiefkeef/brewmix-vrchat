
using Brewchat.Cheat.Modules;
using Brewchat.Cheats.World;
using Brewchat.Game.Wrappers;
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
            if (Input.GetKey(KeyCode.F10)) GameUtils.PlayerModel.Get().StartCoroutine(M4.UdonNukeCoroutine().WrapToIl2Cpp());

            #endregion
        }
        
       
            
       
       
        
    }

    void CommandHandler()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            /*GameUtils.VRChatUtils.VRCKeyboard.InputPopup("Enter a Command", (command) =>
            {
                if (command == "*help")
                {
                    sconsole.internal_log("*debug: {1,2,3}");
                    sconsole.internal_log("*player:teleport: {player}");
                }
            }, MultiLine: false);*/
            GameUtils.VRChatUtils.VRCKeyboard.InputPopup("Enter World ID", (id) =>
            {
                if (id.StartsWith("wrld_"))
                {
                    sconsole.print("WORLD LOADED",0);
                    GameUtils.World.Join(id);
                }
                if (id.StartsWith("avtr_"))
                {
                    sconsole.print("AVATAR LOADED" ,0);
                    GameUtils.PlayerModel.Avatar.SetAvatar(id);
                }
                
            }, MultiLine: false);
        }

        return; 
    }

    /*void Fly()
    {
       
    }*/
}