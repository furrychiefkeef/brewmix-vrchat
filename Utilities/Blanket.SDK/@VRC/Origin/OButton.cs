using BlanketSDK.Utilities;
using UnityEngine;
using VRC.UI.Elements.Controls;


namespace BlanketSDK.Origin.Elements.Controls;

internal class OButton : Templates
{
    /// <summary>
    ///  VRCButton this is used to create a SINGLE_MAINMENU_BUTTON
    /// </summary>
    /// <param name="parent">The parent transform where the button will be placed.</param>
    /// <param name="name">The display name of the button.</param>
    public OButton(Transform parent, float piviot_x ,float piviot_y, string? name, System.Action func)
    {
        if (!USERINTERFACE() | !ORIGIN_BUTTON()) throw new($"button has moved to a different location or index contact: {autumn.ml.Properties.Resources.D_Contact}");
        Transform? obj = UnityEngine.Object.Instantiate(ORIGIN_BUTTON(), parent);
        obj.name = $"{Guid.NewGuid().ToString()}-<Blanket.SDK.ORIGIN_BUTTON-{name}";
        
        #region REQUIRED_COMPONENTS
        UnityEngine.UI.Button? _0 = obj.GetComponent<UnityEngine.UI.Button>();
        TextMeshProUGUIEx? _1 = obj.GetComponentInChildren<TextMeshProUGUIEx>();
        RectTransform? _2 = obj.GetComponent<RectTransform>();
        #endregion
        
        #region TEXT_COMPONENT
        
         _1.text = name;
         _1.richText = true;
            
        #endregion
        
        #region BUTTON_COMPONENT

        UnityEngine.UI.Button.ButtonClickedEvent? BUTTONCLICKED_EVENT_0 = _0.onClick;
        BUTTONCLICKED_EVENT_0.RemoveAllListeners();
        BUTTONCLICKED_EVENT_0.AddListener(func);

        #endregion

        #region PositionCorrection

        if (piviot_x == 0 || piviot_y == 0) _2.pivot = new Vector2(0.5f, 0.5f);
        _2.pivot = new Vector2(piviot_x, piviot_y);
        #endregion
    }
}
    