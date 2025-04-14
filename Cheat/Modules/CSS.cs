

using VRC.UI.Core.Styles;

namespace Brewchat.Cheat.Modules;

internal class CSS : UnityEngine.MonoBehaviour
{
    public void Runtime()
    {
        foreach (var item in UnityEngine.Object.FindObjectsOfType<StyleEngine>())
        {
            item.field_Private_Dictionary_2_String_Color_0["highlight"] = UnityEngine.Color.green;
        }
    }
}
