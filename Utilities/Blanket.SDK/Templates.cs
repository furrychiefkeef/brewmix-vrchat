using UnityEngine;
using VRC.UI.Elements;

namespace BlanketSDK.Utilities;

internal class Templates
{

    public static GameObject[] AdvancedMenus =
    {
        UnityEngine.Object.FindObjectOfType<QuickMenu>().transform.GetChild(0).gameObject,
        UnityEngine.Object.FindObjectOfType<MainMenu>().transform.GetChild(0).gameObject
    };

    protected static GameObject USERINTERFACE() => UnityEngine.Object.FindObjectOfType<UserInterface>().gameObject;

    protected static Transform ORIGIN_BUTTON() => GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/GoButton").transform;
    protected static GameObject ORIGIN_BUTTON_PARENT() =>
        GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress");
    protected static GameObject SCREENSPACE_OVERLAY_UNSCALEDUI() => GameObject.Find("UnscaledUI");
   
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
