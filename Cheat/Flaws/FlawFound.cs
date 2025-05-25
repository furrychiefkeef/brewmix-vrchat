using Brewchat.Game.Wrappers;

namespace Brewchat.Cheat.Flaws;

public class FlawFound
{
    public static void DefaultPrompt()
    {
        GameUtils.UI.HUD.PopUp("The game broke it.", "Flaw Found");
    }

}