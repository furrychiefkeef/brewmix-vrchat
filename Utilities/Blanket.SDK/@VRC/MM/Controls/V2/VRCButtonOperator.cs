using BlanketSDK.Core;
using BlanketSDK.MM.Elements;
using BlanketSDK.QM.Elements.V2;
using Brewchat.Game.Wrappers;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Element;
using VRC.UI.Elements.Controls;

namespace BlanketSDK.MM.Elements.Controls.V2;
class VRCButtonOperator : IMainMenuElements
{
    
    public class VRCParams
    {
        public string? _title = String.Empty;
        public string? _subtitle = String.Empty;
        public string? _text = String.Empty;
        public string? _tooltip = String.Empty;
        public string? _maintooltip = String.Empty;
    }
    public VRCButtonOperator(Transform parent, VRCParams parameters, System.Action func, bool state = false)
    {
        if(!MM() || !MMControls.OperatorButton()) throw new($"element has moved to a different location or index contact: {autumn.ml.Properties.Resources.D_Contact}");
        Transform? obj = UnityEngine.Object.Instantiate(MMControls.OperatorButton(),parent);
        obj.name = $"{Guid.NewGuid().ToString()}-<Blanket.SDK.QM_TOGGLE_OPERATOR_{parameters._title}";

        #region REQUIRED_COMPONENTS

        /*Transform _rightItemContainer = obj.GetChild(0);
        Transform _leftItemContainer = obj.GetChild(1);
        Transform _TitleMainContainer = obj.GetChild(2);*/
        
        
        ButtonsSetting? __0  = obj.GetComponent<ButtonsSetting>();
        #endregion

        #region LOGIC_REQUIRED_API

        AudioDeviceButton _0 = __0._rightButtons[0];

        _0.transform.gameObject.SetActive(true);
        _0._label.text = parameters._text;
        Button.ButtonClickedEvent? LISTENER = _0._button.onClick;
        
        /*MM().field_Protected_MenuStateController_0.int);*/
        LISTENER.AddListener(func);

        #region VRC_BUTTON_HANDLE
        
        _0._button._controlName = $"{parameters._text}.btn_handle";
        _0._button.prop_Boolean_0 = true;
        _0._button._sendAnalytics = false;
        
        #endregion
        
        #endregion
       
        #region TEXT_COMPONENT
        
        TextMeshProUGUIEx? Title = __0._title;
        Title.text = parameters._title;
        Title.richText = true;

        __0._subtitle.transform.gameObject.SetActive(true);
        TextMeshProUGUIEx Subtitles = __0._subtitle;
        Subtitles.richText = true; 
        Subtitles.text = parameters._tooltip;

        ToolTip _tooltip = _0._button._tooltip;
        _tooltip._localizableString = parameters._tooltip.Localizable();
        #endregion
    }
    public VRCButtonOperator(VRCCellContainer panel, VRCParams parameters, System.Action func, bool state = false)
    {
        if(!MM() || !MMControls.OperatorButton()) throw new($"element has moved to a different location or index contact: {autumn.ml.Properties.Resources.D_Contact}");
        Transform? obj = UnityEngine.Object.Instantiate(MMControls.OperatorButton(),panel._verticalLayoutGroup);
        obj.name = $"{Guid.NewGuid()}-<Blanket.SDK.QM_TOGGLE_OPERATOR_{parameters._title}";

        #region REQUIRED_COMPONENTS

        /*Transform _rightItemContainer = obj.GetChild(0);
        Transform _leftItemContainer = obj.GetChild(1);
        Transform _TitleMainContainer = obj.GetChild(2);*/
        
        
        ButtonsSetting? __0  = obj.GetComponent<ButtonsSetting>();
        #endregion

        #region LOGIC_REQUIRED_API

        AudioDeviceButton _0 = __0._rightButtons[0];

        _0.transform.gameObject.SetActive(true);
        _0._label.text = parameters._text;
        Button.ButtonClickedEvent? LISTENER = _0._button.onClick;
        
        /*MM().field_Protected_MenuStateController_0.int);*/
        LISTENER.AddListener(func);

        #region VRC_BUTTON_HANDLE
        
        _0._button._controlName = $"{parameters._text}.btn_handle";
        _0._button.prop_Boolean_0 = true;
        _0._button._sendAnalytics = false;
        
        #endregion
        
        #endregion*/
       
        #region TEXT_COMPONENT
        
        TextMeshProUGUIEx? Title = __0._title;
        Title.text = parameters._title;
        Title.richText = true;

        __0._subtitle.transform.gameObject.SetActive(true);
        __0._subtitle.text = parameters._subtitle;

        __0._tooltips[0]._localizableString = parameters._maintooltip.Localizable();
        __0._rightButtons[0]._tooltips[0]._localizableString = parameters._tooltip.Localizable();
        #endregion
    }
}