using UnityEngine;
using autumn.ml.Properties;
using Brewchat.Game.Wrappers;
using UnityEngine.UI;
using VRC.Core;

namespace brewchat.hybridxcore.bep.Game.Monobehaviours;

public class Nameplates : MonoBehaviour
{
    public Nameplates(IntPtr ptr) : base(ptr) {}
    public UnityEngine.Color Main { get; set; } = Entry.config.Appearance.inGame.VRCUI._lastSelectedColor;
    public Transform Background { get; set; }
    void Start()
    {
        Load();
    }
    public void Load()
    {
         var tagsTransform = gameObject.transform.Find("PlayerNameplate/Canvas/NameplateGroup/Nameplate/Contents");
         
         tagsTransform.Find("Main/Pulse").GetComponent<ImageThreeSlice>().canvasRenderer.SetColor(Main);
         var Main_Glow = tagsTransform.Find("Main/Glow").GetComponent<ImageThreeSlice>();

         var Icon_BG = tagsTransform.Find("Icon/Background").GetComponent<Image>();
         tagsTransform.Find("Icon/Pulse").GetComponent<ImageThreeSlice>().canvasRenderer.SetColor(Main);
         tagsTransform.Find("Icon/Glow").GetComponent<ImageThreeSlice>().canvasRenderer.SetColor(Main);
         tagsTransform.Find("Quick Stats").GetComponent<ImageThreeSlice>().canvasRenderer.SetColor(Main);
         
         Icon_BG.canvasRenderer.SetColor(Main);;
         Main_Glow.canvasRenderer.SetColor(Main);
         Icon_BG.overrideSprite = EngineUtils.Assets.INTERNAL_Texture(autumn.ml.Properties.Resources.capsule_background_bar).ToSprite();


         var Main_Background = UnityEngine.Object.Instantiate(tagsTransform.Find("Main/Background"), tagsTransform.Find("Main").transform);
         Background = Main_Background.transform;
         Main_Background.name = "Custom_Background";
         Main_Background.SetSiblingIndex(2);
         var backgroundComp=  Main_Background.GetComponent<ImageThreeSlice>();
        
        if (Main_Background != null)
        {
            backgroundComp.canvasRenderer.SetColor(Main);
            backgroundComp.color = UnityEngine.Color.white;
            tagsTransform.Find("Main/Background").gameObject.SetActive(false);
        }  
        
    }
}