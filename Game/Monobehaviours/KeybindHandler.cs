using System.Collections;

using Brewchat.Cheat;
using Brewchat.Cheat.Modules;
using Brewchat.Game.Wrappers;
using UnityEngine;

namespace Brewchat.Game.Monobehaviours;
internal class KeybindManager : UnityEngine.MonoBehaviour
{
    public KeybindManager(IntPtr ptr) : base(ptr) {}
    void LateUpdate()
    {
        if (!GameUtils.World.IsLoaded()) return;
        {
            #region Advanced

            CommandHandler();

            #endregion
          
            if(GameUtils.Gameplay.CallbackEvents.IsInMainMenu() || GameUtils.Gameplay.CallbackEvents.IsInQuickMenu()) return;
            
            #region Keybinds

            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Mouse2))
            {
                PlayerModule.Movement.ClickTP();
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.F))
            {
                
            }
            
            #region Camera

            if (Input.GetKeyDown(KeyCode.F5))
            {
                PlayerModule.CameraBuilder.CameraController();
            }

            #endregion

        
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