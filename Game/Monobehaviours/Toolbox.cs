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
            Instance = this;
            DontDestroyOnLoad(this);
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
       GameObject? load = EngineUtils.Assets.AssetBundle(new Uri("https://cdn.discordapp.com/attachments/1365105880965713960/1365105938251386890/intro?ex=68232bba&is=6821da3a&hm=6e8b77a1f2fba56d6a476f260204a060ec26a1c8a216af7f9582d50d9bb31abf&"), "february introduction", false);
       load.GetComponentInChildren<GraphicRaycaster>().enabled = false;
    }
}

