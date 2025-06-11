using System.Runtime.InteropServices;
using BlanketSDK.Core;
using Brewchat.Game.Wrappers;
using UnityEngine;
using UnityEngine.UI;
using VRC.Localization;
using VRC.UI;
using VRC.UI.Elements.Controls;

namespace BlanketSDK.MM.Elements.Controls.AvatarMenu;

internal class VRCAvatarButton: IMainMenuElements
{
    public VRCAvatarButton(string name , string tooltip, Action func, [Optional] UnityEngine.Sprite? sprite, [Optional] int priority )
    {
        if(!MM() || !MMParent() || !MMControls.Buttons.AvatarMenuRelated.AvatarButton()) throw new($"avatar button element has moved to a different location or index contact: {autumn.ml.Properties.Resources.D_Contact}");
        
        UnityEngine.Transform? obj = UnityEngine.Object.Instantiate(MMControls.Buttons.AvatarMenuRelated.AvatarButton(), MMControls.Buttons.AvatarMenuRelated.AvatarButton().parent);
        obj.name = $"{Guid.NewGuid().ToString()}-<Blanket.SDK.MM_AVATAR_BUTTON";
        if (priority != 0 || priority != null) obj.SetSiblingIndex(priority);
        
       
        
        #region REQUIRED_COMPONENTS
        VRCButtonHandle _0 = obj.GetComponent<VRCButtonHandle>();
        ToolTip _1 = obj.GetComponent<ToolTip>();
        Transform? _I  = obj.GetChild(1);
        ImageEx? _2 = _I.GetChild(0).GetComponent<ImageEx>();
        TextMeshProUGUIEx? _3 = obj.GetComponentInChildren<TextMeshProUGUIEx>();
        
        #endregion


        #region REQUIRED_LOGIC_API  
        
        _0._controlName = $"BlanketSDK_AvatarButton_{name}";
        Button.ButtonClickedEvent LISTENER_0_EVENT = _0.onClick;
            
        LISTENER_0_EVENT.RemoveAllListeners();
        LISTENER_0_EVENT.AddListener(func);
        #endregion
        
        #region TOOLTIP_COMPONENT

        _1._localizableString = tooltip.UnityString();
        #endregion
        
        #region TEXT_COMPONENT
        _3.text = name;
        _3.richText = true;
        #endregion
        
        
        #region IMAGE_COMPONENT
        
        if (!sprite || sprite == null) return; 
        _I.gameObject.SetActive(true);
        _2.sprite = sprite;
        
        #endregion
  }
}