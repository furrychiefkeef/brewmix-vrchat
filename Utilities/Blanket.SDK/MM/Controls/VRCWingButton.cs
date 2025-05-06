/*
using BlanketSDK.Utilities;
using UnityEngine;
using VRC.Localization;
using VRC.UI;
using VRC.UI.Elements.Controls;

namespace BlanketSDK.MM.Elements.Controls;

internal class VRCWingButton : Templates
{
    /// <summary>
    ///  VRCSmallButton this is used to create a WING_QUICKMENU_BUTTON
    /// </summary>
    /// <param name="parent">The parent transform where the button will be placed.</param>
    /// <param name="position">add whatever side u want it to be on Left or Right</param>
    /// <param name="name">The display name of the button.</param>
    /// <param name="tooltip">The tooltip text for the button.</param>
    /// <param name="func">The action to execute when the button is clicked.</param>
    /// <param name="sprite">An optional sprite to display on the button.</param>
    public VRCWingButton(Transform parent, Ropie.Wing.Position position, string? name,  string? tooltip, System.Action func, Sprite? sprite = null)
        {
            if (!QuickMenu() | !_MM.WingButton("Left") || !_QM.WingButton("Right")) throw new($"button has moved to a different location or index contact: {brewchat.hybridxcore.bep.Properties.Resources.D_Contact}");
            /*parent ??= _Fallback_Dashboard_Group_MM_UI_WingButtons(Ropie.Wing.SetWingPosition(position));#1#
         
             UnityEngine.Transform? obj = UnityEngine.Object.Instantiate(_MM.WingButton(Ropie.Wing.SetWingPosition(position)), parent);
             obj.name = $"{Guid.NewGuid().ToString()}-<Blanket.SDK.MM_WINGBUTTON_BUTTON";

           
           
            #region REQUIRED_COMPONENTS 
            VRCButtonHandle  _0 = obj.GetComponent<VRCButtonHandle>();
            ImageEx  _1 = obj.GetChild(0).GetChild(1).GetComponent<ImageEx>();
            ToolTip  _2 = obj.GetComponent<ToolTip>();
            TextMeshProUGUIEx _3 = obj.GetComponentInChildren<TextMeshProUGUIEx>();
            #endregion

            #region BUTTON COMPONENT
            UnityEngine.UI.Button.ButtonClickedEvent? BUTTONCLICKED_EVENT_0 = _0.onClick;
            BUTTONCLICKED_EVENT_0.RemoveAllListeners();
            BUTTONCLICKED_EVENT_0.AddListener(func);
            #endregion
            
            #region TEXT COMPONENT
            _3.text = name;
            _3.richText = true;
            #endregion
            
            #region TOOLTIP COMPONENT
            _2._localizableString = tooltip.Localize();
            #endregion
            
            #region IMAGE || IMAGE_EX COMPONENT
            if (!sprite) return;
            _1.sprite = sprite;
            #endregion
        }
   
}   
/*position switch
             {
                 Position.L => "Left",
                 Position.R => "Right",
                 _ => "Unknown"
             });#1#
             */
