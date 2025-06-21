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

            List.Add("[Module>=Player-Movement", "This basically just allows u to send rfc event data globally <color=blue>(VrcEvent.DispatcherRFC)</color>");
            List.Add("[Module>Player]-Movement+Fly", "This basically just allows u to send rfc event data globally <color=blue>(VrcEvent.DispatcherRFC)</color>");
            List.Add("[Module>Player]-Movement_ForceJump", "This basically just allows u to send rfc event data globally <color=blue>(VrcEvent.DispatcherRFC)</color>");

            List.Add("VRC-SDK2", "Basically VRCSDK Utils in a Menu!");
            List.Add("VRC-SDK3", "Basically VRCSDK Utils in a Menu!");
        }
   }
 
}