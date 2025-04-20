using Brewchat.Game.Wrappers;
using UnityEngine;
using VRC.UI.Elements;
using Object = UnityEngine.Object;

namespace brewchat.hybridxcore.bep.Game;

public class XERC
{
    public static bool isInQM() => Object.FindObjectOfType<QuickMenu>().enabled = true;
}

