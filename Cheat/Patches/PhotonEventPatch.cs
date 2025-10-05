/*using System;
using CoreRuntime.Manager;
using ExitGames.Client.Photon;
using MelonLoader;
using Newtonsoft.Json;
using Photon.Realtime;

namespace HexedBase.Patches
{
    internal class PhotonEventPatch
    {
        private delegate bool _PhotonOnEventDelegate(IntPtr instance, IntPtr eventData);
        private static _PhotonOnEventDelegate originalMethod;

        public static void ApplyPatch()
        {

            originalMethod = HookManager.Detour<_PhotonOnEventDelegate>(
                typeof(LoadBalancingClient).GetMethod(nameof(LoadBalancingClient.OnEvent)), 
                Patch
            );
        }

        private static bool Patch(IntPtr instance, IntPtr eventDataPtr)
        {
            EventData eventData = eventDataPtr == IntPtr.Zero ? null : new EventData(eventDataPtr);
            if (eventData == null) return originalMethod(instance, eventDataPtr);
            
            // Add blocking logic here if needed
            // For now, we'll process all events but log them
            
            bool result = originalMethod(instance, eventDataPtr);
            
            // Log events after processing (only if not blocked)
            if (result && Entry.config.Logging.Game.PhotonEvents)
            {
                object Data = null;
                ParameterDictionary Parameters = eventData.Parameters;

                foreach (Il2CppSystem.Collections.Generic.KeyValuePair<byte, Il2CppSystem.Object> data in Parameters)
                {
                    Data = EngineUtils.iL2CPPWrapper.Serialization.FromIL2CPPToManaged<object>(data);
                    string c = JsonConvert.SerializeObject(Data, Formatting.Indented, 
                        new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                    MelonLogger.Msg($"[PROCESSED] hook/photon Player: {eventData.Sender.getPlayer().VRCPlayerApi.displayName} Sent! | EV {eventData.Code} [Key:{eventData.CustomDataKey}] Event Data: \n {c}");
                }
            }
            
            return result;
        }
    }
}*/