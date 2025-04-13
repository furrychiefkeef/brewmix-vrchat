

using UnityEngine;
using VRC.Localization;
using VRC.UI.Elements.Controls;

using BlanketSDK.Utilities;
using UnityEngine.UI;
namespace BlanketSDK.QM;

internal class VRCToggle : Root
{
    public VRCToggle(Transform? parent, string? name, string? tooltip_on, string? tooltip_off, System.Action<bool> func, Sprite? sprite_on = null, Sprite? sprite_off = null)
    {
        if (!Toggle()) throw new($"toggle has moved to a different location or index contact: {brewchat.hybridxcore.bep.Properties.Resources.D_Contact}");
        parent ??= _Fallback_Dashboard_Group_QM_Buttons().transform;

        Transform? obj = UnityEngine.Object.Instantiate(Toggle(), parent);
        obj.name = $"{Guid.NewGuid().ToString()}-<Blanket.SDK.TOGGLE_BUTTON-{name}";

        #region REQUIRED_COMPONENTS
        UnityEngine.UI.Toggle _0 = obj.GetComponent<UnityEngine.UI.Toggle>();
        TextMeshProUGUIEx _1 = obj.GetComponentInChildren<TextMeshProUGUIEx>();
        Image _2on = obj.GetChild(2).GetChild(1).GetComponent<Image>();
        Image _2off = obj.GetChild(2).GetChild(0).GetComponent<Image>();
        ToolTip _3 = obj.GetComponent<ToolTip>();
        #endregion

        #region BUTTON COMPONENT

        UnityEngine.UI.Toggle.ToggleEvent? TOGGLE_EVENT_LISTENER_0 = _0.onValueChanged;
        TOGGLE_EVENT_LISTENER_0.AddListener(func);

        #endregion

        #region TEXT COMPONENT
        _1.text = name;
        _1.richText = true;
        #endregion

        #region IMAGE_EX COMPONENT
        _2on.sprite = sprite_on;
        _2off.sprite = sprite_off;
        #endregion

        #region TOOLTIP COMPONENT
        _3._localizableString = tooltip_on.Localize();
        _3._alternateLocalizableString = tooltip_off.Localize();
        #endregion
    }
}
