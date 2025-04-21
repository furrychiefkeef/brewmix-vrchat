using System.IO;
using System.Reflection;
using UnityEngine;

public class ResourcesManipulator
{
    // Loads a Texture2D from embedded resources (imageName should include extension like "myImage.png")
    internal static Texture2D GetTextureFromResources(string imageName)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        string resourcePath = assembly.GetManifestResourceNames()
            .FirstOrDefault(r => r.EndsWith(imageName));

        if (resourcePath == null)
        {
            Debug.LogError($"Resource '{imageName}' not found in assembly.");
            return null;
        }

        using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
        {
            if (stream == null)
            {
                Debug.LogError("Resource stream was null.");
                return null;
            }

            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            Texture2D tex = new Texture2D(2, 2); // Size will be replaced by LoadImage
            if (tex.LoadImage(buffer))
                return tex;
            else
                return null;
        }
    }
}
