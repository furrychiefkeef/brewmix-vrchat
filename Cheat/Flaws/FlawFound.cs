using Brewchat.Game.Wrappers;

namespace Brewchat.Cheat.Flaws;

public class FlawFound
{
    public static void DefaultPrompt()
    {
        GameUtils.UI.HUD.PopUp("The game broke it.", "Flaw Found");
        EngineUtils.Assets.CreateAudio(new("https://github.com/xthugish/fake-cdn/raw/refs/heads/master/Rewind%20Or%20Dj%20Scratch%20Sound%20effects!.mp3"), loop: false);
    }

}