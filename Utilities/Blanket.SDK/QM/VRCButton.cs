using BlanketSDK.Utilities;
using UnityEngine;
using UnityEngine.UI;
using VRC.Localization;
using VRC.UI.Elements.Controls;

namespace BlanketSDK.QM;

internal class VRCButton : Root
{
    /// <summary>
    ///  VRCButton this is used to create a SINGLE_QUICKMENU_BUTTON
    /// </summary>
    /// <param name="parent">The parent transform where the button will be placed.</param>
    /// <param name="name">The display name of the button.</param>
    /// <param name="tooltip">The tooltip text for the button.</param>
    /// <param name="func">The action to execute when the button is clicked.</param>
    /// <param name="sprite">An optional sprite to display on the button.</param>
    public VRCButton(Transform parent, string? name, string? tooltip, System.Action func, Sprite? sprite = null)
    {
        if (!Button()) throw new($"button has moved to a different location or index contact: {brewchat.hybridxcore.bep.Properties.Resources.D_Contact}");
        parent ??= _Fallback_Dashboard_Group_QM_Buttons().transform;

        Transform? obj = UnityEngine.Object.Instantiate(Button(), parent);
        obj.name = $" {Guid.NewGuid().ToString()}-<Blanket.SDK.SINGLE_BUTTON-{name}";

        #region REQUIRED_COMPONENTS
        VRCButtonHandle _0 = obj.GetComponent<VRCButtonHandle>();
        TextMeshProUGUIEx _1 = obj.GetComponentInChildren<TextMeshProUGUIEx>(true);
        Image _2 = obj.GetChild(3).GetChild(0).GetComponent<Image>();
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

        #region IMAGE COMPONENT
        _2.overrideSprite = sprite;
        _2.sprite = sprite;
        #endregion

        #region TOOLTIP COMPONENT

        UnityEngine.Object.DestroyImmediate(obj.GetComponent<ToolTip>());
        if (_3.GetType() == typeof(ToolTip))  _3._localizableString = tooltip.Localize(); 
        #endregion

    }
}
/*  UnityEngine.UI.Button _0 = obj.GetComponentInChildren<UnityEngine.UI.Button>(); depercated*/