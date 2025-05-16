using Brewchat.Game.Wrappers;
using Syrup.IMGUI;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.SDKBase;

namespace Brewchat.Cheats.SDK2.World;

public static class OptimizeBox
{
    public static void Color() => GameUtils.World.SDK2.InteractWithTrigger("Toggle");
    public static void Walls() => GameUtils.World.SDK2.InteractWithTrigger("Walls Toggle");
    public static void NormalMirror() => GameUtils.World.SDK2.InteractWithTrigger("Normal Mirror");
    public static void OptimizedMirror() =>  GameUtils.World.SDK2.InteractWithTrigger("Optimized Mirror");
    public static void DestoryWalls()
    {
        GameUtils.UI.HUD.PopUp("Walls Destroyed", "Congrats Now Click The Walls Trigger");
        GameObject.Find("BoxNoWalls").SetActive(false);
    }
}