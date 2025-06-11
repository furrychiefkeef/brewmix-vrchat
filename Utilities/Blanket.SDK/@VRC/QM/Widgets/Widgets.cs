using System.Net.Mime;
using System.Runtime.InteropServices;
using BlanketSDK.Core;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using VRC.Udon.Wrapper.Modules;
using VRC.UI;
using VRC.UI.Elements.Controls;

namespace BlanketSDK.Utilities.VRC.QM.Widgets;

internal class VRCWidgets : IQuickMenuElements
{
    public GameObject _obj { get; set; }
    public TextMeshProUGUIEx header_txt { get; set; }
    public bool state;
    public static VRCWidgets Create(string text, [Optional] System.Action func, [Optional] UnityEngine.Color color, [Optional] float duration ,Sprite? sprite = null )
    {
        Transform? obj = UnityEngine.Object.Instantiate(QMElements.Widgets.QMWidgetHeader(), QMElements.Widgets.QMWidgetHeader().parent);

        obj.name = $"{Guid.NewGuid().ToString()}-<Blanket.SDK.QM_WIDGET";
        Transform? Header = obj.GetChild(0);
        
        #region OBJs
        
        Transform? icon_Background  = Header.GetChild(1);
        Transform? LeftItemContainer  = Header.GetChild(2);
        Transform? RightItemContainer  = Header.GetChild(3);
        #endregion
        
        #region REQUIRED_COMPONENTS_HEADER
        ImageEx? _Headerbg = icon_Background.GetComponent<ImageEx>();
        #endregion
        
        #region REQUIRED_COMPONENTS_LEFT_ITEMCONATINER

        TextMeshProUGUIEx? _0 = LeftItemContainer.GetComponentInChildren<TextMeshProUGUIEx>();
    
        ImageEx? _2 = LeftItemContainer.GetComponentInChildren<ImageEx>();

        #endregion
        
        #region REQUIRED_COMPONENTS_RIGHT_ITEMCONATINER
        VRCButtonHandle? _button = RightItemContainer.GetComponentInChildren<VRCButtonHandle>();
        TextMeshProUGUIEx? _text_rightitemcontainer = RightItemContainer.GetComponentInChildren<TextMeshProUGUIEx>();
        #endregion
        
        Button.ButtonClickedEvent? _button_listener = _button.onClick;
        _button_listener.RemoveAllListeners();
        _button_listener.AddListener(func);
        
        if(func == null) _button.gameObject.SetActive(false);
        
        #region TEXT_COMPONENT
        _0.text = text;
        _0.richText = true;
        /*_text_rightitemcontainer.text = description;*/
        
        #endregion 
        
        
        #region IMAGE_COMPONENT

        if (!sprite || !sprite == null) { return null; }
        _2.sprite = sprite;
        
        
        if (color.r == 0 || color.b == 0  || color.g == 0 || color.a == 0) { _Headerbg.canvasRenderer.SetColor(Color.white); }
        _Headerbg.canvasRenderer.SetColor(color);
        #endregion
        /*while (duration > 10)
        {
            obj.gameObject.SetActive(false);
        }*/

        var widget = new VRCWidgets(); 
        widget._obj = obj.gameObject; 
        widget.header_txt = _0;        
        return widget;         
    }

    public void SetState(bool state)
    {
        _obj.GetComponent<LayoutElement>().enabled = state;
        _obj.transform.GetChild(0).gameObject.SetActive(state);
    }
    public void SetText(string text, [Optional] TextMeshProUGUIEx? properties)
    {
        header_txt.text = text;
        properties = header_txt;

    }
    public GameObject GetObject() => _obj;
}