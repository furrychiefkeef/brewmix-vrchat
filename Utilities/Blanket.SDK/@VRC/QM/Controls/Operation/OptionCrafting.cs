using UNIXEngine.IL2CPPModding;
using VRC.Localization;
using VRC.UI.Elements;
using Object = Il2CppSystem.Object;

namespace BlanketSDK.QM.Elements.Controls.Operation;

public class OptionCrafting
{

    public enum ChoiceSelect
    {
        wow,
        bitch,
        nitch
    }

    public static RadialSelector.Option Craft(string text, string tooltip, string value, Enum c)
    {
        // Create the option with localized strings
        var option = new RadialSelector.Option(
            text.Localize(),
            tooltip.Localize(),
            null
        );

        // Set the private fields using reflection or direct assignment
        // Method 1: If you can access them directly
        option.field_Private_LocalizableString_0 = text.Localize();
        option.field_Private_Object_0 = new Il2CppSystem.Enum((IntPtr)ChoiceSelect.wow);
        // Try one of these methods to convert the enum:

        return option;
    }
}
 
