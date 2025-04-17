using BlanketSDK.Utilities;
using UnityEngine;
using VRC.Localization;
using VRC.UI;
using VRC.UI.Elements.Controls;

namespace BlanketSDK.QM;

internal class VRCSmallButton : Root
{
    public VRCSmallButton(Transform parent, string? tooltip ,System.Action func, Sprite sprite)
    {
        if (!ExpandButton()) throw new($"button has moved to a different location or index contact: {brewchat.hybridxcore.bep.Properties.Resources.D_Contact}");
        parent ??= _Fallback_Dashboard_Group_QM_UI_Buttons().transform;
        UnityEngine.Transform? obj = UnityEngine.Object.Instantiate(ExpandButton(), parent);
        obj.name = $"{Guid.NewGuid().ToString()}-<Blanket.SDK.EXPANDSMALL_BUTTON";

        #region REQUIRED_COMPONENTS
        VRCButtonHandle  _0 = obj.GetComponent<VRCButtonHandle>();
        ImageEx  _1 = obj.GetChild(1).GetComponent<ImageEx>();
        ToolTip  _2 = obj.GetChild(1).GetComponent<ToolTip>();
        #endregion

        #region BUTTON COMPONENT
        UnityEngine.UI.Button.ButtonClickedEvent? BUTTONCLICKED_EVENT_0 = _0.onClick;
        BUTTONCLICKED_EVENT_0.RemoveAllListeners();
        BUTTONCLICKED_EVENT_0.AddListener(func);
        #endregion
        
        #region IMAGE || IMAGE_EX COMPONENT
        _1.sprite = sprite;
        #endregion

        #region TOOLTIP COMPONENT
        _2._localizableString = tooltip.Localize();
        #endregion
    }
}