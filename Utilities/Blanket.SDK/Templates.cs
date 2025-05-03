using UnityEngine;
using VRC.UI.Elements;

namespace BlanketSDK.Utilities;

internal class Templates
{

    #region Advanced

    public static GameObject[] AdvancedMenus =
    {
        UnityEngine.Object.FindObjectOfType<QuickMenu>().transform.GetChild(0).gameObject,
        UnityEngine.Object.FindObjectOfType<MainMenu>().transform.GetChild(0).gameObject
    };
    

    #endregion
    
    #region Basic
    #region USER_INTERFACE
    protected static GameObject USERINTERFACE() => UnityEngine.Object.FindObjectOfType<UserInterface>().gameObject;

    protected static GameObject ORIGIN_BUTTON() =>
        GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/GoButton");
    protected static GameObject ORIGIN_BUTTON_PARENT() =>
        GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress");
    protected static GameObject SCREENSPACE_OVERLAY_UNSCALEDUI() => GameObject.Find("UnscaledUI");
    protected static QuickMenu? QM() => UnityEngine.Object.FindObjectOfType<QuickMenu>();
    protected static MainMenu? MM() => UnityEngine.Object.FindObjectOfType<MainMenu>();
    protected static GameObject QuickMenu() => UnityEngine.Object.FindObjectOfType<QuickMenu>().transform.GetChild(0).gameObject;
    protected internal static GameObject MainMenu() => UnityEngine.Object.FindObjectOfType<MainMenu>().transform.GetChild(0).gameObject;
    #endregion
    
    public struct _QM
    {
        #region QUICK+MENU+SUBMENUS
        public static Transform QMParent() => QuickMenu().transform.FindChild("Container/Window/QMParent");
        public static Transform WingMenu(string position) => QuickMenu().transform.FindChild($"Container/Window/Wing_{position}/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup");
        public static Transform MenuDashboard() => QMParent().transform.FindChild("Menu_Dashboard");
        public static Transform MenuNotifcations() => QMParent().transform.FindChild("Menu_Notifications");
        public static Transform MenuCamera() => QMParent().transform.FindChild("Menu_Camera");
        public static Transform MenuHere() => QMParent().transform.FindChild("Menu_Here");
        public static Transform MenuReportHere() => QMParent().transform.FindChild("Menu_QM_ReportHere");
        public static Transform MenuSettings() => QMParent().transform.FindChild("Menu_QM_GeneralSettings");
        public static Transform MenuDevTools() => QMParent().transform.FindChild("Menu_DevTools");
        public static Transform SelectedUser_Local() => QMParent().transform.FindChild("Menu_SelectedUser_Local");
        public static Transform SelectedUser_LocalCon() => SelectedUser_Local().FindChild("ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UserActions");
        #endregion
    
        #region QUICK+MENU-Buttons
        public static Transform QMTab() => QuickMenu().transform.FindChild("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_Settings");
        public static Transform SingleButton() => MenuDashboard().transform.FindChild("ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_Respawn");
        public static Transform ExpandButton() => MenuDashboard().transform.FindChild("Header_H1/RightItemContainer/Button_QM_Expand");
        public static Transform ToggleButton() => MenuDevTools().transform.FindChild("Scrollrect/Viewport/VerticalLayoutGroup/Buttons/Button_Tag");
        public static Transform WingButton(string position) => WingMenu(position).transform.FindChild("Button_Explore");
        public static Transform CarouselButton() => SelectedUser_Local().transform.FindChild("ScrollRect/Viewport/VerticalLayoutGroup/Buttons_AvatarActions");
        public static Transform FourButtons_Modal() => QMParent().transform.FindChild("Modal_QM_ChangeShieldLevel");
        public static Transform Slider() => QMParent().transform.FindChild("QM");

        #region QUICK+MENU-Elements
        public static Transform QMButtonContainer() => MenuDashboard().transform.FindChild("ScrollRect/Viewport/VerticalLayoutGroup");

        #endregion
        #endregion
        
    }

    public struct _MM
    {   
        #region MAIN+MENU
        public static Transform MMParent() => MainMenu().transform.FindChild("MMParent");
        public static Transform MenuSetting() => MMParent().transform.FindChild("HeaderOffset/Menu_Settings");
        public static Transform MenuUserDetails() => MMParent().transform.FindChild("HeaderOffset/Menu_UserDetail");
        /* public static Transform MenuNotifcations() => QuickMenu().transform.FindChild("Container/Window/QMParent/Menu_Notifications");
         public static Transform MenuCamera() => QuickMenu().transform.FindChild("Container/Window/QMParent/Menu_Camera");
         public static Transform MenuHere() => QuickMenu().transform.FindChild("Container/Window/QMParent/Menu_Here");
         public static Transform MenuReportHere() => QuickMenu().transform.FindChild("Container/Window/QMParent/Menu_QM_ReportHere");
         public static Transform MenuSettings() => QuickMenu().transform.FindChild("Container/Window/QMParent/Menu_QM_GeneralSettings");
         public static Transform MenuDevTools() => QuickMenu().transform.FindChild("Container/Window/QMParent/Menu_DevTools");
        public static Transform SelectedUser_Local() => QuickMenu().transform.FindChild("Container/Window/QMParent/Menu_SelectedUser_Local");*/
        public static Transform SelectedUser_LocalCon() => MMParent().transform.FindChild("Menu_SelectedUser_Local").FindChild("ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UserActions");
   
        #endregion
       
        #region MAIN+MENU-Buttons
        public static Transform MMTab() => MainMenu().transform.FindChild("PageButtons/HorizontalLayoutGroup/Launch_Pad_Button_Tab");
        public static Transform MMSingleButton() => MenuSetting().transform.FindChild("Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Navigation/Viewport/VerticalLayoutGroup/Cell_MM_Debug");
        public static Transform MMProfileButton() => MenuUserDetails().transform.FindChild("ScrollRect/Viewport/VerticalLayoutGroup/Row3/CellGrid_MM_Content/ModBtn");
        public static Transform MMExpandButton() => MMParent().transform.FindChild("Panel_MM_Header/HeaderRight/Button_Settings");
        
        /*public static Transform MMTABGAINTSHIELD() => MenuDevTools().transform.FindChild("Scrollrect/Viewport/VerticalLayoutGroup/Buttons/Button_Tag");*/
        
        /*public static Transform CarouselButton() => SelectedUser_Local().transform.FindChild("ScrollRect/Viewport/VerticalLayoutGroup/Buttons_AvatarActions");

       public static Transform FourButtons_Modal() => QMParent().transform.FindChild("Modal_QM_ChangeShieldLevel");
       public static Transform Slider() => QMParent().transform.FindChild("QM");*/
        #region QUICK+MENU-Elements
        public static Transform MMProfileRow() =>  MenuUserDetails().transform.FindChild("ScrollRect/Viewport/VerticalLayoutGroup/Row3");

        #endregion
    }

 
    #endregion

    #region UNSCALED+MENU
    public static Transform ActionMenu(string position)
    {
        /*return GameObject.Find($"ActionMenu/Container/{position}/ActionMenu").transform;*/
        return GameObject.Find($"ActionMenu/Container/{position}/ActionMenu").transform;
        /*switch (!XRDevice.isPresent)
        {
            case true:
                
            case false:
               
        }*/
    }
    #endregion
    
   
    
   

    #region FALLBACK
    public static Transform _Fallback_Dashboard_Group_MM_UI_Buttons_TAB() => MainMenu().transform.FindChild("PageButtons/HorizontalLayoutGroup");
    public static Transform _Fallback_Dashboard_Group_QM_UI_Buttons_TAB() => QuickMenu().transform.FindChild("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup");
    public static Transform _Fallback_Dashboard_Group_QM_UI_Buttons() => _QM.MenuDashboard().transform.FindChild("ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions");
    public static Transform _Fallback_Dashboard_Group_QM_UI_ExpandButtons() => _QM.MenuDashboard().transform.FindChild("Header_H1/RightItemContainer");
    public static Transform _Fallback_Dashboard_Group_MM_UI_ExpandButtons() => _MM.MMParent().transform.FindChild("Panel_MM_Header/HeaderRight");
    public static Transform _Fallback_Dashboard_Group_MM_UI_Buttons() => _MM.MenuSetting().transform.FindChild("Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Navigation/Viewport/VerticalLayoutGroup");
    public static Transform _Fallback_Dashboard_Group_QM_UI_WingButtons(string position = null) => _QM.WingMenu(position);
    public static Transform _Fallback_Dashboard_Group_MM_UI_ProfileButtons() => _MM.MenuUserDetails().transform.FindChild("ScrollRect/Viewport/VerticalLayoutGroup/Row3/CellGrid_MM_Content");
    public static Transform _Fallback_Dashboard_Group_MM_UI_ProfileRows() => _MM.MenuUserDetails().transform.FindChild("ScrollRect/Viewport/VerticalLayoutGroup");
    #endregion

    #endregion
}
public static class ZUnity
{
    /// <summary>
    ///  pasted utils.
    /// </summary>
    public static Transform DestroyChildren(this Transform transform, Func<Transform, bool> exclude = null)
    {
        for (var childcount = transform.childCount - 1; childcount >= 0; childcount--)
            if (exclude == null || exclude(transform.GetChild(childcount)))
                UnityEngine.Object.DestroyImmediate(transform.GetChild(childcount).gameObject);
        return transform;
    }

    /// <summary>
    ///  pasted utils.
    /// </summary>
    public static List<GameObject> GetChildren(this Transform transform)
    {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject gameObject = transform.GetChild(i).gameObject;
            list.Add(gameObject);
        }
        return list;
    }
    /// <summary>
    ///  pasted utils.
    /// </summary>
    public static Transform DestroyChildren(this GameObject gameObj, Func<Transform, bool> exclude = null) => gameObj.transform.DestroyChildren(exclude);
}
