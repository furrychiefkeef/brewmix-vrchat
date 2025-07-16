using BlanketSDK.QM.Elements;
using BlanketSDK.QM.Elements;
using BlanketSDK.QM.Elements.V2;
using BlanketSDK.MM.Elements;
using BlanketSDK.QM.Elements.Controls;
using BlanketSDK.QM.Elements.Modals;
using UnityEngine;
using BlanketSDK.Core;
using VRC.UI.Elements.Controls;
using Brewchat.Game.Wrappers;
using TMPro;

namespace BlanketSDK.Utilities;


internal static class VRCButtonExtensions
{
    public static BlanketSDK.QM.Elements.Controls.VRCButton AddButton(this Transform parent, string name, string? tooltip, System.Action func, Sprite? sprite = null)
    {
        return new VRCButton(parent, name, tooltip, func, sprite);
    }
    public static BlanketSDK.QM.Elements.Controls.VRCButton AddButton(this VRCGroupBox group, string name, string? tooltip, System.Action func, Sprite? sprite = null)
    {
        return new VRCButton(group, name, tooltip, func, sprite);
    }

    public static BlanketSDK.QM.Elements.Controls.VRCButton AddButton(this VRCGroupLayout group, string name, string? tooltip, System.Action func, bool submenu, Sprite? sprite = null)
    {
        return new VRCButton(group, name, tooltip, func, submenu, sprite);
    }
}
internal static class VRCToggleExtensions
{
    public static BlanketSDK.QM.Elements.Controls.VRCToggle AddToggle(this Transform? parent, string? name, string? tooltip_on, string? tooltip_off, System.Action<bool> func, bool state = false, Sprite? sprite_on = null, Sprite? sprite_off = null)
    {
        return new VRCToggle(parent, name, tooltip_on, tooltip_off, func, state, sprite_on, sprite_off);
    }
    public static BlanketSDK.QM.Elements.Controls.VRCToggle AddToggle(this VRCGroupBox group, string? name, string? tooltip_on, string? tooltip_off, System.Action<bool> func, bool state = false, Sprite? sprite_on = null, Sprite? sprite_off = null)
    {
        return new VRCToggle(group, name, tooltip_on, tooltip_off, func, state, sprite_on, sprite_off);
    }

    public static BlanketSDK.QM.Elements.Controls.VRCToggle AddToggle(this VRCGroupLayout group, string? name, string? tooltip_on, string? tooltip_off, System.Action<bool> func, bool state = false, Sprite? sprite_on = null, Sprite? sprite_off = null)
    {
        return new(group, name, tooltip_on, tooltip_off, func, state, sprite_on, sprite_off);
    }
    public static BlanketSDK.QM.Elements.Controls.VRCToggle AddToggle(this VRCModalContainer modal, string? name, string? tooltip_on, string? tooltip_off, System.Action<bool> func, bool state = false, Sprite? sprite_on = null, Sprite? sprite_off = null)
    {
        return new(modal, name, tooltip_on, tooltip_off, func, state, sprite_on, sprite_off);
    }
}
internal static class VRCMiniButtonExtensions
{
    public static BlanketSDK.QM.Elements.Controls.VRCSmallButton AddMiniButton(this VRCModalContainer modal, string? tooltip, System.Action func, Sprite? sprite = null)
    {
        return new(modal, tooltip, func, sprite);
    }
    public static BlanketSDK.QM.Elements.Controls.VRCSmallButton AddMiniButton(this Transform parent, string? tooltip, System.Action func, Sprite? sprite = null)
    {
        return new(parent, tooltip, func, sprite);
    }
}
internal static class VRCModalContainerExtensions
{
    public static BlanketSDK.QM.Elements.Controls.VRCButton AddButton(this VRCModalContainer modal, string name, string? tooltip, System.Action func, Sprite? sprite = null)
    {
        return new VRCButton(modal, name, tooltip, modal.Open, sprite);
    }

}
internal static class VRCPanelExtensions
{
    public static VRCPanelGroupLayout? AddTitle(this VRCPanelGroupLayout panel, string title, TextAlignmentOptions textAlignment = TextAlignmentOptions.TopLeft)
    {
        var obj = UnityEngine.Object.Instantiate(IQuickMenuElements.QMElements.QMPanelElements.QMPanelTitle(), panel._verticalLayoutGroup);
        TextMeshProUGUIEx? TMP_Text = obj.GetComponentInChildren<TextMeshProUGUIEx>();
        TMP_Text._localizableString = title.Localizable();
        TMP_Text.alignment = textAlignment;
        TMP_Text.richText = true;

        return panel;
    }
    public static BlanketSDK.QM.Elements.V2.VRCPanel? AddSeperator(this BlanketSDK.QM.Elements.V2.VRCPanel panel)
    {
        UnityEngine.Object.Instantiate(IQuickMenuElements.QMElements.QMPanelElements.QMSeparator(), panel.container);
        return panel;
    }
    
    public static VRCPanelGroupLayout? AddSeperator(this VRCPanelGroupLayout panel)
    {
        UnityEngine.Object.Instantiate(IQuickMenuElements.QMElements.QMPanelElements.QMSeparator(), panel._verticalLayoutGroup);
        return panel;
    }
     public static VRCPanelFoldout? AddSeperator(this VRCPanelFoldout panel)
    {
        UnityEngine.Object.Instantiate(IQuickMenuElements.QMElements.QMPanelElements.QMSeparator(), panel._verticalLayoutGroup);
        return panel;
    }
    public static Transform? AddSpacer(this Transform parent)
    {
        UnityEngine.Object.Instantiate(IQuickMenuElements.QMElements.QMSpace(), parent);
        return parent;
    }
}

internal static class VRCRadialSelectExtensions
{
    public static BlanketSDK.QM.Elements.Controls.V2.VRCRadialSelect AddRadialSelection(this Transform? parent, string name, string tooltip, bool isChild = false, Action callback = null)
    {
        return new(parent, name, tooltip, isChild, callback);
    }
    public static BlanketSDK.QM.Elements.Controls.V2.VRCRadialSelect AddRadialSelection(this VRCPanelGroupLayout? parent, string name, string tooltip, bool isChild = false, Action callback = null)
    {
        return new(parent, name, tooltip, isChild, callback);
    }
    // public static BlanketSDK.MM.Elements.Controls.V2.VRCRadialSelect AddRadialSelection(this VRCPanelGroupLayout? parent, string name, string tooltip, bool isChild = false, Action callback = null)
    // {
    //     return new(parent, name, tooltip, isChild, callback);
    // }
}