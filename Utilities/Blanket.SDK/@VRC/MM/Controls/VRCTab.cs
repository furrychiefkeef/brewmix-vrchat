using BlanketSDK.Utilities;
using BlanketSDK.Core;
using Brewchat.Game.Wrappers;
using UnityEngine;
using VRC.Localization;
using VRC.UI;
using VRC.UI.Elements.Controls;

namespace BlanketSDK.MM.Elements.Controls;
internal class VRCTab : IMainMenuElements
{
    Transform? obj;

    /// <summary>
    ///  VRCButton this is used to create a TAB_MAINMENU_BUTTON
    /// </summary>
    /// <param name="pinkstar"></param>
    /// <param name="name">The display name of the button.</param>
    /// <param name="tooltip">The tooltip text for the button.</param>
    /// <param name="func">The action to execute when the button is clicked.</param>
    /// <param name="sprite">An optional sprite to display on the button.</param>
    /// <param name="parent">The parent transform where the button will be placed.</param>
    
    public VRCTab(VRCPage page, string? name, string? tooltip, Action func = null, Sprite? sprite = null)
    {
        if (!MainMenu() | !MMControls.Tab()) throw new($"button has moved to a different location or index contact: {autumn.ml.Properties.Resources.D_Contact}");
        Transform parent = MMFallbacks._Fallback_Dashboard_Group_MM_UI_Buttons_TAB();
        obj = UnityEngine.Object.Instantiate(MMControls.Tab(), parent);
        obj.name = $"{Guid.NewGuid()}-<Blanket.SDK.MM_TAB_BUTTON-{name}";

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
        _3._localizableString = tooltip.Localizable();
        #endregion
        
        #region IMAGE || IMAGE_EX COMPONENT
        if (!sprite) return;
        _2.sprite = sprite;
        _2.overrideSprite = sprite;
        #endregion
        


    }
    private GameObject GetBadge() => obj.transform.GetChild(0).gameObject;
    public VRCTab SetBadge(bool Enable) { GetBadge().active = Enable; return this; }
    public VRCTab SetBadgeColor(Color color) { GetBadge().transform.GetChild(0).GetComponent<CanvasRenderer>().SetColor(color);  return this; }
    public VRCTab SetText(string text, TextMeshProUGUIEx? properties = null)
    {
        TextMeshProUGUIEx textMeshProComp = GetBadge().GetComponentInChildren<TextMeshProUGUIEx>();
        textMeshProComp.text = text;
        properties = textMeshProComp;
        return this;
    }
}

/*public VRCButton(VRCModalContainer parent, string? name, string? tooltip, System.Action func, Sprite? sprite = null) : this(parent.parent_menu, name, tooltip, func, sprite) { }*/

/*  UnityEngine.UI.Button _0 = obj.GetComponentInChildren<UnityEngine.UI.Button>(); depercated*/
