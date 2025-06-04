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
    GameServer,
    
}
public class LimeConsole
{
    private static List<string> TEXT_LIST = new();

    public static string Text(string text, Logging logtype)
    {
        TEXT_LIST.Add($"[{logtype}] {text}");
    
        TextMeshProUGUI? TEXT = GameObject.Find("[Console]")
            .transform.FindChild("+Root+")
            .transform.FindChild("GroupLayout")
            .GetComponentInChildren<TMPro.TextMeshProUGUI>();
    
        TEXT.text = string.Join("", TEXT_LIST);
    
        return text;
    }
    public static void Clear() => TEXT_LIST.Clear();
}   