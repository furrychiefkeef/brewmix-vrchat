using UnityEngine;

namespace BlanketSDK.Utilities.Unbearable;

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
    
  
    internal class ClassName // WHY DID I MAKE THIS: CAUSE MY IDE WAS GIVING ME A FUCKING MENTALBREAK DOWN CAUSE MY CODE STYLE NEEDS TO BE CHANGED.
    {
        // Put RootObjects

        protected struct QMMenus
        {
            // Put Menus
            public struct QMSubmenus
            {
                // Put Subs
            }
        }
      

        protected struct QMControls
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
}