using UnityEngine;
using autumn.ml.Properties;
using Brewchat.Game.Wrappers;
using UnityEngine.UI;
using VRC.Core;
using VRC.UI.Elements.Controls;
using MelonLoader;
using VRC.UI;
using System.Net.Http;

namespace brewchat.hybridxcore.bep.Game.Monobehaviours;

public class Nameplates : MonoBehaviour
{
    public enum VoiceGlow
    {

        None,
        Groups_Half,
        Group_Full,
        No_Groups_Full,

    }
    public class NameplateDataManager : MonoBehaviour
    {
        NameplateDataManager(IntPtr ptr) : base(ptr) { }
        public struct Containers
        {

        }
        public struct Colors
        {
            public static Color PulseColor { get; set; }
            public static Dictionary<string, Color> Background = new();
        }
    }
    public Nameplates(IntPtr ptr) : base(ptr) { }
    public UnityEngine.Color Main { get; set; }
    public Sprite? Test { get; set; }
    public Transform Background { get; set; }
    public PlayerNameplate? Nameplate { get; set; }
    public ImageThreeSlice Main_Glow { get; set; }
    public ImageThreeSlice Icon_Glow { get; set; }

    public ChatBubbleDisplay Chatbox { get; set; }
    public NameplateUtils nameplate;

    private static TextMeshProUGUIEx? Nametag { get; set; }
    public VRC.Player Player;
    void Start()
    {
        Player = gameObject.GetComponent<VRC.Player>();
        Main = Player.GetTrustColor();

        Nameplate = Player._vrcplayer.Nameplate;
        Chatbox = Player._vrcplayer.field_Public_ChatBubbleDisplay_0;
        Prettify();
    }
    void Update()
    {
        if (Main_Glow._sprite.name == "capsule_voice_glow_half_groups")
        {
            MelonLogger.Msg("Groups");
            var t = EngineUtils.Assets.INTERNAL_Texture(autumn.ml.Properties.Resources.capsule_voice_glow_half_groups);
            Main_Glow._sprite = Sprite.Create(t, Main_Glow._sprite.rect, Main_Glow._sprite.pivot, Main_Glow._sprite.pixelsPerUnit, 0, SpriteMeshType.FullRect, Main_Glow._sprite.border);
        }
        if (Main_Glow._sprite.name == "capsule_voice_glow_half")
        {
            MelonLogger.Msg("No Groups");
            var A = EngineUtils.Assets.INTERNAL_Texture(autumn.ml.Properties.Resources.capsule_voice_glow_half);
            Main_Glow._sprite = Sprite.Create(A, Main_Glow._sprite.rect, Main_Glow._sprite.pivot, Main_Glow._sprite.pixelsPerUnit, 0, SpriteMeshType.FullRect, Main_Glow._sprite.border);
        }
        if (Main_Glow._sprite.name == "capsule_voice_glow_full_groups")
        {
            MelonLogger.Msg("No Groups");
            var A = EngineUtils.Assets.INTERNAL_Texture(autumn.ml.Properties.Resources.capsule_voice_glow_full_groups);
            Main_Glow._sprite = Sprite.Create(A, Main_Glow._sprite.rect, Main_Glow._sprite.pivot, Main_Glow._sprite.pixelsPerUnit, 0, SpriteMeshType.FullRect, Main_Glow._sprite.border);
        }
        if (Main_Glow._sprite.name == "capsule_voice_glow")
        {
            MelonLogger.Msg("No Groups");
            var A = EngineUtils.Assets.INTERNAL_Texture(autumn.ml.Properties.Resources.capsule_voice_glow);
            Main_Glow._sprite = Sprite.Create(A, Main_Glow._sprite.rect, Main_Glow._sprite.pivot, Main_Glow._sprite.pixelsPerUnit, 0, SpriteMeshType.FullRect, Main_Glow._sprite.border);
        }
    }
    public void Prettify()
    {
        NameplateApplyColoring();
        ChatboxApplyColoring();
    }
    void NameplateApplyColoring()
    {
        nameplate = new NameplateUtils();
        Transform NameplateContents = Nameplate.contents.transform;

        #region Main_Container

        Transform Main_Container = NameplateContents.transform.Find("Main");
        ImageThreeSlice? Main_Background = Main_Container.transform.Find("Background").GetComponent<ImageThreeSlice>();
        Main_Background.canvasRenderer.SetColor(Main);

        ImageThreeSlice? Main_Pulse = Main_Container.transform.Find("Pulse").GetComponent<ImageThreeSlice>();
        Main_Pulse.canvasRenderer.SetColor(Main);

        var Main_Glow_Obj = UnityEngine.Object.Instantiate(Main_Container.transform.Find("Glow"), Main_Container.transform.Find("Glow").parent);
        if (Main_Glow == null)
        {
            Main_Glow = Main_Glow_Obj.GetComponent<ImageThreeSlice>();
            Main_Container.transform.Find("Glow").gameObject.SetActive(false);
        }
        Main_Glow.canvasRenderer.SetColor(Main);


        // if (Main_Glow._sprite.name == "capsule_voice_glow_half")
        // {
        //     Main_Glow._sprite = EngineUtils.Assets.INTERNAL_Texture(autumn.ml.Properties.Resources.capsule_voice_glow_half).ToSprite();
        // }

        var PlayerName = Nameplate.field_Public_TextMeshProUGUIEx_0;
        PlayerName.color = Main;
        PlayerName.richText = true;
        Nametag = PlayerName;

        // Main_Background.name = "Custom_Background";
        // Main_Background.SetSiblingIndex(2);
        // var backgroundComp = Main_Background.GetComponent<ImageThreeSlice>();
        // Transform? Main_Background = UnityEngine.Object.Instantiate(Main_Container.transform.Find("Background"), NameplateContents.transform.Find("Main").transform);
        // Background = Mai n_Background.transform;

        // if (Main_Background != null)
        // {

        //     backgroundComp.canvasRenderer.SetColor(Main);
        //     Main_Container.transform.Find("Background").gameObject.SetActive(false);
        // }

        #endregion;



        #region Icon_Container 

        Transform Icon_Container = NameplateContents.transform.Find("Icon");
        ImageThreeSlice? Icon_Pulse = Icon_Container.transform.Find("Pulse").GetComponent<ImageThreeSlice>();
        Icon_Pulse.canvasRenderer.SetColor(Main);


        var Icon_Glow_Obj = UnityEngine.Object.Instantiate(Icon_Container.transform.Find("Glow"), Icon_Container.transform.Find("Glow").parent);
        if (Icon_Glow == null)
        {
            Icon_Glow = Icon_Glow_Obj.GetComponent<ImageThreeSlice>();
            Icon_Container.transform.Find("Glow").gameObject.SetActive(false);
        }

        Icon_Glow._sprite = EngineUtils.Assets.INTERNAL_Texture(autumn.ml.Properties.Resources.capsule_voice_glow).ToSprite();
        Icon_Glow.canvasRenderer.SetColor(Main);
        Image Icon_BG = Icon_Container.transform.Find("Background").GetComponent<Image>();

        Icon_BG.canvasRenderer.SetColor(Main);

        #endregion

        #region  QuickStats_Container

        Transform? QuickStats_Container = NameplateContents.transform.Find("Quick Stats");
        QuickStats_Container.GetComponent<ImageThreeSlice>().canvasRenderer.SetColor(Main);

        #endregion

        #region Misc
        Transform? StatusLine_Container = NameplateContents.transform.Find("Status Line");
        ImageThreeSlice? Pronouns = StatusLine_Container.transform.Find("Pronouns").GetComponent<ImageThreeSlice>();
        Pronouns.canvasRenderer.SetColor(Main);

        ImageThreeSlice? Platform = StatusLine_Container.transform.Find("Platform").GetComponent<ImageThreeSlice>();
        Platform.canvasRenderer.SetColor(Main);

        ImageThreeSlice? Status = StatusLine_Container.transform.Find("Status Icons").GetComponent<ImageThreeSlice>();
        Status.canvasRenderer.SetColor(Main);

        ImageThreeSlice? Interact = StatusLine_Container.transform.Find("InteractionStatus/InteractBackground").GetComponent<ImageThreeSlice>();
        Interact.canvasRenderer.SetColor(Main);


        if (Player.APIUser.id == "usr_cfa86227-1978-4094-b0a4-2e5034f6bb48" || Player.APIUser.id == "usr_ae92db01-2d25-485a-9330-60bff18d5b5f")
        {
            Dev();
            var DevRole = UnityEngine.Object.Instantiate(Interact.transform.parent.transform, Interact.transform.parent.transform.parent);
            DevRole.gameObject.SetActive(true);
            var DevRoleIcon = DevRole.GetComponentInChildren<Image>();

            DevRoleIcon.sprite = EngineUtils.Assets.INTERNAL_Texture(autumn.ml.Properties.Resources.Logo).ToSprite();
            DevRoleIcon.color = new Color(0.965f, 0.180f, 0.584f, 1.000f);
            DevRole.GetComponentInChildren<ImageThreeSlice>().canvasRenderer.SetColor(new Color(0.965f, 0.180f, 0.584f, 1.000f));

            var ModRole = UnityEngine.Object.Instantiate(Interact.transform.parent.transform, Interact.transform.parent.transform.parent);
            ModRole.gameObject.SetActive(true);
            var Mod_Icon = ModRole.GetComponentInChildren<Image>();
            Mod_Icon.sprite = EngineUtils.Assets.INTERNAL_Texture(autumn.ml.Properties.Resources.sprite_tool).ToSprite();
            Mod_Icon.color = new Color(0.455f, 0.941f, 0.906f, 1.000f);
            ModRole.GetComponentInChildren<ImageThreeSlice>().canvasRenderer.SetColor(new Color(0.455f, 0.941f, 0.906f, 1.000f));

            var MadUser = UnityEngine.Object.Instantiate(Interact.transform.parent.transform, Interact.transform.parent.transform.parent);
            MadUser.gameObject.SetActive(true);
            var MadUser_Icon = MadUser.GetComponentInChildren<Image>();

            MadUser_Icon.sprite = EngineUtils.Assets.Sprite(new Uri("https://2silly4u.chat/images/icons/hammer-crash.png"));
            MadUser_Icon.color = new Color(0.549f, 0.961f, 0.231f, 1.000f);
            MadUser.GetComponentInChildren<ImageThreeSlice>().canvasRenderer.SetColor(new Color(0.549f, 0.961f, 0.231f, 1.000f));
        }

        Transform? GroupInfo_Container = NameplateContents.transform.Find("Group Info");
        ImageThreeSlice? GroupInfo_Background = GroupInfo_Container.GetComponent<ImageThreeSlice>();
        GroupInfo_Background.canvasRenderer.SetColor(Main);

        //  Icon_BG.overrideSprite = EngineUtils.Assets.INTERNAL_Texture(autumn.ml.Properties.Resources.capsule_background_bar).ToSprite();

        #endregion




    }
    void ChatboxApplyColoring()
    {
        if (Player.APIUser.id == "usr_cfa86227-1978-4094-b0a4-2e5034f6bb48") { Chatbox._chatText.richText = true; Chatbox._mirrorChatBubbleDisplay._chatText.richText = true; }

        ImageThreeSlice? Background_TypingIndicator = Chatbox._typingIndicator._canvasGroup.GetComponent<ImageThreeSlice>();
        ImageThreeSlice? Background_Chatbox = Chatbox._canvasGroup.GetComponent<ImageThreeSlice>();

        Background_Chatbox.canvasRenderer.SetColor(Main);
        Background_TypingIndicator.canvasRenderer.SetColor(Main);

        ImageThreeSlice? Background_MirrorTypingIndicator = Chatbox._mirrorChatBubbleDisplay._canvasGroup.GetComponent<ImageThreeSlice>();
        ImageThreeSlice? Background_MirrorChatbox = Chatbox._canvasGroup.GetComponent<ImageThreeSlice>();

        Background_MirrorChatbox.canvasRenderer.SetColor(Main);
        Background_MirrorTypingIndicator.canvasRenderer.SetColor(Main);

        // if(Chatbox._chatText.text.Contains("<size") {})
    }
    public void SetNameText(string txt) => Nameplate.field_Public_TextMeshProUGUIEx_0.text = txt;
    public void Dev()
    {
        Nameplate.field_Public_TextMeshProUGUIEx_0.text = $"<color=#FF7F50>DEV</color> {Player.APIUser.displayName}";
    }
    public string GetPlayerName(string txt) => Nameplate.field_Public_TextMeshProUGUIEx_0.text;
    public class NameplateUtils
    {
        
       
        public void AddStatusIcon(Sprite sprite)
        {

        }
    }
    public struct Renderer
    {
        public bool canSeeThroughWalls(bool enable)
        {
            switch (enable)
            {
                case true:
                    break;
                    
            }
            return false;
        }
    }
}