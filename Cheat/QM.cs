using System.Diagnostics;
using BepInEx.Unity.IL2CPP.Utils;
using Syrup.IMGUI;
using BlanketSDK.QM;

using BlanketSDK.Utilities;
using BlanketSDK.QM.Modals;
using Brewchat.Cheat.Modules;
using Brewchat.Cheats.World;
using Brewchat.Game.Wrappers;
using ExitGames.Client.Photon;
using Il2CppInterop.Runtime;
using UnityEngine;
using VRC.Core;
using VRC.SDK3.Components;

namespace Brewchat.Cheat;

internal class QM 
{
    public static Coroutine coroutine;
    public static void Apply()
    {
        try
        {
            #region Modal_1
            VRCModalContainer? Modal = new("Player+Movement");
            VRCSmallButton acee = new(Modal, "Injection", () => sconsole.print("passion_fruit",1) );
            VRCButton? T = new (Root._Fallback_Dashboard_Group_QM_UI_Buttons(), "Player", "Modify The Player", () => Modal.Open());
            VRCButton? E = new (Root._Fallback_Dashboard_Group_QM_UI_Buttons(), "CSS", "Load Custom StyleEngine", () => CSS.Online());
            VRCToggle? Fly_Toggle = new(Modal, "Fly", "Fly On", "Fly Off", (x) =>
            {
                PlayerModule.Movement.Fly(); 
            });
            VRCToggle? Event12 = new(Modal, "BlockEvent 12", "", "Fly Off", (x) =>
            {
                Config.Protection.block_event12 = !Config.Protection.block_event12;
            });
            VRCToggle? Event1 = new(Modal, "VoiceData", "", "Fly Off", (x) =>
            {
                if (x)
                {
                    GameUtils.PlayerModel.Get().gameObject.GetOrAddComponent<Exploits>().enabled = x;
                }
            });
            VRCToggle? Force_Jump = new(Modal, "Force Jump On", "Force Jump Off", "Fly Off", (x) =>
            {
            });

            #endregion


        }
        catch (Exception e) { sconsole.print(e.Message + "\n" + e.InnerException ,2); }
        finally{sconsole.print("Created The QuickMenu+BlanketSDK.UI",1);}
    }
}
