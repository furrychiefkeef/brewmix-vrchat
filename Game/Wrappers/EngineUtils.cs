using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Brewchat.Game.Wrappers
{
    internal class EngineUtils
    {
        public static class Conversion
        {
            
            
            public static Color HexToColor(string hex)
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
            }
        }
        public static class Assets
        {
            public static Texture2D LoadTexture(string dir)
            {
                Texture2D? texture = null;
                if (File.Exists(dir))
                {
                    byte[]? data = File.ReadAllBytes(dir);
                    Texture2D _texture = new Texture2D(2048, 2048);
                    texture.LoadImage(data);
                    if (texture != null) texture.name = Path.GetFileNameWithoutExtension(dir);
                    texture = _texture;
                    return texture;
                }
                return texture;
            }
            public static Texture2D LoadWebTexture(string dir)
            {
                Texture2D? texture = null;
                if (File.Exists(dir))
                {
                    byte[]? data = File.ReadAllBytes(dir);
                    Texture2D _texture = new Texture2D(2048, 2048);
                    texture.LoadImage(data);
                    if (texture != null) texture.name = Path.GetFileNameWithoutExtension(dir);
                    texture = _texture;
                    return texture;
                }
                return texture;
            }

            
            public static Sprite LoadSprite(string dir) => LoadTexture(dir).ToSprite();
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



