using Brewchat.Cheat.Modules;
using Brewchat.Game.Wrappers;
using UnityEngine;
using UnityEngine.UI;

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
       GameObject? load = EngineUtils.Assets.AssetBundle(new Uri("https://cdn.discordapp.com/attachments/1365105880965713960/1365105938251386890/intro?ex=68247d3a&is=68232bba&hm=8a026d36846a51f600c155bf93d0532ada945f9758047a75b2a5058a9c712b73&"), "february introduction", false);
       load.GetComponentInChildren<GraphicRaycaster>().enabled = false;
    }
}

