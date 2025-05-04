using System.Reflection;
using Brewchat.Game.Wrappers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using WebSocketSharp;

namespace Brewchat.Game.Wrappers
{
    internal static class EngineUtils
    {
        public struct Internals
        {
            private static Dictionary<string, List<GameObject>> customTaggedObjects = new Dictionary<string, List<GameObject>>();
            public static void SetCustomTag(GameObject obj, string tagName)
            {
                // Make sure the dictionary has an entry for this tag
                if (!customTaggedObjects.ContainsKey(tagName))
                {
                    customTaggedObjects.Add(tagName, new List<GameObject>());
                }
            
                // Add the object to the custom tag list if not already there
                if (!customTaggedObjects[tagName].Contains(obj))
                {
                    customTaggedObjects[tagName].Add(obj);
                    Entry.Console.LogInfo($"Added custom tag '{tagName}' to {obj.name}");
                }
            }
        
            // Method to check if an object has your custom tag
            public bool HasCustomTag(GameObject obj, string tagName)
            {
                if (customTaggedObjects.ContainsKey(tagName))
                {
                    return customTaggedObjects[tagName].Contains(obj);
                }
                return false;
            }
        
            // Method to get all objects with a specific custom tag
            public List<GameObject> FindGameObjectsWithCustomTag(string tagName)
            {
                if (customTaggedObjects.ContainsKey(tagName))
                {
                    return new List<GameObject>(customTaggedObjects[tagName]);
                }
                return new List<GameObject>();
            }
        }
        public struct Conversion 
        {
            public static Color HexToColor(string hex)
            {
                ColorUtility.TryParseHtmlString(hex, out Color color);
                return color;
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
             public static GameObject? CreateAudio(Uri url)
            {
                UnityWebRequest www = UnityWebRequest.Get(url.ToString());
                var operation = www.SendWebRequest();
                while (!operation.isDone) System.Threading.Thread.Sleep(1);
                if (www.result != UnityWebRequest.Result.Success) return null;
                AudioClip clip = WebRequestWWW.InternalCreateAudioClipUsingDH(www.downloadHandler, www.url, false,false,0);
                
                if (clip == null) return null;
                if (url.IsFile) clip.name = Path.GetFileNameWithoutExtension(url.LocalPath);
                var src = new GameObject("Audio").AddComponent<AudioSource>();
                src.clip = clip;
                src.hideFlags += 32;
                src.loop = true;
                src.volume = 1f;
                src.bypassListenerEffects = true;
                src.bypassReverbZones = true;
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
            [Obsolete]
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
                HttpClient http = new();
                byte[] data = http.GetByteArrayAsync(url.ToString()).GetAwaiter().GetResult();
                Texture2D texture2D = new(0, 0);
                texture2D.LoadImage(data);
                texture2D.name = Path.GetFileNameWithoutExtension(url.LocalPath);
                return texture2D;
            }
            
            [Obsolete]
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

