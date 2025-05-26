using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using static UniGif;

public class AnimationManager : MonoBehaviour
{
    // Static cache for loaded GIFs
    private static Dictionary<string, GifData> loadedGifs = new Dictionary<string, GifData>();
    
    // Instance animations
    private Dictionary<Image, AnimationState> activeAnimations = new Dictionary<Image, AnimationState>();
    
    private void LateUpdate()
    {
        List<Image> toRemove = new List<Image>();
        
        foreach (var kvp in activeAnimations)
        {
            Image image = kvp.Key;
            AnimationState state = kvp.Value;
            
            if (image == null)
            {
                toRemove.Add(image);
                continue;
            }
            
            // Only animate if active
            if (!image.gameObject.activeInHierarchy)
                continue;
                
            state.timer += Time.deltaTime;
            
            if (state.timer >= state.currentDelay)
            {
                // Next frame
                state.currentFrame = (state.currentFrame + 1) % state.gifData.sprites.Count;
                
                // Set sprite
                image.sprite = state.gifData.sprites[state.currentFrame];
                
                // Set next delay
                state.currentDelay = state.gifData.delays[state.currentFrame];
                state.timer = 0f;
            }
        }
        
        // Clean up null references
        foreach (var img in toRemove)
        {
            activeAnimations.Remove(img);
        }
    }
    
    public void PlayAnimation(Image image, string gifPath)
    {
        if (image == null || string.IsNullOrEmpty(gifPath))
        {
            Debug.LogError("Invalid image or gif path");
            return;
        }
        
        // Stop existing animation
        if (activeAnimations.ContainsKey(image))
        {
            activeAnimations.Remove(image);
        }
        
        // Load GIF if not cached
        if (!loadedGifs.ContainsKey(gifPath))
        {
            LoadGifSync(gifPath);
        }
        
        // Start animation if GIF loaded successfully
        if (loadedGifs.ContainsKey(gifPath))
        {
            var gifData = loadedGifs[gifPath];
            var animState = new AnimationState
            {
                gifData = gifData,
                currentFrame = 0,
                currentDelay = gifData.delays[0],
                timer = 0f
            };
            
            activeAnimations[image] = animState;
            
            // Set first frame immediately
            image.sprite = gifData.sprites[0];
            
            Debug.Log($"Started animation with {gifData.sprites.Count} frames");
        }
        else
        {
            Debug.LogError($"Failed to load GIF: {gifPath}");
        }
    }
    
    private void LoadGifSync(string gifPath)
    {
        try
        {
            byte[] gifData = null;
            
            // Load file
            if (gifPath.StartsWith("http"))
            {
                Debug.LogError("HTTP URLs not supported in sync loading. Use local files only.");
                return;
            }
            else
            {
                if (!File.Exists(gifPath))
                {
                    Debug.LogError($"GIF file not found: {gifPath}");
                    return;
                }
                gifData = File.ReadAllBytes(gifPath);
            }
            
            Debug.Log($"Loaded {gifData.Length} bytes from {gifPath}");
            
            // Decode GIF synchronously
            List<GifTexture> frames = null;
            int loopCount = 0;
            int width = 0, height = 0;
            
            // Use UniGif's synchronous method if available, otherwise use callback
            var result = UnityGIF.GetCurrentFrame();
            
            if (frames == null || frames.Count == 0)
            {
                Debug.LogError("No frames decoded from GIF");
                return;
            }
            
            Debug.Log($"Decoded {frames.Count} frames from GIF");
            
            // Create sprites and delays
            List<Sprite> sprites = new List<Sprite>();
            List<float> delays = new List<float>();
            
            foreach (var frame in frames)
            {
                if (frame.m_texture2d != null)
                {
                    Sprite sprite = Sprite.Create(
                        frame.m_texture2d,
                        new Rect(0, 0, frame.m_texture2d.width, frame.m_texture2d.height),
                        new Vector2(0.5f, 0.5f)
                    );
                    sprites.Add(sprite);
                    delays.Add(frame.m_delaySec > 0 ? frame.m_delaySec : 0.1f);
                }
            }
            
            // Cache the GIF data
            loadedGifs[gifPath] = new GifData
            {
                sprites = sprites,
                delays = delays
            };
            
            Debug.Log($"Successfully cached GIF with {sprites.Count} sprites");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error loading GIF: {e.Message}\n{e.StackTrace}");
        }
    }
    
    public void StopAnimation(Image image)
    {
        if (activeAnimations.ContainsKey(image))
        {
            activeAnimations.Remove(image);
        }
    }
    
    public bool IsAnimating(Image image)
    {
        return activeAnimations.ContainsKey(image);
    }
    
    private void OnDestroy()
    {
        activeAnimations.Clear();
    }
    
    // Data structures
    private class GifData
    {
        public List<Sprite> sprites;
        public List<float> delays;
    }
    
    private class AnimationState
    {
        public GifData gifData;
        public int currentFrame;
        public float currentDelay;
        public float timer;
    }
}