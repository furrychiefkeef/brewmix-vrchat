using BlanketSDK.Utilities;
using BlanketSDK.Core;
using UnityEngine;
using VRC.UI.Elements.Controls;

namespace BlanketSDK.MM.Elements;

internal class VRCRowGroup : IMainMenuElements
{
    public Transform? container;
    public VRCRowGroup(Transform parent, string header, bool alwaysVisable = true)
    {
        if (!MainMenu() || !MMControls.Buttons.ProfileButton()) throw new($"profile row element has moved to a different location or index contact: {autumn.ml.Properties.Resources.D_Contact}");
        parent ??= MMFallbacks._Fallback_Dashboard_Group_MM_UI_ProfileRows();
        Transform? obj = UnityEngine.Object.Instantiate(MMElements.ProfileRow(), parent);
        obj.name = $"{Guid.NewGuid().ToString()}-<Blanket.SDK.MM_PROFILE_ROW_{header}";
        obj.SetSiblingIndex(2 + 1);
      
        /*obj.gameObject.SetActive(alwaysVisable);*/
        
        Transform? _header = obj.GetChild(0);
        Transform? _cellgrid = obj.GetChild(1);

        #region REQUIRED_COMPONENTS

        TextMeshProUGUIEx? _0 = _header.GetChild(1).GetComponentInChildren<TextMeshProUGUIEx>();
        RectTransform? _1 = obj.GetComponent<RectTransform>();
     
        #endregion
        
        #region TEXT_COMPONENT

        _0.text = header;
        _0.richText = true;

        #endregion
        
        #region LOGIC_REQUIRED
        
        if(header == null) UnityEngine.Object.Destroy(_header.transform.gameObject);
        _cellgrid.DestroyChildren();
        container = _cellgrid;
        
        #endregion
    }
}