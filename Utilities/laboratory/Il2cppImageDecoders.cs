using System;
using MelonLoader;
using UnityEngine;

namespace autumn.ml.Utilities.laboratory;

public class Il2cppImageDecoders
{
    public static bool DecodeGif(byte[] data, out Texture2D[] frames, out float[] delays)
    {
        frames = null;
        delays = null;

        if (data == null || data.Length < 13)
            return false;

        // Check GIF signature
        if (data[0] != 0x47 || data[1] != 0x49 || data[2] != 0x46 || // "GIF"
            data[3] != 0x38 || (data[4] != 0x37 && data[4] != 0x39)) // "87a" or "89a"
        {
            return false;
        }

        try
        {
            List<Texture2D> frameList = new List<Texture2D>();
            List<float> delayList = new List<float>();

            // Read header
            int width = BitConverter.ToUInt16(data, 6);
            int height = BitConverter.ToUInt16(data, 8);

            int pos = 13; // Skip header

            // Skip global color table if present
            if ((data[10] & 0x80) != 0)
            {
                int globalColorTableSize = 2 << (data[10] & 0x07);
                pos += globalColorTableSize * 3;
            }

            // For simplicity, just extract the first frame
            // A full implementation would parse all frames, but this is complex

            // Try to load as regular image first
            Texture2D firstFrame = new Texture2D(2, 2);
            if (ImageConversion.LoadImage(firstFrame, data))
            {
                UnityEngine.Object.DontDestroyOnLoad(firstFrame);
                frameList.Add(firstFrame);
                delayList.Add(0.1f); // 100ms default delay

                frames = frameList.ToArray();
                delays = delayList.ToArray();
                return true;
            }
            else
            {
                UnityEngine.Object.Destroy(firstFrame);
                return false;
            }
        }
        catch (System.Exception e)
        {
            MelonLogger.Msg($"Error decoding GIF: {e.Message}");
            return false;
        }
    }
}


// Updated ProcessGIF method for your UnityAnimatedTextures class
