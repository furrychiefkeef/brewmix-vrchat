using Brewchat.Game.Wrappers;
using UnityEngine;

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
        GameUtils.HUD.LoadingPopUp("","","",null,null);
    }
    public void PopUp2Loading()
    {
        GameUtils.HUD.LoadingPopUp2("", "", "", null, null);
    }
}

