/*using BlanketSDK.Core;
using BlanketSDK.QM.Elements.V2;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Element;
using VRC.UI.Elements.Controls;

namespace BlanketSDK.QM.Elements.Controls.V2;
class VRCButtonOperator : IQuickMenuElements
{
    public VRCButtonOperator(VRCPanel panel, string name, System.Action func, bool state = false)
    {
        if(!QM() || !QMElements.QMPanel("Sharing") || !QMControls.OperatorToggle()) throw new($"element has moved to a different location or index contact: {brewchat.hybridxcore.bep.Properties.Resources.D_Contact}");
        Transform? obj  = UnityEngine.Object.Instantiate(QMControls.OperatorToggle(), panel.container);
        obj.name = $"{Guid.NewGuid().ToString()}-<Blanket.SDK.QM_TOGGLE_OPERATOR_{name}";

        #region REQUIRED_COMPONENTS

        ButtonsSetting? __0  = obj.GetComponent<ButtonsSetting>();
        #endregion

        #region LOGIC_REQUIRED_API

        /*VRCButtonHandle? _0 = __0.;
        RadioButton? __1 = __0._toggleSwitch;
        __1.field_Private_Single_0 = 27.4f; // Fix it going off element
        __1.field_Private_Single_1 = 95.4124f; // Fix it going off element
        Toggle.ToggleEvent? LISTENER = _0.onValueChanged;#1#

        _0.isOn = state;
        /*QM().field_Protected_MenuStateController_0.inte);
        LISTENER.AddListener(func);
    
        _0._controlName = name;
        _0.field_Private_Boolean_0 = true;
        _0._sendAnalytics = false;
        #endregion#1#
       
        #region TEXT_COMPONENT
        
        TextMeshProUGUIEx? Title = __0._title;
        Title.text = name;
        Title.richText = true;

        #endregion
    }
}*/