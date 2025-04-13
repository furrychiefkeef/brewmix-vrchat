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

      
        new VRCToggle(Root.Button().parent, "Single ex3e", null, null, (x) =>
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

        new VRCToggle(Root.Button().parent, "Single ex3ea", null, null, (x) =>
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
