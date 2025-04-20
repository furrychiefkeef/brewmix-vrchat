using Brewchat.Game.Wrappers;
using Syrup.IMGUI;
using UnityEngine;
using VRC.UI.Elements;
using Object = UnityEngine.Object;

namespace brewchat.hybridxcore.bep.Game;

public static class XERC
{
    private static MainMenu cachedMainMenu;

    public static bool IsInMainMenu
    {
        get
        {
            if (cachedMainMenu == null)
                cachedMainMenu = UnityEngine.Object.FindObjectOfType<MainMenu>();

            return cachedMainMenu != null && cachedMainMenu.enabled;
        }
    }
}