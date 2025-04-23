using Brewchat.Cheat.Modules;
using Syrup.IMGUI;
using UnityEngine;

namespace Brewview;

public static class CorutineHandler
{
    public static System.Collections.IEnumerator CSSONLINE_RUNTIME()
    {
        CSS.Online();
        yield return new WaitForSeconds(0.12f); // Delay for 2 seconds (adjust as needed)
        // You can add more code here if needed after the delay
    }
}