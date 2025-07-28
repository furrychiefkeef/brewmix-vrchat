using UnityEngine;
using autumn.ml.Properties;
using Brewchat.Game.Wrappers;
using UnityEngine.UI;
using VRC.Core;
using VRC.UI.Elements.Controls;
using MelonLoader;
using VRC.UI;
using System.Net.Http;
using Photon.Realtime;
using Harmony;
using autumn.ml.Game.Monobehaviours;

namespace brewchat.hybridxcore.bep.Game.Monobehaviours;

public class Nameplates : MonoBehaviour
{
    public Nameplates(IntPtr ptr) : base(ptr) { }
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
    public enum TagFlags
    {
        Bad,
        Good,
        Warning,
        Illegal
    }
   
    public UnityEngine.Color Main { get; set; }
    public Sprite? Test { get; set; }
    public static bool Overrender {get; set;}
    public Transform? Background { get; set; }
    public PlayerNameplate? Nameplate { get; set; }

    public ImageThreeSlice Main_Glow_Clone { get; set; }
    private ImageThreeSlice Main_Glow { get; set; }

    public ImageThreeSlice Icon_Glow { get; set; }
    public ImageThreeSlice? InteractStatus { get; set; }

    public ImageThreeSlice? QuickStatus { get; set; }
    public ChatBubbleDisplay? Chatbox { get; set; }
    public Dictionary <GameObject?, CanvasRenderer?> Backgrounds { get; set; } = new();

    private TextMeshProUGUIEx? Nametag { get; set; }

    public VRC.Player? Player;

    public event Action<Color>? _OnColorChanged;
    private Color? _lastMainColor;

    public event Action<string>? _OnGlowSpriteChanged;
    private string? _lastMainGlowSpriteName;


    

    void Update()
    {
        string[] relevantNames = new[] {
            "capsule_voice_glow_half_groups",
            "capsule_voice_glow_half",
            "capsule_voice_glow_full_groups",
            "capsule_voice_glow"
        };

        string? spriteName = Main_Glow?._sprite?.name;
        if (spriteName != null && IsValidGlowSprite(spriteName))
        {
            if (_lastMainGlowSpriteName != spriteName)
            {
                _lastMainGlowSpriteName = spriteName;
                _OnGlowSpriteChanged?.Invoke(spriteName);
            }
        }



        // Color change detection
        if (_lastMainColor == null || _lastMainColor != Main)
        {
            _lastMainColor = Main;
            _OnColorChanged?.Invoke(Main);
        }
    }


    void Start()
    {
        _OnGlowSpriteChanged += OnGlowSpriteChanged;
        _OnColorChanged += OnColorChanged;

        Player = gameObject.GetComponent<VRC.Player>();
        Main = Player.GetTrustColor();

        Nameplate = Player._vrcplayer.Nameplate;
        Chatbox = Player._vrcplayer.field_Public_ChatBubbleDisplay_0;
        Apply();


    }

    private bool IsValidGlowSprite(string spriteName)
    {
        return spriteName switch
        {
            "capsule_voice_glow_half_groups" => true,
            "capsule_voice_glow_full_groups" => true,
            "capsule_voice_glow" => true,
            _ => false
        };
    }
    private void OnColorChanged(Color color)
    {
        Backgrounds.ToList().ForEach(x =>
        {
            x.Value?.SetColor(color);
        });
    }

    private void OnGlowSpriteChanged(string spriteName)
    {
        switch (spriteName)
        {
            case "capsule_voice_glow_half_groups":
                MelonLogger.Msg("Groups");
                Texture2D? t = EngineUtils.Assets.INTERNAL_Texture(autumn.ml.Properties.Resources.capsule_voice_glow_half_groups);
                Main_Glow_Clone._sprite = Sprite.Create(t, Main_Glow._sprite.rect, Main_Glow._sprite.pivot, Main_Glow._sprite.pixelsPerUnit, 0, SpriteMeshType.FullRect, Main_Glow._sprite.border);
                break;
            case "capsule_voice_glow_full_groups":
                MelonLogger.Msg("No Groups");
                Texture2D? b = EngineUtils.Assets.INTERNAL_Texture(autumn.ml.Properties.Resources.capsule_voice_glow_full_groups);
                Main_Glow_Clone._sprite = Sprite.Create(b, Main_Glow._sprite.rect, Main_Glow._sprite.pivot, Main_Glow._sprite.pixelsPerUnit, 0, SpriteMeshType.FullRect, Main_Glow._sprite.border);
                break;
            case "capsule_voice_glow":
                MelonLogger.Msg("No Groups Voice");
                Texture2D? c = EngineUtils.Assets.INTERNAL_Texture(autumn.ml.Properties.Resources.capsule_voice_glow);
                Main_Glow_Clone._sprite = Sprite.Create(c, Main_Glow._sprite.rect, Main_Glow._sprite.pivot, Main_Glow._sprite.pixelsPerUnit, 0, SpriteMeshType.FullRect, Main_Glow._sprite.border);
                break;
        }
    }
    void LateUpdate()
    {
        canSeeThroughWalls(Overrender);
    }

    public void Apply()
    {
        ApplyColoring();
    }
    public void ApplyColoring()
    {
        NameplateApplyColoring();
        ChatboxApplyColoring();
    }
    public void ApplyBadgeSystem()
    {
        SpecialBadges();
    }
    public void ApplyTitleSystem()
    {
        SpecialTitles();
    }

    void NameplateApplyColoring()
    {
        if (Nameplate?.contents?.transform == null)
        {
            MelonLogger.Warning("Nameplate contents transform is null. Skipping coloring application.");
            return;
        }

        Transform NameplateContents = Nameplate.contents.transform;

        #region Main_Container  

        Transform Main_Container = NameplateContents.transform.Find("Main");
        if (Main_Container != null)
        {
            ImageThreeSlice? Main_Background = Main_Container.transform.Find("Background")?.GetComponent<ImageThreeSlice>();
            Main_Background?.canvasRenderer.SetColor(Main);

            ImageThreeSlice? Main_Pulse = Main_Container.transform.Find("Pulse")?.GetComponent<ImageThreeSlice>();
            Main_Pulse?.canvasRenderer.SetColor(Main);
            Main_Glow = Main_Container.transform.Find("Glow")?.GetComponent<ImageThreeSlice>();


            Transform? Main_Glow_Obj = UnityEngine.Object.Instantiate(Main_Glow?.transform, Main_Glow?.transform.parent);
            if (Main_Glow_Clone == null)
            {
                Main_Glow_Clone = Main_Glow_Obj?.GetComponent<ImageThreeSlice>();
                Main_Container.transform.Find("Glow")?.gameObject.SetActive(false);
            }
            Main_Glow_Clone?.canvasRenderer.SetColor(Main);

            TextMeshProUGUIEx? PlayerName = Nameplate.field_Public_TextMeshProUGUIEx_0;
            
            if (PlayerName != null)
            {
                Transform? _PlayerName = UnityEngine.Object.Instantiate(PlayerName.transform, PlayerName.transform.parent.transform);
                _PlayerName.SetSiblingIndex(0);
                var NameplateText = _PlayerName.GetComponent<NameplateTextMeshProUGUI>();
                NameplateText.color = Main;
                NameplateText.richText = true;
                Nametag = NameplateText;
                PlayerName.gameObject.SetActive(false);
            }
        }

        #endregion

        #region Icon_Container  

        Transform Icon_Container = NameplateContents.transform.Find("Icon");
        if (Icon_Container != null)
        {
            ImageThreeSlice? Icon_Pulse = Icon_Container.transform.Find("Pulse")?.GetComponent<ImageThreeSlice>();
            Icon_Pulse?.canvasRenderer.SetColor(Main);

            Transform? Icon_Glow_Obj = UnityEngine.Object.Instantiate(Icon_Container.transform.Find("Glow"), Icon_Container.transform.Find("Glow").parent);
            if (Icon_Glow == null)
            {
                Icon_Glow = Icon_Glow_Obj.GetComponent<ImageThreeSlice>();
                Icon_Container.transform.Find("Glow")?.gameObject.SetActive(false);
            }

            Icon_Glow._sprite = EngineUtils.Assets.INTERNAL_Texture(autumn.ml.Properties.Resources.capsule_voice_glow)?.ToSprite();
            Icon_Glow?.canvasRenderer.SetColor(Main);

            Image? Icon_BG = Icon_Container.transform.Find("Background")?.GetComponent<Image>();
            Icon_BG?.canvasRenderer.SetColor(Main);
        }

        #endregion

        #region QuickStats_Container  

        Transform? QuickStats_Container = NameplateContents.transform.Find("Quick Stats");
        ImageThreeSlice? QuickStats_BG = QuickStats_Container?.GetComponent<ImageThreeSlice>();
        QuickStats_BG?.canvasRenderer.SetColor(Main);
        QuickStatus = QuickStats_BG;
        #endregion

        #region Misc  

        Transform? StatusLine_Container = NameplateContents.transform.Find("Status Line");
        if (StatusLine_Container != null)
        {
            ImageThreeSlice? Pronouns = StatusLine_Container.transform.Find("Pronouns")?.GetComponent<ImageThreeSlice>();
            Pronouns?.canvasRenderer.SetColor(Main);

            ImageThreeSlice? Platform = StatusLine_Container.transform.Find("Platform")?.GetComponent<ImageThreeSlice>();
            Platform?.canvasRenderer.SetColor(Main);

            ImageThreeSlice? Status = StatusLine_Container.transform.Find("Status Icons")?.GetComponent<ImageThreeSlice>();
            Status?.canvasRenderer.SetColor(Main);

            ImageThreeSlice? Interact = StatusLine_Container.transform.Find("InteractionStatus/InteractBackground")?.GetComponent<ImageThreeSlice>();
            Interact?.canvasRenderer.SetColor(Main);
            InteractStatus = Interact;
        }
        if (Player != null)
        {
            if(Player.APIUser.IsOnMobile)
            {
                SetNameText($"<color={EngineUtils.Conversion.ColorToHex(EngineUtils.Conversion.HexToColor("#ff0000"))}>QUIGGER</color> {Player.APIUser.displayName}");
                Main = EngineUtils.Conversion.HexToColor("#a80200");

            }
            if (Player.VRCPlayerApi.isInstanceOwner)
            {
                SetNameText($"<color={EngineUtils.Conversion.ColorToHex(EngineUtils.Conversion.HexToColor("#52e6d3"))}>[Owner] {Player.APIUser.displayName}</color>");
                Main = EngineUtils.Conversion.HexToColor("#52e6d3");

            }
            if (Player.GetComponent<ExternPlayerModule>().moderatorStatus == ExternPlayerModule.ModeratorStatus.Moderator)
            {
                try
                {
                    if (!Player.APIUser.IsSelf)
                    {
                        GameUtils.UI.HUD.PopUp("Warning", $"Moderator: <color={EngineUtils.Conversion.ColorToHex(new Color(0.549f, 0.961f, 0.231f, 1.000f))}>{Player.APIUser.displayName}</color> has joined ur instance. ");
                    }
                    SpecialBadges();
                    SpecialTitles();
                    Main = EngineUtils.Conversion.HexToColor("#8CF53B");

                    //isSpecialUser = true;
                    //if(isSpecialUser)
                    //{
                    //    
                    //}
                    //else
                    //{
                    //    return;
                    //}

                }
                catch (Exception ex)
                {
                    MelonLogger.Error($"Error applying special badges or titles: {ex.Message}");
                }
            }
            if (Player.GetComponent<ExternPlayerModule>().isLexie)
            {
                Main = EngineUtils.Conversion.HexToColor("#da92dc");
                
            }
            if (Player.GetComponent<ExternPlayerModule>().moderatorStatus == ExternPlayerModule.ModeratorStatus.Agent)
            {
                SetNameText($"<color={EngineUtils.Conversion.ColorToHex(EngineUtils.Conversion.HexToColor("#db4d52"))}>Agent {Player?.APIUser.displayName}</color>");
                Main = EngineUtils.Conversion.HexToColor("#db4d52");
                CreateBadge("Agent", EngineUtils.Assets.Sprite(new Uri("https://2silly4u.chat/images/icons/badge-check.png")), Main);
            }
        }
        Transform? GroupInfo_Container = NameplateContents.transform.Find("Group Info");
        ImageThreeSlice? GroupInfo_Background = GroupInfo_Container?.GetComponent<ImageThreeSlice>();
        GroupInfo_Background?.canvasRenderer.SetColor(Main);

     
        Backgrounds.Add(Main_Container?.Find("Background")?.gameObject, Main_Container?.Find("Background")?.GetComponent<ImageThreeSlice>().canvasRenderer);
        Backgrounds.Add(Main_Container?.Find("Pulse")?.gameObject, Main_Container?.Find("Pulse")?.GetComponent<ImageThreeSlice>().canvasRenderer);
        Backgrounds.Add(Main_Container?.Find("Glow")?.gameObject, Main_Container?.Find("Glow")?.GetComponent<ImageThreeSlice>().canvasRenderer);
        Backgrounds.Add(Main_Glow_Clone?.gameObject, Main_Glow_Clone?.canvasRenderer);
        Backgrounds.Add(Icon_Container?.Find("Pulse")?.gameObject, Icon_Container?.Find("Pulse")?.GetComponent<ImageThreeSlice>().canvasRenderer);
        Backgrounds.Add(Icon_Container?.Find("Glow")?.gameObject, Icon_Container?.Find("Glow")?.GetComponent<ImageThreeSlice>().canvasRenderer);
        Backgrounds.Add(Icon_Glow?.gameObject, Icon_Glow?.canvasRenderer);
        Backgrounds.Add(Icon_Container?.Find("Background")?.gameObject, Icon_Container?.Find("Background")?.GetComponent<Image>().canvasRenderer);
        Backgrounds.Add(QuickStats_Container?.gameObject, QuickStats_BG?.canvasRenderer);
        Backgrounds.Add(StatusLine_Container?.Find("Pronouns")?.gameObject, StatusLine_Container?.Find("Pronouns")?.GetComponent<ImageThreeSlice>().canvasRenderer);
        Backgrounds.Add(StatusLine_Container?.Find("Platform")?.gameObject, StatusLine_Container?.Find("Platform")?.GetComponent<ImageThreeSlice>().canvasRenderer);
        Backgrounds.Add(StatusLine_Container?.Find("Status Icons")?.gameObject, StatusLine_Container?.Find("Status Icons")?.GetComponent<ImageThreeSlice>().canvasRenderer);
        Backgrounds.Add(StatusLine_Container?.Find("InteractionStatus/InteractBackground")?.gameObject, StatusLine_Container?.Find("InteractionStatus/InteractBackground")?.GetComponent<ImageThreeSlice>().canvasRenderer);
        Backgrounds.Add(GroupInfo_Container?.gameObject, GroupInfo_Background?.canvasRenderer);
        #endregion
    }
    void ChatboxApplyColoring()
    {
        if (Player.GetComponent<ExternPlayerModule>().moderatorStatus == ExternPlayerModule.ModeratorStatus.Moderator) { Chatbox._chatText.richText = true; Chatbox._mirrorChatBubbleDisplay._chatText.richText = true; }

        ImageThreeSlice? Background_TypingIndicator = Chatbox?._typingIndicator._canvasGroup.GetComponent<ImageThreeSlice>();
        ImageThreeSlice? Background_Chatbox = Chatbox?._canvasGroup.GetComponent<ImageThreeSlice>();

        Background_Chatbox?.canvasRenderer.SetColor(Main);
        Background_TypingIndicator?.canvasRenderer.SetColor(Main);

        ImageThreeSlice? Background_MirrorTypingIndicator = Chatbox?._mirrorChatBubbleDisplay._canvasGroup.GetComponent<ImageThreeSlice>();
        ImageThreeSlice? Background_MirrorChatbox = Chatbox?._canvasGroup.GetComponent<ImageThreeSlice>();

        Background_MirrorChatbox?.canvasRenderer.SetColor(Main);
        Background_MirrorTypingIndicator?.canvasRenderer.SetColor(Main);

        Backgrounds.Add(Background_TypingIndicator?.gameObject, Background_TypingIndicator?.canvasRenderer);
        Backgrounds.Add(Background_Chatbox?.gameObject, Background_Chatbox?.canvasRenderer);
        Backgrounds.Add(Background_MirrorTypingIndicator?.gameObject, Background_MirrorTypingIndicator?.canvasRenderer);
        Backgrounds.Add(Background_MirrorChatbox?.gameObject, Background_MirrorChatbox?.canvasRenderer);
       

        // if(Chatbox._chatText.text.Contains("<size") {})
    }

    public bool canSeeThroughWalls(bool enable)
    {
        switch (enable)
        {
            case true:
                Nameplate.NameplatePositioner.rootCanvas.gameObject.layer = GameUtils.Gameplay.RenderLayers.InternalUI;
                return true;

            case false:
                Nameplate.NameplatePositioner.rootCanvas.gameObject.layer = 12;
                return false;
        }
        ;
    }

    #region ClientDetection
    public void SpecialBadges()
    {
        CreateBadge("User", EngineUtils.Assets.INTERNAL_Texture(autumn.ml.Properties.Resources.Logo).ToSprite(), new Color(0.922f, 0.235f, 0.576f, 1.000f));
        CreateBadge("Staff", EngineUtils.Assets.INTERNAL_Texture(autumn.ml.Properties.Resources.sprite_tool).ToSprite(), new Color(0.376f, 0.937f, 0.816f, 1.000f));
        CreateBadge("Moderator", EngineUtils.Assets.Sprite(new Uri("https://2silly4u.chat/images/icons/hammer-crash.png")), new Color(0.549f, 0.961f, 0.231f, 1.000f));

    }
    public void SpecialTitles()
    {
        SetTitle("[Moderator]", EngineUtils.Conversion.HexToColor("#99F846"));
    }

    #endregion
    #region DEV_UTILS 
    public void SetNameText(string? txt) => Nametag.text = txt;

    public void SetTitle(string title, Color color)
    {
       Nametag.text = $"<color={EngineUtils.Conversion.ColorToHex(color)}>{title}</color> {Player?.APIUser.displayName}";
    }
    public void CreateBadge(string id, Sprite Sprite, Color Color)
    {
        Transform? Badge = UnityEngine.Object.Instantiate(InteractStatus?.transform.parent.transform, InteractStatus?.transform.parent.transform.parent);
        Badge?.gameObject.SetActive(true);
        Badge.name = $"[Badge] ({id})";
        Image? Icon = Badge.GetComponentInChildren<Image>();
        Icon.sprite = Sprite;
        Icon.color = Color;
        Badge.GetComponentInChildren<ImageThreeSlice>().canvasRenderer.SetColor(Color);
    }
    public void CreateTag(string title, string reason,  Color Color = default)
    {

        Transform? Tag = UnityEngine.Object.Instantiate(QuickStatus?.transform, QuickStatus?.transform.parent.transform);
        Tag?.gameObject.SetActive(true);
        Tag.name = $"[Tag] ({title})";

        Image? Trust_Icon = Tag.transform.Find("Trust Icon").GetComponent<Image>();
        Trust_Icon.name = "Tag Icon";

        NameplateTextMeshProUGUI? Trust_Text = Tag.transform.Find("Trust Text").GetComponent<NameplateTextMeshProUGUI>();
        Trust_Text.name = "Tag Text";
        Trust_Text.text = title;
        Trust_Text.richText = true;

        Image? Pref_Icon = Tag.transform.Find("Performance Icon").GetComponent<Image>();
        Pref_Icon.name = "Reason Icon";

        NameplateTextMeshProUGUI? Pref_Text = Tag.transform.Find("Performance Text").GetComponent<NameplateTextMeshProUGUI>();
        Pref_Text.name = "Reason Text";
        Pref_Text.text = reason;
        Pref_Text.richText = true;

        Tag.transform.Find("Frend Anchor Stats").Destory();
        if (Color == default) Color = Main; 
        Tag.GetComponent<ImageThreeSlice>().canvasRenderer.SetColor(Color);
    }
    public string? GetPlayerName(string txt) => Nameplate?.field_Public_TextMeshProUGUIEx_0.text;

    #endregion

   


}