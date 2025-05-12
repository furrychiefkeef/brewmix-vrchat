using UnityEngine;

namespace BlanketSDK.Core;

/// <summary>
/// VRCUI_CLASSMENU ELEMENTS I DID THIS CAUSE I WAS TERRIFIED OF MY CODE STYLE.
/// </summary>

public class IActionMenuElements
{
    public static Transform[] ActionMenus()
    {
        return
        [
            GameObject.Find("ActionMenu/Container/MenuR/ActionMenu")?.transform,
            GameObject.Find("ActionMenu/Container/MenuL/ActionMenu")?.transform
        ];
    }

    // Put RootObjects
    ActionMenu ActionMenu() => ActionMenus()[0].GetComponentInChildren<ActionMenu>();

    protected struct AcMenus
    {

        // Put Menus
        public struct AcSubmenus
        {


            // Put Subs
        }
    }


    protected struct AcControls
    {
        // Put Controls
        public struct Buttons
        {
            //Put Buttons   
        }

    }

    protected struct QMElements
    {
        //Put Elements
    }

    protected struct QMFallbacks
    {
        //Put Fallbacks
    }
}