using System;
using UnityEngine;

namespace Syrup.IMGUI;
internal static class sconsole
{
    /*static string _App = "brewchat";
    const string _Version = "v2024.2.11";*/
    static string[] glyphs { get; } = 
    { 
        "<color=#ed1093>#</color>", 
        "<color=#9cd1a2>+</color>", 
        "<color=#fe0f05>-</color>", 
        "<color=#db4d52>!</color>" 
    };

    private static List<string> States { get; } = new();
    static string GetTime() => DateTime.Now.ToString("hh.mm:ss:fff");
    public static System.Collections.Generic.List<string> text = new();
    public static void internal_log(string str)
    {
        text.Add(str);
        simgui.scrollPosition.y = Mathf.Infinity;
    }
    public static void prefix(this string child) => internal_log(child);
    public static void clr() => text.Clear();
    public static string print(string msg, int v)
    {
        internal_log($"<color=#ffffff>{GetTime()}</color> {glyphs[v]} ->  | {msg}");
        /*log(
            $"<color={ColorPalette.CArray[3]}>[{GetTime()}]</color>" +
/*            $"[{_Version}]" +#1#
              $"<color={ColorPalette.CArray[0]}>[{_App}]</color>{msg}");*/
        return msg;

    }
    public static string print(string flag, string direction, string msg, int v)
    {
        internal_log($"<color=#ffffff>{GetTime()}</color> {glyphs[v]} ->  {$"[{flag}{direction}"}] | {msg}");
        /*log(
            $"<color={ColorPalette.CArray[3]}>[{GetTime()}]</color>" +
/*            $"[{_Version}]" +#1#
              $"<color={ColorPalette.CArray[0]}>[{_App}]</color>{msg}");*/
        return msg;

    }
    /*public static string SError(string msg)
    {
        internal_log(
            $"<color={ColorPalette.CArray[3]}>[{GetTime()}]</color>" +
               $"<color={ColorPalette.CArray[1]}>[X]</color>{msg}");
        return msg;

    }
    public static string SWarn(string msg)
    {
        internal_log(
            $"<color={ColorPalette.CArray[3]}>[{GetTime()}]</color>" +
               $"<color={ColorPalette.CArray[2]}>[!]</color>{msg}");
        return msg;

    }*/
}