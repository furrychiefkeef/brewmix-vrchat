using Valve.VR;
using System.Collections.Generic;

class InfoAboutCheat
{

   public static class Tooltips
   {
        public static Dictionary<string, string> List {get;} =  new();

        public static string GetTooltipString(string? Key) => List.Where( x => x.Key.Contains(Key)).Select(x => x.Value).FirstOrDefault();

        public static void Initalize()
        {
            List.Add("RFC-Exploit", "This basically just allows u to send rfc event data globally <color=blue>(VrcEvent.DispatcherRFC)</color>");
            List.Add("Tab", "<b><color=#FF0000>i</color><color=#FF1A00>t</color><color=#22CFDB>'</color><color=#FF4F00>s</color> <color=#FF8300>a</color> <color=#FFB800>l</color><color=#FFD300>u</color><color=#FFED00>x</color><color=#F6FF00>u</color><color=#DBFF00>r</color><color=#C1FF00>y</color> <color=#8CFF00>t</color><color=#72FF00>o</color> <color=#3DFF00>e</color><color=#23FF00>v</color><color=#08FF00>e</color><color=#00FF11>n</color> <color=#00FF46>b</color><color=#00FF60>e</color> <color=#00FF95>o</color><color=#00FFAF>w</color><color=#00FFCA>n</color><color=#00FFE4>i</color><color=#00FEFF>n</color><color=#00E4FF>g</color> <color=#00AFFF>t</color><color=#0095FF>h</color><color=#007BFF>i</color><color=#0060FF>s</color> <color=#002BFF>p</color><color=#0011FF>r</color><color=#0800FF>o</color><color=#2300FF>j</color><color=#3D00FF>e</color><color=#5700FF>c</color><color=#7200FF>t</color> <color=red>becareful what you show</color> <color=#8C00FF>.</color> <color=#C100FF>t</color><color=#DB00FF>a</color><color=#F600FF>k</color><color=#FF00ED>e</color> <color=#FF00B8>c</color><color=#FF009E>a</color><color=#FF0083>r</color><color=#FF0069>e</color> <color=#FF0034><</color><color=#FF001A>3</color></b>");
            List.Add("[Module>=Player-Movement", "This basically just allows u to send rfc event data globally <color=blue>(VrcEvent.DispatcherRFC)</color>");
            List.Add("[Module>Player]-Movement+Fly", "This basically just allows u to send rfc event data globally <color=blue>(VrcEvent.DispatcherRFC)</color>");
            List.Add("[Module>Player]-Movement_ForceJump", "This basically just allows u to send rfc event data globally <color=blue>(VrcEvent.DispatcherRFC)</color>");

            List.Add("VRC-SDK2", "Basically VRCSDK Utils in a Menu!");
            List.Add("VRC-SDK3", "Basically VRCSDK Utils in a Menu!");
        }
   }
 
}