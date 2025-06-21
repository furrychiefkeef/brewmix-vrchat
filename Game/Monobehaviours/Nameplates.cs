using autumn.ml.Properties;
using Brewchat.Game.Wrappers;
using UnityEngine.UI;
using VRC.Core;

namespace brewchat.hybridxcore.bep.Game.Monobehaviours;

public class Nameplates : UnityEngine.MonoBehaviour
{
    public Nameplates(IntPtr ptr) : base(ptr) {}
    void Start()
    {
        SetupPlayerInfo();
    }
    public void SetupPlayerInfo()
    {
         var tagsTransform = gameObject.transform.Find("PlayerNameplate/Canvas/NameplateGroup/Nameplate/Contents");
         var getplayercolor = VRCPlayer.Method_Public_Static_Color_APIUser_0(GameUtils.PlayerModel.LocalEntity().APIUser);
         
         tagsTransform.Find("Main/Background").GetComponent<ImageThreeSlice>().color = getplayercolor;
         tagsTransform.Find("Main/Pulse").GetComponent<ImageThreeSlice>().color = getplayercolor;
         var Main_Glow = tagsTransform.Find("Main/Glow").GetComponent<ImageThreeSlice>();
         var Icon_BG = tagsTransform.Find("Icon/Background").GetComponent<Image>();
         tagsTransform.Find("Icon/Pulse").GetComponent<ImageThreeSlice>().color = getplayercolor;
         tagsTransform.Find("Icon/Glow").GetComponent<ImageThreeSlice>().color = getplayercolor;
         tagsTransform.Find("Quick Stats").GetComponent<ImageThreeSlice>().color = getplayercolor;
         
         Icon_BG.color = getplayercolor;
         Main_Glow.color = getplayercolor;
         Icon_BG.overrideSprite = EngineUtils.Assets.INTERNAL_Texture(Resources.capsule_background_bar).ToSprite();
    }
}