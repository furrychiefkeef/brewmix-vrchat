
using BlanketSDK.Utilities;

using UnityEngine;
using VRC.UI.Elements.Controls;
using BlanketSDK.Core;
using UnityEngine.UI;

namespace BlanketSDK.QM.Elements;

internal class VRCGroupLayout : IQuickMenuElements
{
    public Transform? container;
    public VRCGroupLayout(VRCPage page, string name = null, bool isCollapsible  = true, bool alwaysVisable = true)
    {
        if (!QMMenus.QMSubmenus.Menu_Here() || !QMElements.QMLayoutGroup()) throw new($"element has moved to a different location or index contact: {brewchat.hybridxcore.bep.Properties.Resources.D_Contact}");
        Transform? obj = UnityEngine.Object.Instantiate(QMElements.QMLayoutGroup(), page.box);

        
        #region Unessasary Objects this will be fixed in the next update.
        UnityEngine.Object.Destroy(obj.GetChild(0).transform.gameObject);
        UnityEngine.Object.Destroy(obj.GetChild(1).transform.gameObject);
        UnityEngine.Object.Destroy(obj.GetChild(2).transform.gameObject);
        UnityEngine.Object.Destroy(obj.GetChild(5).transform.gameObject);
        UnityEngine.Object.Destroy(obj.GetChild(6).transform.gameObject);
        UnityEngine.Object.Destroy(obj.GetChild(7).transform.gameObject);
        UnityEngine.Object.Destroy(obj.GetChild(8).transform.gameObject);
        UnityEngine.Object.Destroy(obj.GetChild(9).transform.gameObject);
        UnityEngine.Object.Destroy(obj.GetChild(10).transform.gameObject);
        UnityEngine.Object.Destroy(obj.GetChild(11).transform.gameObject);
        #endregion
        
        obj.name = $"{Guid.NewGuid().ToString()}-<Blanket.SDK.QM_GROUPLAYOUT";
        
        Transform? _header = obj.GetChild(3);
       
        
        Transform? _container = obj.GetChild(4);
        _container.DestroyChildren();

        #region header
        
        #region REQUIRED_COMPONENTS
        VRCToggleHandle  _0 =  _header.GetComponentInChildren<VRCToggleHandle>();
        TextMeshProUGUIEx? _1 =  _header.GetComponentInChildren<TextMeshProUGUIEx>();

        #endregion

        #endregion

        #region TEXT COMPONENT
        _1.text = name;
        _1.richText = true;

        #endregion

        #region BUTTON_COMPONENT

        _0.isOn = alwaysVisable;
        Toggle.ToggleEvent TOGGLE_BUTTONEVENT = _0.onValueChanged = new();
        TOGGLE_BUTTONEVENT.RemoveAllListeners();
        TOGGLE_BUTTONEVENT.AddListener(new Action<bool>(x => _container.gameObject.SetActive(x)));
        
        #endregion
        
        #region LOGIC_REQUIRED
        if (name == null) UnityEngine.Object.DestroyImmediate(_1.gameObject);
        if(!isCollapsible) UnityEngine.Object.DestroyImmediate(_header,true);
        container = _container;
        
        #endregion

    }
}