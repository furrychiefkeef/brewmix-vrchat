using System.Reflection;
using Syrup.IMGUI;

namespace sysPuller;
public class ResourcesManipulator
{
    // Loads a Texture2D from embedded resources (imageName should include extension like "myImage.png")
    /// <summary>
    /// [pasted utils] lil bit of rewrote.
    /// </summary>
    /// <param name="file">the file name that's needed.</param>
    /// <returns></returns>
    internal static byte[] GetEmbeddedFile(string file)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        string? resourcePath = assembly.GetManifestResourceNames()
            .FirstOrDefault(r => r.EndsWith(file));
        
        if (resourcePath == null) { sconsole.internal_log($"Resource '{file}' not found in assembly."); return null; }
        using (Stream? stream = assembly.GetManifestResourceStream(resourcePath))
        {
            if (stream == null) { sconsole.internal_log("Resource stream was null."); return null; }
            byte[] buffer = new byte[stream.Length];
            stream.ReadAsync(buffer, 0, buffer.Length).GetAwaiter().GetResult();
            return buffer;
            
        }
    }  
    public static void RequestCalls(Action method_0, int _)
    {
        for (int i = 0; i < _; i++) method_0();
    }
}



