using BlanketSDK.QM.Elements.Modals;
using BlanketSDK.Core;
using UnityEngine;
using VRC.Localization;
using VRC.UI;
using VRC.UI.Elements.Controls;

namespace BlanketSDK.QM.Elements.Controls;

internal class VRCButton : IQuickMenuElements
{
    TextMeshProUGUIEx TMPro;
    UnityEngine.UI.Button.ButtonClickedEvent? BUTTONCLICKED_EVENT_0;
    /// <summary>
    ///  VRCButton this is used to create a SINGLE_QUICKMENU_BUTTON
    /// </summary>
    /// <param name="parent">The parent transform where the button will be placed.</param>
    /// <param name="name">The display name of the button.</param>
    /// <param name="tooltip">The tooltip text for the button.</param>
    /// <param name="func">The action to execute when the button is clicked.</param>
    /// <param name="sprite">An optional sprite to display on the button.</param>
    public VRCButton(Transform parent, string? name, string? tooltip, System.Action func, bool submenu = false, Sprite? sprite = null)
    {
        if (!QuickMenu() | !QMControls.Buttons.SingleButton()) throw new($"button has moved to a different location or index contact: {brewchat.hybridxcore.bep.Properties.Resources.D_Contact}");
        parent ??= QMFallbacks._Fallback_Dashboard_Group_QM_UI_Buttons();
        Transform? obj = UnityEngine.Object.Instantiate(QMControls.Buttons.SingleButton(), parent);
        obj.name = $"{Guid.NewGuid().ToString()}-<Blanket.SDK.QM_SINGLE_BUTTON-{name}";

        #region REQUIRED_COMPONENTS
        VRCButtonHandle _0 = obj.GetComponent<VRCButtonHandle>();
        TextMeshProUGUIEx _1 = obj.GetComponentInChildren<TextMeshProUGUIEx>(true);
        ImageEx _2 = obj.GetChild(3).GetChild(0).GetComponent<ImageEx>();
        obj.GetChild(4).GetComponent<ImageEx>().gameObject.SetActive(submenu); // Expanded Button
        UnityEngine.Object.DestroyImmediate(obj.GetComponent<ToolTip>());
        ToolTip _3 = obj.GetComponent<ToolTip>();
        #endregion
        #region BUTTON COMPONENT
        
        BUTTONCLICKED_EVENT_0 = _0.onClick;
        BUTTONCLICKED_EVENT_0.RemoveAllListeners();
        BUTTONCLICKED_EVENT_0.AddListener(func);
        
        #endregion

        #region TEXT COMPONENT
        _1.text = name;
        _1.richText = true;
        TMPro = _1;
        #endregion
        
        #region TOOLTIP COMPONENT
        if (_3.GetType() == typeof(ToolTip)) _3._localizableString = tooltip.Localize(); 
        #endregion
        
        #region IMAGE || IMAGE_EX COMPONENT
        if (!sprite) return;
        _2.sprite = sprite;
        #endregion

    }
    public VRCButton(VRCModalContainer modal, string? name, string? tooltip, System.Action func, Sprite? sprite = null)
    {
        if (!QuickMenu() | !QMControls.Buttons.SingleButton()) throw new($"button has moved to a different location or index contact: {brewchat.hybridxcore.bep.Properties.Resources.D_Contact}");
        Transform? obj = UnityEngine.Object.Instantiate(QMControls.Buttons.SingleButton(), modal.parent_menu);
        obj.name = $"{Guid.NewGuid().ToString()}-<Blanket.SDK.QM_SINGLE_BUTTON-{name}";

        #region REQUIRED_COMPONENTS
        VRCButtonHandle _0 = obj.GetComponent<VRCButtonHandle>();
        TextMeshProUGUIEx _1 = obj.GetComponentInChildren<TextMeshProUGUIEx>(true);
        ImageEx _2 = obj.GetChild(3).GetChild(0).GetComponent<ImageEx>();
        
        UnityEngine.Object.DestroyImmediate(obj.GetComponent<ToolTip>());
        ToolTip _3 = obj.GetComponent<ToolTip>();
        #endregion

        #region BUTTON COMPONENT
        BUTTONCLICKED_EVENT_0 = _0.onClick;
        
        BUTTONCLICKED_EVENT_0.RemoveAllListeners();
        BUTTONCLICKED_EVENT_0.AddListener(func);
        _0._controlName = modal.Name;
        #endregion

        #region TEXT COMPONENT
        _1.text = name;
        _1.richText = true;
        TMPro = _1;
        #endregion
        
        #region TOOLTIP COMPONENT
        if (_3.GetType() == typeof(ToolTip)) _3._localizableString = tooltip.Localize(); 
        #endregion
        
        #region IMAGE || IMAGE_EX COMPONENT
        if (!sprite) return;
        _2.sprite = sprite;
        #endregion

    }
    public VRCButton(VRCPage page, Transform parent, string? name, string? tooltip, System.Action func = null, Sprite? sprite = null)
    {
        if (!QuickMenu() | !QMControls.Buttons.SingleButton()) throw new($"button has moved to a different location or index contact: {brewchat.hybridxcore.bep.Properties.Resources.D_Contact}");
        Transform? obj = UnityEngine.Object.Instantiate(QMControls.Buttons.SingleButton(), parent);
        obj.name = $"{Guid.NewGuid().ToString()}-<Blanket.SDK.QM_SINGLE_BUTTON-{name}";

        #region REQUIRED_COMPONENTS
        VRCButtonHandle _0 = obj.GetComponent<VRCButtonHandle>();
        TextMeshProUGUIEx _1 = obj.GetComponentInChildren<TextMeshProUGUIEx>(true);
        ImageEx _2 = obj.GetChild(3).GetChild(0).GetComponent<ImageEx>();
        
        obj.GetChild(4).GetComponent<ImageEx>().gameObject.SetActive(true);
        
        UnityEngine.Object.DestroyImmediate(obj.GetComponent<ToolTip>());
        ToolTip _3 = obj.GetComponent<ToolTip>();
        #endregion

        #region BUTTON COMPONENT
        _0._controlName = page.Name;
        if (func != null)
        {
            BUTTONCLICKED_EVENT_0 = _0.onClick;
            BUTTONCLICKED_EVENT_0 = new();
            BUTTONCLICKED_EVENT_0.AddListener(func);
        }
      
        #endregion

        #region TEXT COMPONENT
        _1.text = name;
        _1.richText = true;
        TMPro = _1;
        #endregion
        
        #region TOOLTIP COMPONENT
        if (_3.GetType() == typeof(ToolTip)) _3._localizableString = tooltip.Localize(); 
        #endregion
        
        #region IMAGE || IMAGE_EX COMPONENT
        if (!sprite) return;
        _2.sprite = sprite;
        #endregion

    }
    public void Invoke()
    {
        BUTTONCLICKED_EVENT_0?.Invoke();
    }
    public TextMeshProUGUIEx SetText(string text)
    {
        TMPro.text = text;
        return TMPro;
    }
    /*public VRCButton(VRCModalContainer parent, string? name, string? tooltip, System.Action func, Sprite? sprite = null) : this(parent.parent_menu, name, tooltip, func, sprite) { }*/
    public VRCButton(VRCGroupBox parent, string? name, string? tooltip, System.Action func, bool submenu, Sprite? sprite = null) : this(parent.container, name, tooltip, func, sprite) { }
    public VRCButton(VRCGroupLayout parent, string? name, string? tooltip, System.Action func, bool submenu, Sprite? sprite = null) : this(parent.container, name, tooltip, func, submenu ,sprite) { }
}