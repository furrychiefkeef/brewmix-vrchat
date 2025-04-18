using Brewchat.Game.Wrappers;
using UnityEngine;
using Object = UnityEngine.Object;

namespace brewchat.hybridxcore.bep.Game;

public class XERC
{
    public static void isInQM()
    {

    }

    public static bool IsChatboxOpen() 
    {
        // Cache this reference if possible instead of finding it every time
        var chatController = Object.FindObjectOfType<MonoBehaviour1PublicKe_cObInObTeBoObBoBoUnique>();
        if (chatController != null)
        {
            var uiPage = chatController.Method_Public_UIPage_0();
            if (uiPage != null)
            {
                return uiPage.prop_Boolean_0;
            }
        }
        return false;
    }
}
