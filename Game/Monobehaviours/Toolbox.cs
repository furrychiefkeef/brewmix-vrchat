using Brewchat.Cheat.Modules;
using Brewchat.Game.Wrappers;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

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
        EngineUtils.Assets.AssetBundle(new Uri("https://github.com/riotlu/fake-cdn/blob/master/loading?raw=true"),"welcome_canvas", true);
    }
    public void CameraIQW(PlayerModule.PointOfViews pointOfView)
    {
        PlayerModule.View.ChangeCamera(pointOfView);
    }
}

