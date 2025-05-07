using BlanketSDK.Utilities;
using BlanketSDK.Utilities.Unbearable;
using Brewchat;
using Syrup.IMGUI;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI;
using VRC.UI.Elements;
using VRC.UI.Elements.Controls;
using VRC.UI.Elements.Menus;

namespace BlanketSDK.MM.Elements;

internal class VRCPage : IMainMenuElements
{
    public SettingsPage page;
    public string Name;
    public Transform? navigation_buttons;
    public Transform? _content_container;
    public VRCPage(string name, Sprite? sprite)
    {
        if (!MMParent() || !MMMenus.MMSubmenus.Menu_Setting()) throw new($"element has moved to a different location or index contact: {brewchat.hybridxcore.bep.Properties.Resources.D_Contact}");
        Transform? obj = UnityEngine.Object.Instantiate(MMMenus.MMSubmenus.Menu_Setting(), MMParent().GetChild(2));
        obj.gameObject.SetActive(false);
        obj.SetSiblingIndex(5);
        
        Transform? _Panel = obj
            .GetChild(0)
            .GetChild(0)
            .GetChild(2);
        
        Transform? header = _Panel.FindChild("DynamicSidePanel_Header"); //  Index Child = 0
        Transform? nav_container = _Panel.FindChild("ScrollRect_Navigation"); //  Index Child = 1
        Transform? content_container = _Panel.FindChild("ScrollRect_Content"); // Index Child = 2
        
        Name = $"Menu_{name}";
        obj.name = Name;

        #region REQUIRED_COMPONENTS

        SettingsPage? _0 = obj.GetComponent<SettingsPage>();
        obj.GetComponent<UnityEngine.Canvas>().enabled = true;
        obj.GetComponent<UnityEngine.CanvasGroup>().enabled = true;
        obj.GetComponent<UnityEngine.UI.GraphicRaycaster>().enabled = true;
        TextMeshProUGUIEx? _1 = header.GetComponentInChildren<TextMeshProUGUIEx>();
        ImageEx? _2 = header.GetComponentInChildren<ImageEx>();
        
        #endregion

        #region TEXT COMPONENT
        _1.text = name;
        _1.richText = true;

        #endregion

        #region REQUIRED_LOGIC

        page = _0;
        page.enabled = true;
        page.field_Private_Boolean_1 = true;
        page.field_Public_String_0 = Name;
        page.field_Private_List_1_UIPage_0.Add(page);

        if (MM()?.field_Protected_MenuStateController_0 != null &&
            MM()?.field_Protected_MenuStateController_0.field_Public_Il2CppReferenceArray_1_UIPage_0 != null)
        {
            MM()?.field_Protected_MenuStateController_0.field_Private_Dictionary_2_String_UIPage_0.Add(Name, page);

            List<UIPage>?  List = MM()?.field_Protected_MenuStateController_0.field_Public_Il2CppReferenceArray_1_UIPage_0
                .ToList(); List?.Add(page);

            MM().field_Protected_MenuStateController_0.field_Public_Il2CppReferenceArray_1_UIPage_0 = List.ToArray();

            if (Config.Advanced.DebugMode)
            {
                foreach (var VARIABLE in MM().field_Protected_MenuStateController_0.field_Public_Il2CppReferenceArray_1_UIPage_0.ToList()) 
                    sconsole.internal_log($"{VARIABLE.field_Public_String_0}"); sconsole.internal_log($"{Name} was created");
            }
        }

        #endregion
        
        #region _NavBar
        nav_container.GetComponent<VRCScrollRect>().field_Public_Boolean_0 = true;
        VerticalLayoutGroup? _navGroup = nav_container.GetComponentInChildren<VerticalLayoutGroup>();
        _navGroup.transform.DestroyChildren();
        _navGroup.GetComponent<Canvas>().enabled = true;
        _navGroup.GetComponent<GraphicRaycaster>().enabled = true;
        _navGroup.transform.parent.GetComponentInChildren<VRCRectMask2D>().field_Private_Boolean_0 = true;
        navigation_buttons = _navGroup.transform;

        #endregion
        
        #region ContentBox

        content_container.GetComponent<VRCScrollRect>().field_Public_Boolean_0 = true;
        VerticalLayoutGroup? _contentGroup = content_container.GetComponentInChildren<VerticalLayoutGroup>();
        _contentGroup.transform.DestroyChildren();
        /*_contentGroup.GetComponent<Canvas>().enabled = true;
        _contentGroup.GetComponent<GraphicRaycaster>().enabled = true;*/
       
        _contentGroup.transform.parent.GetComponentInChildren<VRCRectMask2D>().field_Private_Boolean_0 = true;
        _content_container = _contentGroup.transform;
        
        #endregion
        
        #region IMAGE || IMAGE_EX COMPONENT
        if (!sprite) return;
        _2.sprite = sprite;
        #endregion
    }
}


