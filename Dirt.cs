using DiscordRPC;

namespace autumn
{
    internal class Dirt
    {
        // Declare it, but don't initialize it here
        static DiscordRpcClient rpc;

        public static void InitializeRPC()
        {
            // Initialize it inside the method
            if (rpc == null) // Check to prevent re-initialization if called multiple times
            {
                rpc = new DiscordRpcClient("13848597374835738185"); // Your Application ID
                rpc.Initialize(); // Don't forget to call Initialize() on the client!
            }

            rpc.SetPresence(new RichPresence()
            {
                
                Buttons = new DiscordRPC.Button[]
                {
                    new DiscordRPC.Button()
                    {
                        Label = "Visionary",
                        Url = "https://discord.gg/your-invite-code" // Provide a valid URL
                    }
                },
                State = "Playing", // Example state
                Details = "Modding VRChat" // Example details
            });
        }
    }
}