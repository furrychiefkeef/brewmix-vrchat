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

    public struct Logging
    {
        public static bool LogPhotonData { get; set; } = true;
        public static bool LogRPCData { get; set; }

        public static bool LogMessageData_Event43 { get; set; }
        public static bool LogVoicePackets_Event1 { get; set; } = true;
    }

    public struct Protection
    {
        public static bool block_event1 { get; set; }
        public static bool block_event11 { get; set; }
        public static bool block_event12 { get; set; }
        
    } 
    public struct Appearance
    {
        public static string QueueAudio { get; set; } = "https://github.com/riotlu/fake-cdn/raw/refs/heads/master/soundcloud_1947871183_audio.ogg?raw=true";
        public static UnityEngine.Color Theme { get; set; } = EngineUtils.Conversion.HexToColor("#c60e7b");
    }
   


}
