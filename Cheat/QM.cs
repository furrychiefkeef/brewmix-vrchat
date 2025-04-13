using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syrup.IMGUI;

using BlanketSDK.QM;
using BlanketSDK.Utilities;
namespace brewchat.Cheat;

internal class QM
{
    public static void Apply()
    {

        new VRCButton(Root.Button().parent, "Single x3c", null, () =>
        {
            sconsole.SLog("WOAH A BUTTON WAS CLICKED GET STARTED ON THE REAL WORK");
        });

        new VRCToggle(null, "Single ex3e", null, null, (x) =>
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
