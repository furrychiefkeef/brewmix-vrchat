using System.Collections;
using BlanketSDK.Utilities;
using BlanketSDK.Core;
using Brewchat.Game.Wrappers;
using MaterialEditing.UI;
using Syrup.IMGUI;
using UnityEngine;
using UnityEngine.UI;
using UNIXEngine.IL2CPPModding.Utils.Collections;
using VRC.UI;
using VRC.UI.Core.Styles;
using VRC.UI.Elements;
using Resources = brewchat.hybridxcore.bep.Properties.Resources;

namespace Brewchat.Cheat.Modules;
internal class CSS : UnityEngine.MonoBehaviour
{ 
    public static CSS? Instance { get; private set; }
    public static PlayerNameplate _nameplate;
    private void Awake()
    {
        if (Instance is null)
        {
            Instance = this; DontDestroyOnLoad(this);
        }   
        else Destroy(this);
    }

    public static void Offline()
    {
        #region Offline

        FindObjectsOfType<UnityEngine.UI.Button>().ToList().ForEach(button_colors =>
        {
            ColorBlock colorBlock = button_colors.colors;
            colorBlock.normalColor = EngineUtils.Conversion.HexToColor("#c60e7b");
            colorBlock.highlightedColor = EngineUtils.Conversion.HexToColor("#ff14a0");
            colorBlock.selectedColor = EngineUtils.Conversion.HexToColor("#c60e7b");
            colorBlock.disabledColor = Color.white;
            button_colors.colors = colorBlock;
        });
        
        #endregion
         
    }
    public static void Online()
    {
        #region UI

        /*
        var verify = IMainMenuElements.MMMenus.MMSubmenus.MenuUser_Details().transform.FindChild("ScrollRect/Viewport/VerticalLayoutGroup/Row1/Profile/DetailsArea/ScrollRect/Viewport/VerticalLayoutGroup/Field_AgeVerification/Icon_AgeVerification");
        verify.GetComponent<ImageEx>().color = Color.red;*/
  
        foreach (GameObject gameobject in Templates.AdvancedMenus)
        {
            var render =  gameobject.GetComponentInChildren<VRCUIBackground>();
            if (render is null) return;
            Texture? texture = EngineUtils.Assets.Texture(new Uri("https://github.com/riotlu/fake-cdn/blob/master/FurryOrgy_1.png?raw=true"));
            Image? rm = render.field_Private_Image_0;
            Material? rm_m = rm.material;
            rm_m.mainTexture = texture;
            rm_m.Background_Parallax(texture);
        }

        #endregion
        
        #region ActionMenu

        foreach (var obj in IActionMenuElements.ActionMenus())
        {
            /*ActionMenu? _ActionMenu = obj.GetComponent<ActionMenu>();*/
        
        if (obj != null)
        {
            Il2CppSystem.Object divider_container = obj.transform.GetChild(0).GetChild(2);

            Il2CppSystem.Collections.IEnumerable childrenCollection = divider_container.Cast<Il2CppSystem.Collections.IEnumerable>();
            if (childrenCollection == null)
            {

                sconsole.print($"Container children collection is null",2);
                return;
            }

            #region action-menu_divider.png
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
                        image.color = Config.Appearance.Platte.Main;
                        image.sprite = EngineUtils.Assets.INTERNAL_Texture(Resources.action_menu_divider).ToSprite();
                    }
                }
            }

            PedalGraphic Action_Menu_GRAPHICS_BG = obj.transform.GetChild(0).GetChild(1).GetComponent<PedalGraphic>();
            Action_Menu_GRAPHICS_BG._texture = EngineUtils.Assets.INTERNAL_Texture(Resources.action_menu_bg);
            Action_Menu_GRAPHICS_BG.color = EngineUtils.Conversion.HexToColor("#c60e7b");
            
            UnityEngine.UI.Image Action_Menu_GRAPHICS_CURSOR = obj.transform.GetChild(0).GetChild(4).GetComponent<UnityEngine.UI.Image>();
            Action_Menu_GRAPHICS_CURSOR.sprite = EngineUtils.Assets.INTERNAL_Texture(Resources.action_menu_joystick).ToSprite();
            Action_Menu_GRAPHICS_CURSOR.color = EngineUtils.Conversion.HexToColor("#c60e7b");
            PedalGraphic? Center_Removable = obj.transform.GetChild(0).GetChild(0).GetComponent<PedalGraphic>();
            
            DestroyImmediate(Center_Removable,true);
        }
       
            #endregion
        }

        #endregion

        #region STYLE_ENGINE+QM+MM_COLORS
        
        Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppArrayBase<StyleEngine>? VRC_UI = FindObjectsOfType<StyleEngine>();
        foreach (StyleEngine? X in VRC_UI)
        {
            #region UI_Theme

            foreach (var ddic in X.field_Private_Dictionary_2_String_Color_0)
            {
                if (ddic.Key.Contains("Active"))
                {
                    
                }
            }
            X.field_Private_Dictionary_2_String_Color_0["Highlight"] = EngineUtils.Conversion.HexToColor("#c60e7b"); 
            X.field_Private_Dictionary_2_String_Color_0["Active"] = EngineUtils.Conversion.HexToColor("#c60e7b");
            X.field_Private_Dictionary_2_String_Color_0["Active2"] = EngineUtils.Conversion.HexToColor("#c60e7b");
            X.field_Private_Dictionary_2_String_Co lor_0["BottomTabSelected"] = new UnityEngine.Color(0.9f, 0.1f, 0.8f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Subheader"] = EngineUtils.Conversion.HexToColor("#c60e7b");
            X.field_Private_Dictionary_2_String_Color_0["Hover"] = EngineUtils.Conversion.HexToColor("#c60e7b");
            X.field_Private_Dictionary_2_String_Color_0["ActiveDark"] = EngineUtils.Conversion.HexToColor("#c60e7b");
            X.field_Private_Dictionary_2_String_Color_0["BottomTabHover"] = new UnityEngine.Color(0.8f, 0.3f, 0.5f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["IconActive"] = EngineUtils.Conversion.HexToColor("#c60e7b");
            X.field_Private_Dictionary_2_String_Color_0["IconWhite"] = new UnityEngine.Color(0.95f, 0.95f, 0.95f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["IconHover"] = new UnityEngine.Color(1f, 0.8f, 0.2f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["CategorySelectorIcon"] = new UnityEngine.Color(0.8f, 0.3f, 0.5f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["IconHighlight"] = new UnityEngine.Color(1f, 0.5f, 0f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["IconInactive"] = new UnityEngine.Color(0.6f, 0.6f, 0.6f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["IconGrey"] = new UnityEngine.Color(0.5f, 0.5f, 0.5f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["NavigationButton"] = EngineUtils.Conversion.HexToColor("#c60e7b");
            X.field_Private_Dictionary_2_String_Color_0["ButtonHover"] = EngineUtils.Conversion.HexToColor("#c60e7b");
            X.field_Private_Dictionary_2_String_Color_0["ButtonHoverTeal"] = EngineUtils.Conversion.HexToColor("#c60e7b");
            X.field_Private_Dictionary_2_String_Color_0["ButtonBorder"] = new UnityEngine.Color(0.3f, 0.3f, 0.3f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["ButtonHighlight"] = new UnityEngine.Color(0.9f, 0.7f, 0.1f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["ButtonBg"] = new UnityEngine.Color(0.15f, 0.15f, 0.15f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["ButtonCell"] = new UnityEngine.Color(0.25f, 0.25f, 0.25f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["ButtonQMSelected"] = EngineUtils.Conversion.HexToColor("#c60e7b");
            X.field_Private_Dictionary_2_String_Color_0["WingBG"] = new UnityEngine.Color(0.12f, 0.12f, 0.15f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["ButtonOutline"] = new UnityEngine.Color(0.4f, 0.4f, 0.4f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["WhiteBG"] = new UnityEngine.Color(0.9f, 0.9f, 0.9f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["NavigationBG"] = new UnityEngine.Color(0.18f, 0.18f, 0.2f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Outline"] = new UnityEngine.Color(0.7f, 0.7f, 0.7f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["BG"] = EngineUtils.Conversion.HexToColor("#c60e7b");
            X.field_Private_Dictionary_2_String_Color_0["MenuBG"] = EngineUtils.Conversion.HexToColor("#c60e7b");
            X.field_Private_Dictionary_2_String_Color_0["BottomTab"] = EngineUtils.Conversion.HexToColor("#c60e7b");
            X.field_Private_Dictionary_2_String_Color_0["Backgrounds"] = EngineUtils.Conversion.HexToColor("#c60e7b");

            #endregion

            #region TEXT

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

            #region SUB_TEXT

            X.field_Private_Dictionary_2_String_Color_0["Subtext1"] = new UnityEngine.Color(0.78f, 0.75f, 0.82f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Subtext2"] = new UnityEngine.Color(0.65f, 0.65f, 0.7f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Subtext3"] = new UnityEngine.Color(0.55f, 0.55f, 0.6f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Subtext4"] = new UnityEngine.Color(0.5f, 0.5f, 0.55f, 1f);

            #endregion
           
            
            #endregion
            
            #region Miscellaneous

            #region TrustRanks
            
            X.field_Private_Dictionary_2_String_Color_0["Trusted"] = new UnityEngine.Color(0.2f, 0.4f, 1f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Known"] = new UnityEngine.Color(0.3f, 0.8f, 0.4f, 1f);
            
            #endregion
            X.field_Private_Dictionary_2_String_Color_0["AlertBg"] = new UnityEngine.Color(0.9f, 0.2f, 0.2f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["NegativeBalance"] = new UnityEngine.Color(0.95f, 0.3f, 0.3f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["PositiveBalance"] = new UnityEngine.Color(0.3f, 0.95f, 0.3f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["VRCPlusTutorial"] = new UnityEngine.Color(0.95f, 0.8f, 0.2f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Yellow"] = new UnityEngine.Color(1f, 0.92f, 0.16f, 1f);
            
            X.field_Private_Dictionary_2_String_Color_0["Blue"] = new UnityEngine.Color(0.15f, 0.6f, 0.95f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["TextHighlight"] = new UnityEngine.Color(0.9f, 0.9f, 0.2f, 1f);
            
            X.field_Private_Dictionary_2_String_Color_0["Notification"] = new UnityEngine.Color(1f, 0.3f, 0.3f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["DisabledRedBg"] = new UnityEngine.Color(0.5f, 0.2f, 0.2f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["FriendRequest"] = new UnityEngine.Color(0.1f, 0.7f, 0.3f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["VRChatPlusYellow"] = new UnityEngine.Color(1f, 0.84f, 0f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["FriendSafeAlert"] = new UnityEngine.Color(0.2f, 0.8f, 0.8f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["DiscountBG"] = new UnityEngine.Color(0.2f, 0.5f, 0.2f, 1f);

            #endregion
            
            #region Colors

            X.field_Private_Dictionary_2_String_Color_0["Black"] = new UnityEngine.Color(0.05f, 0.05f, 0.05f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["NearBlack"] = new UnityEngine.Color(0.1f, 0.1f, 0.1f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Grey1"] = new UnityEngine.Color(0.2f, 0.2f, 0.2f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Grey2"] = new UnityEngine.Color(0.3f, 0.3f, 0.3f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Grey3"] = new UnityEngine.Color(0.4f, 0.4f, 0.4f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Grey4"] = new UnityEngine.Color(0.5f, 0.5f, 0.5f, 1f);
            X.field_Private_Dictionary_2_String_Color_0["Grey5"] = new UnityEngine.Color(0.6f, 0.6f, 0.6f, 1f); 
            X.field_Private_Dictionary_2_String_Color_0["White"] = UnityEngine.Color.white;

            #endregion
           
        }
        #endregion
    }
}

       
        