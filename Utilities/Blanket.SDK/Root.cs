using UnityEngine;
using UnityEngine.XR;
using VRC.UI.Elements;

namespace BlanketSDK.Utilities;

internal class Root
{
    #region USER_INTERFACE
    protected static GameObject USERINTERFACE() => UnityEngine.Object.FindObjectOfType<UserInterface>().gameObject;
    protected static GameObject SCREENSPACE_OVERLAY_UNSCALEDUI() => GameObject.Find("UnscaledUI");
    
    protected static GameObject QuickMenu() => UnityEngine.Object.FindObjectOfType<QuickMenu>().transform.GetChild(0).gameObject;
    protected static GameObject MainMenu() => UnityEngine.Object.FindObjectOfType<MainMenu>().transform.GetChild(0).gameObject;
    #endregion
    
    #region QM
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
    
    
    #region MM
    public static Transform MMParent() => MainMenu().transform.FindChild("Container/Window/MMParent");
    /*public static Transform MenuDashboard() => QuickMenu().transform.FindChild("Container/Window/QMParent/Menu_Dashboard");
    public static Transform MenuNotifcations() => QuickMenu().transform.FindChild("Container/Window/QMParent/Menu_Notifications");
    public static Transform MenuCamera() => QuickMenu().transform.FindChild("Container/Window/QMParent/Menu_Camera");
    public static Transform MenuHere() => QuickMenu().transform.FindChild("Container/Window/QMParent/Menu_Here");
    public static Transform MenuReportHere() => QuickMenu().transform.FindChild("Container/Window/QMParent/Menu_QM_ReportHere");
    public static Transform MenuSettings() => QuickMenu().transform.FindChild("Container/Window/QMParent/Menu_QM_GeneralSettings");
    public static Transform MenuDevTools() => QuickMenu().transform.FindChild("Container/Window/QMParent/Menu_DevTools");
    public static Transform SelectedUser_Local() => QuickMenu().transform.FindChild("Container/Window/QMParent/Menu_SelectedUser_Local");*/
    #endregion
    public static Transform ActionMenu(string position)
    {
        return SCREENSPACE_OVERLAY_UNSCALEDUI().transform.FindChild($"ActionMenu/Container/{position}/ActionMenu");
        /*switch (XRDevice.isPresent)
        {
            case true:
                
            case false:
                return GameObject.Find($"ActionMenu/Container/{position}/ActionMenu").transform;
        }*/
    }
    
    #region QM_Buttons
    public static Transform Button() => MenuDashboard().transform.FindChild("ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_Respawn");
    public static Transform Toggle() => MenuDevTools().transform.FindChild("Scrollrect/Viewport/VerticalLayoutGroup/Buttons/Button_Tag");
    public static Transform CarouselButton() => SelectedUser_Local().transform.FindChild("ScrollRect/Viewport/VerticalLayoutGroup/Buttons_AvatarActions");

    public static Transform FourButtons_Modal() => QMParent().transform.FindChild("Modal_QM_ChangeShieldLevel");
    public static Transform Slider() => QMParent().transform.FindChild("QM");

   
    #endregion
    #region Fallback
    public static Transform _Fallback_Dashboard_Group_QM_Buttons() => MenuDashboard().transform.FindChild("ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions");
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
