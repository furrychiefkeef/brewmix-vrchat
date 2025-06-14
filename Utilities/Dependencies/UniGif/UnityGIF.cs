using Brewchat.Game.Wrappers;
using UnityEngine;

public static class UnityGIF
{
    private static Texture2D[] gifFrames;
    private static float[] frameDelays;
    private static int currentFrame;
    private static bool isPlaying;

    public static void LoadGif(Uri path)
    {
        byte[] gifData = File.ReadAllBytes(path.LocalPath);
        MelonLoader.MelonCoroutines.Start(DecodeGif(gifData));
    }

    private static System.Collections.IEnumerator DecodeGif(byte[] gifData)
    {
        List<UniGif.GifTexture> frames = new List<UniGif.GifTexture>();
        yield return UniGif.GetTextureListCoroutine(gifData, (textures, loopCount, width, height) =>
        {
            frames = textures;
        });

        if (frames.Count > 0)
        {
            gifFrames = frames.Select(f => f.m_texture2d).ToArray();
            frameDelays = frames.Select(f => f.m_delaySec).ToArray();
            MelonLoader.MelonCoroutines.Start(PlayGifAnimation());
        }
    }

    private static System.Collections.IEnumerator PlayGifAnimation()
    {
        isPlaying = true;
        while (isPlaying)
        {
            currentFrame = (currentFrame + 1) % gifFrames.Length;
            yield return new WaitForSeconds(frameDelays[currentFrame]);
        }
    }

    public static Texture2D GetCurrentFrame()
    {
        return gifFrames?[currentFrame];
    }

    public static void StopAnimation()
    {
        isPlaying = false;
    }
}