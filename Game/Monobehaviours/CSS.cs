using BlanketSDK.Utilities;
using BlanketSDK.Core;    
using Brewchat.Game.Wrappers;
using MaterialEditing.UI;
using MelonLoader; 
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Core.Styles;
using VRC.UI.Elements;

namespace Brewchat.Cheat.Modules
{
    internal static class ThemeData
    {
       // Primary Theme Colors

    // UI Theme Specific
    public static Color BottomTabSelected { get; set; }
    public static Color BottomTabHover { get; set; }
    public static Color IconWhite { get; set; }
    public static Color IconHover { get; set; }
    public static Color CategorySelectorIcon { get; set; }
    public static Color IconHighlight { get; set; }
    public static Color IconInactive { get; set; }
    public static Color IconGrey { get; set; }
    public static Color ButtonBorder { get; set; }
    public static Color ButtonHighlightStyle { get; set; }
    public static Color ButtonBg { get; set; }
    public static Color ButtonCell { get; set; }
    public static Color WingBG { get; set; }
    public static Color ButtonOutline { get; set; }
    public static Color WhiteBG { get; set; }
    public static Color NavigationBG { get; set; }
    public static Color Outline { get; set; }

    // Additional UI Elements
    public static Color Highlight { get; set; }
    public static Color Active { get; set; }
    public static Color Active2 { get; set; }
    public static Color Subheader { get; set; }
    public static Color Hover { get; set; }
    public static Color ActiveDark { get; set; }
    public static Color IconActive { get; set; }
    public static Color NavigationButton { get; set; }
    public static Color ButtonHover { get; set; }
    public static Color ButtonHoverTeal { get; set; }
    public static Color ButtonQMSelected { get; set; }
    public static Color BG { get; set; }
    public static Color MenuBG { get; set; }
    public static Color BottomTab { get; set; }
    public static Color Backgrounds { get; set; }

    // Text Colors
    public static Color Font1 { get; set; }
    public static Color Font2 { get; set; }
    public static Color Font3 { get; set; }
    public static Color Font4 { get; set; }
    public static Color Font5 { get; set; }
    public static Color Font6 { get; set; }
    public static Color Font7 { get; set; }
    public static Color Font8 { get; set; }
    public static Color Font9 { get; set; }
    public static Color Font10 { get; set; }
    public static Color Font11 { get; set; }
    public static Color Font12 { get; set; }

    // SubText Colors
    public static Color Subtext1 { get; set; }
    public static Color Subtext2 { get; set; }
    public static Color Subtext3 { get; set; }
    public static Color Subtext4 { get; set; }

    // Miscellaneous Colors
    public static Color AlertBg { get; set; }
    public static Color NegativeBalance { get; set; }
    public static Color PositiveBalance { get; set; }
    public static Color VRCPlusTutorial { get; set; }
    public static Color Yellow { get; set; }
    public static Color Trusted { get; set; }
    public static Color Blue { get; set; }
    public static Color TextHighlight { get; set; }
    public static Color Known { get; set; }
    public static Color Notification { get; set; }
    public static Color DisabledRedBg { get; set; }
    public static Color FriendRequest { get; set; }
    public static Color VRChatPlusYellow { get; set; }
    public static Color FriendSafeAlert { get; set; }
    public static Color DiscountBG { get; set; }

    // Standard Colors
    public static Color Black { get; set; }
    public static Color NearBlack { get; set; }
    public static Color Grey1 { get; set; }
    public static Color Grey2 { get; set; }
    public static Color Grey3 { get; set; }
    public static Color Grey4 { get; set; }
    public static Color Grey5 { get; set; }
    public static Color UnityWhite { get; set; }
    }

    internal class CSS : UnityEngine.MonoBehaviour
    {
        public CSS(IntPtr ptr) : base(ptr){}
        public static CSS? Instance { get; private set; }
        // public static PlayerNameplate _nameplate; // Unused field removed

        private void Awake()
        {
            if (Instance == null) // Use == null for Unity objects
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // Apply to gameObject, not 'this' directly for MonoBehaviours
            }
            else
            {
                Destroy(this);
            }
        }

        public static void Offline()
        {
            #region Offline Button Colors
            var buttons = FindObjectsOfType<UnityEngine.UI.Button>();
            foreach (var button in buttons)
            {
                if (button == null) continue; // Should not happen, but good practice

                ColorBlock colors = button.colors;
                colors.normalColor = ThemeData.ButtonBg;
                colors.highlightedColor = ThemeData.Highlight;
                colors.selectedColor = ThemeData.ButtonQMSelected;
                colors.disabledColor = ThemeData.UnityWhite; // Or a specific disabled color from your theme
                button.colors = colors;
            }
            #endregion
        }

        public static void Online()
        {
            ApplyAdvancedMenuBackgrounds();
            ApplyActionMenuStyling();
            ApplyGlobalStyleEngineColors();
        }

        private static void ApplyAdvancedMenuBackgrounds()
        {
            #region UI Backgrounds
            foreach (GameObject advancedMenuObject in Templates.AdvancedMenus)
            {
                if (advancedMenuObject == null) continue;

                var backgroundRenderer = advancedMenuObject.GetComponentInChildren<VRCUIBackground>();
                if (backgroundRenderer == null)
                {
                    MelonLogger.Msg($"VRCUIBackground not found in {advancedMenuObject.name}", 1);
                    continue;
                }

                Texture2D? texture = EngineUtils.Assets.Texture(new Uri("https://github.com/riotlu/fake-cdn/blob/master/FurryOrgy_1.png?raw=true"));
                if (texture == null)
                {
                    MelonLogger.Msg($"Failed to load texture for AdvancedMenus background from URI.", 2);
                    continue;
                }

                Image? imageComponent = backgroundRenderer.field_Private_Image_0;
                if (imageComponent == null)
                {
                    MelonLogger.Msg($"Image (field_Private_Image_0) not found in VRCUIBackground for {advancedMenuObject.name}", 1);
                    continue;
                }

                Material? material = imageComponent.material; // Consider instantiating: imageComponent.material = new Material(imageComponent.material);
                if (material == null)
                {
                    MelonLogger.Msg($"Material not found for Image in {advancedMenuObject.name}", 1);
                    continue;
                }

                material.mainTexture = texture;
                material.Background_Parallax(texture); // Assuming this is an extension or valid method for the material
            }
            #endregion
        }

        private static void ApplyActionMenuStyling()
        {
            #region ActionMenu
            foreach (var actionMenuInstance in IActionMenuElements.ActionMenus())
            {
                if (actionMenuInstance == null) continue;

                Transform? actionMenuRoot = actionMenuInstance.transform.GetChild(0);
                if (actionMenuRoot == null)
                {
                    MelonLogger.Msg("ActionMenu root transform (child 0) not found.", 2);
                    continue;
                }

                // Dividers
                Transform? dividerContainerParent = actionMenuRoot.GetChild(2);
                if (dividerContainerParent != null)
                {
                    // The original code casts to Il2CppSystem.Collections.IEnumerable.
                    // This implies dividerContainerParent might not be a simple Transform whose children are dividers,
                    // but an Il2Cpp object that *is* a collection or wraps one.
                    Il2CppSystem.Object il2cppDividerContainer = dividerContainerParent.Cast<Il2CppSystem.Object>(); // Assuming GetChild returns a Component/GameObject, then cast to object
                    Il2CppSystem.Collections.IEnumerable? childrenCollection = il2cppDividerContainer?.Cast<Il2CppSystem.Collections.IEnumerable>();

                    if (childrenCollection == null)
                    {
                        MelonLogger.Msg("ActionMenu divider container's children collection is null or not IEnumerable.", 2);
                    }
                    else
                    {
                        foreach (var childIl2CppObj in childrenCollection)
                        {
                            if (childIl2CppObj == null) continue;
                            Transform? childTransform = childIl2CppObj.Cast<Transform>(); // Assumes elements are Transforms
                            if (childTransform == null) continue;

                            var dividers = childTransform.GetComponentsInChildren<Transform>(true)
                                .Where(t => t != null && t.name == "Divider(Clone)");

                            foreach (Transform dividerTransform in dividers)
                            {
                                Image? image = dividerTransform.GetComponentInChildren<Image>(true);
                                if (image != null)
                                {
                                    image.color = ThemeData.BG; // Using specific config color
                                    Sprite? dividerSprite = EngineUtils.Assets.INTERNAL_Texture(autumn.ml.Properties.Resources.action_menu_divider)?.ToSprite();
                                    if (dividerSprite != null) image.sprite = dividerSprite;
                                    else MelonLogger.Msg("Failed to load action_menu_divider sprite.", 1);
                                }
                            }
                        }
                    }
                }
                else MelonLogger.Msg("ActionMenu divider container (child 0 -> child 2) not found.", 2);


                // Background Graphic
                PedalGraphic? backgroundGraphic = actionMenuRoot.GetChild(1)?.GetComponent<PedalGraphic>();
                if (backgroundGraphic != null)
                {
                    backgroundGraphic._texture = EngineUtils.Assets.INTERNAL_Texture(autumn.ml.Properties.Resources.action_menu_bg);
                    backgroundGraphic.color =  Color.white;
                }
                else MelonLogger.Msg("ActionMenu background PedalGraphic (child 0 -> child 1) not found.", 1);

                // Cursor Graphic
                Image? cursorImage = actionMenuRoot.GetChild(4)?.GetComponent<Image>();
                if (cursorImage != null)
                {
                    Sprite? cursorSprite = EngineUtils.Assets.INTERNAL_Texture(autumn.ml.Properties.Resources.action_menu_joystick)?.ToSprite();
                    if (cursorSprite != null) cursorImage.sprite = cursorSprite;
                    else MelonLogger.Msg("Failed to load action_menu_joystick sprite.", 1);
                    cursorImage.color = Color.white;
                }
                else MelonLogger.Msg("ActionMenu cursor Image (child 0 -> child 4) not found.", 1);

                // Center Removable Graphic
                PedalGraphic? centerRemovable = actionMenuRoot.GetChild(0)?.GetComponent<PedalGraphic>();
                if (centerRemovable != null)
                {
                    DestroyImmediate(centerRemovable, true);
                }
                // else sconsole.print("ActionMenu center PedalGraphic (child 0 -> child 0) not found for removal.", 0); // Optional log
            }
            #endregion
        }

        private static void ApplyGlobalStyleEngineColors()
        {
            #region STYLE_ENGINE COLORS
            var styleEngines = FindObjectsOfType<StyleEngine>();
            foreach (StyleEngine styleEngine in styleEngines)
            {
                if (styleEngine == null) continue;

                var colors = styleEngine.field_Private_Dictionary_2_String_Color_0;
                if (colors == null)
                {
                    MelonLogger.Msg("StyleEngine color dictionary (field_Private_Dictionary_2_String_Color_0) is null.", 2);
                    continue;
                }

// UI Elements
                ThemeData.BottomTabSelected = colors["BottomTabSelected"];
                ThemeData.BottomTabHover = colors["BottomTabHover"];
                ThemeData.IconWhite = colors["IconWhite"];
                ThemeData.IconHover = colors["IconHover"];
                ThemeData.CategorySelectorIcon = colors["CategorySelectorIcon"];
                ThemeData.IconHighlight = colors["IconHighlight"];
                ThemeData.IconInactive = colors["IconInactive"];
                ThemeData.IconGrey = colors["IconGrey"];
                ThemeData.ButtonBorder = colors["ButtonBorder"];
                ThemeData.ButtonHighlightStyle = colors["ButtonHighlight"];
                ThemeData.ButtonBg = colors["ButtonBg"];
                ThemeData.ButtonCell = colors["ButtonCell"];
                ThemeData.WingBG = colors["WingBG"];
                ThemeData.ButtonOutline = colors["ButtonOutline"];
                ThemeData.WhiteBG = colors["WhiteBG"];
                ThemeData.NavigationBG = colors["NavigationBG"];
                ThemeData.Outline = colors["Outline"];

                // TEXT
                ThemeData.Font1 = colors["Font1"];
                ThemeData.Font2 = colors["Font2"];
                ThemeData.Font3 = colors["Font3"];
                ThemeData.Font4 = colors["Font4"];
                ThemeData.Font5 = colors["Font5"];
                ThemeData.Font6 = colors["Font6"];
                ThemeData.Font7 = colors["Font7"];
                ThemeData.Font8 = colors["Font8"];
                ThemeData.Font9 = colors["Font9"];
                ThemeData.Font10 = colors["Font10"];
                ThemeData.Font11 = colors["Font11"];
                ThemeData.Font12 = colors["Font12"];

                // SUB_TEXT
                ThemeData.Subtext1 = colors["Subtext1"];
                ThemeData.Subtext2 = colors["Subtext2"];
                ThemeData.Subtext3 = colors["Subtext3"];
                ThemeData.Subtext4 = colors["Subtext4"];

                // Miscellaneous
                ThemeData.AlertBg = colors["AlertBg"];
                ThemeData.NegativeBalance = colors["NegativeBalance"];
                ThemeData.PositiveBalance = colors["PositiveBalance"];
                ThemeData.VRCPlusTutorial = colors["VRCPlusTutorial"];
                ThemeData.Yellow = colors["Yellow"];
                ThemeData.Trusted = colors["Trusted"];
                ThemeData.Blue = colors["Blue"];
                ThemeData.TextHighlight = colors["TextHighlight"];
                ThemeData.Known = colors["Known"];
                ThemeData.Notification = colors["Notification"];
                ThemeData.DisabledRedBg = colors["DisabledRedBg"];
                ThemeData.FriendRequest = colors["FriendRequest"];
                ThemeData.VRChatPlusYellow = colors["VRChatPlusYellow"];
                ThemeData.FriendSafeAlert = colors["FriendSafeAlert"];
                ThemeData.DiscountBG = colors["DiscountBG"];

                // Colors
                ThemeData.Black = colors["Black"];
                ThemeData.NearBlack = colors["NearBlack"];
                ThemeData.Grey1 = colors["Grey1"];
                ThemeData.Grey2 = colors["Grey2"];
                ThemeData.Grey3 = colors["Grey3"];
                ThemeData.Grey4 = colors["Grey4"];
                ThemeData.Grey5 = colors["Grey5"];
                ThemeData.UnityWhite = colors["White"];

                // Additional UI elements with their own variables
                ThemeData.Highlight = colors["Highlight"];
                ThemeData.Active = colors["Active"];
                ThemeData.Active2 = colors["Active2"];
                ThemeData.Subheader = colors["Subheader"];
                ThemeData.Hover = colors["Hover"];
                ThemeData.ActiveDark = colors["ActiveDark"];
                ThemeData.IconActive = colors["IconActive"];
                ThemeData.NavigationButton = colors["NavigationButton"];
                ThemeData.ButtonHover = colors["ButtonHover"];
                ThemeData.ButtonHoverTeal = colors["ButtonHoverTeal"];
                ThemeData.ButtonQMSelected = colors["ButtonQMSelected"];
                ThemeData.BG = colors["BG"];
                ThemeData.MenuBG = colors["MenuBG"];
                ThemeData.BottomTab = colors["BottomTab"];
                ThemeData.Backgrounds = colors["Backgrounds"];
            }
            #endregion
        }
    }
}