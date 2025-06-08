using TMPro;
using UnityEngine;

namespace brewchat.hybridxcore.bep.Cheat.IMGUI;

public enum Logging
{
    Debug,
    Error,
    Info,
    Player,
    Dectection,
    Operator,
    World,
    Udon,
    SDK3,
    Hooks,
    Photon,
    GameServer
    
}
public class LimeConsole
{
    private static List<string> TEXT_LIST = new();

    public static string Text(string text, Logging logtype)
    {
        TEXT_LIST.Add($"[{logtype}] {text}");

        #region LOGIC
        TextMeshProUGUI? TEXT = GameObject.Find("Canvas_Console").transform.FindChild("Scroll View").transform.FindChild("Viewport").GetComponentInChildren<TMPro.TextMeshProUGUI>();
        #endregion
            
   
        TEXT.text = string.Join("", TEXT_LIST); 
    
        return text;
    }
    public static void Clear() => TEXT_LIST.Clear();
}   