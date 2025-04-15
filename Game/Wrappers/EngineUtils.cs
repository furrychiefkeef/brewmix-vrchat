using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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

                // Handle different hex formats
                if (hex.Length == 6) // RRGGBB format
                {
                    byte r = Convert.ToByte(hex.Substring(0, 2), 16);
                    byte g = Convert.ToByte(hex.Substring(2, 2), 16);
                    byte b = Convert.ToByte(hex.Substring(4, 2), 16);
                    return new Color(r / 255f, g / 255f, b / 255f, 1f);
                }
                else if (hex.Length == 8) // RRGGBBAA format
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
                // Implement depending on your mod framework
                // This is a simplified example
                byte[] fileData = File.ReadAllBytes(dir);
                Texture2D texture = new Texture2D(2048, 2048);
                texture.LoadImage(fileData);
                texture.name = Path.GetFileNameWithoutExtension(dir);
                return texture;
            }
            public static Sprite LoadSprite(string dir)
            {
                // Implement depending on your mod framework
                // This is a simplified example
                byte[] fileData = File.ReadAllBytes(dir);
                Texture2D texture = new Texture2D(2048, 2048);
                texture.LoadImage(fileData);
                texture.name = Path.GetFileNameWithoutExtension(dir);
                return texture.ToSprite();
            }
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



