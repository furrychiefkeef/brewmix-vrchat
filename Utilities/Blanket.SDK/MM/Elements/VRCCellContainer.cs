using BlanketSDK.Utilities;
using HarmonyLib;
using UnityEngine;
using VRC.UI.Element;

namespace BlanketSDK.MM.Elements;

internal class VRCCellContainer : Templates
{
    public VRCCellContainer(VRCPage page, string name, int priority)
    {
        if (!_MM.MenuSetting() || !_MM.VRCCellContainer()) throw new($"element has moved to a different location or index contact: {brewchat.hybridxcore.bep.Properties.Resources.D_Contact}");
        Transform? obj = UnityEngine.Object.Instantiate(_MM.VRCCellContainer(), page._content_container);
        obj.name = $"{Guid.NewGuid().ToString()}-<Blanket.SDK.MM_CELL_CONTAINER";
        obj.gameObject.SetActive(false);
        if(priority != null) obj.SetSiblingIndex(priority);

       
        #region REQUIRED_COMPONENTS

        var lod =  obj.GetComponent<MonoBehaviour2PublicOb_r_cOb_o_s_aOb_l_tUnique>();

        #endregion

        List<SettingsCategory> List =  page.page._settingCategories.ToList();
        List.Add(lod);
        page.page._settingCategories = List.ToArray();


    }
}