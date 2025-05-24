using Brewchat.Wrappers;
using UnityEngine;

namespace Brewchat.Cheat;

public class UICore
{
    public static class Sprites
    {
        public static UnityEngine.Sprite Video(bool status = true)
        {
            Sprite? video = AstriekLib.Sprites.Video(status);
            return video;
        }
        public static UnityEngine.Sprite Ghost(bool status = true)
        {
            Sprite? ch = AstriekLib.Sprites.Video(status);
            return ch;
        }
    }
}