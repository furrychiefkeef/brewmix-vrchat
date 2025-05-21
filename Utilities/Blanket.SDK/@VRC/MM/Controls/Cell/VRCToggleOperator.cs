/*using BlanketSDK.Core;
using BlanketSDK.QM.Elements.V2;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements.Controls;

namespace BlanketSDK.MM.Elements.Controls.V2;
class VRCToggleOperator : IMainMenuElements
{
    public VRCToggleOperator(VRCPanel panel, string name, Action<bool> func, bool state = false)
    {
        if(!MM() || !MMElements.Panel() || !MMControls.OperatorToggle()) throw new($"element has moved to a different location or index contact: {brewchat.hybridxcore.bep.Properties.Resources.D_Contact}");
        Transform? obj  = UnityEngine.Object.Instantiate(MMControls.OperatorToggle(), panel.container);
        obj.name = $"{Guid.NewGuid().ToString()}-<Blanket.SDK.QM_TOGGLE_OPERATOR_{name}";

        #region REQUIRED_COMPONENTS

        MonoBehaviour2PublicSiObSi_tCaOb_t_cBoObUnique? __0  = obj.GetComponent<MonoBehaviour2PublicSiObSi_tCaOb_t_cBoObUnique>();
        #endregion

        #region LOGIC_REQUIRED_API

        VRCToggleHandle? _0 = __0._toggle;
        RadioButton? __1 = __0._toggleSwitch;
        __1.field_Private_Single_0 = 27.4f; // Fix it going off element
        __1.field_Private_Single_1 = 95.4124f; // Fix it going off element
        Toggle.ToggleEvent? LISTENER = _0.onValueChanged;
        
        _0.isOn = state;
        /*QM().field_Protected_MenuStateController_0.inte);#1#
        LISTENER.AddListener(new System.Action<bool>(x =>
        {
            __1.Method_Public_Void_Boolean_PDM_0(x);
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
    }
}*/