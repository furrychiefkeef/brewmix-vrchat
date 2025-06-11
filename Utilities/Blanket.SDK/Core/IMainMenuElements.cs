using UnityEngine;
using VRC.UI.Elements;

namespace BlanketSDK.Core
{
    /// <summary>
    /// VRCUI_CLASSMENU ELEMENTS I DID THIS CAUSE I WAS TERRIFIED OF MY CODE STYLE.
    /// </summary>
    /// 
    internal class IMainMenuElements // WHY DID I MAKE THIS: CAUSE MY IDE WAS GIVING ME A FUCKING MENTALBREAK DOWN CAUSE MY CODE STYLE NEEDS TO BE CHANGED.
    {
        internal static GameObject MainMenu() { return UnityEngine.Object.FindObjectOfType<MainMenu>().transform.GetChild(0).gameObject; }
        protected static MainMenu MM() { return UnityEngine.Object.FindObjectOfType<MainMenu>(); }
        public static Transform MMParent() { return MainMenu().transform.FindChild("MMParent"); }

        // Put RootObjects

        public static class MMMenus
        {
            public static Transform WingMenu(string position) { return MainMenu().transform.FindChild($"Wing_{position}/Container/InnerContainer"); }

            // Put Menus
            public static class MMSubmenus
            {
                public static Transform Menu_Setting() { return MMParent().transform.FindChild("HeaderOffset/Menu_Settings"); }
                public static Transform Menu_WorldInfo() { return MMParent().transform.FindChild("HeaderOffset/Menu_MM_WorldInformation"); }
                public static Transform Menu_Worlds() { return MMParent().transform.FindChild("HeaderOffset/Menu_MM_Worlds"); }
                public static Transform MenuUser_Details() { return MMParent().transform.FindChild("HeaderOffset/Menu_UserDetail"); }
                public static Transform MenuAvatar_AVM() { return MMParent().transform.FindChild("HeaderOffset/Menu_MM_Avatars_AVM"); }
            }
        }

        protected static class MMElements
        {
            //Put Elements  
            public static Transform WorldRow() { return MMMenus.MMSubmenus.Menu_WorldInfo().transform.FindChild("Panel_World_Information/Content/Viewport/BodyContainer_World_Details/ScrollRect/Viewport/VerticalLayoutGroup/Actions"); }
            public static Transform ProfileRow() { return MMMenus.MMSubmenus.MenuUser_Details().transform.FindChild("ScrollRect/Viewport/VerticalLayoutGroup/Row3"); }
            public static Transform CellContainer(string Name) { return MMMenus.MMSubmenus.Menu_Setting().transform.FindChild($"Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Viewport/VerticalLayoutGroup/{Name}"); }
            public static Transform Panel() { return CellContainer("Debug").transform.FindChild("Debug"); }
            /*public static Transform WingButtonContainer(string position) => WingMenu(position).transform.FindChild("WingMenu/ScrollRect/Viewport/VerticalLayoutGroup");*/
        }

        protected static class MMControls
        {
            // Put Controls
            public static Transform Tab() { return MainMenu().transform.FindChild("PageButtons/HorizontalLayoutGroup/Launch_Pad_Button_Tab"); }
            public static Transform OperatorRadialSelection() { return MMElements.CellContainer("ComfortAndSafety").transform.FindChild("Sharing/QM_Settings_Panel/VerticalLayoutGroup/DirectSharingVisibility"); }
            public static Transform OperatorToggle() { return MMElements.CellContainer("Debug").transform.FindChild("AvatarDebugging/Settings_Panel_1/VerticalLayoutGroup/AvatarDebug"); }
            
            public static class Buttons
            {
                //Put Buttons       
                public static class AvatarMenuRelated
                {
                    public static Transform AvatarButtonSmall() { return MMMenus.MMSubmenus.MenuAvatar_AVM().transform.FindChild("Dynamic_Content_Container/Viewport/Panel_SelectedAvatar/Avatar_Marketplace_Panel/Avatar_Functions_Container/Favorite_Avatar_Button"); }
                    public static Transform AvatarButton() { return MMMenus.MMSubmenus.MenuAvatar_AVM().transform.FindChild("Dynamic_Content_Container/Viewport/Panel_SelectedAvatar/Avatar_Marketplace_Panel/Avatar_Details_Button"); }
                }

                public static class WorldMenuRelated
                {
                    public static Transform CellButton() { return MMMenus.MMSubmenus.Menu_Worlds().transform.Find("Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Viewport/Header_MM_H2/RightItemContainer/Button_CellSize"); }
                }
               
                public static Transform SingleButton() { return MMMenus.MMSubmenus.Menu_Setting().transform.FindChild("Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Navigation/Viewport/VerticalLayoutGroup/Cell_MM_Debug"); }

                public static Transform WingButton(string position) { return MMMenus.WingMenu(position).transform.FindChild("WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Explore"); }
                public static Transform ProfileButton() { return MMMenus.MMSubmenus.MenuUser_Details().transform.FindChild("ScrollRect/Viewport/VerticalLayoutGroup/Row3/CellGrid_MM_Content/ModBtn"); }

                public static Transform ExpandButton() { return MMParent().transform.FindChild("Panel_MM_Header/HeaderRight/Button_Settings"); }
                /*public static Transform MMTABGAINTSHIELD() => MenuDevTools().transform.FindChild("Scrollrect/Viewport/VerticalLayoutGroup/Buttons/Button_Tag");*/
            }
        }

        internal static class MMFallbacks
        {
            public static Transform _Fallback_Dashboard_Group_MM_UI_Buttons_TAB() { return MainMenu().transform.FindChild("PageButtons/HorizontalLayoutGroup"); }
            public static Transform _Fallback_Dashboard_Group_MM_UI_ExpandButtons() { return MMParent().transform.FindChild("Panel_MM_Header/HeaderRight"); }
            public static Transform _Fallback_Dashboard_Group_MM_UI_Buttons() { return MMMenus.MMSubmenus.Menu_Setting().transform.FindChild("Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Navigation/Viewport/VerticalLayoutGroup"); }
            public static Transform _Fallback_Dashboard_Group_MM_UI_WingButtons(string position) { return MMMenus.WingMenu(position).transform.FindChild("WingMenu/ScrollRect/Viewport/VerticalLayoutGroup"); }
            public static Transform _Fallback_Dashboard_Group_MM_UI_ProfileButtons() { return MMMenus.MMSubmenus.MenuUser_Details().transform.FindChild("ScrollRect/Viewport/VerticalLayoutGroup/Row3/CellGrid_MM_Content"); }
            public static Transform _Fallback_Dashboard_Group_MM_UI_ProfileRows() { return MMMenus.MMSubmenus.MenuUser_Details().transform.FindChild("ScrollRect/Viewport/VerticalLayoutGroup"); }
        }

        internal static class MENUMENU_FLAGS
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
                DevTools
            }
        }
    }
}