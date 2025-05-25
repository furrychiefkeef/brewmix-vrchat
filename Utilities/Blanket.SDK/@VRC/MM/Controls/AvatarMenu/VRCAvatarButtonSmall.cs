using System.Runtime.InteropServices;
using BlanketSDK.Core;
using UnityEngine.UI;
using VRC.Localization;
using VRC.UI;
using VRC.UI.Elements.Controls;

namespace BlanketSDK.MM.Elements.Controls.AvatarMenu;

internal class VRCAvatarButtonSmall : IMainMenuElements
{
    public VRCAvatarButtonSmall(string identifier , string tooltip, Action func, [Optional] UnityEngine.Sprite? sprite, [Optional] int priority )
    {
        if(!MM() || !MMParent() || !MMControls.Buttons.AvatarMenuRelated.AvatarButtonSmall()) throw new($"avatar button element has moved to a different location or index contact: {brewchat.hybridxcore.bep.Properties.Resources.D_Contact}");
        
        UnityEngine.Transform? obj = UnityEngine.Object.Instantiate(MMControls.Buttons.AvatarMenuRelated.AvatarButtonSmall(), MMControls.Buttons.AvatarMenuRelated.AvatarButtonSmall().parent);
        obj.name = $"{Guid.NewGuid().ToString()}-<Blanket.SDK.MM_AVATAR_BUTTON";
        if(priority != 0 || priority != null) obj.SetSiblingIndex(priority);
        
        UnityEngine.Object.DestroyImmediate(obj.GetComponent<MonoBehaviourPublicLoInLoBoInLoInLoLoLoUnique>());
        UnityEngine.Object.DestroyImmediate(obj.GetComponent<MonoBehaviourPublicSt_sStObBoStObLoAcObUnique>());
       
        
        #region REQUIRED_COMPONENTS
        VRCButtonHandle _0 = obj.GetComponent<VRCButtonHandle>();
        ToolTip _1 = obj.GetComponent<ToolTip>();
        ImageEx? _2 = obj.GetChild(1).GetComponent<ImageEx>();

        #endregion


        #region REQUIRED_LOGIC_API
        
        _0._controlName = $"BlanketSDK_AvatarButton_{identifier}";
        Button.ButtonClickedEvent LISTENER_0_EVENT = _0.onClick;
        
        LISTENER_0_EVENT.RemoveAllListeners();
        LISTENER_0_EVENT.AddListener(func);
        #endregion
        
        #region TOOLTIP_COMPONENT

        _1._localizableString = tooltip.Localize();
        #endregion
        
        #region TEXT_COMPONENT
        
        if (!sprite || sprite == null) return; 
        _2.sprite = sprite;
        #endregion
  }
}