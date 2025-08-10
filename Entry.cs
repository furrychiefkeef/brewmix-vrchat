using System;
using System.Reflection;
using Harmony;
using MelonLoader;
using Mono.CSharp;
using MonoMod.Utils;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.UI;

[assembly: MelonInfo(typeof(Entry), "Vanity", "Nightly", "Apache/l4veabl3 (Loveable)")]
public class Entry : MelonMod
{

    internal static Entry GetEntry() => new();
    internal ModInfo? modInfo => new();
    public static Config config = new();

    public HarmonyLib.Harmony Hooks => new("[2577]");
    internal class ModInfo
    {
        public Prefixes prefixes => new();
        public class Prefixes
        {
            public static string VariableName = "_VRLone";
        }
        public readonly string? Enviroment = Environment.GetEnvironmentVariable(Prefixes.VariableName, EnvironmentVariableTarget.User);
        public string? _currentName { get; set; } = "Vanity";
        public string? _currentVersion { get; set; } = "<2>.<5>.<7>.<7>";
        public string? _currentHash { get; set; } = System.Reflection.Assembly.GetExecutingAssembly().GetHashCode().ToString();
        public static Dictionary<string, string>? _custom_properties { get; set; } = new();
    }

    #region ML
    public override void OnEarlyInitializeMelon()
    {
        // if(modInfo.Enviroment == null) Environment.SetEnvironmentVariable(modInfo.VariableName, modInfo._currentName, EnvironmentVariableTarget.User);
        // else  modInfo._currentName = modInfo.Enviroment;

        if (File.Exists("config.json")) config = Config.LoadFromFile("config.json");
        else config = new();
    }
    public override void OnInitializeMelon()
    {
        foreach (Type behaviour in Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(MonoBehaviour))))
        {
            MelonLogger.Msg($"Adding Components: + {behaviour.Assembly.EntryPoint}+>::{behaviour.Namespace}");
            ClassInjector.RegisterTypeInIl2Cpp(behaviour, true);
            continue;
        }
    }

    public override void OnLateInitializeMelon() => Hooks.PatchAll();
    public override void OnDeinitializeMelon() => Config.SaveToFile(config, "config.json");
    #endregion



    #region SceneView
    public override void OnSceneWasInitialized(int buildIndex, string sceneName)
    {

    }
    public override void OnSceneWasLoaded(int buildIndex, string sceneName)
    {

    }
    public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
    {

    }
    //private Texture2D[] gifFrames;
    //private float[] delays;
    //private int currentFrame;
    //private float timer;
    //private RawImage targetImage;

    //public void Start()
    //{
    //    GameObject canvas = new GameObject("GifCanvas");
    //    UnityEngine.Object.DontDestroyOnLoad(canvas);
    //    canvas.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
    //    canvas.AddComponent<CanvasScaler>();
    //    canvas.AddComponent<GraphicRaycaster>();

    //    targetImage = new GameObject("GifImage").AddComponent<RawImage>();
    //    targetImage.transform.SetParent(canvas.transform, false);
    //    targetImage.rectTransform.sizeDelta = new Vector2(256, 256);
    //    targetImage.rectTransform.anchoredPosition = Vector2.zero;

    //    string gifPath = "C:\\Users\\riot\\Pictures\\b23360ff15e98040d3f9c66516d1eebb.gif"; // 👈 IMPORTANT: Set your file path here
    //    if (!File.Exists(gifPath))
    //    {
    //        MelonLogger.Error($"GIF file not found at path: {gifPath}");
    //        return;
    //    }

    //    byte[] gifBytes = File.ReadAllBytes(gifPath);
    //    MelonCoroutines.Start(LoadGif(gifBytes));
    //}

    ////private System.Collections.IEnumerator LoadGif(byte[] gifData)
    //{
    //    // FIX: Changed the callback to accept four arguments: list, loop count, width, and height.
    //    yield return UniGif.GetTextureListCoroutine(gifData, (gifTexList, loopCount, width, height) =>
    //    {
    //        if (gifTexList == null || !gifTexList.Any())
    //        {
    //            MelonLogger.Error("Failed to load GIF frames.");
    //            return;
    //        }

    //        // Extract the Texture2D from each GifTexture object.
    //        gifFrames = gifTexList.Select(gt => gt.m_texture2d).ToArray();

    //        // FIX: Extract the delay for each frame from the 'm_delaySec' field in each GifTexture.
    //        delays = gifTexList.Select(gt => gt.m_delaySec).ToArray();

    //        currentFrame = 0;
    //        timer = 0f;
            
    //        if (gifFrames.Length > 0)
    //        {
    //            targetImage.texture = gifFrames[0];
    //            // Optional: You can use the returned width and height to set the image size.
    //            // targetImage.rectTransform.sizeDelta = new Vector2(width, height);
    //        }
    //    });
    //}

    //public override void OnLateUpdate()
    //{

    //    if (gifFrames == null || gifFrames.Length == 0)
    //    {
    //        return;
    //    }

    //    timer += Time.deltaTime;

    //    if (timer >= delays[currentFrame])
    //    {
    //        timer -= delays[currentFrame];
    //        currentFrame = (currentFrame + 1) % gifFrames.Length;
    //        targetImage.texture = gifFrames[currentFrame];
    //    }
    //}
    #endregion

}
