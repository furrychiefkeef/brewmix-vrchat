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
        static bool _ui = false;

        [HarmonyPostfix, HarmonyPatch(typeof(QuickMenu), nameof(QuickMenu.Start))]
        public static void PlayerModel_Initalized(ref QuickMenu __instance)
        {
            if (__instance != null && !_ui)
            {
                _ui = true;
                UnityEngine.Object.DestroyImmediate(UnityEngine.Object.FindObjectOfType<MainMenuContent>()._bannerCarousel,true);
                QM.Apply();
            }
        }
    }
}
