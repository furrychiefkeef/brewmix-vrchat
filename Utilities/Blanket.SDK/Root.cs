using UnityEngine;
using UnityEngine.XR;
using VRC.UI.Elements;

namespace BlanketSDK.Utilities;

internal class Root
{

    #region Advanced

    public static GameObject[] AdvancedMenus =
    {
        UnityEngine.Object.FindObjectOfType<QuickMenu>().transform.GetChild(0).gameObject,
        UnityEngine.Object.FindObjectOfType<MainMenu>().transform.GetChild(0).gameObject
    };
    

    #endregion
    #region USER_INTERFACE
    protected static GameObject USERINTERFACE() => UnityEngine.Object.FindObjectOfType<UserInterface>().gameObject;
    protected static GameObject SCREENSPACE_OVERLAY_UNSCALEDUI() => GameObject.Find("UnscaledUI");
    
    protected static GameObject QuickMenu() => UnityEngine.Object.FindObjectOfType<QuickMenu>().transform.GetChild(0).gameObject;
    protected internal static GameObject MainMenu() => UnityEngine.Object.FindObjectOfType<MainMenu>().transform.GetChild(0).gameObject;
    #endregion
    
    #region QUICK+MENU
    public static Transform QMParent() => QuickMenu().transform.FindChild("Container/Window/QMParent");
    public static Transform MenuDashboard() => QuickMenu().transform.FindChild("Container/Window/QMParent/Menu_Dashboard");
    public static Transform MenuNotifcations() => QuickMenu().transform.FindChild("Container/Window/QMParent/Menu_Notifications");
    public static Transform MenuCamera() => QuickMenu().transform.FindChild("Container/Window/QMParent/Menu_Camera");
    public static Transform MenuHere() => QuickMenu().transform.FindChild("Container/Window/QMParent/Menu_Here");
    public static Transform MenuReportHere() => QuickMenu().transform.FindChild("Container/Window/QMParent/Menu_QM_ReportHere");
    public static Transform MenuSettings() => QuickMenu().transform.FindChild("Container/Window/QMParent/Menu_QM_GeneralSettings");
    public static Transform MenuDevTools() => QuickMenu().transform.FindChild("Container/Window/QMParent/Menu_DevTools");
    public static Transform SelectedUser_Local() => QuickMenu().transform.FindChild("Container/Window/QMParent/Menu_SelectedUser_Local");
    
    #endregion
    
    #region MAIN+MENU
    public static Transform MMParent() => MainMenu().transform.FindChild("MMParent");
    /*public static Transform MenuDashboard() => QuickMenu().transform.FindChild("Container/Window/QMParent/Menu_Dashboard");
    public static Transform MenuNotifcations() => QuickMenu().transform.FindChild("Container/Window/QMParent/Menu_Notifications");
    public static Transform MenuCamera() => QuickMenu().transform.FindChild("Container/Window/QMParent/Menu_Camera");
    public static Transform MenuHere() => QuickMenu().transform.FindChild("Container/Window/QMParent/Menu_Here");
    public static Transform MenuReportHere() => QuickMenu().transform.FindChild("Container/Window/QMParent/Menu_QM_ReportHere");
    public static Transform MenuSettings() => QuickMenu().transform.FindChild("Container/Window/QMParent/Menu_QM_GeneralSettings");
    public static Transform MenuDevTools() => QuickMenu().transform.FindChild("Container/Window/QMParent/Menu_DevTools");
    public static Transform SelectedUser_Local() => QuickMenu().transform.FindChild("Container/Window/QMParent/Menu_SelectedUser_Local");*/
    #endregion

    #region UNSCALED+MENU
    public static Transform ActionMenu(string position)
    {
        return SCREENSPACE_OVERLAY_UNSCALEDUI().transform.FindChild($"ActionMenu/Container/{position}/ActionMenu");
        /*switch (!XRDevice.isPresent)
        {
            case true:
                
            case false:
                return GameObject.Find($"ActionMenu/Container/{position}/ActionMenu").transform;
        }*/
    }
    #endregion
    
    #region QUICK+MENU-Buttons
    public static Transform SingleButton() => MenuDashboard().transform.FindChild("ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_Respawn");
    public static Transform ExpandButton() => MenuDashboard().transform.FindChild("Header_H1/RightItemContainer/Button_QM_Expand");
    public static Transform ToggleButton() => MenuDevTools().transform.FindChild("Scrollrect/Viewport/VerticalLayoutGroup/Buttons/Button_Tag");
    
    public static Transform CarouselButton() => SelectedUser_Local().transform.FindChild("ScrollRect/Viewport/VerticalLayoutGroup/Buttons_AvatarActions");

    public static Transform FourButtons_Modal() => QMParent().transform.FindChild("Modal_QM_ChangeShieldLevel");
    public static Transform Slider() => QMParent().transform.FindChild("QM");


   
    #endregion
    
    #region FALLBACK
    public static Transform _Fallback_Dashboard_Group_QM_UI_Buttons() => MenuDashboard().transform.FindChild("ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions");
    protected static Transform _Fallback_Dashboard_Group_QM_UI_ExpandButtons() => MenuDashboard().transform.FindChild("Header_H1/RightItemContainer");
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
