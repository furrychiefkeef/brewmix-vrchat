using UnityEngine;
using UnityEngine.UI;
using UNIXEngine.IL2CPPModding.Utils.Collections;

public class GifLoader : MonoBehaviour
{
    
    public class AnimatedGifData
    {
        public Image image;
        public List<Sprite> frames;
        public List<float> delays;
        public int currentFrame;
        public float timer;

        public AnimatedGifData(Image image, List<Sprite> frames, List<float> delays)
        {
            this.image = image;
            this.frames = frames;
            this.delays = delays;
            currentFrame = 0;
            timer = 0f;
        }
    }
    List<AnimatedGifData> gifs = new List<AnimatedGifData>();


    private static GifLoader? _instance;
    public static GifLoader Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<GifLoader>();
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Add a 
    /// </summary>
    /// <param name="image">The Image Compontent (You Can Change This Later)</param>
    /// <param name="url">The URL OR LOCALPATH </param>
    public void AddAnimatedGif(Image image, Uri? url)
    {
        StartCoroutine(LoadGifCoroutine(image, url).WrapToIl2Cpp());
    }
    
    System.Collections.IEnumerator LoadGifCoroutine(Image image, Uri? url)
    {
        
        List<Sprite> frames = new List<Sprite>();
        List<float> delays = new List<float>();

        if (url == null) throw new ArgumentNullException(nameof(url));
        byte[] data;
        if (url.IsFile) data = File.ReadAllBytes(url.LocalPath); 
        else if (url.Scheme == Uri.UriSchemeHttp || url.Scheme == Uri.UriSchemeHttps)
        {
            using HttpClient http = new();
            data = http.GetByteArrayAsync(url.ToString()).GetAwaiter().GetResult();
        }
        else throw new NotSupportedException($"Unsupported URI scheme: {url.Scheme}");

        
        yield return UniGif.GetTextureListCoroutine(data, (gifTexList, loopCount, width, height) =>
        {
            foreach (var frame in gifTexList)
            {
                var tex = frame.m_texture2d;

                // Prevent Unity from destroying this texture on scene change
                DontDestroyOnLoad(tex);
                
                Sprite sprite = Sprite.Create(
                 tex,
                    new Rect(0, 0, frame.m_texture2d.width, frame.m_texture2d.height),
                    new Vector2(0.5f, 0.5f)
                );
                
                DontDestroyOnLoad(sprite); // ALSO preserve the Sprite object
                
                tex.hideFlags = HideFlags.DontUnloadUnusedAsset;
                sprite.hideFlags = HideFlags.DontUnloadUnusedAsset;
                
                frames.Add(sprite);
                delays.Add(frame.m_delaySec);
            }

            gifs.Add(new AnimatedGifData(image, frames, delays));
        });

        // Optional: wait until it's confirmed added (if UniGif returns early)
    }
    void FixedUpdate()
    {
        foreach (var gif in gifs)
        {
            if (gif.frames.Count == 0) continue;

            gif.timer += Time.deltaTime;

            if (gif.timer >= gif.delays[gif.currentFrame])
            {
                gif.timer = 0f;
                gif.currentFrame = (gif.currentFrame + 1) % gif.frames.Count;
            }

            // Always re-apply the sprite every frame (in case the UI cleared it)
            gif.image.sprite = gif.frames[gif.currentFrame];
        }
    }
    
    void OnDestroy()
    {
        foreach (var gif in gifs)
        {
            foreach (var sprite in gif.frames)
            {
                DestroyImmediate(sprite.texture);
                Destroy(sprite);
            }
        }
    }
    
    /// <summary>
    /// All this does is create a isolated image just in case vrchat or unity compontents fuck with something major that breaks the gif or kills the textures.
    /// </summary>
    /// <param name="reference"></param>
    /// <returns></returns>
    public static Image CreateIsolatedImage(Image reference)
    {
        GameObject clone = GameObject.Instantiate(reference.gameObject, reference.transform.parent);
        clone.name = reference.gameObject.name + "_ModClone";

        // Optional: move it on top visually
        clone.transform.SetAsFirstSibling();

        Image newImage = clone.GetComponent<Image>();
        newImage.color = Color.white;
        return newImage;
    }
}