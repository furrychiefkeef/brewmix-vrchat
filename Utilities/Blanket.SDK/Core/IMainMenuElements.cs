using UnityEngine;
using VRC.UI.Elements;

namespace BlanketSDK.Core;

/// <summary>
/// VRCUI_CLASSMENU ELEMENTS I DID THIS CAUSE I WAS TERRIFIED OF MY CODE STYLE.
/// </summary>
/// 
internal class IMainMenuElements // WHY DID I MAKE THIS: CAUSE MY IDE WAS GIVING ME A FUCKING MENTALBREAK DOWN CAUSE MY CODE STYLE NEEDS TO BE CHANGED.
{
    internal static GameObject MainMenu() => UnityEngine.Object.FindObjectOfType<MainMenu>().transform.GetChild(0).gameObject;
    protected static MainMenu? MM() => UnityEngine.Object.FindObjectOfType<MainMenu>();
    public static Transform MMParent() => MainMenu().transform.FindChild("MMParent");

    // Put RootObjects

    protected interface MMMenus
    {
        public static Transform WingMenu(string position) => MainMenu().transform.FindChild($"Wing_{position}/Container/InnerContainer");

        // Put Menus
        public interface MMSubmenus
        {
            public static Transform Menu_Setting() => MMParent().transform.FindChild("HeaderOffset/Menu_Settings");

            public static Transform MenuUser_Details() => MMParent().transform.FindChild("HeaderOffset/Menu_UserDetail");
            public static Transform MenuAvatarAVM() => MMParent().transform.FindChild("HeaderOffset/Menu_MM_Avatars_AVM");
        }
    }

    protected interface MMElements
    {
        //Put Elements
        public static Transform ProfileRow() => MMMenus.MMSubmenus.MenuUser_Details().transform.FindChild("ScrollRect/Viewport/VerticalLayoutGroup/Row3");

        public static Transform CellContainer(string Name) => MMMenus.MMSubmenus.Menu_Setting().transform.FindChild($"Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Viewport/VerticalLayoutGroup/{Name}");
        public static Transform Panel() => CellContainer("Debug").transform.FindChild("Debug");
        /*public static Transform WingButtonContainer(string position) => WingMenu(position).transform.FindChild("WingMenu/ScrollRect/Viewport/VerticalLayoutGroup");*/
    }

    protected interface MMControls
    {
        // Put Controls
        public static Transform Tab() => MainMenu().transform.FindChild("PageButtons/HorizontalLayoutGroup/Launch_Pad_Button_Tab");
        public static Transform OperatorRadialSelection() => MMElements.CellContainer("ComfortAndSafety").transform.FindChild("Sharing/QM_Settings_Panel/VerticalLayoutGroup/DirectSharingVisibility");
        public static Transform OperatorToggle() => MMElements.CellContainer("Debug").transform.FindChild("AvatarDebugging/Settings_Panel_1/VerticalLayoutGroup/AvatarDebug");
        public interface Buttons
        {
            //Put Buttons       
            public static Transform AvatarButton() => MMMenus.MMSubmenus.MenuAvatarAVM().transform.FindChild("Dynamic_Content_Container/Viewport/Panel_SelectedAvatar/Avatar_Marketplace_Panel/Avatar_Functions_Container/Favorite_Avatar_Button");
            public static Transform SingleButton() => MMMenus.MMSubmenus.Menu_Setting().transform.FindChild("Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Navigation/Viewport/VerticalLayoutGroup/Cell_MM_Debug");

            public static Transform WingButton(string position) => MMMenus.WingMenu(position).transform.FindChild("WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Explore");
            public static Transform ProfileButton() => MMMenus.MMSubmenus.MenuUser_Details().transform.FindChild("ScrollRect/Viewport/VerticalLayoutGroup/Row3/CellGrid_MM_Content/ModBtn");

            public static Transform ExpandButton() => MMParent().transform.FindChild("Panel_MM_Header/HeaderRight/Button_Settings");
            /*public static Transform MMTABGAINTSHIELD() => MenuDevTools().transform.FindChild("Scrollrect/Viewport/VerticalLayoutGroup/Buttons/Button_Tag");*/
        }

    }

    internal interface MMFallbacks
    {
        public static Transform _Fallback_Dashboard_Group_MM_UI_Buttons_TAB() => MainMenu().transform.FindChild("PageButtons/HorizontalLayoutGroup");
        public static Transform _Fallback_Dashboard_Group_MM_UI_ExpandButtons() => MMParent().transform.FindChild("Panel_MM_Header/HeaderRight");
        public static Transform _Fallback_Dashboard_Group_MM_UI_Buttons() => MMMenus.MMSubmenus.Menu_Setting().transform.FindChild("Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Navigation/Viewport/VerticalLayoutGroup");
        public static Transform _Fallback_Dashboard_Group_MM_UI_WingButtons(string position) => MMMenus.WingMenu(position).transform.FindChild("WingMenu/ScrollRect/Viewport/VerticalLayoutGroup");
        public static Transform _Fallback_Dashboard_Group_MM_UI_ProfileButtons() => MMMenus.MMSubmenus.MenuUser_Details().transform.FindChild("ScrollRect/Viewport/VerticalLayoutGroup/Row3/CellGrid_MM_Content");
        public static Transform _Fallback_Dashboard_Group_MM_UI_ProfileRows() => MMMenus.MMSubmenus.MenuUser_Details().transform.FindChild("ScrollRect/Viewport/VerticalLayoutGroup");
    }

    internal interface MENUMENU_FLAGS
    {
        public enum Menus
        {
            Here,
            Dashboard,
            DevTools,
            SelectedUser_Local,
            QM_GeneralSettings,
        }

        public enum Panels
        {
            Sharing,
            Debug,
            DevTools,
        }
    }
}