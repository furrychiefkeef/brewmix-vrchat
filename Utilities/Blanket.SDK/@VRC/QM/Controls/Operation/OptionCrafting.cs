using VRC.Localization;
using VRC.UI.Elements;

namespace BlanketSDK.QM.Elements.Controls.Operation;

public class OptionCrafting
{
    public static RadialSelector.Option Craft(string __1, string __2, string __3)
    {
        return new RadialSelector.Option(__1.Localize(), __2.Localize(), __3.Localize());
    }
}