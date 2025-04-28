using Brewchat.Cheat.Modules;
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
        GameUtils.UI.HUD.LoadingPopUp("","","",null,null);
    }
    public void PopUp2Loading()
    {
        GameUtils.UI.HUD.LoadingPopUp2("", "", "", null, null);
    }
    public static void LoadMyUI()
    {
        EngineUtils.Assets.AssetBundle(new Uri("https://cdn.discordapp.com/attachments/1365105880965713960/1365105938251386890/intro?ex=680ebc7a&is=680d6afa&hm=4b457a778bb60e370cb3f8efcc13af01c5b812b02f175ab2a055b03ed3d9f8e4&"),
            "february introduction", false);
    }

    public static void MusicPlayer()
    {
        GameObject au = new("Audio_Player");
        var src = au.AddComponent<AudioSource>();
        src.clip = EngineUtils.Assets.AudioClip(new Uri(""));
        src.hideFlags += 32;
        src.loop = true;
        src.volume = 1f;
        src.bypassListenerEffects = true;
        src.bypassReverbZones = true;
        src.Play();
      
        DontDestroyOnLoad(au);
    }
    public void CameraIQW(PlayerModule.Camera.P0V pov)
    {
        PlayerModule.Camera.View.ChangeCamera(pov);
    }
}

