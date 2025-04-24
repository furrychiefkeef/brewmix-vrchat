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
    public static bool BlockEvent12 { get; set; } 
    public static float? FlySpeed { get; set; } = 6.5f;

    public bool SetFlyEnable(bool state) =>  Fly = state;
    public bool GetFlyEnable(Properties pp ){ return Fly; }

}

