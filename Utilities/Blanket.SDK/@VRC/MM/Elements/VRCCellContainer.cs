using System.Runtime.InteropServices;
using BlanketSDK.Utilities;
using BlanketSDK.Core;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Element;
using VRC.UI.Elements.Controls;

namespace BlanketSDK.MM.Elements;

internal class VRCCellContainer : IMainMenuElements
{
    public GameObject Object;
    public Transform? _panelContainer;

    public VRCCellContainer(VRCPage page, string name, [Optional] int priority)
    {
        if (!MMMenus.MMSubmenus.Menu_Setting() || !MMElements.CellContainer()) throw new($"element has moved to a different location or index contact: {brewchat.hybridxcore.bep.Properties.Resources.D_Contact}");
        Transform? obj = UnityEngine.Object.Instantiate(MMElements.CellContainer(), page._content_container);
        Object = obj.gameObject;
        obj.name = $"Cell_{name}";
        obj.gameObject.SetActive(false);
        obj.SetSiblingIndex(priority);


        #region REQUIRED_COMPONENTS

        var lod = obj.GetComponent<MonoBehaviour2PublicOb_r_cOb_o_s_aOb_l_tUnique>();

        #endregion


        #region REQUIRED_LOGIC

        Transform? Foldout = obj.transform.GetChild(0).GetChild(1);
        var Label = Foldout.GetComponentInChildren<TextMeshProUGUIEx>();

        Label.text = name;
        Label.richText = true;
        
        Transform? panel = obj.transform.GetChild(0).GetChild(2).GetComponentInChildren<VerticalLayoutGroup>().transform;

        panel.DestroyChildren(child => child.GetSiblingIndex() != 0);
        _panelContainer = panel;
        
        #endregion
    }
}
      