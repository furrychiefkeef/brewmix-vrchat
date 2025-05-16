using BlanketSDK.QM.Elements;
using BlanketSDK.QM.Elements.Controls;
using BlanketSDK.QM.Elements.Modals;
using UnityEngine;

namespace BlanketSDK.Utilities;

internal static class SDKextensions
{
   #region VRC_QM

   #region VRC_QM_BUTTONS
    public static BlanketSDK.QM.Elements.Controls.VRCButton AddButton(this Transform parent, string name, string? tooltip, System.Action func, Sprite? sprite = null)
    {
        return new VRCButton(parent, name, tooltip, func, sprite);
    }
    public static BlanketSDK.QM.Elements.Controls.VRCButton AddButton(this VRCGroupBox group, string name, string? tooltip, System.Action func, Sprite? sprite = null)
    {
        return new VRCButton(group, name, tooltip, func, sprite);
    } 
    
    public static BlanketSDK.QM.Elements.Controls.VRCButton AddButton(this VRCGroupLayout group, string name, string? tooltip, System.Action func, bool submenu, Sprite? sprite = null)
    {
        return new VRCButton(group, name, tooltip, func, submenu, sprite);
    }
    
    [Obsolete("I have no clue what i did.")]
    public static BlanketSDK.QM.Elements.Controls.VRCButton AddButton(this VRCPage page, Transform parent,string name, string? tooltip, System.Action func, bool submenu, Sprite? sprite = null)
    {
        return new VRCButton(page, parent, name, tooltip, func, sprite);
    }
    #region VRC_QM_MODAL

    public static BlanketSDK.QM.Elements.Controls.VRCButton AddButton(this VRCModalContainer modal, string name, string? tooltip, System.Action func, Sprite? sprite = null)
    {
        return new VRCButton(modal, name, tooltip, func, sprite);
    }
    public static BlanketSDK.QM.Elements.Controls.VRCButton OpenModal(this VRCModalContainer modal, string name, string? tooltip, Sprite? sprite = null)
    {
        
        return new VRCButton(modal, name, tooltip, modal.Open, sprite);
    }
    #endregion

    #endregion
    

   #endregion
  
    
    
   
}