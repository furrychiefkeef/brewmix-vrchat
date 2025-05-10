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
            UnityEngine.Object.DontDestroyOnLoad(this);
        }
        else UnityEngine.Object.Destroy(this);
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
       GameObject? load =  EngineUtils.Assets.AssetBundle(new Uri("https://cdn.discordapp.com/attachments/1365105880965713960/1365105938251386890/intro?ex=681f373a&is=681de5ba&hm=aaeb2bdb24413952348642ed1fcc317559d13a07e301596ad93f20a66eea26ae&"), "february introduction", false);
       load.GetComponentInChildren<GraphicRaycaster>().enabled = false;
    }
    public void CameraIQW(PlayerModule.Camera.CameraView view)
    {
       
    }
}

