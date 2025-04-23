using Cysharp.Threading.Tasks;
using UnityEngine;
using WebSocketSharp;
using Object = System.Object;

namespace Brewchat.Game.Wrappers
{
    internal static class EngineUtils
    {
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
            #region Imagebased
            public static Texture2D? INTERNAL_Texture(byte[] data)
            {
                Texture2D texture = new(0, 0);
                texture.LoadImage(data);
                if (texture != null) texture.name = "resources.dev";
                return texture;
            }
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
                texture2D.name = Path.GetFileNameWithoutExtension(url.ToString());
                return texture2D;
            }
  
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
            public static UnityEngine.Object AssetBundle(string? dir, string? name, bool? keep)
            {   
                if(dir.IsNullOrEmpty()) return null;
                AssetBundle? bundle = UnityEngine.AssetBundle
                    .LoadFromFileAsync(dir)
                    .GetAwaiter().GetResult();

                UnityEngine.Object obj = bundle.LoadAssetAsync<GameObject>(name)
                    .GetAwaiter().GetResult();
                if(keep == true) UnityEngine.Object.DontDestroyOnLoad(obj);
                return obj;
            }
            public static UnityEngine.GameObject AssetBundle(Uri? url, string? name, bool? keep, bool unload = false)
            {   
                var data = Encoding.ASCII.GetBytes(url.ToString());
                AssetBundle? bundle = UnityEngine.AssetBundle.LoadFromMemory(data);
                bundle.Unload(unload);
                UnityEngine.GameObject obj = bundle.LoadAsset<GameObject>(name);
                if(keep is true) UnityEngine.Object.DontDestroyOnLoad(obj);
                UnityEngine.Object.Instantiate(obj);
                return obj;
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

