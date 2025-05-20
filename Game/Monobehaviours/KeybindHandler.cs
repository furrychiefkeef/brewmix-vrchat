
using Brewchat.Cheat;
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
    }

    /*void Fly()
    {
       
    }*/
}