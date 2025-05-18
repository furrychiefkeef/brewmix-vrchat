using BlanketSDK.Core;
using BlanketSDK.QM.Elements.V2;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements.Controls;

namespace BlanketSDK.QM.Elements.Controls.V2;
class VRCToggleOperator : IQuickMenuElements
{
    public VRCToggleOperator(VRCPanel panel, string name, Action<bool> func, bool state = false)
    {
        if(!QM() || !QMElements.QMPanel("Sharing") || !QMControls.OperatorToggle()) throw new($"element has moved to a different location or index contact: {brewchat.hybridxcore.bep.Properties.Resources.D_Contact}");
        Transform? obj  = UnityEngine.Object.Instantiate(QMControls.OperatorToggle(), panel.container);
        obj.name = $"{Guid.NewGuid().ToString()}-<Blanket.SDK.QM_TOGGLE_OPERATOR_{name}";

        #region REQUIRED_COMPONENTS

        MonoBehaviour2PublicSiObSi_tCaOb_t_cBoObUnique? __0  = obj.GetComponent<MonoBehaviour2PublicSiObSi_tCaOb_t_cBoObUnique>();
        #endregion

        #region LOGIC_REQUIRED_API

        VRCToggleHandle? _0 = __0._toggle;
        UnityEngine.Object.DestroyImmediate(__0._toggleSwitch);
        Toggle.ToggleEvent? LISTENER = _0.onValueChanged = new();
        
        _0.isOn = state;
        
        LISTENER.RemoveAllListeners();
        LISTENER.AddListener(func);
        
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
}