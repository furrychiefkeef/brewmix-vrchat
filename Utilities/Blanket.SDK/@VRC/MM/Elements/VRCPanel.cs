using BlanketSDK.Core;
using BlanketSDK.Utilities;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;

namespace BlanketSDK.MM.Elements.Controls;


internal class VRCPanel : IMainMenuElements
{
    public static Transform _panel;
    VRCPanel(VRCCellContainer cell, string name)
    {
        if (!MM() || !MMElements.CellContainer("Debug")) throw new($"element has moved to a different location or index contact: {autumn.ml.Properties.Resources.D_Contact}");
        var obj = UnityEngine.Object.Instantiate(MMElements.Panel(), cell._panelContainer);

        var Header = obj.GetChild(0);
        var Panel = obj.GetChild(1);
        Panel.DestroyChildren();

        _panel = Panel;
    }
}