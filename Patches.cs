using System;
using System.Collections.Generic;
using brewchat.Cheat;
using HarmonyLib;

using VRC.Core;
using VRC.UI.Elements;
using VRC.UI.Elements.Menus;


namespace brewchat.hybridxcore.bep
{
    [HarmonyPatch]
    internal class Patches
    {


        [HarmonyPostfix, HarmonyPatch(typeof(QuickMenu), nameof(QuickMenu.OnEnable))]
        public static void PlayerModel_Initalized(ref QuickMenu __instance)
        {
                QM.Apply();
        }
    }
}
