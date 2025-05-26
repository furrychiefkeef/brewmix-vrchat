using System.Reflection;
using Brewchat.Game.Wrappers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using WebSocketSharp;

namespace Brewchat.Game.Wrappers
{
    /// <summary>
    /// The Game Engine Wrapper (UnityEngine)
    /// </summary>
    internal static class EngineUtils
    {

        public struct Conversion
        {
            public static Color HexToColor(string hex)
            {
                ColorUtility.TryParseHtmlString(hex, out Color color);
                return color;
            }

            public static string ColorToHex(Color color)
            {
                return ColorUtility.ToHtmlStringRGB(color);
            }
        }

        public struct Assets 
        {
            #region Media
            public static AudioClip? AudioClip(Uri url)
            {
                UnityWebRequest www = UnityWebRequest.Get(url.ToString());
                var operation = www.SendWebRequest();
                while (!operation.isDone) System.Threading.Thread.Sleep(1);
                if (www.result != UnityWebRequest.Result.Success) return null;
                AudioClip clip = WebRequestWWW.InternalCreateAudioClipUsingDH(www.downloadHandler, www.url, false,false,0);
                
                if (clip == null) return null;
                if (url.IsFile) clip.name = Path.GetFileNameWithoutExtension(url.LocalPath);
                return clip;
            }
             public static GameObject? CreateAudio(Uri url, float volume = 1f, bool loop = true, bool bypassAllEffects = true)
            {
                UnityWebRequest www = UnityWebRequest.Get(url.ToString());
                var operation = www.SendWebRequest();
                while (!operation.isDone) System.Threading.Thread.Sleep(1);
                if (www.result != UnityWebRequest.Result.Success) return null;
                AudioClip clip = WebRequestWWW.InternalCreateAudioClipUsingDH(www.downloadHandler, www.url, false,false,0);
                
                if (clip == null) return null;
                var src = new GameObject($"Audio: [{url.LocalPath}]").AddComponent<AudioSource>();
                if (url.IsFile)
                {
                    src.name = Path.GetFileNameWithoutExtension(url.LocalPath);
                    clip.name = src.name;
                }
                src.clip = clip;
                src.hideFlags += 32;
                src.loop = loop;
                src.volume = volume;
                src.bypassListenerEffects = bypassAllEffects;
                src.bypassReverbZones = bypassAllEffects;
                src.Play();
                UnityEngine.Object.DontDestroyOnLoad(src);
                return src.gameObject;
            }
            public static AudioSource? AudioOverride(Uri url)
            {
                UnityWebRequest www = UnityWebRequest.Get(url.ToString());
                var operation = www.SendWebRequest();
                while (!operation.isDone) System.Threading.Thread.Sleep(1);
                if (www.result != UnityWebRequest.Result.Success) return null;
                AudioClip clip = WebRequestWWW.InternalCreateAudioClipUsingDH(www.downloadHandler, www.url, false,false,0);
                AudioSource? audiosrc = null;
                
                if (audiosrc == null || clip == null) return null;
                if (url.IsFile) clip.name = Path.GetFileNameWithoutExtension(url.LocalPath);
                audiosrc.clip = clip;
                audiosrc.hideFlags += 32;
                audiosrc.Play();
                return audiosrc;
            }

            #endregion
            #region Imagebased
 
            public static Texture2D? INTERNAL_Texture(byte[] data)
            {
                Texture2D texture = new(0, 0);
                texture.LoadImage(data);
                if (texture != null) texture.name = "resources.dev";
                return texture;
            }
            [Obsolete("We used Uri Now.")]
            public static Texture2D? Texture(string dir)
            {
                byte[] data = File.ReadAllBytesAsync(dir).GetAwaiter().GetResult();
                Texture2D texture = new(0, 0);
                texture.LoadImage(data);
                if (texture != null) texture.name = Path.GetFileNameWithoutExtension(dir);
                return texture;
            }
          
            public static Texture2D? Texture(Uri? url)
            {
                if (url == null) throw new ArgumentNullException(nameof(url));
                byte[] data;
                if (url.IsFile) data = File.ReadAllBytes(url.LocalPath); // Works with file:// or plain path
                else if (url.Scheme == Uri.UriSchemeHttp || url.Scheme == Uri.UriSchemeHttps)
                {
                    using HttpClient http = new();
                    data = http.GetByteArrayAsync(url.ToString()).GetAwaiter().GetResult();
                }
                else throw new NotSupportedException($"Unsupported URI scheme: {url.Scheme}");

                Texture2D texture2D = new(2048, 2048); // Always give a dummy size
                texture2D.LoadImage(data);
                texture2D.name = Path.GetFileNameWithoutExtension(url.LocalPath);
                return texture2D;
            }
            
            [Obsolete("We used Uri Now.")]
            public static Sprite Sprite(string dir) 
                => Texture(dir).ToSprite();
            public static Sprite Data_LoadSprite(Uri? dir) 
                => Texture(dir).ToSprite();
            #endregion

            #region Source
            public static UnityEngine.Object INTERNAL_AssetBundle(byte[]? data, string? name, bool? keep)
            {   
                if(data?.Length == 0) return null;
                AssetBundle? bundle = UnityEngine.AssetBundle
                    .LoadFromMemoryAsync(data)
                    .GetAwaiter().GetResult();

                UnityEngine.Object obj = bundle.LoadAssetAsync<GameObject>(name)
                    .GetAwaiter().GetResult();
                if(keep == true) UnityEngine.Object.DontDestroyOnLoad(obj);
                return obj;
            }
            [Obsolete]
            public static UnityEngine.Object AssetBundle(string? dir, string? prefab_name, bool? keep)
            {   
                if(dir.IsNullOrEmpty()) return null;
                AssetBundle? bundle = UnityEngine.AssetBundle
                    .LoadFromFileAsync(dir)
                    .GetAwaiter().GetResult();

                UnityEngine.Object obj = bundle.LoadAssetAsync<GameObject>(prefab_name)
                    .GetAwaiter().GetResult();
                if(keep == true) UnityEngine.Object.DontDestroyOnLoad(obj);
                return obj;
            }
            
            public static UnityEngine.GameObject AssetBundle(Uri? url, string? prefab_name, bool? keep)
            {
                HttpClient client = new ();
                byte[] data = client.GetByteArrayAsync(url).Result;
                AssetBundle? bundle = UnityEngine.AssetBundle.LoadFromMemory(data);
 
                GameObject? obj = bundle.LoadAsset<GameObject>(prefab_name);
                GameObject? instance = UnityEngine.Object.Instantiate(obj);
                if (keep is true) UnityEngine.Object.DontDestroyOnLoad(instance);
                bundle.Unload(false);
                return instance;
            }
            #endregion
        }
    }
    public static class TextureExtensions
    {
        public static Sprite ToSprite(this Texture2D? texture)
        {
            // Create a sprite from the texture using default game settings
            return Sprite.Create(
                texture, 
                new Rect(0, 0, texture.width, texture.height), 
                Vector2.zero // Using game's default settings
            );
        }

        public static Texture2D? Change(this Texture2D texture, string dir) => EngineUtils.Assets.Texture(dir);
    }
}

