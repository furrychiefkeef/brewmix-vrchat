using BlanketSDK.Core;
using BlanketSDK.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace BlanketSDK.QM.Elements.V2;

internal class VRCPanel : IQuickMenuElements
{
        public Transform container;
        public VRCPanel()
        {
                if(!QM() || !QMElements.QMPanelGroup($"{QUICKMENU_FLAGS.Panels.Sharing}")) throw new($"panel has moved to a different location or index contact: {brewchat.hybridxcore.bep.Properties.Resources.D_Contact}");
                
                Transform? obj = UnityEngine.Object.Instantiate(QMElements.QMPanel(), QMElements.QMPanelGroup($"{QUICKMENU_FLAGS.Panels.Sharing}").parent);
                obj.name = $"{Guid.NewGuid().ToString()}-<Blanket.SDK.QM_PANEL";
                
                VerticalLayoutGroup? _0 = obj.GetComponentInChildren<VerticalLayoutGroup>();
                if (_0.transform.childCount != 0) _0.transform.DestroyChildren(child => child.GetSiblingIndex() != 0);
                container = _0.transform;
               
        }
}