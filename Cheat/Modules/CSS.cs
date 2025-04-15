

using System.Reflection;
using BlanketSDK.Utilities;
using Brewchat.Game.Wrappers;
using UnityEngine;
using VRC.UI.Core.Styles;

namespace Brewchat.Cheat.Modules;

internal class CSS : UnityEngine.MonoBehaviour
{
    public  Texture? texture { get; set; }
    public void Runtime()
    {
        
        #region ActionMenu

        GameObject? obj = Root.ActionMenu("MenuR");
        ActionMenu? _ActionMenu = obj.GetComponent<ActionMenu>();
        if (obj != null)
        {
            PedalGraphic? Center_Removable = obj.transform.GetChild(0).GetChild(0).GetComponent<PedalGraphic>();
            UnityEngine.Object.DestroyImmediate(Center_Removable,true);    
            
            PedalGraphic Action_Menu_GRAPHICS = obj.transform.GetChild(0).GetChild(1).GetComponent<PedalGraphic>();
            texture = EngineUtils.Assets.LoadTextureFromModAssets($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\Brew\\radial.png");
            Action_Menu_GRAPHICS._texture = texture; 
            Action_Menu_GRAPHICS.color = EngineUtils.Conversion.HexToColor("604b55");
        }
       
        #endregion
    
        
        #region STYLE_ENGINE
        Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppArrayBase<StyleEngine>? A = GameObject.FindObjectsOfType<StyleEngine>();
        foreach (var item in A)
        {

            /*foreach (string? key in item.field_Private_Dictionary_2_String_Color_0._keys)
            {
                File.WriteAllText("wrongton", $"\n{key}");
            }*/
            
            // Using random colors for each key in the dictionary
            item.field_Private_Dictionary_2_String_Color_0["Highlight"] = EngineUtils.Conversion.HexToColor("604b55"); 
            item.field_Private_Dictionary_2_String_Color_0["Active"] = EngineUtils.Conversion.HexToColor("604b55");
            item.field_Private_Dictionary_2_String_Color_0["Active2"] = EngineUtils.Conversion.HexToColor("604b55");
            item.field_Private_Dictionary_2_String_Color_0["BottomTabSelected"] = new UnityEngine.Color(0.9f, 0.1f, 0.8f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["Subheader"] = EngineUtils.Conversion.HexToColor("604b55");
            item.field_Private_Dictionary_2_String_Color_0["Hover"] = EngineUtils.Conversion.HexToColor("604b55");
            item.field_Private_Dictionary_2_String_Color_0["ActiveDark"] = EngineUtils.Conversion.HexToColor("604b55");
            item.field_Private_Dictionary_2_String_Color_0["BottomTabHover"] = new UnityEngine.Color(0.8f, 0.3f, 0.5f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["IconActive"] = EngineUtils.Conversion.HexToColor("604b55");
            item.field_Private_Dictionary_2_String_Color_0["IconWhite"] = new UnityEngine.Color(0.95f, 0.95f, 0.95f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["IconHover"] = new UnityEngine.Color(1f, 0.8f, 0.2f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["CategorySelectorIcon"] =  new UnityEngine.Color(0.8f, 0.3f, 0.5f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["IconHighlight"] = new UnityEngine.Color(1f, 0.5f, 0f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["IconInactive"] = new UnityEngine.Color(0.6f, 0.6f, 0.6f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["IconGrey"] = new UnityEngine.Color(0.5f, 0.5f, 0.5f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["NavigationButton"] = EngineUtils.Conversion.HexToColor("604b55");
            item.field_Private_Dictionary_2_String_Color_0["ButtonHover"] = EngineUtils.Conversion.HexToColor("604b55");
            item.field_Private_Dictionary_2_String_Color_0["ButtonHoverTeal"] = EngineUtils.Conversion.HexToColor("604b55");
            item.field_Private_Dictionary_2_String_Color_0["ButtonBorder"] = new UnityEngine.Color(0.3f, 0.3f, 0.3f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["ButtonHighlight"] = new UnityEngine.Color(0.9f, 0.7f, 0.1f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["ButtonBg"] = new UnityEngine.Color(0.15f, 0.15f, 0.15f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["ButtonCell"] = new UnityEngine.Color(0.25f, 0.25f, 0.25f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["ButtonQMSelected"] = EngineUtils.Conversion.HexToColor("604b55");
            item.field_Private_Dictionary_2_String_Color_0["WingBG"] = new UnityEngine.Color(0.12f, 0.12f, 0.15f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["ButtonOutline"] = new UnityEngine.Color(0.4f, 0.4f, 0.4f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["WhiteBG"] = new UnityEngine.Color(0.9f, 0.9f, 0.9f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["NavigationBG"] = new UnityEngine.Color(0.18f, 0.18f, 0.2f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["Outline"] = new UnityEngine.Color(0.7f, 0.7f, 0.7f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["BG"] = EngineUtils.Conversion.HexToColor("604b55");
            item.field_Private_Dictionary_2_String_Color_0["BottomTab"] = EngineUtils.Conversion.HexToColor("604b55");
            item.field_Private_Dictionary_2_String_Color_0["Backgrounds"] = EngineUtils.Conversion.HexToColor("604b55");
/*           item.field_Private_Dictionary_2_String_Color_0["Font1"] = new UnityEngine.Color(0.9f, 0.9f, 0.9f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["Font2"] = new UnityEngine.Color(0.8f, 0.8f, 0.8f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["Font3"] = new UnityEngine.Color(0.7f, 0.7f, 0.7f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["Font4"] = new UnityEngine.Color(0.6f, 0.6f, 0.6f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["Font5"] = new UnityEngine.Color(0.5f, 0.5f, 0.5f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["Font6"] = new UnityEngine.Color(0.4f, 0.4f, 0.4f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["Font7"] = new UnityEngine.Color(0.85f, 0.7f, 0.5f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["Font8"] = new UnityEngine.Color(0.7f, 0.5f, 0.85f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["Font9"] = new UnityEngine.Color(0.5f, 0.85f, 0.7f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["Font10"] = new UnityEngine.Color(0.6f, 0.8f, 0.2f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["Font11"] = new UnityEngine.Color(0.2f, 0.6f, 0.8f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["Font12"] = new UnityEngine.Color(0.8f, 0.2f, 0.6f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["AlertBg"] = new UnityEngine.Color(0.9f, 0.2f, 0.2f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["NegativeBalance"] = new UnityEngine.Color(0.95f, 0.3f, 0.3f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["PositiveBalance"] = new UnityEngine.Color(0.3f, 0.95f, 0.3f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["Subtext4"] = new UnityEngine.Color(0.5f, 0.5f, 0.55f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["VRCPlusTutorial"] = new UnityEngine.Color(0.95f, 0.8f, 0.2f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["Yellow"] = new UnityEngine.Color(1f, 0.92f, 0.16f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["Trusted"] = new UnityEngine.Color(0.2f, 0.4f, 1f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["Blue"] = new UnityEngine.Color(0.15f, 0.6f, 0.95f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["TextHighlight"] = new UnityEngine.Color(0.9f, 0.9f, 0.2f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["Known"] = new UnityEngine.Color(0.3f, 0.8f, 0.4f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["Notification"] = new UnityEngine.Color(1f, 0.3f, 0.3f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["DisabledRedBg"] = new UnityEngine.Color(0.5f, 0.2f, 0.2f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["FriendRequest"] = new UnityEngine.Color(0.1f, 0.7f, 0.3f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["VRChatPlusYellow"] = new UnityEngine.Color(1f, 0.84f, 0f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["FriendSafeAlert"] = new UnityEngine.Color(0.2f, 0.8f, 0.8f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["DiscountBG"] = new UnityEngine.Color(0.2f, 0.5f, 0.2f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["Black"] = new UnityEngine.Color(0.05f, 0.05f, 0.05f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["NearBlack"] = new UnityEngine.Color(0.1f, 0.1f, 0.1f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["Grey1"] = new UnityEngine.Color(0.2f, 0.2f, 0.2f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["Grey2"] = new UnityEngine.Color(0.3f, 0.3f, 0.3f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["Grey3"] = new UnityEngine.Color(0.4f, 0.4f, 0.4f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["Grey4"] = new UnityEngine.Color(0.5f, 0.5f, 0.5f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["Grey5"] = new UnityEngine.Color(0.6f, 0.6f, 0.6f, 1f);
            item.field_Private_Dictionary_2_String_Color_0["White"] = UnityEngine.Color.white;*/

        }
        #endregion
    }
}

       
        