using UnityEngine;

namespace BlanketSDK.Core;

/// <summary>
/// VRCUI_CLASSMENU ELEMENTS I DID THIS CAUSE I WAS TERRIFIED OF MY CODE STYLE.
/// </summary>

public class IActionMenuElements
{
    public static Transform[] ActionMenus()
    {
        try
        {
            // Use Resources.FindObjectsOfTypeAll to find inactive objects
            var allTransforms = Resources.FindObjectsOfTypeAll<Transform>();

            var actionMenuR = allTransforms.FirstOrDefault(t =>
                t.name == "ActionMenu" &&
                t.transform.parent?.name == "MenuR" &&
                t.transform.parent?.parent?.name == "Container" &&
                t.transform.parent?.parent?.parent?.name == "ActionMenu");

            var actionMenuL = allTransforms.FirstOrDefault(t =>
                t.name == "ActionMenu" &&
                t.transform.parent?.name == "MenuL" &&
                t.transform.parent?.parent?.name == "Container" &&
                t.transform.parent?.parent?.parent?.name == "ActionMenu");

            return new Transform[] { actionMenuR, actionMenuL }.Where(t => t != null).ToArray();
        }
        catch
        {
            // Fallback to original method if the above fails
            return new Transform[]
            {
            GameObject.Find("ActionMenu/Container/MenuR/ActionMenu")?.transform,
            GameObject.Find("ActionMenu/Container/MenuL/ActionMenu")?.transform
            }.Where(t => t != null).ToArray();
        }
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