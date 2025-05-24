using Brewchat.Game.Wrappers;
using brewchat.hybridxcore.bep.Properties;

namespace Brewchat.Wrappers;

public class AstriekLib
{
    public static class Sprites
    {
        public static UnityEngine.Sprite? Video(bool state)
        {
            if (state)  return EngineUtils.Assets.INTERNAL_Texture(Resources.sprite_video).ToSprite();
            return EngineUtils.Assets.INTERNAL_Texture(Resources.sprite_video_slash).ToSprite();
        }
    }
    
    
    public static string  CommandBuilder(Func<string> func)
    {
        return func.Invoke();
    }   
    
    
    
}


