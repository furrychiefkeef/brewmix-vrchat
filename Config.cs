using Brewchat.Game.Wrappers;

namespace Brewchat;

public class Config
{
    public enum Properties
    {
        Fly,
        Speed,
        Event12,
    }
    #region MainMenu+QuickMenu
    
    public static string? QM_BG;
    public static string? MM_BG;
    
    #endregion

    public static bool Fly { get; set; }
    public static float? FlySpeed { get; set; } = 6.5f;

    public bool SetFlyEnable(bool state) =>  Fly = state;
    public bool GetFlyEnable(Properties pp ) { return Fly; }

    public struct Logging
    {
        public static bool LogPhotonData { get; set; }
        public static bool LogRPCData { get; set; }

        public static bool LogMessageData_Event43 { get; set; } 
        public static bool LogVoicePackets_Event1 { get; set; }
    }

    public struct Protection
    {
        public static bool block_event1 { get; set; }
        public static bool block_event11 { get; set; }
        public static bool block_event12 { get; set; }
        
    } 
    public struct Appearance
    {
        public static UnityEngine.Color Theme { get; set; } = EngineUtils.Conversion.HexToColor("#c60e7b");
    }
   


}
