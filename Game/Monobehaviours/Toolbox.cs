using Brewchat.Cheat.Modules;
using Brewchat.Game.Wrappers;
using Transmtn.DTO.Notifications;
using UnityEngine;
using UnityEngine.UI;
using VRC.Core;
using VRC.Localization;
using Resources = brewchat.hybridxcore.bep.Properties.Resources;

namespace Brewchat.Game.Monobehaviours;

internal class Toolbox : UnityEngine.MonoBehaviour
{
    public static Toolbox? Instance { get; private set; }
    private void Awake()
    {
        if (Instance is null)
        {
            Instance = this; DontDestroyOnLoad(this);
        }
        else Destroy(this);
    }
    
    public void PopUpLoading()
    {
        GameUtils.UI.HUD.LoadingPopUp("","","",null,null);
    }
    public void PopUp2Loading()
    {
        GameUtils.UI.HUD.LoadingPopUp2("", "", "", null, null);
    }
    public static void LoadMyUI()
    {
       GameObject? load = EngineUtils.Assets.AssetBundle(new Uri("https://cdn.discordapp.com/attachments/1365105880965713960/1365105938251386890/intro?ex=6832fdba&is=6831ac3a&hm=9d35ad83670e7d0ab3f01d835e16d77d3016b3f53028ca40125667940c960522&"), "february introduction", false);
       load.GetComponentInChildren<GraphicRaycaster>().enabled = false;
    }
    public void LoadMyUI_Console()
    {
        GameObject? load = EngineUtils.Assets.INTERNAL_AssetBundle(Resources.assetbundle_console, "Canvas", true);
        
       
        load.name = "Canvas_Console";
        load.transform.FindChild("Scroll View").transform.FindChild("Viewport")
            .GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "";
    }
    
    
}

