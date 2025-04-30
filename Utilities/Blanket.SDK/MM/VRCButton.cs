using BlanketSDK.QM.Modals;
using BlanketSDK.Utilities;
using UnityEngine;
using UnityEngine.UI;
using VRC.Localization;
using VRC.UI;
using VRC.UI.Elements.Controls;

namespace BlanketSDK.MM;

internal class VRCButton : Root
{
    /// <summary>
    ///  VRCButton this is used to create a SINGLE_MAINMENU_BUTTON
    /// </summary>
    /// <param name="parent">The parent transform where the button will be placed.</param>
    /// <param name="name">The display name of the button.</param>
    /// <param name="tooltip">The tooltip text for the button.</param>
    /// <param name="func">The action to execute when the button is clicked.</param>
    /// <param name="sprite">An optional sprite to display on the button.</param>
    public VRCButton(Transform parent, string? name, string? tooltip, System.Action func, Sprite? sprite = null)
    {
        if (!MainMenu() | !MMSingleButton()) throw new($"button has moved to a different location or index contact: {brewchat.hybridxcore.bep.Properties.Resources.D_Contact}");
        parent ??= _Fallback_Dashboard_Group_MM_UI_Buttons();

        Transform? obj = UnityEngine.Object.Instantiate(MMSingleButton(), parent);
        obj.name = $"{Guid.NewGuid().ToString()}-<MMBLANKED-{name}";

        #region REQUIRED_COMPONENTS
        VRCButtonHandle _0 = obj.GetComponent<VRCButtonHandle>();
        TextMeshProUGUIEx _1 = obj.GetChild(2).GetComponentInChildren<TextMeshProUGUIEx>(true);
        ImageEx _2 = obj.GetChild(6).GetComponent<ImageEx>();
        ToolTip _3 = obj.GetComponent<ToolTip>();
        #endregion

        #region BUTTON COMPONENT
        UnityEngine.UI.Button.ButtonClickedEvent? BUTTONCLICKED_EVENT_0 = _0.onClick;
        BUTTONCLICKED_EVENT_0.RemoveAllListeners();
        BUTTONCLICKED_EVENT_0.AddListener(func);

        #endregion

        #region TEXT COMPONENT
        _1.text = name;
        _1.richText = true;
        #endregion

        #region IMAGE || IMAGE_EX COMPONENT
        _2.overrideSprite = sprite;
        _2.sprite = sprite;
        #endregion

        #region TOOLTIP COMPONENT
        _3._localizableString = tooltip.Localize(); 
        #endregion

    }
    public VRCButton(VRCModalContainer parent, string? name, string? tooltip, System.Action func, Sprite? sprite = null) : this(parent.parent_menu, name, tooltip, func, sprite) { }
}
/*  UnityEngine.UI.Button _0 = obj.GetComponentInChildren<UnityEngine.UI.Button>(); depercated*/