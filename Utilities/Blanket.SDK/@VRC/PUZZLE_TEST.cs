using BlanketSDK.Core;
using BlanketSDK.QM.Elements.V2;
using UnityEngine;

namespace BlanketSDK.Utilities.VRC;

internal static class PUZZLE_TEST
{
    public static VRCPanel? AddSeperator(this VRCPanel panel)
    {
        UnityEngine.Object.Instantiate(IQuickMenuElements.QMElements.QMPanelElements.QMSeparator(), panel.container);
        return panel;
    }
    public static Transform? AddSpacer(this Transform parent)
    {
        UnityEngine.Object.Instantiate(IQuickMenuElements.QMElements.QMSpace(), parent);
        return parent;
    }
}