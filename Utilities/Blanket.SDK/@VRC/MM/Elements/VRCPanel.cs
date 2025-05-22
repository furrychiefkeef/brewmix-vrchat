using BlanketSDK.Core;

namespace BlanketSDK.MM.Elements.Controls;

internal class VRCPanel : IMainMenuElements
{
    VRCPanel(VRCCellContainer cell, string name)
    {
        if (!MM() || !MMElements.CellContainer("Debug")) throw new($"element has moved to a different location or index contact: {brewchat.hybridxcore.bep.Properties.Resources.D_Contact}");

        UnityEngine.Object.Instantiate(MMElements.Panel(), cell._panelContainer);
    }
}