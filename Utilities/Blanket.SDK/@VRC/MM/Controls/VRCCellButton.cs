using BlanketSDK.Utilities;
using BlanketSDK.Core;
using UnityEngine;
using UnityEngine.UI;
using VRC.Localization;
using VRC.UI;
using VRC.UI.Elements;
using VRC.UI.Elements.Controls;
using Object = Il2CppSystem.Object;

namespace BlanketSDK.MM.Elements.Controls;

internal class VRCCellButton : IMainMenuElements
{
    /// <summary>
    ///  VRCButton this is used to create a SINGLE_MAINMENU_CELLBUTTON
    /// </summary>
    /// <param name="parent">The parent transform where the button will be placed.</param>
    /// <param name="name">The display name of the button.</param>
    /// <param name="tooltip">The tooltip text for the button.</param>
    /// <param name="func">The action to execute when the button is clicked.</param>
    /// <param name="sprite">An optional sprite to display on the button.</param>
    public VRCCellButton(Transform parent, string? name, string? tooltip, System.Action func, Sprite? sprite = null)
    {
        if (!MainMenu() | !MMControls.Buttons.SingleButton()) throw new($"button has moved to a different location or index contact: {brewchat.hybridxcore.bep.Properties.Resources.D_Contact}");
        parent ??= MMFallbacks._Fallback_Dashboard_Group_MM_UI_Buttons();

        Transform? obj = UnityEngine.Object.Instantiate(MMControls.Buttons.SingleButton(), parent);
        obj.name = $"{Guid.NewGuid().ToString()}-<Blanket.SDK.MM_SINGLE_BUTTON-{name}";

        #region REQUIRED_COMPONENTS
        VRCButtonHandle _0 = obj.GetComponent<VRCButtonHandle>();
        
        TextMeshProUGUIEx _1 = obj.GetChild(2).GetComponentInChildren<TextMeshProUGUIEx>(true);
        ImageEx _2 = obj.GetChild(6).GetComponent<ImageEx>();
        ToolTip _3 = obj.GetComponent<ToolTip>();
        #endregion

        #region BUTTON COMPONENT
        _0._controlName = null;
        UnityEngine.UI.Button.ButtonClickedEvent? BUTTONCLICKED_EVENT_0 = _0.onClick;
        BUTTONCLICKED_EVENT_0.RemoveAllListeners();
        BUTTONCLICKED_EVENT_0.AddListener(func);

        #endregion

        #region TEXT COMPONENT
        _1.text = name;
        _1.richText = true;
        #endregion
        
        #region TOOLTIP COMPONENT
        _3._localizableString = tooltip.Localize(); 
        #endregion
        
        #region IMAGE || IMAGE_EX COMPONENT
        if(!sprite) return;
        _2.sprite = sprite;
        #endregion

    }
    public VRCCellButton(VRCPage page, VRCCellContainer Menu, string? name, string? tooltip, Sprite? sprite = null)
    {
        if (!MainMenu() | !MMControls.Buttons.SingleButton()) throw new($"button has moved to a different location or index contact: {brewchat.hybridxcore.bep.Properties.Resources.D_Contact}");
        page.navigation_buttons ??= MMFallbacks._Fallback_Dashboard_Group_MM_UI_Buttons();

        Transform? obj = UnityEngine.Object.Instantiate(MMControls.Buttons.SingleButton(), page.navigation_buttons);
        obj.name = $"{Guid.NewGuid().ToString()}-<Blanket.SDK.MM_SINGLE_BUTTON-{name}";

        #region REQUIRED_COMPONENTS
        VRCButtonHandle _0 = obj.GetComponent<VRCButtonHandle>();
        CellButtonHandler? _1 = obj.GetComponent<CellButtonHandler>();
        TextMeshProUGUIEx _2 = obj.GetChild(2).GetComponentInChildren<TextMeshProUGUIEx>(true);
        ImageEx _3 = obj.GetChild(6).GetComponent<ImageEx>();
        ToolTip _4 = obj.GetComponent<ToolTip>();
        #endregion

        #region BUTTON COMPONENT
        _0._controlName = Menu.Object.gameObject.name;
            

        #endregion

        #region LOGIC_REQUIRED

        if (Menu != null)
        {
            _1._targetObjectToEnable = Menu.Object;  
        }
   
        #endregion
        
        #region TEXT COMPONENT
        _2.text = name;
        _2.richText = true;
        #endregion
        
        #region TOOLTIP COMPONENT
        _4._localizableString = tooltip.Localize(); 
        #endregion
        
        #region IMAGE || IMAGE_EX COMPONENT
        if(!sprite) return;
        _3.sprite = sprite;
        #endregion

    }
}
/*  UnityEngine.UI.Button _0 = obj.GetComponentInChildren<UnityEngine.UI.Button>(); depercated*/