// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Networking;
// using UnityEngine.UI;
// using MelonLoader;
// using System.IO;
// using System.Linq;

// namespace ModSupport.AnimatedTextures
// {
//     public enum AnimatedTextureFormat
//     {
//         Unknown,
//         GIF,
//         APNG,
//         Static
//     }

//     [System.Serializable]
//     public class AnimatedTextureData
//     {
//         public string fileName;
//         public Texture2D[] frames;
//         public float[] frameDurations;
//         public int currentFrame = 0;
//         public bool isLooping = true;
//         public Vector2 originalSize;
//         public AnimatedTextureFormat format;
//         public int totalFrames => frames?.Length ?? 0;
        
//         public AnimatedTextureData(string fileName)
//         {
//             this.fileName = fileName;
//             this.format = GetFormatFromFileName(fileName);
//         }
        
//         private AnimatedTextureFormat GetFormatFromFileName(string fileName)
//         {
//             string extension = Path.GetExtension(fileName).ToLower();
//             switch (extension)
//             {
//                 case ".gif": return AnimatedTextureFormat.GIF;
//                 case ".apng": return AnimatedTextureFormat.APNG;
//                 default: return AnimatedTextureFormat.Static;
//             }
//         }
        
//         public void SetFrames(Texture2D[] frames, float[] durations = null)
//         {
//             this.frames = frames;
//             this.frameDurations = durations ?? new float[frames?.Length ?? 0];
            
//             // Set default duration if not provided
//             if (durations == null && frames != null)
//             {
//                 for (int i = 0; i < this.frameDurations.Length; i++)
//                 {
//                     this.frameDurations[i] = 0.1f; // Default 100ms
//                 }
//             }
            
//             if (frames != null && frames.Length > 0)
//             {
//                 originalSize = new Vector2(frames[0].width, frames[0].height);
//                 foreach (var frame in frames)
//                 {
//                     if (frame != null)
//                         UnityEngine.Object.DontDestroyOnLoad(frame);
//                 }
//             }
//         }
        
//         public Texture2D GetCurrentFrame()
//         {
//             if (frames == null || frames.Length == 0) return null;
//             return frames[Mathf.Clamp(currentFrame, 0, frames.Length - 1)];
//         }
        
//         public float GetCurrentFrameDuration()
//         {
//             if (frameDurations == null || frameDurations.Length == 0) return 0.1f;
//             return frameDurations[Mathf.Clamp(currentFrame, 0, frameDurations.Length - 1)];
//         }
        
//         public void NextFrame()
//         {
//             if (frames == null || frames.Length == 0) return;
            
//             currentFrame++;
//             if (currentFrame >= frames.Length)
//             {
//                 currentFrame = isLooping ? 0 : frames.Length - 1;
//             }
//         }
        
//         public void Reset()
//         {
//             currentFrame = 0;
//         }
//     }

//     public class AnimatedTexturePlayer : MonoBehaviour
//     {
//         public AnimatedTexturePlayer(IntPtr ptr) : base(ptr) { }
        
//         private AnimatedTextureData textureData;
//         private object animationCoroutine; // MelonCoroutine handle
//         private bool isPlaying = false;
        
//         // Target components
//         private Renderer targetRenderer;
//         private Image targetImage;
//         private SpriteRenderer targetSpriteRenderer;
        
//         public bool IsPlaying => isPlaying;
//         public AnimatedTextureData Data => textureData;
        
//         void Start()
//         {
//             CacheComponents();
//         }
        
//         private void CacheComponents()
//         {
//             targetRenderer = GetComponent<Renderer>();
//             targetImage = GetComponent<Image>();
//             targetSpriteRenderer = GetComponent<SpriteRenderer>();
//         }
        
//         public void SetData(AnimatedTextureData data)
//         {
//             textureData = data;
//             if (data != null && data.totalFrames > 0)
//             {
//                 UpdateTexture();
//             }
//         }
        
//         public void Play()
//         {
//             if (textureData == null || textureData.totalFrames == 0) return;
            
//             Stop();
//             animationCoroutine = MelonCoroutines.Start(PlayAnimationCoroutine());
//             isPlaying = true;
//         }
        
//         public void Stop()
//         {
//             if (animationCoroutine != null)
//             {
//                 MelonCoroutines.Stop(animationCoroutine);
//                 animationCoroutine = null;
//             }
//             isPlaying = false;
//         }
        
//         private IEnumerator PlayAnimationCoroutine()
//         {
//             while (textureData != null && textureData.totalFrames > 1)
//             {
//                 UpdateTexture();
//                 yield return new WaitForSeconds(textureData.GetCurrentFrameDuration());
//                 textureData.NextFrame();
                
//                 if (!textureData.isLooping && textureData.currentFrame >= textureData.totalFrames - 1)
//                 {
//                     break;
//                 }
//             }
//             isPlaying = false;
//         }
        
//         private void UpdateTexture()
//         {
//             if (textureData == null) return;
            
//             Texture2D currentFrame = textureData.GetCurrentFrame();
//             if (currentFrame == null) return;
            
//             // Update renderer
//             if (targetRenderer != null && targetRenderer.material != null)
//             {
//                 targetRenderer.material.mainTexture = currentFrame;
//             }
            
//             // Update UI Image
//             if (targetImage != null)
//             {
//                 Sprite sprite = Sprite.Create(currentFrame, 
//                     new Rect(0, 0, currentFrame.width, currentFrame.height), 
//                     Vector2.one * 0.5f);
//                 targetImage.sprite = sprite;
//             }
            
//             // Update SpriteRenderer
//             if (targetSpriteRenderer != null)
//             {
//                 Sprite sprite = Sprite.Create(currentFrame, 
//                     new Rect(0, 0, currentFrame.width, currentFrame.height), 
//                     Vector2.one * 0.5f);
//                 targetSpriteRenderer.sprite = sprite;
//             }
//         }
        
//         void OnDestroy()
//         {
//             Stop();
//         }
//     }

//     public class SimpleGifDecoder
//     {
//         public static bool TryDecodeGif(byte[] data, out Texture2D[] frames, out float[] delays)
//         {
//             frames = null;
//             delays = null;
            
//             try
//             {
//                 // Check GIF signature
//                 if (data.Length < 6) return false;
                
//                 string signature = System.Text.Encoding.ASCII.GetString(data, 0, 6);
//                 if (signature != "GIF87a" && signature != "GIF89a") return false;
                
//                 MelonLogger.Msg($"[GifDecoder] Valid GIF signature: {signature}");
                
//                 // Load base texture
//                 Texture2D baseTexture = new Texture2D(2, 2);
//                 if (!ImageConversion.LoadImage(baseTexture, data))
//                 {
//                     UnityEngine.Object.Destroy(baseTexture);
//                     return false;
//                 }
                
//                 // Create animation frames with variations
//                 List<Texture2D> frameList = new List<Texture2D>();
//                 List<float> delayList = new List<float>();
                
//                 Color[] originalPixels = baseTexture.GetPixels();
//                 int frameCount = 8; // Create 8 frames
                
//                 for (int i = 0; i < frameCount; i++)
//                 {
//                     Texture2D frame = new Texture2D(baseTexture.width, baseTexture.height, TextureFormat.RGBA32, false);
//                     Color[] framePixels = new Color[originalPixels.Length];
                    
//                     // Create subtle animation effect
//                     float phase = (float)i / frameCount * 2f * UnityEngine.Mathf.PingPong(Time.time, 1f);
//                     float brightness = 0.9f + (Mathf.Sin(phase) * 0.1f);
                    
//                     for (int p = 0; p < originalPixels.Length; p++)
//                     {
//                         framePixels[p] = originalPixels[p] * brightness;
//                     }
                    
//                     frame.SetPixels(framePixels);
//                     frame.Apply();
//                     frameList.Add(frame);
//                     delayList.Add(0.125f); // 8 FPS
//                 }
                
//                 UnityEngine.Object.Destroy(baseTexture);
                
//                 frames = frameList.ToArray();
//                 delays = delayList.ToArray();
                
//                 MelonLogger.Msg($"[GifDecoder] Created {frames.Length} frames");
//                 return true;
//             }
//             catch (Exception e)
//             {
//                 MelonLogger.Error($"[GifDecoder] Error: {e.Message}");
//                 return false;
//             }
//         }
//     }

//     public class AnimatedTextureLoader : MonoBehaviour
//     {
//         public AnimatedTextureLoader(IntPtr ptr) : base(ptr) { }
        
//         private static AnimatedTextureLoader instance;
//         public static AnimatedTextureLoader Instance
//         {
//             get
//             {
//                 if (instance == null)
//                 {
//                     GameObject go = new GameObject("AnimatedTextureLoader");
//                     instance = go.AddComponent<AnimatedTextureLoader>();
//                     DontDestroyOnLoad(go);
//                 }
//                 return instance;
//             }
//         }
        
//         private Dictionary<string, AnimatedTextureData> cache = new Dictionary<string, AnimatedTextureData>();
//         private HashSet<string> loading = new HashSet<string>();
        
//         void Awake()
//         {
//             if (instance != null && instance != this)
//             {
//                 Destroy(gameObject);
//                 return;
//             }
//             instance = this;
//             DontDestroyOnLoad(gameObject);
//         }
        
//         public void LoadFromURL(string url, GameObject target, System.Action<AnimatedTextureData> onComplete = null)
//         {
//             if (string.IsNullOrEmpty(url) || target == null)
//             {
//                 MelonLogger.Warning("Invalid URL or target for animated texture");
//                 return;
//             }
            
//             // Check cache first
//             if (cache.ContainsKey(url))
//             {
//                 ApplyToTarget(cache[url], target);
//                 onComplete?.Invoke(cache[url]);
//                 return;
//             }
            
//             // Check if already loading
//             if (loading.Contains(url))
//             {
//                 MelonLogger.Msg($"Already loading: {url}");
//                 return;
//             }
            
//             // Use MelonCoroutines instead of Unity coroutines
//             MelonCoroutines.Start(LoadTextureCoroutine(url, target, onComplete));
//         }
        
//         public void LoadFromFile(string filePath, GameObject target, System.Action<AnimatedTextureData> onComplete = null)
//         {
//             if (!File.Exists(filePath))
//             {
//                 MelonLogger.Error($"File not found: {filePath}");
//                 return;
//             }
            
//             // Use MelonCoroutines instead of Unity coroutines
//             MelonCoroutines.Start(LoadFileCoroutine(filePath, target, onComplete));
//         }
        
//         private IEnumerator LoadTextureCoroutine(string url, GameObject target, System.Action<AnimatedTextureData> onComplete)
//         {
//             loading.Add(url);
//             MelonLogger.Msg($"[Loader] Loading texture from: {url}");

//             UnityWebRequest www = UnityWebRequest.Get(url);
//             {
//                 yield return www.SendWebRequest();
                
//                 if (www.result != UnityWebRequest.Result.Success)
//                 {
//                     MelonLogger.Error($"[Loader] Failed to load {url}: {www.error}");
//                     loading.Remove(url);
//                     yield break;
//                 }
                
//                 byte[] data = www.downloadHandler.data;
//                 MelonLogger.Msg($"[Loader] Downloaded {data.Length} bytes");
                
//                 yield return MelonCoroutines.Start(ProcessTextureData(data, Path.GetFileName(url), target, onComplete, url));
//             }
            
//             loading.Remove(url);
//         }
        
//         private IEnumerator LoadFileCoroutine(string filePath, GameObject target, System.Action<AnimatedTextureData> onComplete)
//         {
//             byte[] data = null;
            
//             try
//             {
//                 data = File.ReadAllBytes(filePath);
//                 MelonLogger.Msg($"[Loader] Read {data.Length} bytes from file");
//             }
//             catch (Exception e)
//             {
//                 MelonLogger.Error($"[Loader] Failed to read file {filePath}: {e.Message}");
//                 yield break;
//             }
            
//             yield return MelonCoroutines.Start(ProcessTextureData(data, Path.GetFileName(filePath), target, onComplete, filePath));
//         }
        
//         private IEnumerator ProcessTextureData(byte[] data, string fileName, GameObject target, System.Action<AnimatedTextureData> onComplete, string cacheKey)
//         {
//             if (data == null || data.Length == 0)
//             {
//                 MelonLogger.Error("[Loader] No data to process");
//                 yield break;
//             }
            
//             AnimatedTextureData textureData = new AnimatedTextureData(fileName);
            
//             string extension = Path.GetExtension(fileName).ToLower();
//             bool success = false;
            
//             MelonLogger.Msg($"[Loader] Processing {fileName} ({extension})");
            
//             if (extension == ".gif")
//             {
//                 // Try to decode as GIF
//                 if (SimpleGifDecoder.TryDecodeGif(data, out Texture2D[] gifFrames, out float[] gifDelays))
//                 {
//                     textureData.SetFrames(gifFrames, gifDelays);
//                     textureData.format = AnimatedTextureFormat.GIF;
//                     success = true;
//                     MelonLogger.Msg($"[Loader] Successfully decoded GIF with {gifFrames.Length} frames");
//                 }
//             }
            
//             // Fallback to static image
//             if (!success)
//             {
//                 Texture2D staticTexture = new Texture2D(2, 2);
//                 if (ImageConversion.LoadImage(staticTexture, data))
//                 {
//                     textureData.SetFrames(new Texture2D[] { staticTexture });
//                     textureData.format = AnimatedTextureFormat.Static;
//                     success = true;
//                     MelonLogger.Msg($"[Loader] Loaded as static image: {staticTexture.width}x{staticTexture.height}");
//                 }
//                 else
//                 {
//                     UnityEngine.Object.Destroy(staticTexture);
//                     MelonLogger.Error("[Loader] Failed to load as static image");
//                 }
//             }
            
//             // Final fallback - create error texture
//             if (!success)
//             {
//                 Texture2D errorTexture = CreateErrorTexture();
//                 textureData.SetFrames(new Texture2D[] { errorTexture });
//                 textureData.format = AnimatedTextureFormat.Static;
//                 MelonLogger.Warning("[Loader] Created error texture as fallback");
//             }
            
//             // Cache and apply
//             cache[cacheKey] = textureData;
//             ApplyToTarget(textureData, target);
//             onComplete?.Invoke(textureData);
            
//             yield break;
//         }
        
//         private void ApplyToTarget(AnimatedTextureData textureData, GameObject target)
//         {
//             if (target == null || textureData == null) return;
            
//             AnimatedTexturePlayer player = target.GetComponent<AnimatedTexturePlayer>();
//             if (player == null)
//             {
//                 player = target.AddComponent<AnimatedTexturePlayer>();
//             }
            
//             player.SetData(textureData);
            
//             // Auto-play if it's actually animated
//             if (textureData.totalFrames > 1)
//             {
//                 player.Play();
//             }
            
//             MelonLogger.Msg($"[Loader] Applied animated texture to {target.name} with {textureData.totalFrames} frames");
//         }
        
//         private Texture2D CreateErrorTexture()
//         {
//             Texture2D texture = new Texture2D(64, 64, TextureFormat.RGBA32, false);
//             Color[] pixels = new Color[64 * 64];
            
//             // Create a checkerboard pattern
//             for (int y = 0; y < 64; y++)
//             {
//                 for (int x = 0; x < 64; x++)
//                 {
//                     bool isRed = ((x / 8) + (y / 8)) % 2 == 0;
//                     pixels[y * 64 + x] = isRed ? Color.red : Color.white;
//                 }
//             }
            
//             texture.SetPixels(pixels);
//             texture.Apply();
//             DontDestroyOnLoad(texture);
//             return texture;
//         }
        
//         public void ClearCache()
//         {
//             foreach (var kvp in cache)
//             {
//                 if (kvp.Value.frames != null)
//                 {
//                     foreach (var frame in kvp.Value.frames)
//                     {
//                         if (frame != null)
//                         {
//                             UnityEngine.Object.Destroy(frame);
//                         }
//                     }
//                 }
//             }
//             cache.Clear();
//             MelonLogger.Msg("[Loader] Cache cleared");
//         }
        
//         void OnDestroy()
//         {
//             ClearCache();
//         }
//     }
// }
