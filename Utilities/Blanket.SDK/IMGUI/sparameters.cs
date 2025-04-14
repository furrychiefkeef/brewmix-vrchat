using System;
using System.IO;
using UnityEngine;
using VRC.Fonts;

namespace Syrup
{
    internal class sparameters
    {
        // Adding direct access to a fallback font for emergencies
        
 
        
        public static GUIStyle Text(int fontSize)
        {
            GUIStyle _text = new GUIStyle();
            
            try
            {
                _text.fontSize = fontSize;
                _text.normal.textColor = Color.white;
                _text.wordWrap = true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error in _Text: {ex.Message}");
                // Create a very basic style with Unity's default font
                _text = new GUIStyle(GUI.skin.label);
                _text.fontSize = fontSize;
            }
            
            return _text;
        }
    }
    public class ColorPalette
    {
        // Avoid IL2CPP issues by using direct method
        
            public static string[] CArray = {
                "#ffffff",
                "#ffffff",
                "#ffffff", 
                "#ffffff",
                "#ffffff",
                "#ffffff"
            };

        }

        public class Colors
        {
            public static string Font = "#ffffff";
            public static string Main = "#ffffff";
            public static string Error = "#ffffff";
            public static string Warning = "#ffffff";
        }
    }
