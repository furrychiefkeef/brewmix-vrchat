using UnityEngine;
using VRC.UI;
namespace BlanketSDK.Core;

public class iVRCAssets
{

    // Put Sprites
    interface Sprites
    {
        public enum Sprites
        {
            Rocket,
            Ban,
            Kick,
            People,
        }

        public static Sprite? Get(Sprites _sprites)
        {
            foreach (ImageEx gameObject in UnityEngine.Object.FindObjectsOfType<ImageEx>())
            {
                gameObject.name.Contains($"Icon/{_sprites}");
                return gameObject.sprite;
            }

            return null;
        }
    }
       
}
