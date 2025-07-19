using UnityEngine;
using autumn.ml.Properties;
using Brewchat.Game.Wrappers;
using UnityEngine.UI;
using VRC.Core;

namespace brewchat.hybridxcore.bep.Game.Monobehaviours;

public class Nameplates : MonoBehaviour
{
    public class NameplateDataManager : MonoBehaviour
    {
        public struct Containers
        {
            
        }
        public struct Colors
        {
            public static Color PulseColor;
            public static Dictionary<string, Color> Background = new();
        } 
    }
    public Nameplates(IntPtr ptr) : base(ptr) { }
    public UnityEngine.Color Main { get; set; }
    public Transform Background { get; set; }
    public PlayerNameplate Nameplate;
    public VRC.Player player;
    void Start()
    {
        player = gameObject.GetComponent<VRC.Player>();
        Nameplate = player._vrcplayer.Nameplate;
        NameplateApplyColoring();
    }
    public void NameplateApplyColoring()
    {
        Main = player.GetTrustColor();
        Transform NameplateContents = Nameplate.contents.transform;

        #region Main_Container
        Transform Main_Container = NameplateContents.transform.Find("Main");

        Transform? Main_Background = UnityEngine.Object.Instantiate(Main_Container.transform.Find("Background"), NameplateContents.transform.Find("Main").transform);
        Background = Main_Background.transform;

        ImageThreeSlice Main_Pulse = Main_Container.transform.Find("Pulse").GetComponent<ImageThreeSlice>();
        Main_Pulse.canvasRenderer.SetColor(Main);

        ImageThreeSlice Main_Glow = Main_Container.transform.Find("Glow").GetComponent<ImageThreeSlice>();
        Main_Glow.canvasRenderer.SetColor(Main);

        Main_Background.name = "Custom_Background";
        Main_Background.SetSiblingIndex(2);
        var backgroundComp = Main_Background.GetComponent<ImageThreeSlice>();

        if (Main_Background != null)
        {
            backgroundComp.canvasRenderer.SetColor(Main);
            Main_Container.transform.Find("Background").gameObject.SetActive(false);
        }

        #endregion;



        #region Icon_Container 

        Transform Icon_Container = NameplateContents.transform.Find("Icon");
        ImageThreeSlice Icon_Pulse = Icon_Container.transform.Find("Pulse").GetComponent<ImageThreeSlice>();
        ImageThreeSlice Icon_Glow = Icon_Container.transform.Find("Glow").GetComponent<ImageThreeSlice>();
        Image Icon_BG = Icon_Container.transform.Find("Background").GetComponent<Image>();
        Icon_BG.canvasRenderer.SetColor(Main);


        #endregion

        #region  QuickStats_Container

        Transform? QuickStats_Container = NameplateContents.transform.Find("Quick Stats");
        QuickStats_Container.GetComponent<ImageThreeSlice>().canvasRenderer.SetColor(Main);

        #endregion
        
        #region Misc
        //  Icon_BG.overrideSprite = EngineUtils.Assets.INTERNAL_Texture(autumn.ml.Properties.Resources.capsule_background_bar).ToSprite();

        #endregion




    }
}