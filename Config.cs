using Brewchat.Game.Wrappers;
using UnityEngine;

namespace Brewchat;

public class Config
{
    private static Config? _instance;
    public Config Instance 
    {
        get
        {
            _instance ??= new Config();
            return _instance;
        }
    }     
    public class Exploits
    {
        public static bool RPC_ForceGlobal { get; set; } = false;
    }
 
    public class Player
    {
        public static class Airborn
        {
            public static class Fly 
            {
                public static bool Enabled { get; set; }
                public static float? Speed { get; set; } = 6.5f;
            }
        }
        public static  class Grounded
        {
             public static float? WalkSpeed { get; set; } = 6.5f;
             public static float? RunSpeed { get; set; } = 6.5f;
             public static float? JumpHeight { get; set; } = 6.5f;
        }

    }
    public struct Render
    {
        public static class Overlays
        {
            public static bool Player_Wallhack { get; set; } = false;
            public static bool Nameplate_Wallhack { get; set; }
            public class ESP 
            {
                public static bool Enabled { get; set; }
                public static bool Nameplates { get; set; }
                public static bool Player_Boxes { get; set; }
                public static bool Player_Names { get; set; }
                public static bool Player_Speed { get; set; }
                public static bool Lines { get; set; }
                public static bool Outlines { get; set; }
            }
        }
       
       
    }
    
    public class Logging
    {
        public static class VRChat
        {
            public static bool PhotonEvents { get; set; } = false;
            public static bool RPCs { get; set; } = false;
            public static bool UdonObjects { get; set; } = false;
        }
      

    }

    public class Protection
    {
       
    }

    public class Appearance
    {
        public static class inGame
        {
            public static string? QueueAudio { get; set; }
            public static class FlatLoadingOverlay 
            { 
                public static string? Image { get; set; }
                public static string? Gif { get; set; }
                public static Color? Tint { get; set; }
                public static Color? Base { get; set; }
            } 
            public static bool HideLoadingOverlay { get; set; }
            public static string? Mm_Background { get; set; }
            public static string? Qm_Background { get; set; }
        }
    }
    
    public class Advanced
    {
        public static bool DebugMode { get; set; } = false;
    }
    
}