using BlanketSDK.QM.Elements;
using BlanketSDK.Utilities;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;
using VRC.UI.Elements.Controls;

namespace BlanketSDK.QM.Elements;

internal class VRCGroupBox : Templates
{
    public Transform? container;
    public VRCGroupBox(VRCPage page, string name = null)
    {
        if (!_QM.MenuDashboard() || !_QM.QMButtonContainer()) throw new($"element has moved to a different location or index contact: {brewchat.hybridxcore.bep.Properties.Resources.D_Contact}");
        Transform? obj = UnityEngine.Object.Instantiate(_QM.QMButtonContainer(), page.box);
        UnityEngine.Object.Destroy(obj.GetChild(1).transform.gameObject);
        UnityEngine.Object.Destroy(obj.GetChild(2).transform.gameObject);
        obj.name = $"{Guid.NewGuid().ToString()}-<Blanket.SDK.QM_GROUPBOX";
        
        Transform? _header = obj.GetChild(3);
        if (name == null) UnityEngine.Object.Destroy(_header.transform.gameObject);
        
        Transform? _container = obj.GetChild(4);
        _container.DestroyChildren();

        #region header
        
        #region REQUIRED_COMPONENTS

        TextMeshProUGUIEx? _1 =  _header.GetComponentInChildren<TextMeshProUGUIEx>();

        #endregion

        #endregion

        #region TEXT COMPONENT
        _1.text = name;
        _1.richText = true;

        #endregion


        #region LOGIC_REQUIRED

        container = _container;
        
        #endregion

    }
}