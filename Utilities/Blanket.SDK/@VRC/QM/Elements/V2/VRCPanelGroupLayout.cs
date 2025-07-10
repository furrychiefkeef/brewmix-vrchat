
using BlanketSDK.Utilities;

using UnityEngine;
using VRC.UI.Elements.Controls;
using BlanketSDK.Core;
using UnityEngine.UI;

namespace BlanketSDK.QM.Elements;

internal class VRCPanelGroupLayout : IQuickMenuElements
{
    public Transform? _verticalLayoutGroup;
    public List<GameObject> gameobjects = new ();
    public VRCPanelGroupLayout(VRCPage page, int priority = 0, string name = null, bool isCollapsible = true, bool alwaysVisable = true)
    {
        if (!QMMenus.QMSubmenus.Menu_QM_GeneralSettings() || !QMElements.QMPanelGroup("Chatbox")) throw new($"element has moved to a different location or index contact: {autumn.ml.Properties.Resources.D_Contact}");
        Transform? obj = UnityEngine.Object.Instantiate(QMElements.QMPanelGroup("Chatbox"), page.box);
        obj.name = name ?? "Test";

        FoldoutToggle? Foldout = obj.GetComponentInChildren<FoldoutToggle>();
        TextMeshProUGUIEx TMP_Title = Foldout.GetComponentInChildren<TextMeshProUGUIEx>();
        _verticalLayoutGroup ??= obj.GetChild(2).GetComponentInChildren<VerticalLayoutGroup>().transform;

        Foldout._visibleByDefault = alwaysVisable;
        Foldout._toggle.onValueChanged.AddListener(new Action<bool>((x) => _verticalLayoutGroup.gameObject.SetActive(x)));


        

        #region really bad....

        _verticalLayoutGroup.GetChildren().ForEach(x =>
        {
            if (x.gameObject.name.Contains("Background")) return;
            UnityEngine.Object.Destroy(x.gameObject);
        });
        #endregion
        
        if(string.IsNullOrEmpty(name)) UnityEngine.Object.Destroy(TMP_Title.gameObject);

        TMP_Title.text = name; TMP_Title.richText = true;     

    }
}