using BlanketSDK.QM.Elements.Modals;
using BlanketSDK.Core;
using Brewchat.Game.Wrappers;
using UnityEngine;
using VRC.Localization;
using VRC.UI;
using VRC.UI.Elements.Controls;
using BlanketSDK.Utilities;
using VRC.UI.Elements.Buttons;

namespace BlanketSDK.QM.Elements.Controls;

internal class VRCCarouselButton : IQuickMenuElements
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
    /// 
    /// 
    public class Option
    {
        public Sprite? Sprite;
        public string Tooltip;
        public Action Callback;
    }
    public VRCCarouselButton(Transform parent, string? name, string? tooltip, System.Action func = null, bool submenu = false, Sprite? sprite = null)
    {
        if (!QuickMenu() | !QMElements.SelectedUser.SelectedUser_ThreeSwitchButton()) throw new($"button has moved to a different location or index contact: {autumn.ml.Properties.Resources.D_Contact}");
        parent ??= QMFallbacks._Fallback_Dashboard_Group_QM_UI_Buttons();
        Transform? obj = UnityEngine.Object.Instantiate(QMElements.SelectedUser.SelectedUser_ThreeSwitchButton(), parent);
        obj.name = $"{Guid.NewGuid()}-<Blanket.SDK.QM_SINGLE_BUTTON-{name}";

        #region REQUIRED_COMPONENTS
        obj.GetChildren().ToList().ForEach((x) =>
        {
            if (x.transform.GetSiblingIndex() != 0) UnityEngine.Object.Destroy(x);
        });
        AvatarVisibilityThreeWaySwitch _0 = obj.GetComponentInChildren<AvatarVisibilityThreeWaySwitch>(true);
        // ImageEx _2 = obj.GetChild(3).GetChild(0).GetComponent<ImageEx>();

        // obj.GetChild(4).GetComponent<ImageEx>().gameObject.SetActive(submenu); // Expanded Button
        // UnityEngine.Object.DestroyImmediate(obj.GetComponent<ToolTip>());
        // ToolTip _3 = obj.GetComponent<ToolTip>();

        var Button_A = _0.field_Public_Button_0;
        var Button_B = _0.field_Public_Button_1;
        var Button_C = _0.field_Public_Button_2;
        #endregion
        #region BUTTON COMPONENT



        // BUTTONCLICKED_EVENT_0 = _0.onClick;
        // BUTTONCLICKED_EVENT_0.RemoveAllListeners();
        // BUTTONCLICKED_EVENT_0.AddListener(func);

        #endregion

        #region TEXT COMPONENT

        TextMeshProUGUIEx Title = Button_B.GetComponentInChildren<TextMeshProUGUIEx>();
        Title.text = name;
        Title.richText = true;
        TMPro = Title;

        #endregion

        #region TOOLTIP COMPONENT
        // if (_3.GetType() == typeof(ToolTip)) _3._localizableString = tooltip.Localizable();
        #endregion

        #region IMAGE || IMAGE_EX COMPONENT
        // if (!sprite) return;
        // _2.sprite = sprite;
        #endregion

    }
    // public VRCButton(VRCModalContainer modal, string? name, string? tooltip, System.Action func, Sprite? sprite = null)
    // {
    //     if (!QuickMenu() | !QMControls.Buttons.SingleButton()) throw new($"button has moved to a different location or index contact: {autumn.ml.Properties.Resources.D_Contact}");
    //     Transform? obj = UnityEngine.Object.Instantiate(QMControls.Buttons.SingleButton(), modal._verticalLayoutGroup);
    //     obj.name = $"{Guid.NewGuid().ToString()}-<Blanket.SDK.QM_SINGLE_BUTTON-{name}";

    //     #region REQUIRED_COMPONENTS
    //     VRCButtonHandle _0 = obj.GetComponent<VRCButtonHandle>();
    //     TextMeshProUGUIEx _1 = obj.GetComponentInChildren<TextMeshProUGUIEx>(true);
    //     ImageEx _2 = obj.GetChild(3).GetChild(0).GetComponent<ImageEx>();
        
    //     UnityEngine.Object.DestroyImmediate(obj.GetComponent<ToolTip>());
    //     ToolTip _3 = obj.GetComponent<ToolTip>();
    //     #endregion

    //     #region BUTTON COMPONENT
    //     BUTTONCLICKED_EVENT_0 = _0.onClick;
        
    //     BUTTONCLICKED_EVENT_0.RemoveAllListeners();
    //     BUTTONCLICKED_EVENT_0.AddListener(func);
    //     _0._controlName = modal.Name;
    //     #endregion

    //     #region TEXT COMPONENT
    //     _1.text = name;
    //     _1.richText = true;
    //     TMPro = _1;
    //     #endregion
        
    //     #region TOOLTIP COMPONENT
    //     if (_3.GetType() == typeof(ToolTip)) _3._localizableString = tooltip.Localizable(); 
    //     #endregion
        
    //     #region IMAGE || IMAGE_EX COMPONENT
    //     if (!sprite) return;
    //     _2.sprite = sprite;
    //     #endregion

    // }
    // public VRCButton(VRCPage page, Transform parent, string? name, string? tooltip, System.Action func = null, Sprite? sprite = null)
    // {
    //     if (!QuickMenu() | !QMControls.Buttons.SingleButton()) throw new($"button has moved to a different location or index contact: {autumn.ml.Properties.Resources.D_Contact}");
    //     Transform? obj = UnityEngine.Object.Instantiate(QMControls.Buttons.SingleButton(), parent);
    //     obj.name = $"{Guid.NewGuid()}-<Blanket.SDK.QM_SINGLE_BUTTON-{name}";

    //     #region REQUIRED_COMPONENTS
    //     VRCButtonHandle _0 = obj.GetComponent<VRCButtonHandle>();
    //     TextMeshProUGUIEx _1 = obj.GetComponentInChildren<TextMeshProUGUIEx>(true);
    //     ImageEx _2 = obj.GetChild(3).GetChild(0).GetComponent<ImageEx>();
        
    //     obj.GetChild(4).GetComponent<ImageEx>().gameObject.SetActive(true);
        
    //     UnityEngine.Object.DestroyImmediate(obj.GetComponent<ToolTip>());
    //     ToolTip _3 = obj.GetComponent<ToolTip>();
    //     #endregion

    //     #region BUTTON COMPONENT
    //     _0._controlName = page.Name;
    //     if (func != null)
    //     {
    //         BUTTONCLICKED_EVENT_0 = _0.onClick;
    //         BUTTONCLICKED_EVENT_0 = new();
    //         BUTTONCLICKED_EVENT_0.AddListener(func);
    //     }
      
    //     #endregion

    //     #region TEXT COMPONENT
    //     _1.text = name;
    //     _1.richText = true;
    //     TMPro = _1;
    //     #endregion
        
    //     #region TOOLTIP COMPONENT
    //     if (_3.GetType() == typeof(ToolTip)) _3._localizableString = tooltip.Localizable(); 
    //     #endregion
        
    //     #region IMAGE || IMAGE_EX COMPONENT
    //     if (!sprite) return;
    //     _2.sprite = sprite;
    //     #endregion

    // }
    
    #region DEV_ONLY_ADVANCED_METHODS
    public void Invoke()
    {
        BUTTONCLICKED_EVENT_0?.Invoke();
    }
    public TextMeshProUGUIEx SetText(string text)
    {
        TMPro.text = text;
        return TMPro;
    }
    #endregion
 
    /*public VRCButton(VRCModalContainer parent, string? name, string? tooltip, System.Action func, Sprite? sprite = null) : this(parent.parent_menu, name, tooltip, func, sprite) { }*/
    public VRCCarouselButton(VRCGroupBox parent, string? name, string? tooltip, System.Action func, bool submenu = false, Sprite? sprite = null) : this(parent.container, name, tooltip, func, sprite) { }
    public VRCCarouselButton(VRCGroupLayout parent, string? name, string? tooltip, System.Action func, bool submenu = false, Sprite? sprite = null) : this(parent.container.parent.parent, name, tooltip, func, submenu , sprite) { }
}