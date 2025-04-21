using VRC.UI.Elements;

namespace brewchat.hybridxcore.bep.Game;

public static class XERC
{
    static MainMenu? cache;
    public static bool IsInMainMenu
    {
        get
        {
            if (cache == null)
                cache = UnityEngine.Object.FindObjectOfType<MainMenu>();

            return cache != null && cache.enabled;
        }
    }
}