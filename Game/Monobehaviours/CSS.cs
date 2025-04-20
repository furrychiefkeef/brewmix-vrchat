using System.Reflection;
using BlanketSDK.Utilities;
using Brewchat.Game.Wrappers;
using Syrup.IMGUI;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI;
using VRC.UI.Core.Styles;
using VRC.UI.Elements;
using VRC.UI.Elements.Controls;

namespace Brewchat.Cheat.Modules;
internal class CSS : UnityEngine.MonoBehaviour
{
    public static CSS? Instance { get; private set; }
    private void Awake()
    {
        if (Instance is null)
        {
            Instance = this;
            UnityEngine.Object.DontDestroyOnLoad(this);
        }   
        else UnityEngine.Object.Destroy(this);
    }

    public void Runtime()
    {
        var B = GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/BackgroundLayer01").GetComponent<ImageEx>();
        if (B != null)
        { 
            Texture texture = EngineUtils.Assets.Data_LoadTexture_0("https://github.com/riotlu/fake-cdn/blob/master/GmVTVqlbIAElnaf.png?raw=true");
            texture.name = "BREWCHAT";
            var T = B.material;
            B.material.mainTexture = texture;
            B.material.SetTextureImpl(0, texture);
            sconsole.print($"SHADER KEYWORD {T.shaderKeywords.ToString()}", 0);
            sconsole.print($"SHADER _MATERIAL {T.shader.name.ToString()}", 0);
      
        }
        
        /*
        #region MAINMENU_BG
        
        ImageEx? IMAGE_EX = Root.MMParent().GetChild(0).GetComponent<VRC.UI.ImageEx>();
        
        var SPRITE_CHEAT =  EngineUtils.Assets.Data_LoadSprite("https://github.com/riotlu/fake-cdn/blob/master/GmVTVqlbIAElnaf.png?raw=true");
        IMAGE_EX.sprite = SPRITE_CHEAT;
        
        Root.MMParent().GetChild(0).GetComponent<VRCUIBackground>()
            .field_Private_Image_0 = IMAGE_EX;
        #endregion
        
        #region ActionMenu

        Transform? obj = Root.ActionMenu("MenuR");
        ActionMenu? _ActionMenu = obj.GetComponent<ActionMenu>();
        
        if (obj != null)
        {
            Il2CppSystem.Object divider_container = obj.transform.GetChild(0).GetChild(2);

            Il2CppSystem.Collections.IEnumerable childrenCollection = divider_container.Cast<Il2CppSystem.Collections.IEnumerable>();
            if (childrenCollection == null)
            {

                sconsole.print($"Container children collection is null",2);
                return;
            }
            
            foreach (var dividerObj in childrenCollection)
            {
                if (dividerObj == null) continue;
                Transform dividerTransform = dividerObj.Cast<Transform>();
                if (dividerTransform == null) continue;
                
                var dividers = dividerTransform.GetComponentsInChildren<Transform>(true)
                    .Where(t => t != null && t.name == "Divider(Clone)")
                    .ToArray();
    
                foreach (Transform? divider in dividers)
                {
                    Image image = divider.GetComponentInChildren<Image>(true);
                    if (image != null)
                    { 
                        image.color = EngineUtils.Conversion.HexToColor("#604b55");
                        image.sprite = EngineUtils.Assets.LoadTexture($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\Brew\\VRC-UI\\action-menu_divider.png").ToSprite();
                    }
                }
            }
            PedalGraphic? Center_Removable = obj.transform.GetChild(0).GetChild(0).GetComponent<PedalGraphic>();
            UnityEngine.Object.DestroyImmediate(Center_Removable,true);    
            
            PedalGraphic Action_Menu_GRAPHICS_BG = obj.transform.GetChild(0).GetChild(1).GetComponent<PedalGraphic>();
            UnityEngine.UI.Image Action_Menu_GRAPHICS_CURSOR = obj.transform.GetChild(0).GetChild(4).GetComponent<UnityEngine.UI.Image>();
            
            Action_Menu_GRAPHICS_BG._texture = EngineUtils.Assets
                .LoadTexture($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\Brew\\VRC-UI\\action-menu_bg.png");
            
            Action_Menu_GRAPHICS_CURSOR.sprite = EngineUtils.Assets
                .LoadTexture($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\Brew\\VRC-UI\\action-menu_joystick.png").ToSprite();
                
            Action_Menu_GRAPHICS_BG.color = EngineUtils.Conversion.HexToColor("604b55");
            Action_Menu_GRAPHICS_CURSOR.color = EngineUtils.Conversion.HexToColor("604b55");
        }
       
        #endregion
        */
        
        #region STYLE_ENGINE+QM+MM
        
        Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppArrayBase<StyleEngine>? VRC_UI = GameObject.FindObjectsOfType<StyleEngine>();
        foreach (StyleEngine? X in VRC_UI)
        {
            X.field_Private_Dictionary_2_String_Color_0["Highlight"] = EngineUtils.Conversion.HexToColor("#604b55"); 
            X.field_Private_Dictionary_2_String_Color_0["Active"] = EngineUtils.Conversion.HexToColor("#604b55");
            X.field_Private_Dictionary_2_String_Color_0["Active2"] = EngineUtils.Conversion.HexToColor("#604b55");
            X.field_Private_Dictionary_2_String_Color_0["BottomTabSelected"] = new UnityEngine.Color(0.9f, 0.1f, 0.8f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Subheader"] = EngineUtils.Conversion.HexToColor("#604b55");
            X.field_Private_Dictionary_2_String_Color_0["Hover"] = EngineUtils.Conversion.HexToColor("#604b55");
            X.field_Private_Dictionary_2_String_Color_0["ActiveDark"] = EngineUtils.Conversion.HexToColor("#604b55");
            X.field_Private_Dictionary_2_String_Color_0["BottomTabHover"] = new UnityEngine.Color(0.8f, 0.3f, 0.5f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["IconActive"] = EngineUtils.Conversion.HexToColor("#604b55");
            X.field_Private_Dictionary_2_String_Color_0["IconWhite"] = new UnityEngine.Color(0.95f, 0.95f, 0.95f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["IconHover"] = new UnityEngine.Color(1f, 0.8f, 0.2f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["CategorySelectorIcon"] = new UnityEngine.Color(0.8f, 0.3f, 0.5f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["IconHighlight"] = new UnityEngine.Color(1f, 0.5f, 0f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["IconInactive"] = new UnityEngine.Color(0.6f, 0.6f, 0.6f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["IconGrey"] = new UnityEngine.Color(0.5f, 0.5f, 0.5f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["NavigationButton"] = EngineUtils.Conversion.HexToColor("#604b55");
            X.field_Private_Dictionary_2_String_Color_0["ButtonHover"] = EngineUtils.Conversion.HexToColor("#604b55");
            X.field_Private_Dictionary_2_String_Color_0["ButtonHoverTeal"] = EngineUtils.Conversion.HexToColor("#604b55");
            X.field_Private_Dictionary_2_String_Color_0["ButtonBorder"] = new UnityEngine.Color(0.3f, 0.3f, 0.3f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["ButtonHighlight"] = new UnityEngine.Color(0.9f, 0.7f, 0.1f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["ButtonBg"] = new UnityEngine.Color(0.15f, 0.15f, 0.15f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["ButtonCell"] = new UnityEngine.Color(0.25f, 0.25f, 0.25f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["ButtonQMSelected"] = EngineUtils.Conversion.HexToColor("#604b55");
            X.field_Private_Dictionary_2_String_Color_0["WingBG"] = new UnityEngine.Color(0.12f, 0.12f, 0.15f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["ButtonOutline"] = new UnityEngine.Color(0.4f, 0.4f, 0.4f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["WhiteBG"] = new UnityEngine.Color(0.9f, 0.9f, 0.9f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["NavigationBG"] = new UnityEngine.Color(0.18f, 0.18f, 0.2f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Outline"] = new UnityEngine.Color(0.7f, 0.7f, 0.7f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["BG"] = EngineUtils.Conversion.HexToColor("#604b55");
            X.field_Private_Dictionary_2_String_Color_0["MenuBG"] = EngineUtils.Conversion.HexToColor("#604b55");
            X.field_Private_Dictionary_2_String_Color_0["BottomTab"] = EngineUtils.Conversion.HexToColor("#604b55");
            X.field_Private_Dictionary_2_String_Color_0["Backgrounds"] = EngineUtils.Conversion.HexToColor("#604b55");
            X.field_Private_Dictionary_2_String_Color_0["Font1"] = new UnityEngine.Color(0.9f, 0.9f, 0.9f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Font2"] = new UnityEngine.Color(0.8f, 0.8f, 0.8f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Font3"] = new UnityEngine.Color(0.7f, 0.7f, 0.7f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Font4"] = new UnityEngine.Color(0.6f, 0.6f, 0.6f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Font5"] = new UnityEngine.Color(0.5f, 0.5f, 0.5f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Font6"] = new UnityEngine.Color(0.4f, 0.4f, 0.4f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Font7"] = new UnityEngine.Color(0.85f, 0.7f, 0.5f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Font8"] = new UnityEngine.Color(0.7f, 0.5f, 0.85f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Font9"] = new UnityEngine.Color(0.5f, 0.85f, 0.7f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Font10"] = new UnityEngine.Color(0.6f, 0.8f, 0.2f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Font11"] = new UnityEngine.Color(0.2f, 0.6f, 0.8f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Font12"] = new UnityEngine.Color(0.8f, 0.2f, 0.6f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Subtext1"] = new UnityEngine.Color(0.78f, 0.75f, 0.82f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Subtext2"] = new UnityEngine.Color(0.65f, 0.65f, 0.7f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Subtext3"] = new UnityEngine.Color(0.55f, 0.55f, 0.6f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Subtext4"] = new UnityEngine.Color(0.5f, 0.5f, 0.55f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["AlertBg"] = new UnityEngine.Color(0.9f, 0.2f, 0.2f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["NegativeBalance"] = new UnityEngine.Color(0.95f, 0.3f, 0.3f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["PositiveBalance"] = new UnityEngine.Color(0.3f, 0.95f, 0.3f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["VRCPlusTutorial"] = new UnityEngine.Color(0.95f, 0.8f, 0.2f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Yellow"] = new UnityEngine.Color(1f, 0.92f, 0.16f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Trusted"] = new UnityEngine.Color(0.2f, 0.4f, 1f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Blue"] = new UnityEngine.Color(0.15f, 0.6f, 0.95f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["TextHighlight"] = new UnityEngine.Color(0.9f, 0.9f, 0.2f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Known"] = new UnityEngine.Color(0.3f, 0.8f, 0.4f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Notification"] = new UnityEngine.Color(1f, 0.3f, 0.3f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["DisabledRedBg"] = new UnityEngine.Color(0.5f, 0.2f, 0.2f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["FriendRequest"] = new UnityEngine.Color(0.1f, 0.7f, 0.3f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["VRChatPlusYellow"] = new UnityEngine.Color(1f, 0.84f, 0f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["FriendSafeAlert"] = new UnityEngine.Color(0.2f, 0.8f, 0.8f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["DiscountBG"] = new UnityEngine.Color(0.2f, 0.5f, 0.2f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Black"] = new UnityEngine.Color(0.05f, 0.05f, 0.05f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["NearBlack"] = new UnityEngine.Color(0.1f, 0.1f, 0.1f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Grey1"] = new UnityEngine.Color(0.2f, 0.2f, 0.2f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Grey2"] = new UnityEngine.Color(0.3f, 0.3f, 0.3f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Grey3"] = new UnityEngine.Color(0.4f, 0.4f, 0.4f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Grey4"] = new UnityEngine.Color(0.5f, 0.5f, 0.5f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Grey5"] = new UnityEngine.Color(0.6f, 0.6f, 0.6f, 1f); 
            X.field_Private_Dictionary_2_String_Color_0["White"] = UnityEngine.Color.white;
        }
        #endregion
    }
}

       
        