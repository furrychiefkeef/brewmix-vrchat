using BlanketSDK.Utilities;
using UnityEngine;
using VRC.Localization;
using VRC.UI;
using VRC.UI.Elements.Controls;

namespace BlanketSDK.QM.Elements.Controls;
internal class VRCTab : Templates
{
    /// <summary>
    ///  VRCButton this is used to create a TAB_QUICKMENU_BUTTON
    /// </summary>
    /// <param name="page"> The VRCUIPage
    /// <param name="name">The display name of the button.</param>
    /// <param name="tooltip">The tooltip text for the button.</param>
    /// <param name="func">The action to execute when the button is clicked.</param>
    /// <param name="sprite">An optional sprite to display on the button.</param>
    public VRCTab(VRCPage page, string? tooltip, System.Action func = null, Sprite? sprite = null)
    {
        if (!QuickMenu() | !_QM.Tab()) throw new($"button has moved to a different location or index contact: {brewchat.hybridxcore.bep.Properties.Resources.D_Contact}");
        Transform parent = _Fallback_Dashboard_Group_QM_UI_Buttons_TAB();
        Transform? obj = UnityEngine.Object.Instantiate(_QM.Tab(), parent);
        obj.name = $"{Guid.NewGuid().ToString()}-<Blanket.SDK.QM_TAB_BUTTON-{page.Name}";

        #region REQUIRED_COMPONENTS

        MenuTab _0 = obj.GetComponent<MenuTab>();
        ImageEx _1 = obj.GetChild(2).GetComponent<ImageEx>();
        ToolTip _2 = obj.GetComponent<ToolTip>();

        #endregion
        
        #region REQUIRED_LOGIC

        _0.field_Private_MenuStateController_0 = QM()?.field_Protected_MenuStateController_0;
        _0._controlName = page.Name;

        #endregion
        
        #region BUTTON COMPONENT

        UnityEngine.UI.Button.ButtonClickedEvent? BUTTONCLICKED_EVENT_0 = _0.onClick;
       
        BUTTONCLICKED_EVENT_0.RemoveAllListeners();
        BUTTONCLICKED_EVENT_0.AddListener(func);

        #endregion

        #region TOOLTIP COMPONENT
        _2._localizableString = tooltip.Localize();
        #endregion
  
        #region IMAGE || IMAGE_EX COMPONENT
        if (!sprite) return;
        _1.sprite = sprite;
        _1.overrideSprite = sprite;
        #endregion
    }
}
/*public VRCButton(VRCModalContainer parent, string? name, string? tooltip, System.Action func, Sprite? sprite = null) : this(parent.parent_menu, name, tooltip, func, sprite) { }*/

/*  UnityEngine.UI.Button _0 = obj.GetComponentInChildren<UnityEngine.UI.Button>(); depercated*/
