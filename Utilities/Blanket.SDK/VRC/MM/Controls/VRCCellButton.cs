using BlanketSDK.Utilities;
using BlanketSDK.Utilities.Unbearable;
using UnityEngine;
using UnityEngine.UI;
using VRC.Localization;
using VRC.UI;
using VRC.UI.Elements.Controls;

namespace BlanketSDK.MM.Elements.Controls;

internal class VRCCellButton : IMainMenuElements
{
    /// <summary>
    ///  VRCButton this is used to create a SINGLE_MAINMENU_CELLBUTTON
    /// </summary>
    /// <param name="parent">The parent transform where the button will be placed.</param>
    /// <param name="name">The display name of the button.</param>
    /// <param name="tooltip">The tooltip text for the button.</param>
    /// <param name="func">The action to execute when the button is clicked.</param>
    /// <param name="sprite">An optional sprite to display on the button.</param>
    public VRCCellButton(Transform parent, string? name, string? tooltip, Sprite? sprite = null)
    {
        if (!MainMenu() | !MMControls.Buttons.SingleButton()) throw new($"button has moved to a different location or index contact: {brewchat.hybridxcore.bep.Properties.Resources.D_Contact}");
        parent ??= MMFallbacks._Fallback_Dashboard_Group_MM_UI_Buttons();

        Transform? obj = UnityEngine.Object.Instantiate(MMControls.Buttons.SingleButton(), parent);
        obj.name = $"{Guid.NewGuid().ToString()}-<Blanket.SDK.MM_SINGLE_BUTTON-{name}";

        #region REQUIRED_COMPONENTS
        VRCButtonHandle _0 = obj.GetComponent<VRCButtonHandle>();
        _0._controlName = name;
        TextMeshProUGUIEx _1 = obj.GetChild(2).GetComponentInChildren<TextMeshProUGUIEx>(true);
        ImageEx _2 = obj.GetChild(6).GetComponent<ImageEx>();
        ToolTip _3 = obj.GetComponent<ToolTip>();
        #endregion

        #region BUTTON COMPONENT
        /*UnityEngine.UI.Button.ButtonClickedEvent? BUTTONCLICKED_EVENT_0 = _0.onClick;
        BUTTONCLICKED_EVENT_0.RemoveAllListeners();
        BUTTONCLICKED_EVENT_0.AddListener(func);*/

        #endregion

        #region TEXT COMPONENT
        _1.text = name;
        _1.richText = true;
        #endregion
        
        #region TOOLTIP COMPONENT
        _3._localizableString = tooltip.Localize(); 
        #endregion
        
        #region IMAGE || IMAGE_EX COMPONENT
        if(!sprite) return;
        _2.sprite = sprite;
        #endregion

    }
    public VRCCellButton(VRCPage parent, string? name, string? tooltip, Sprite? sprite = null) : this(parent.navigation_buttons, name, tooltip, sprite) { }
}
/*  UnityEngine.UI.Button _0 = obj.GetComponentInChildren<UnityEngine.UI.Button>(); depercated*/