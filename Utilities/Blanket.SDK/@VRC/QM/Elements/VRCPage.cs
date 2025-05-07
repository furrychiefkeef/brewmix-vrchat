using BlanketSDK.Core;
using BlanketSDK.Utilities;
using Brewchat;
using Syrup.IMGUI;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements;
using VRC.UI.Elements.Controls;
using VRC.UI.Elements.Menus;

namespace BlanketSDK.QM.Elements;

internal class VRCPage : IQuickMenuElements
{
    private Transform? _obj;
    static UIPage page;
    public string Name;
    public Transform? box;

    public VRCPage(string name)
    {
        if (!QMParent() || !QMMenus.QMSubmenus.Menu_DevTools()) throw new($"element has moved to a different location or index contact: {brewchat.hybridxcore.bep.Properties.Resources.D_Contact}");
        _obj = UnityEngine.Object.Instantiate(QMMenus.QMSubmenus.Menu_DevTools(), QMParent());
        _obj.gameObject.SetActive(false);
        _obj.SetSiblingIndex(5);
        
        Name = $"Menu_{name}";
        _obj.name = Name;

        #region REQUIRED_COMPONENTS

        DevMenu? _0 = _obj.GetComponent<DevMenu>();
        _obj.GetComponent<UnityEngine.Canvas>().enabled = true;
        _obj.GetComponent<UnityEngine.CanvasGroup>().enabled = true;
        _obj.GetComponent<UnityEngine.UI.GraphicRaycaster>().enabled = true;
        TextMeshProUGUIEx? _1 = _obj.GetChild(0).GetChild(1).GetComponentInChildren<TextMeshProUGUIEx>();

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

        if (QM()?.field_Protected_MenuStateController_0 != null &&
            QM()?.field_Protected_MenuStateController_0.field_Public_Il2CppReferenceArray_1_UIPage_0 != null)
        {
            QM()?.field_Protected_MenuStateController_0.field_Private_Dictionary_2_String_UIPage_0.Add(Name, page);

            List<UIPage>?  List = QM()?.field_Protected_MenuStateController_0.field_Public_Il2CppReferenceArray_1_UIPage_0
                .ToList(); List?.Add(page);
            

            QM().field_Protected_MenuStateController_0.field_Public_Il2CppReferenceArray_1_UIPage_0 = List.ToArray();

            if (Config.Advanced.DebugMode)
            {
                foreach (var VARIABLE in QM().field_Protected_MenuStateController_0
                             .field_Public_Il2CppReferenceArray_1_UIPage_0.ToList())
                    sconsole.internal_log($"{VARIABLE.field_Public_String_0}");
                sconsole.internal_log($"{Name} was created");
            }
        }

        #endregion
        
        #region OBJs

        var group = _obj.GetComponentInChildren<VerticalLayoutGroup>();
        group.transform.DestroyChildren();
        box = group.transform;

        #endregion
    }

    public void Open()
    {
        _obj.gameObject.SetActive(true);
        _obj.GetComponent<UnityEngine.Canvas>().enabled = true;
        _obj.GetComponent<UnityEngine.CanvasGroup>().enabled = true;
        _obj.GetComponent<UnityEngine.UI.GraphicRaycaster>().enabled = true;
        page.Method_Protected_Void_Boolean_TransitionType_0(true, UIPage.TransitionType.InPlace);
    }

    void Close()
    {
        
    }
}


