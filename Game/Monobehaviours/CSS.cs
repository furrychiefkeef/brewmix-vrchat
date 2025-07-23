using BlanketSDK.Utilities;
using BlanketSDK.Core;
using Brewchat.Game.Wrappers;
using MaterialEditing.UI;
using MelonLoader;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Core.Styles;
using VRC.UI.Elements;
using VRC.Core;
using VRC.UI;
using static MelonLoader.MelonLogger;

namespace Brewchat.Cheat.Modules
{
    public class GetCurrentThemeData
    {
        public static Color Highlight
        {
            get
            {
                Color originalColor = IMainMenuElements.MMControls.Buttons.SingleButton().FindChild("Background_SelectedState").GetComponent<ImageEx>().color;
                return new Color(originalColor.r, originalColor.g, originalColor.b, 1f); // Adjust alpha for highlight effect
            }
        }
    }       
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
        public static GameObject QueueAudio;
        public CSS(IntPtr ptr) : base(ptr) {}
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

        public static void SetLoadingAudio()
        {
            if (QueueAudio == null)
            {
                AudioSource? LoadingScreenAudio = VRCUiPageLoading.field_Internal_Static_VRCUiPageLoading_0.gameObject.transform.GetChild(1).GetComponent<AudioSource>();
                UnityEngine.Object.DestroyImmediate(LoadingScreenAudio);
                QueueAudio = EngineUtils.Assets.CreateAudio(new(Entry.config.Appearance.inGame.QueueAudio));
                QueueAudio.transform.parent = VRCUiPageLoading.field_Internal_Static_VRCUiPageLoading_0.gameObject.transform;
            }
            else return;
          
        }

        public static void Update()
        {
            
        }
        public static void Online()
        {
            Entry.config.Appearance.inGame.VRCUI._lastSelectedColor = GetCurrentThemeData.Highlight;
            Config.SaveToFile(Entry.config, "config.json");
            ApplyActionMenuStyling();
            ApplyAdvancedMenuBackgrounds();
            ApplyHighlight();
        }
        public static void Offline()
        {
            #region Offline Button Colors
            var buttons = Resources.FindObjectsOfTypeAll<UnityEngine.UI.Button>();
            foreach (var button in buttons)
            {
                if (button == null) continue; // Should not happen, but good practice

                ColorBlock colors = button.colors;
                colors.normalColor = Entry.config.Appearance.inGame.VRCUI._lastSelectedColor;
                colors.highlightedColor =  Entry.config.Appearance.inGame.VRCUI._lastSelectedColor;
                colors.selectedColor = Entry.config.Appearance.inGame.VRCUI._lastSelectedColor;
                colors.disabledColor = ThemeData.UnityWhite; // Or a specific disabled color from your theme
                button.colors = colors;
            }
            #endregion
        }
        public static void ApplyHighlight()
        {
            GameUtils.Camera.Get.transform.GetComponents<HighlightsFXStandalone>().ToList().ForEach((fx) =>
            {
                if (fx.field_Protected_Color_0 != Entry.config.Appearance.inGame.VRCUI._lastSelectedColor)
                {
                    fx.field_Protected_Color_0 = Entry.config.Appearance.inGame.VRCUI._lastSelectedColor;
                }
            });
           
        }
        public static void ApplyAdvancedMenuBackgrounds()
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

                Texture2D? texture = EngineUtils.Assets.Texture(new Uri(Entry.config.Appearance.inGame.Qm_Background));
                if (texture == null)
                {
                    MelonLogger.Msg($"Failed to load texture for AdvancedMenus background from URI.", 2);
                    continue;
                }

                Image? imageComponent = backgroundRenderer.field_Private_Image_0;
                if (imageComponent == null)
                {
                    MelonLogger.Msg($"Image ({backgroundRenderer.field_Private_Image_0} not found in VRCUIBackground for {advancedMenuObject.name}", 1);
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

        public static void ApplyActionMenuStyling()
        {
            #region ActionMenu
            foreach (Transform actionMenuInstance in IActionMenuElements.ActionMenus())
            {
                if (actionMenuInstance == null) continue;

                // Get action menu root - check if it has enough children
                if (actionMenuInstance.childCount == 0)
                {
                    MelonLogger.Msg("ActionMenu has no children.", 2);
                    continue;
                }

                Transform actionMenuRoot = actionMenuInstance.GetChild(0);
                if (actionMenuRoot == null)
                {
                    MelonLogger.Msg("ActionMenu root transform (child 0) not found.", 2);
                    continue;
                }

                // Apply Dividers Styling
                ApplyDividerStyling(actionMenuRoot);

                // Apply Background Graphic
                ApplyBackgroundGraphic(actionMenuRoot);

                // Apply Cursor Graphic  
                ApplyCursorGraphic(actionMenuRoot);

                // Remove Center Graphic
                RemoveCenterGraphic(actionMenuRoot);
            }
            #endregion
        }
        private static void ApplyDividerStyling(Transform actionMenuRoot)
        {
            try
            {
                // Dividers are typically at child index 2
                if (actionMenuRoot.childCount > 2)
                {
                    Transform dividerContainer = actionMenuRoot.GetChild(2);
                    if (dividerContainer != null)
                    {
                        // Iterate through all divider children
                        for (int i = 0; i < dividerContainer.childCount; i++)
                        {
                            Transform childTransform = dividerContainer.GetChild(i);
                            if (childTransform == null) continue;

                            // Find all divider components recursively
                            var dividers = childTransform.GetComponentsInChildren<Transform>(true)
                                .Where(t => t != null && t.name.Contains("Divider"));

                            foreach (Transform dividerTransform in dividers)
                            {
                                Image dividerImage = dividerTransform.GetComponentInChildren<Image>(true);
                                if (dividerImage != null)
                                {
                                    dividerImage.color = Entry.config.Appearance.inGame.VRCUI._lastSelectedColor;

                                    // Apply custom divider texture
                                    try
                                    {
                                        Texture2D dividerTexture = EngineUtils.Assets.INTERNAL_Texture(autumn.ml.Properties.Resources.action_menu_divider);
                                        if (dividerTexture != null)
                                        {
                                            Sprite dividerSprite = dividerTexture.ToSprite();
                                            if (dividerSprite != null)
                                                dividerImage.sprite = dividerSprite;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MelonLogger.Error($"Failed to apply divider texture: {ex.Message}");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MelonLogger.Error($"Error applying divider styling: {ex.Message}");
            }
        }

        private static void ApplyBackgroundGraphic(Transform actionMenuRoot)
        {
            try
            {
                // Background is typically at child index 1
                if (actionMenuRoot.childCount > 1)
                {
                    Transform backgroundChild = actionMenuRoot.GetChild(1);
                    if (backgroundChild != null)
                    {
                        PedalGraphic backgroundGraphic = backgroundChild.GetComponent<PedalGraphic>();
                        if (backgroundGraphic != null)
                        {
                            Texture2D? bgTexture = EngineUtils.Assets.INTERNAL_Texture(autumn.ml.Properties.Resources.action_menu_bg);
                            if (bgTexture != null)
                            {
                                backgroundGraphic._texture = bgTexture;
                                backgroundGraphic.color = GetCurrentThemeData.Highlight; // Use theme color instead of hardcoded
                            }
                            else
                            {
                                MelonLogger.Msg("Failed to load action_menu_bg texture.", 1);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MelonLogger.Error($"Error applying background graphic: {ex.Message}");
            }
        }

        private static void ApplyCursorGraphic(Transform actionMenuRoot)
        {
            try
            {
                // Cursor is typically at child index 4
                if (actionMenuRoot.childCount > 4)
                {
                    Transform cursorChild = actionMenuRoot.GetChild(4);
                    if (cursorChild != null)
                    {
                        Image cursorImage = cursorChild.GetComponent<Image>();
                        if (cursorImage != null)
                        {
                            Texture2D? cursorTexture = EngineUtils.Assets.INTERNAL_Texture(autumn.ml.Properties.Resources.action_menu_joystick);
                            if (cursorTexture != null)
                            {
                                Sprite cursorSprite = cursorTexture.ToSprite();
                                if (cursorSprite != null)
                                {
                                    cursorImage.sprite = cursorSprite;
                                    cursorImage.color = Entry.config.Appearance.inGame.VRCUI._lastSelectedColor; // Use consistent theme color
                                }
                            }
                            else
                            {
                                MelonLogger.Msg("Failed to load action_menu_joystick texture.", 1);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MelonLogger.Error($"Error applying cursor graphic: {ex.Message}");
            }
        }

        private static void RemoveCenterGraphic(Transform actionMenuRoot)
        {
            try
            {
                // Center graphic is typically at child index 0
                if (actionMenuRoot.childCount > 0)
                {
                    Transform centerChild = actionMenuRoot.GetChild(0);
                    if (centerChild != null)
                    {
                        PedalGraphic centerRemovable = centerChild.GetComponent<PedalGraphic>();
                        if (centerRemovable != null)
                        {
                            UnityEngine.Object.DestroyImmediate(centerRemovable, true);
                            MelonLogger.Msg("Successfully removed center graphic.", 0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MelonLogger.Error($"Error removing center graphic: {ex.Message}");
            }
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
        public struct Refreshers 
        {
            public static void RefreshLoadingAudio() 
            {
                AudioSource cli = QueueAudio.GetComponent<AudioSource>();
                cli.clip = EngineUtils.Assets.AudioClip(new Uri(Entry.config.Appearance.inGame.QueueAudio));
            }
            public static void RefreshActionMenuColor()
            { 
                foreach (Transform actionMenuInstance in IActionMenuElements.ActionMenus())
                {
                    if (actionMenuInstance == null) continue;

                    // Get action menu root - check if it has enough children
                    if (actionMenuInstance.childCount == 0)
                    {
                        MelonLogger.Msg("ActionMenu has no children.", 2);
                        continue;
                    }

                    Transform actionMenuRoot = actionMenuInstance.GetChild(0);
                    if (actionMenuRoot == null)
                    {
                        MelonLogger.Msg("ActionMenu root transform (child 0) not found.", 2);
                        continue;
                    }

                    // Apply Dividers Styling
                    ApplyDividerStyling(actionMenuRoot);

                    // Apply Background Graphic
                    ApplyBackgroundGraphic(actionMenuRoot);

                    // Apply Cursor Graphic  
                    ApplyCursorGraphic(actionMenuRoot);

                    // Remove Center Graphic
                }
            }
        }
    }
}