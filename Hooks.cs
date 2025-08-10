
using HexedBase.Patches;

public static class HooksManager
{
    public static void ApplyAll()
    {
        // Core application hooks
        VRCApplicationAwakePatch.ApplyPatch();

        // UI related hooks
        LoadingOverlayPatch.ApplyPatch();
        VRCLoadingPagePatch.ApplyPatch();
        QuickMenuPatch.ApplyPatch();
        MainMenuPatch.ApplyPatch();

        // Pickup and interaction hooks
        VRCPickupPatch.ApplyPatch();
        UdonDropPickupPatch.ApplyPatch();
        UdonEventPatch.ApplyPatch();

        // Player and networking hooks
        PlayerJoinPatch.ApplyPatch();
        PlayerLeavePatch.ApplyPatch();
        PhotonEventPatch.ApplyPatch();
        RFCEventPatch.ApplyPatch();

        // API modification hooks
        AvatarCopyPatch.ApplyPatch();
        VRCPlusSpoofPatch.ApplyPatch();

        // Update manager
        UpdateManagerPatch.ApplyPatch();

        Console.WriteLine("All patches applied successfully!");
    }
}