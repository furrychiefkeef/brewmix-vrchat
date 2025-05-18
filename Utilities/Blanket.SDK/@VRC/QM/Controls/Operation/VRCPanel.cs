using BlanketSDK.Core;
using BlanketSDK.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace BlanketSDK.QM.Elements.Controls.Operation;

internal class VRCPanel : IQuickMenuElements
{
        public Transform container;
        public VRCPanel()
        {
                if(!QM() || !QMElements.QM_Settings_Panel()) throw new($"panel has moved to a different location or index contact: {brewchat.hybridxcore.bep.Properties.Resources.D_Contact}");
                
                Transform? obj = UnityEngine.Object.Instantiate(QMElements.QM_Settings_Panel(), QMElements.QM_Settings_Panel().parent);
                obj.name = $"{Guid.NewGuid().ToString()}-<Blanket.SDK.QM_PANEL";
                
                VerticalLayoutGroup? _0 = obj.GetComponentInChildren<VerticalLayoutGroup>();
                _0.transform.DestroyChildren();
                container = _0.transform;
        }
}