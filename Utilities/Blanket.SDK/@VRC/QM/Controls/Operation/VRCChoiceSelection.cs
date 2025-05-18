using System.Runtime.InteropServices;
using BlanketSDK.Core;
using VRC.UI.Elements;
using VRC.UI.Elements.Controls;

namespace BlanketSDK.QM.Elements.Controls.Operation;

internal class VRCChoiceSelection : IQuickMenuElements
{
   

    public VRCChoiceSelection(VRCPanel vrcPanel ,string name, [Optional] System.Action callback)
    {
        if(!QM() || !QMParent() || !QMMenus.QMSubmenus.Menu_QM_GeneralSettings()) throw new($"{name} element has moved to a different location or index contact: {brewchat.hybridxcore.bep.Properties.Resources.D_Contact}");
        var obj = UnityEngine.Object.Instantiate(QMControls.OptionSelection(), vrcPanel.container);
        
        var _RightContainer = obj.GetChild(0); 
        var _LeftContainer = obj.GetChild(1);
        
        obj.name = $"{Guid.NewGuid().ToString()}-<Blanket.SDK.QM_CHOICE_SELECTION";
        var _0 =  obj.GetComponent<RadialSelector>();

        var Title = _LeftContainer.GetComponentInChildren<TextMeshProUGUIEx>();
        Title.text = name;
        Title.richText = true;
        
        
    }
}