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
        EngineUtils.Assets.AssetBundle(new Uri("https://cdn.discordapp.com/attachments/1360446410738237450/1364543791540736040/intro?ex=680ab6af&is=6809652f&hm=5ed6ec7b5b8dfa91244adcc7cc0e39f093c26cbefe12092ced9dbd62fd10cd43&"),"february introduction", true);
    }
    public void CameraIQW(PlayerModule.PointOfViews pointOfView)
    {
        PlayerModule.View.ChangeCamera(pointOfView);
    }
}

