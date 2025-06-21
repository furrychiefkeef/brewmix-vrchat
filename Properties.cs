using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;
using VRC.Core;
using UnityEngine.Experimental.GlobalIllumination;
using static Config.LoggingModel;

public class Config
{
   

    public PlayerModel Player { get; set; } = new();
    public RenderModel Render { get; set; } = new();
    public LoggingModel Logging { get; set; } = new();
    public ModulesModel Modules { get; set; } = new();
    public ProtectionModel Protection { get; set; } = new();
    public AppearanceModel Appearance { get; set; } = new();
    public AdvancedModel Advanced { get; set; } = new();
    
    public class ModulesModel
    {
        public RFCTamperingModel RFCTampering {get;set;} = new();
        public UdonModel Udon {get;set;} = new();
        public class RFCTamperingModel   
        {
                public SDK2Model SDK2 {get; set;} = new();
                public class SDK2Model
                {
                  public bool EnforceToAll { get; set; } = false;
                }
        } 
        public class UdonModel
        {
            public bool isBlocked  { get; set ; } = false;
        }
    }
    
    public class PlayerModel
    {
        public AirbornModel Airborn { get; set; } = new();
        public GroundedModel Grounded { get; set; } = new();

        public class AirbornModel
        {
            public FlightModel Flight { get; set; } = new();
            public float Gravity {get; set;}

            public class FlightModel
            {
                public bool Enabled { get; set; } = false;
                public float? Speed { get; set; } = 6.5f;
            }
        }
        
        public class GroundedModel
        {
            public float? WalkSpeed { get; set; } = 6.5f;
            public float? RunSpeed { get; set; } = 6.5f;
            public float? JumpHeight { get; set; } = 6.5f;
        }
    }
    
    public class RenderModel
    {
        public OverlaysModel Overlays { get; set; } = new();
        
        public class OverlaysModel
        {
            public ESPModel ESP { get; set; } = new();
            public ChamsModel Chams { get; set; } = new();
            public bool Player_Wallhack { get; set; } = false;
            public bool Nameplate_Wallhack { get; set; } = false;

            public class ESPModel
            {
                public bool Enabled { get; set; }
                public bool Nameplates { get; set; }
                public bool Player_Boxes { get; set; }
                public bool Player_Names { get; set; }
                public bool Player_Speed { get; set; }
                public bool Lines { get; set; }
                public bool Outlines { get; set; }
            }
            
            public class ChamsModel
            {
                public bool Enabled { get; set; }
                public bool UseBaseColor { get; set; }
                public bool Tint { get; set; }
            }
        }
    }
    
    public class LoggingModel
    {
        public GameModel Game { get; set; } = new();
        
        public class GameModel
        {
            public bool PhotonEvents { get; set; } = false;
            public bool RPCs { get; set; } = false;
            public bool UdonObjects { get; set; } = true;
        }
    }

    public class ProtectionModel
    {
        // Empty for now
    }

    public class AppearanceModel
    {
        public inGameModel inGame { get; set; } = new();

        public class inGameModel
        {
            public FlatLoadingOverlayModel FlatLoadingOverlay { get; set; } = new();
            public string? QueueAudio { get; set; }
            public bool HideLoadingOverlay { get; set; }
            public string? Mm_Background { get; set; }
            public string? Qm_Background { get; set; }

            public class FlatLoadingOverlayModel
            {
                public string? Image { get; set; }
                public string? Gif { get; set; }

                // Convert Unity Color to serializable format
                [JsonIgnore]
                public Color Tint { get; set; } = Color.white;

                [JsonProperty("Tint")]
                public SerializableColor TintSerializable
                {
                    get => new(Tint);
                    set => Tint = value.ToColor();
                }

                [JsonIgnore]
                public Color Base { get; set; } = Color.white;

                [JsonProperty("Base")]
                public SerializableColor BaseSerializable
                {
                    get => new(Base);
                    set => Base = value.ToColor();
                }
            }
        }
    }

    public class AdvancedModel
    {
        public bool DebugMode { get; set; } = false;
        public bool MurderPlayerNode { get; set; } = false;
    }

    // Serializable Color struct to avoid Unity Color circular references
    [Serializable]
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
            JsonSerializerSettings options = new()
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
            JsonSerializerSettings options = new()
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