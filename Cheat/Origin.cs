using BlanketSDK.Origin.Elements.Controls;

namespace Brewchat.Cheat;

public class Origin
{
    public static void Apply()
    {
        new OButton(null,1.5f,1.5f,"End Mission", () =>
        {
            Entry.Console.LogMessage("Disposable Waste");
        });
    }
}