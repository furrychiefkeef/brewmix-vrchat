

using UnityEngine;
using VRC.Localization;
using VRC.UI.Elements.Controls;

using BlanketSDK.Utilities;
using BlanketSDK.QM.Elements.Modals;
using VRC.UI;
using VRC.UI.Elements.Tooltips;

namespace BlanketSDK.QM.Elements.Controls;
internal class VRCToggle : Templates
{
    /// <summary>
    ///  VRCButton this is used to create a TOGGLE_QUICKMENU_BUTTON
    /// </summary>
    /// <param name="parent">The parent transform where the button will be placed.</param>
    /// <param name="name">The display name of the button.</param>
    /// <param name="tooltip_on">The tooltip text for the button.</param>
    /// <param name="tooltip_off">The tooltip text for the button.</param>
    /// <param name="func">The action to execute when the button is clicked.</param>
    /// <param name="sprite_on">An optional sprite to display on the button.</param>
    /// <param name="sprite_off">An optional sprite to display on the button.</param>
    public VRCToggle(Transform? parent, string? name, string? tooltip_on, string? tooltip_off, System.Action<bool> func, bool state = false, Sprite? sprite_on = null, Sprite? sprite_off = null)
    {
        if (!QuickMenu() | !_QM.ToggleButton()) throw new($"toggle has moved to a different location or index contact: {brewchat.hybridxcore.bep.Properties.Resources.D_Contact}");
        parent ??= _Fallback_Dashboard_Group_QM_UI_Buttons();

        Transform? obj = UnityEngine.Object.Instantiate(_QM.ToggleButton(), parent);
        obj.name = $"{Guid.NewGuid().ToString()}-<Blanket.SDK.QM_TOGGLE_BUTTON-{name}";

        #region REQUIRED_COMPONENTS
        VRCToggleHandle _0 = obj.GetComponent<VRCToggleHandle>();
        
        TextMeshProUGUIEx _1 = obj.GetComponentInChildren<TextMeshProUGUIEx>();
        ImageEx _2on = obj.GetChild(2).GetChild(1).GetComponent<ImageEx>();
        ImageEx _2off = obj.GetChild(2).GetChild(0).GetComponent<ImageEx>();
        UiToggleTooltip _3 = obj.GetComponent<UiToggleTooltip>();
        #endregion

        #region BUTTON COMPONENT

        UnityEngine.UI.Toggle.ToggleEvent? TOGGLE_EVENT_LISTENER_0 = _0.onValueChanged;
        _0.isOn = state;
        TOGGLE_EVENT_LISTENER_0.AddListener(func);
        _0._controlName = name;
        #endregion

        #region TEXT COMPONENT
        _1.text = name;
        _1.richText = true;
        #endregion
        
        #region TOOLTIP COMPONENT
        _3._localizableString = tooltip_on.Localize();
        _3._alternateLocalizableString = tooltip_off.Localize();
        #endregion
        
        #region IMAGE || IMAGE_EX COMPONENT
        if(!sprite_on | !sprite_off) return;
        _2on.sprite = sprite_on;
        _2off.sprite = sprite_off;
        #endregion
    }
    public VRCToggle(VRCModalContainer parent, string? name, string? tooltip_on, string? tooltip_off, System.Action<bool> func, Sprite? sprite_on = null, Sprite? sprite_off = null) : this(parent.parent_menu, name, tooltip_on, tooltip_off, func, sprite_on, sprite_off) { }
}
