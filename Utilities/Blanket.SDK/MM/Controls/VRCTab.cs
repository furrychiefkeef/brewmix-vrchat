using BlanketSDK.Utilities;
using UnityEngine;
using VRC.Localization;
using VRC.UI;
using VRC.UI.Elements.Controls;

namespace BlanketSDK.MM.Elements.Controls;
internal class VRCTab : Templates
{
    /// <summary>
    ///  VRCButton this is used to create a TAB_MAINMENU_BUTTON
    /// </summary>
    /// <param name="pinkstar"></param>
    /// <param name="name">The display name of the button.</param>
    /// <param name="tooltip">The tooltip text for the button.</param>
    /// <param name="func">The action to execute when the button is clicked.</param>
    /// <param name="sprite">An optional sprite to display on the button.</param>
    /// <param name="parent">The parent transform where the button will be placed.</param>
    public VRCTab(VRCPage page,string? name, string? tooltip, Action func = null, Sprite? sprite = null)
    {
        if (!MainMenu() | !_MM.Tab()) throw new($"button has moved to a different location or index contact: {brewchat.hybridxcore.bep.Properties.Resources.D_Contact}");
        Transform parent = _Fallback_Dashboard_Group_MM_UI_Buttons_TAB();
        Transform? obj = UnityEngine.Object.Instantiate(_MM.Tab(), parent);
        obj.name = $"{Guid.NewGuid().ToString()}-<Blanket.SDK.MM_TAB_BUTTON-{name}";

        #region REQUIRED_COMPONENTS

        MenuTab _0 = obj.GetComponent<MenuTab>();
        TextMeshProUGUIEx _1 = obj.GetComponentInChildren<TextMeshProUGUIEx>();
        ImageEx _2 = obj.GetChild(2).GetComponent<ImageEx>();
        ToolTip _3 = obj.GetComponent<ToolTip>();
        
        #endregion

        #region LOGIC_REQUIRED

        _0._controlName = page.Name;

        #endregion
        #region BUTTON COMPONENT

        UnityEngine.UI.Button.ButtonClickedEvent? BUTTONCLICKED_EVENT_0 = _0.onClick;
        _0.field_Private_MenuStateController_0 = MM()?.field_Protected_MenuStateController_0;
        BUTTONCLICKED_EVENT_0.RemoveAllListeners();
        BUTTONCLICKED_EVENT_0.AddListener(func);

        #endregion
        
        #region TEXT COMPONENT
        _1.text = name;
        _1.richText = true;
        #endregion
        
        #region TOOLTIP COMPONENT
        _3._localizableString = tooltip.Localize();
        #endregion
        
        #region IMAGE || IMAGE_EX COMPONENT
        if (!sprite) return;
        _2.sprite = sprite;
        _2.overrideSprite = sprite;
        #endregion
    }
}
/*public VRCButton(VRCModalContainer parent, string? name, string? tooltip, System.Action func, Sprite? sprite = null) : this(parent.parent_menu, name, tooltip, func, sprite) { }*/

/*  UnityEngine.UI.Button _0 = obj.GetComponentInChildren<UnityEngine.UI.Button>(); depercated*/
