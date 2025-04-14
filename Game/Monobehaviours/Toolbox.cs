using System.Reflection;
using BepInEx;
using BepInEx.Unity.IL2CPP;
using brewchat.hybridxcore.bep.Properties;
using Brewchat.Game.Wrappers;
using UnityEngine;

namespace Brewchat.Game.Monobehaviours;

internal class Toolbox : UnityEngine.MonoBehaviour
{
    public static Toolbox? Instance { get; private set; }
    public static Texture2D T { get; set; }
    private void Awake()
    {
        if (Instance is null)
        {
            Instance = this;
            UnityEngine.Object.DontDestroyOnLoad(this);
        }
        else UnityEngine.Object.Destroy(this);
    }
    void Start()
    {
        T = EngineUtils.Assets.LoadTextureFromModAssets($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\Brew\\radial.png");
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

