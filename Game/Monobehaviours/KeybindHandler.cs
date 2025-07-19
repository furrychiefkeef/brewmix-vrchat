using System.Collections;
using autumn.ml.Cheat.Modules.Global;
using Brewchat.Cheat;
using Brewchat.Cheat.Modules;
using Brewchat.Game.Wrappers;
using UnityEngine;

namespace Brewchat.Game.Monobehaviours;

internal class KeybindManager : UnityEngine.MonoBehaviour
{
    public KeybindManager(IntPtr ptr) : base(ptr) { }

    public KeybindManager Instance { get; set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(this);
    }
    void Update()
    {

        if (!GameUtils.World.IsLoaded()) return;
        
            #region Advanced


            if (Input.GetKeyDown(KeyCode.Insert))
            {
                TempCommandHandler();
            }
            
            #endregion

            if (GameUtils.Gameplay.CallbackEvents.IsInMainMenu() || GameUtils.Gameplay.CallbackEvents.IsInQuickMenu()) return;

            #region Keybinds

            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Mouse2))
            {
                PlayerModule.Movement.ClickTP();
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.F))
            {
                Entry.config.Player.Airborn.Flight.Enabled = !Entry.config.Player.Airborn.Flight.Enabled;
            }
            

            #region Camera

            if (Input.GetKeyDown(KeyCode.F5))
            {
                PlayerModule.CameraBuilder.CameraController();
            }
            #endregion


            #endregion
       
    }
    void TempCommandHandler()
    {

        GameUtils.VRChatUtils.VRCKeyboard.InputPopup("Enter a Command ", (id) =>
        {
          
        }, MultiLine: false, Placeholder: "type /help");
        
    }
}