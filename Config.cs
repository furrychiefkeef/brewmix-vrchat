namespace Brewchat;

public class Config
{
    #region MainMenu+QuickMenu
    
    public static string? QM_BG;
    public static string? MM_BG;
    
    #endregion
    
    
    public static bool Fly { get; set; } = false;
    public static float? FlySpeed { get; set; } = 6.5f;
}