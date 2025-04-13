using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syrup.IMGUI;

using BlanketSDK.QM;
using BlanketSDK.Utilities;
using UnityEngine;
using brewchat.hybridxcore.bep.Game.Wrappers;
namespace brewchat.Cheat;

internal class QM
{
    public static void Apply()
    {


        new VRCButton(null, "ae", "smart", () =>
        {
            GameUtils.HUD.Notifcation("TEST", "TEST", null);
        });

        new VRCToggle(null, "ac", "smart", "lart", (x) =>
        {
            if (x)
            {
                sconsole.SLog($"WOAH A BUTTON WAS CLICKED GET STARTED ON THE REAL WORK {x}");
            }
            else
            {
                sconsole.SLog($"WOAH A BUTTON WAS CLICKED GET STARTED ON THE REAL WORK {x}");
            }
        });
    }
}
