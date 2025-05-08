using Brewchat.Game.Wrappers;

namespace Brewchat;

public class Config
{
    public enum Properties
    {
        Fly,
        Speed,
        abruptness2motion,
    }

    #region MainMenu+QuickMenu

    public static string? QM_BG;
    public static string? MM_BG;

    #endregion


    public struct Movement
    {
        #region Fly

        public static bool fly { get; set; }
        public static bool abruptness2motion { get; set; }
        public static float? speed { get; set; } = 6.5f;

        #endregion

    }

    public static class testconfig
    {
        public static bool player_wallhack { get; set; } = false;
    }
    public struct Render
    {
        public static bool player_wallhack { get; set; } = false;
        public static bool esp { get; set; } = false;
        public static bool nameplate_wallhack { get; set; }
    }


    public struct Logging
    {
        public static bool LogPhotonData { get; set; } = true;
        public static bool LogRPCData { get; set; }

        public static bool LogMessageData_Event43 { get; set; }
        public static bool LogVoicePackets_Event1 { get; set; } = false;
    }

    public struct Protection
    {
        public static bool block_event1 { get; set; }
        public static bool block_event11 { get; set; }
        public static bool block_event12 { get; set; }

    }

    public struct Appearance
    {
        public static string QueueAudio { get; set; } =
            "https://github.com/riotlu/fake-cdn/raw/refs/heads/master/Audio/1 pound bish.mp3?raw=true";

        public struct Platte
        {
            public static UnityEngine.Color Main { get; set; } = EngineUtils.Conversion.HexToColor("#c60e7b");
            public static UnityEngine.Color Nameplate { get; set; } = EngineUtils.Conversion.HexToColor("#c60e7b");

            public static UnityEngine.Color Chatbox { get; set; } = EngineUtils.Conversion.HexToColor("#c60e7b");
            public static UnityEngine.Color HighlightFX { get; set; } = EngineUtils.Conversion.HexToColor("#c60e7b");
            /*Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ;
           Color Hover { get; set; } ; = EngineUtils.Conversion.HexToColor("#c60e7b");*/
        }
    }

    public struct Advanced
    {
        public static bool DebugMode { get; set; } = false;
    }
}


/*public static T Get<T>(Properties pp)
   {
       object result = pp switch
       {
           Properties.Fly => Movement.fly,
           Properties.Speed => Movement.speed ?? 6.5f, // Default value if null
           Properties.abruptness2motion => Movement.abruptness2motion,
           _ => default(T)
       };
       return result == null ? default(T) : (T)result;
   }*/
    
 
