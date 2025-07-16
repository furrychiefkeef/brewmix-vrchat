using UnityEngine;
using System;
using BlanketSDK.Core;
using VRC.UI.Elements.Controls;
using Brewchat.Game.Wrappers;
using Unity.Collections;
using UnityEngine.UI;
using BlanketSDK.Utilities;

namespace BlanketSDK.QM.Elements.V2;

internal class VRCPanelFoldout : IQuickMenuElements
{
    MonoBehaviour1PublicGa_cBu_vObCa_v_eLoBoUnique? Logic;
    public Transform? _verticalLayoutGroup;
    private ToolTip toolTip;
    public TextMeshProUGUIEx _textMeshPro;
    public bool isOpen;
    private bool SetActive;
    public VRCPanelFoldout(Transform? parent, string name, string tooltip = null, bool isOpen = false)
    {
        if (!QuickMenu() && !QMElements.QMPanel("Chatbox")) throw new("button has moved to a different location or index contact: {autumn.ml.Properties.Resources.D_Contact}");
        Transform? obj = UnityEngine.Object.Instantiate(QMElements.QMPanelElements.QMFoldout(), parent);
        parent ??= QMElements.QMPanelElements.QMFoldout().transform;

        if (Logic is null) Logic = obj.GetComponent<MonoBehaviour1PublicGa_cBu_vObCa_v_eLoBoUnique>();

        if (!string.IsNullOrEmpty(tooltip))
        {
            toolTip = Logic._visibilityButton.gameObject.AddComponent<ToolTip>();
        }

        // if (string.IsNullOrEmpty(name)) UnityEngine.Object.DestroyImmediate(Logic._visibilityButtonLabel);
        Logic._collapsedTitle = $"Show {name}</color>".Localizable();
        Logic._expandedTitle = $"Hide {name}".Localizable();
        if (_verticalLayoutGroup is null) _verticalLayoutGroup = Logic._contentContainer.transform;
        _verticalLayoutGroup.GetChildren().ForEach(x =>
        {
            if (x.gameObject.name == "TopDivider") return;
            UnityEngine.Object.Destroy(x.gameObject);
        });

        Logic._visibilityButton.onClick.AddListener(new Action(() =>
        {
            Logic._contentContainer.active = !Logic._contentContainer.active;
            toolTip._localizableString = $"{(Logic._contentContainer.active ? "Hides" : "Shows")}: {tooltip}".Localizable();
        }));
    }
    public VRCPanelFoldout(VRCPanelGroupLayout parent, string name, string tooltip = null, bool isOpen = false): this(parent._verticalLayoutGroup, name, tooltip, isOpen) {}
}
