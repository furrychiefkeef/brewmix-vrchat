using UnityEngine;
using VRC.UI.Elements;

namespace BlanketSDK.Core;

/// <summary>
/// VRCUI_QUICKMENU ELEMENTS I DID THIS CAUSE I WAS TERRIFIED OF MY CODE STYLE.
/// </summary>
internal class IQuickMenuElements // WHY DID I MAKE THIS: CAUSE MY IDE WAS GIVING ME A FUCKING MENTALBREAK DOWN CAUSE MY CODE STYLE NEEDS TO BE CHANGED.
{
    internal static GameObject QuickMenu() => UnityEngine.Object.FindObjectOfType<QuickMenu>().transform.GetChild(0).gameObject;

    protected static QuickMenu? QM() => UnityEngine.Object.FindObjectOfType<QuickMenu>();
    public static Transform QMParent() => QuickMenu().transform.FindChild("Container/Window/QMParent");

    protected interface QMMenus
    {
        public static Transform WingMenu(string position) => QuickMenu().transform.FindChild($"Container/Window/Wing_{position}/Container/InnerContainer");

        public interface QMSubmenus
        {
            public static Transform Menu_Dashboard() => QMParent().transform.FindChild("Menu_Dashboard");
            public static Transform Menu_Here() => QMParent().transform.FindChild("Menu_Here");
            public static Transform Menu_DevTools() => QMParent().transform.FindChild("Menu_DevTools");
            public static Transform SelectedUser_Local() => QMParent().transform.FindChild("Menu_SelectedUser_Local");
            public static Transform Menu_QM_GeneralSettings() => QMParent().transform.FindChild("Menu_QM_GeneralSettings");
        }
    }

    internal interface QMElements
    {
        [Obsolete("QMButtonContainer is not QMButtonGroup")]
        public static Transform QMButtonGrp() => QMMenus.QMSubmenus.Menu_Dashboard().transform.FindChild("ScrollRect/Viewport/VerticalLayoutGroup");
        public static Transform QMLayoutGroup() => QMMenus.QMSubmenus.Menu_Here().transform.FindChild("ScrollRect/Viewport/VerticalLayoutGroup");
        public static Transform QMSpace() => QMMenus.QMSubmenus.Menu_Here().transform.FindChild("ScrollRect/Viewport/VerticalLayoutGroup/Spacer");
        public static Transform FourButtons_Modal() => QMParent().transform.FindChild("Modal_QM_ChangeShieldLevel");
        public static Transform QMPanelGroup(string MenuName) => QMMenus.QMSubmenus.Menu_QM_GeneralSettings().transform.FindChild($"Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/{MenuName}");
        public static Transform QMPanel() => QMPanelGroup("Sharing").transform.FindChild("QM_Settings_Panel");
        public struct QMPanelElements
        {
            public static Transform QMSeparator() => QMPanelGroup("Sharing").transform.FindChild("VerticalLayoutGroup/Separator");
        }
        /*public static Transform WingButtonContainer(string position) => WingMenu(position).transform.FindChild("WingMenu/ScrollRect/Viewport/VerticalLayoutGroup");*/

        public interface SelectedUser
        {
            public static Transform SelectedUser_ButtonContainer() => QMMenus.QMSubmenus.SelectedUser_Local().FindChild("ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UserActions");
        }
    }
    internal interface QMControls
    {
        public static Transform Tab() => QuickMenu().transform.FindChild("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_Settings");
        public static Transform OperatorRadialSelection() => QMElements.QMPanelGroup("Sharing").transform.FindChild("QM_Settings_Panel/VerticalLayoutGroup/DirectSharingVisibility");
        public static Transform OperatorToggle() => QMElements.QMPanelGroup("Debug").transform.FindChild("QM_Settings_Panel/VerticalLayoutGroup/PinFPS&Ping");
        public static Transform OperatorButton() => QMElements.QMPanelGroup("Debug").transform.FindChild("QM_Settings_Panel/VerticalLayoutGroup/DebugButtons");
        public interface Buttons
        {
            
            public static Transform SingleButton() => QMMenus.QMSubmenus.Menu_Dashboard().transform.FindChild("ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_Respawn");

            public static Transform ExpandButton() => QMMenus.QMSubmenus.Menu_Dashboard().transform.FindChild("Header_H1/RightItemContainer/Button_QM_Expand");

            public static Transform ToggleButton() => QMMenus.QMSubmenus.Menu_DevTools().transform.FindChild("Scrollrect/Viewport/VerticalLayoutGroup/Buttons/Button_Tag");

            public static Transform CarouselButton() => QMMenus.QMSubmenus.SelectedUser_Local().transform.FindChild("ScrollRect/Viewport/VerticalLayoutGroup/Buttons_AvatarActions");
            public static Transform WingButton(string position) => QMMenus.WingMenu(position).transform.FindChild("WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Explore");
        }
    }
    internal interface QMFallbacks
    {
        public static Transform _Fallback_Dashboard_Group_QM_UI_Buttons_TAB() => QuickMenu().transform.FindChild("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup");

        public static Transform _Fallback_Dashboard_Group_QM_UI_ExpandButtons() => QMMenus.QMSubmenus.Menu_Dashboard().transform.FindChild("Header_H1/RightItemContainer");

        public static Transform _Fallback_Dashboard_Group_QM_UI_Buttons() => QMMenus.QMSubmenus.Menu_Dashboard().transform.FindChild("ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions");
        public static Transform _Fallback_Dashboard_Group_QM_UI_WingButtons(string position) => QMMenus.WingMenu(position).transform.FindChild("WingMenu/ScrollRect/Viewport/VerticalLayoutGroup");
        public static Transform _Fallback_Dashboard_Group_QM_UI_ChoiceSelection_Element() => QMMenus.QMSubmenus.Menu_QM_GeneralSettings();
    }

    internal interface QUICKMENU_FLAGS
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


class W_I_P
{
    /*public static Transform Menu_Notifcations() => QMParent().transform.FindChild("Menu_Notifications");
    public static Transform Menu_Camera() => QMParent().transform.FindChild("Menu_Camera");
    public static Transform Menu_Here() => QMParent().transform.FindChild("Menu_Here");
    public static Transform Menu_ReportHere() => QMParent().transform.FindChild("Menu_QM_ReportHere");
    public static Transform Menu_Settings() => QMParent().transform.FindChild("Menu_QM_GeneralSettings");*/
}