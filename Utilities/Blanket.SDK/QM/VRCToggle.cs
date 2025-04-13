

using UnityEngine;
using VRC.Localization;
using VRC.UI.Elements.Controls;

using BlanketSDK.Utilities;
namespace BlanketSDK.QM;

internal class VRCToggle : Root
{
    public VRCToggle(Transform parent, string? name, string? tooltip_on, string? tooltip_off, System.Action<bool> func, Sprite? sprite_on = null, Sprite? sprite_off = null)
    {
        if (!Toggle()) throw new($"toggle has moved to a different location or index contact: {brewchat.hybridxcore.bep.Properties.Resources.D_Contact}");
        parent ??= _Fallback_Dashboard_Group_QM_Buttons().transform;

        Transform? buttonObject = UnityEngine.Object.Instantiate(Toggle(), parent);
        buttonObject.name = $"{Guid.NewGuid().ToString()}-<Blanket.SDK.TOGGLE_BUTTON-{name}";

        #region REQUIRED_COMPONENTS
        UnityEngine.UI.Toggle _0 = buttonObject.GetComponentInChildren<UnityEngine.UI.Toggle>();
        TextMeshProUGUIEx _1 = buttonObject.GetComponentInChildren<TextMeshProUGUIEx>();
        ImageEx _2 = buttonObject.GetComponentInChildren<ImageEx>();
        ToolTip _3 = buttonObject.GetComponentInChildren<ToolTip>();
        #endregion

        #region BUTTON COMPONENT
        UnityEngine.UI.Toggle.ToggleEvent? TOGGLE_EVENT_LISTENER_0 = _0.onValueChanged;
        TOGGLE_EVENT_LISTENER_0.RemoveAllListeners();
        TOGGLE_EVENT_LISTENER_0.AddListener(func);

        #endregion

        #region TEXT COMPONENT
        _1.text = name;
        _1.richText = true;
        #endregion

        #region IMAGE_EX COMPONENT
        _2.sprite = sprite_on;
        _2.sprite = sprite_off;
        #endregion

        #region TOOLTIP COMPONENT
        _3._localizableString = tooltip_on.Localize();
        _3._alternateLocalizableString = tooltip_on.Localize();
        #endregion
    }
}
