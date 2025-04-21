using System.Net;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Brewchat.Game.Wrappers
{
    internal static class EngineUtils
    {
        public static class Conversion
        {
            public static Color HexToColor(string hex)
            {
                ColorUtility.TryParseHtmlString(hex, out Color color);
                return color;
            }
        }
        public static class Assets
        {
            public static Texture2D LoadTexture(string dir)
            {
                byte[] data = File.ReadAllBytesAsync(dir).GetAwaiter().GetResult();
                Texture2D texture = new(0, 0);
                texture.LoadImage(data);
                if (texture != null) texture.name = Path.GetFileNameWithoutExtension(dir);
                return texture;
            }
            public static Texture2D LoadTexture(byte[] data)
            {
                Texture2D texture = new(0, 0);
                texture.LoadImage(data);
                if (texture != null) texture.name = "resources.dev";
                return texture;
            }
            public static Texture2D Data_LoadTexture(string dir)
            {
                HttpClient http = new();
                byte[] data = http.GetByteArrayAsync(dir).GetAwaiter().GetResult();
                Texture2D texture2D = new(0, 0);
                texture2D.LoadImage(data);
                texture2D.name = Path.GetFileNameWithoutExtension(dir);
                return texture2D;
            }
  
            public static Sprite LoadSprite(string dir) 
                => LoadTexture(dir).ToSprite();
            public static Sprite Data_LoadSprite(string dir) 
                => Data_LoadTexture(dir).ToSprite();
        }
    }
    public static class TextureExtensions
    {
        public static Sprite ToSprite(this Texture2D texture)
        {
            // Create a sprite from the texture using default game settings
            return Sprite.Create(
                texture, 
                new Rect(0, 0, texture.width, texture.height), 
                Vector2.zero // Using game's default settings
            );
        }
    }
}


/*public static Color HexToColor(string hex)
{
             
    // Remove # if present
    if (hex.StartsWith("#"))
        hex = hex.Substring(1);
               
    if (hex.Length == 6)
    {
        byte r = Convert.ToByte(hex.Substring(0, 2), 16);
        byte g = Convert.ToByte(hex.Substring(2, 2), 16);
        byte b = Convert.ToByte(hex.Substring(4, 2), 16);
        return new Color(r / 255f, g / 255f, b / 255f, 1f);
    }
    else if (hex.Length == 8) 
    {
        byte r = Convert.ToByte(hex.Substring(0, 2), 16);
        byte g = Convert.ToByte(hex.Substring(2, 2), 16);
        byte b = Convert.ToByte(hex.Substring(4, 2), 16);
        byte a = Convert.ToByte(hex.Substring(6, 2), 16);
        return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
    }

    Debug.LogError("Invalid hex color format: " + hex);
    return Color.white;
}*/