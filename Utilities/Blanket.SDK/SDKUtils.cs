using BlanketSDK.QM;

namespace BlanketSDK.Utilities;

public static class SDKUtils
{
    public class Wing
    {
        public enum Position { L, R, Both }
        public static string SetWingPosition(Position Orientation)
        {
            return Orientation switch
            {
                Position.L => "Left",
                Position.R => "Right",
                Position.Both => "Both",
                _ => "Unknown"
            };
        }
    }
  
  
}