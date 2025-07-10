using BlanketSDK.Core;
using BlanketSDK.QM.Elements.V2;
using Brewchat.Game.Wrappers;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements;
using VRC.UI.Elements.Controls;

namespace BlanketSDK.QM.Elements.Controls.V2;
internal class VRCToggleOperator : IQuickMenuElements
{
    public VRCToggleOperator(Transform parent, string name, string tooltip_on, string tooltip_off, Action<bool> func, bool state = false)
    {
        if (!QM() || !QMElements.QMPanel("Debug") || !QMControls.OperatorToggle()) throw new($"element has moved to a different location or index contact: {autumn.ml.Properties.Resources.D_Contact}");
        Transform? obj = UnityEngine.Object.Instantiate(QMControls.OperatorToggle(), parent);
        obj.name = $"{Guid.NewGuid()}-<Blanket.SDK.QM_TOGGLE_OPERATOR_{name}";

        #region REQUIRED_COMPONENTS

        ToggleSelector? __0 = obj.GetComponent<ToggleSelector>();
        #endregion

        #region LOGIC_REQUIRED_API

        VRCToggleHandle? _0 = __0._toggle;
        RadioButton? __1 = __0._toggleSwitch;
        __1.field_Private_Single_0 = 27.5f; // Fix it going off element
        __1.field_Private_Single_1 = 95.4124f; // Fix it going off element
        Toggle.ToggleEvent? LISTENER = _0.onValueChanged;

        _0.isOn = state;
        /*QM().field_Protected_MenuStateController_0.inte);*/
        LISTENER.AddListener(new System.Action<bool>(x =>
        {
            __1.Method_Public_Void_Boolean_PDM_1(x);
            func(x);
        }));
        _0._controlName = name;
        _0.field_Private_Boolean_0 = true;
        _0._sendAnalytics = false;
        #endregion

        #region TEXT_COMPONENT

        TextMeshProUGUIEx? Title = __0._title;
        Title.text = name;
        Title.richText = true;

        #endregion

        #region Tooltip

        __0._tooltips[0]._localizableString = tooltip_on.Localizable();
        __0._tooltips[0]._alternateLocalizableString = tooltip_off.Localizable();
        
        #endregion
    }
}