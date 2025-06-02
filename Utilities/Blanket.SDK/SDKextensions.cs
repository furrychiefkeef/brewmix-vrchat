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
    public static BlanketSDK.QM.Elements.Controls.VRCToggle AddToggle(this Transform? parent, string? name, string? tooltip_on, string? tooltip_off, System.Action<bool> func, bool state = false, Sprite? sprite_on = null, Sprite? sprite_off = null)
    {
        return new VRCToggle(parent, name, tooltip_on, tooltip_off, func, state, sprite_on, sprite_off);
    }
    public static BlanketSDK.QM.Elements.Controls.VRCToggle AddToggle(this VRCGroupBox group, string? name, string? tooltip_on, string? tooltip_off, System.Action<bool> func, bool state = false, Sprite? sprite_on = null, Sprite? sprite_off = null)
    {
        return new VRCToggle(group, name, tooltip_on, tooltip_off, func, state, sprite_on, sprite_off);
    } 
    
    public static BlanketSDK.QM.Elements.Controls.VRCToggle AddToggle(this VRCGroupLayout group, string? name, string? tooltip_on, string? tooltip_off, System.Action<bool> func, bool state = false, Sprite? sprite_on = null, Sprite? sprite_off = null)
    {
        return new(group, name, tooltip_on, tooltip_off, func, state, sprite_on, sprite_off);
    }
    
    /*[Obsolete("I have no clue what i did.")]
    public static BlanketSDK.QM.Elements.Controls.VRCButton AddToggle(this VRCPage page, Transform parent, string? name, string? tooltip_on, string? tooltip_off, System.Action<bool> func, bool state = false, Sprite? sprite_on = null, Sprite? sprite_off = null)
    {
        return new VRCButton(page, parent, name, tooltip, func, sprite);
    }*/
    #region VRC_QM_MODAL

    public static BlanketSDK.QM.Elements.Controls.VRCButton AddButton(this VRCModalContainer modal, string name, string? tooltip, System.Action func, Sprite? sprite = null)
    {
        return new VRCButton(modal, name, tooltip, func, sprite);
    }
    public static BlanketSDK.QM.Elements.Controls.VRCToggle AddToggle(this VRCModalContainer modal, string? name, string? tooltip_on, string? tooltip_off, System.Action<bool> func, bool state = false, Sprite? sprite_on = null, Sprite? sprite_off = null)
    {
        return new(modal, name, tooltip_on, tooltip_off, func, state, sprite_on, sprite_off);
    }
    public static BlanketSDK.QM.Elements.Controls.VRCSmallButton AddMiniButton(this VRCModalContainer modal, string? tooltip, System.Action func, Sprite? sprite = null)
    {
        return new(modal,tooltip,func,sprite);
    }
    public static BlanketSDK.QM.Elements.Controls.VRCButton OpenModal(this VRCModalContainer modal, string name, string? tooltip, Sprite? sprite = null)
    {
        
        return new VRCButton(modal, name, tooltip, modal.Open, sprite);
    }
    #endregion

    #endregion
    

   #endregion

   #region VRC_MM

   

   #endregion
    
    
   
}