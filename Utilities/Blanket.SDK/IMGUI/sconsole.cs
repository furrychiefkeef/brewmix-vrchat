using System;
using UnityEngine;

namespace Syrup.IMGUI;
internal static class sconsole
{
    static string _App = "BREW";
    const string _Version = "v2024.2.11";

    static string GetTime() => DateTime.Now.ToString("hh:mm:ss:fff");
    public static System.Collections.Generic.List<string> text = new();
    public static void log(string t)
    {
        text.Add(t);
        simgui.scrollPosition.y = Mathf.Infinity;
    }
    public static void prefix(this string child) => log(child);
    public static void clr() => text.Clear();
    public static string SLog(string msg)
    {
        log(
            $"<color={ColorPalette.CArray[3]}>[{GetTime()}]</color>" +
/*            $"[{_Version}]" +*/
              $"<color={ColorPalette.CArray[0]}>[{_App}]</color>{msg}");
        return msg;

    }
    public static string SError(string msg)
    {
        log(
            $"<color={ColorPalette.CArray[3]}>[{GetTime()}]</color>" +
               $"<color={ColorPalette.CArray[1]}>[X]</color>{msg}");
        return msg;

    }
    public static string SWarn(string msg)
    {
        log(
            $"<color={ColorPalette.CArray[3]}>[{GetTime()}]</color>" +
               $"<color={ColorPalette.CArray[2]}>[!]</color>{msg}");
        return msg;

    }
}