using UnityEngine;

namespace MaterialEditing.UI;

public static class MaterialUtils
{

        public static Material? Background_Parallax(this Material target, Texture2D? texture)
        {
            if (target == null || texture == null) return null;
            target.SetTexture("_ParallaxBackground", texture);
            target.SetTexture("_ParallaxMidground", texture);
            target.SetTexture("_ParallaxForeground", texture);
            return target;
        }
        
        public static Material? Background_Parallax(this Material target, Texture? texture)
        {
            if (target == null || texture == null) return null;
            target.SetTexture("_ParallaxBackground", texture);
            target.SetTexture("_ParallaxMidground", texture);
            target.SetTexture("_ParallaxForeground", texture);
            return target;  
        }
}

      