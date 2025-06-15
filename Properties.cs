using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;

public class Config
{
   

    public Player _Player { get; set; } = new();
    public Render _Render { get; set; } = new();
    public Logging _Logging { get; set; } = new();
    public Modules _Modules { get; set; } = new();
    public Protection _Protection { get; set; } = new();
    public Appearance _Appearance { get; set; } = new();
    public Advanced _Advanced { get; set; } = new();
    
    public class Modules
    {
        public RFCTampering _RFCTampering {get;set;} = new();
        public  class RFCTampering
        {
             public SDK2 _SDK2 {get;set;} = new();
                public class SDK2
                {
                     public bool EnforceToAll { get; set; } = false;
                }
        } 
    }
    
    public class Player
    {
        public Airborn _Airborn { get; set; } = new();
        public Grounded _Grounded { get; set; } = new();

        public class Airborn
        {
            public Flight _Flight { get; set; } = new();
            public float Gravity {get; set;}

            public class Flight
            {
                public bool Enabled { get; set; } = false;
                public float? Speed { get; set; } = 6.5f;
            }
        }
        
        public class Grounded
        {
            public float? WalkSpeed { get; set; } = 6.5f;
            public float? RunSpeed { get; set; } = 6.5f;
            public float? JumpHeight { get; set; } = 6.5f;
        }
    }
    
    public class Render
    {
        public Overlays _Overlays { get; set; } = new();
        
        public class Overlays
        {
            public ESP _ESP { get; set; } = new();
            public Chams _Chams { get; set; } = new();
            public bool Player_Wallhack { get; set; } = false;
            public bool Nameplate_Wallhack { get; set; } = false;

            public class ESP
            {
                public bool Enabled { get; set; }
                public bool Nameplates { get; set; }
                public bool Player_Boxes { get; set; }
                public bool Player_Names { get; set; }
                public bool Player_Speed { get; set; }
                public bool Lines { get; set; }
                public bool Outlines { get; set; }
            }
            
            public class Chams
            {
                public bool Enabled { get; set; }
                public bool UseBaseColor { get; set; }
                public bool Tint { get; set; }
            }
        }
    }
    
    public class Logging
    {
        public Game _Game { get; set; } = new();
        
        public class Game
        {
            public bool PhotonEvents { get; set; } = false;
            public bool RPCs { get; set; } = false;
            public bool UdonObjects { get; set; } = false;
        }
    }

    public class Protection
    {
        // Empty for now
    }

    public class Appearance
    {
        public inGame _inGame { get; set; } = new();
        
        public class inGame
        {
            public FlatLoadingOverlay _FlatLoadingOverlay { get; set; } = new();
            public string? QueueAudio { get; set; }
            public bool HideLoadingOverlay { get; set; }
            public string? Mm_Background { get; set; }
            public string? Qm_Background { get; set; }
            
            public class FlatLoadingOverlay 
            { 
                public string? Image { get; set; }
                public string? Gif { get; set; }
                
                // Convert Unity Color to serializable format
                [JsonIgnore]
                public Color Tint { get; set; } = Color.white;
                
                [JsonProperty("Tint")]
                public SerializableColor TintSerializable
                {
                    get => new SerializableColor(Tint);
                    set => Tint = value.ToColor();
                }
                
                [JsonIgnore]
                public Color Base { get; set; } = Color.white;
                
                [JsonProperty("Base")]
                public SerializableColor BaseSerializable
                {
                    get => new SerializableColor(Base);
                    set => Base = value.ToColor();
                }
            } 
        }
    }

    public class Advanced
    {
        public bool DebugMode { get; set; } = false;
    }

    // Serializable Color struct to avoid Unity Color circular references
    [System.Serializable]
    public struct SerializableColor
    {
        public float r, g, b, a;

        public SerializableColor(Color color)
        {
            r = color.r;
            g = color.g;
            b = color.b;
            a = color.a;
        }

        public SerializableColor(float r, float g, float b, float a = 1f)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public Color ToColor()
        {
            return new Color(r, g, b, a);
        }

        public static implicit operator Color(SerializableColor sc)
        {
            return sc.ToColor();
        }

        public static implicit operator SerializableColor(Color c)
        {
            return new SerializableColor(c);
        }
    }

    public static void SaveToFile(Config config, string path)
    {
        try
        {
            JsonSerializerSettings options = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                TypeNameHandling = TypeNameHandling.None
            };
            
            string json = JsonConvert.SerializeObject(config, options);
            File.WriteAllText(path, json);
        }
        catch (Exception ex)
        {
            // Use MelonLoader logging if available, otherwise Debug.LogError
            try
            {
                MelonLoader.MelonLogger.Error($"Failed to save config: {ex.Message}");
            }
            catch
            {
                Debug.LogError($"Failed to save config: {ex.Message}");
            }
        }
    }

    public static Config LoadFromFile(string path)
    {
        try
        {
            if (!File.Exists(path)) 
                return new Config();

            string json = File.ReadAllText(path);
            JsonSerializerSettings options = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                TypeNameHandling = TypeNameHandling.None
            };
            
            return JsonConvert.DeserializeObject<Config>(json, options) ?? new Config();
        }
        catch (Exception ex)
        {
            try
            {
                MelonLoader.MelonLogger.Error($"Failed to load config: {ex.Message}");
            }
            catch
            {
                Debug.LogError($"Failed to load config: {ex.Message}");
            }
            return new Config();
        }
    }
}