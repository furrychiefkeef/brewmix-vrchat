using System;
using Brewchat.Cheat.Modules;
using Brewchat.Game.Wrappers;
using MelonLoader;

namespace autumn.ml.Game.Monobehaviours;


public class DebugView 
{
        public static string prefix = "&";
        public static string val = "";
        public static Dictionary<string, Action> commandActions = new Dictionary<string, Action>();
        public static void Initialize()
        {
            // Add commands and their corresponding actions
           
        }
        
        private static void AddCommand(string command, Action action)
        {
            if (!commandActions.ContainsKey(command)) commandActions.Add(command, action);
        }
}
