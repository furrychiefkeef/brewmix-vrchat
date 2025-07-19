using System.Collections;

using System.Diagnostics;
using Brewchat.Cheat.Modules;
using Brewchat.Game.Wrappers;
using MelonLoader;
using Photon.Realtime;
using Unity.Collections;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;

namespace Brewchat.Game.Monobehaviours;
public class ESP : MonoBehaviour
{
    public ESP(IntPtr ptr) : base(ptr) { }
    public static ESP? Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else Destroy(this);
    }
    public Dictionary <string, HighlightsFXStandalone>? FXs;
    public Color CurrentColor;
    public HighlightsFXStandalone? FX_Entities;
    public HighlightsFXStandalone? FX_Pickups;
    public static List<VRC.Player> Players {get; set;} = new();
    public static List<VRC_Pickup> Pickups {get; set;} = new();
   
    void OnEnable() 
    {
        if (FX_Entities == null && FX_Pickups == null)
        {

            FX_Entities = GameUtils.Camera.Get.gameObject.AddComponent<HighlightsFXStandalone>();
            FX_Pickups = GameUtils.Camera.Get.gameObject.AddComponent<HighlightsFXStandalone>();
            
        }

    }
    void Update()
    {
            FX_Pickups.enabled = enabled;
            FX_Entities.enabled = enabled;


            FX_Entities.blurDownsampleFactor = Entry.config.Render.Overlays.ESP.Parameters.blurDownsampleFactor;
            FX_Entities.blurSize = Entry.config.Render.Overlays.ESP.Parameters.blurSize;
            FX_Entities.blurIterations = Entry.config.Render.Overlays.ESP.Parameters.blurIterations;


            FX_Entities.field_Protected_Color_0 = Entry.config.Appearance.inGame.VRCUI._lastSelectedColor;
            FX_Pickups.field_Protected_Color_0 = Entry.config.Appearance.inGame.VRCUI._lastSelectedColor;

            FX_Pickups.blurDownsampleFactor = Entry.config.Render.Overlays.ESP.Parameters.blurDownsampleFactor;
            FX_Pickups.blurIterations = Entry.config.Render.Overlays.ESP.Parameters.blurIterations;
            FX_Pickups.blurSize = Entry.config.Render.Overlays.ESP.Parameters.blurSize;
    }
    void OnDisable()
    {
        FX_Entities.enabled = false;
        FX_Pickups.enabled = false;
    }

    public void Add(VRC_Pickup pickup)
    {
        HighlightPickups(pickup);
        Pickups.Add(pickup);
    }  
    public void Add(VRC.Player player) 
    {
        HighlightPlayerCapsule(player);
        Players.Add(player);
    }
    public void Remove(VRC.Player player) 
    {
        HighlightPlayerCapsule(player, false);
        Players.Remove(player);
    }
    public void PlayerESPClear() 
    {
        Players.Clear();
        FX_Entities.field_Protected_HashSet_1_MeshFilter_0.Clear();
    }
    public void PickupESPClear() => FX_Pickups.field_Protected_HashSet_1_MeshFilter_0.Clear();

    public void ClearAll()
    {
        PlayerESPClear();
    }
    void HighlightPlayerCapsule(VRC.Player player, bool enable = true)
    {
        PlayerSelector Capsule = player._laserSelectRegion;
        if (GameUtils.Camera.Get != null)
        {
            FX_Entities?.field_Protected_HashSet_1_MeshFilter_0?.AddIfNotPresent(Capsule.GetComponent<MeshFilter>());
            return;
        }
    }
    void HighlightPickups(VRC_Pickup pickup)
    {
        if (GameUtils.Camera.Get != null)
        {
            if (pickup.name != "MirrorPickup")
            {
                pickup.GetComponentsInChildren<MeshFilter>(false).ToList().ForEach((x =>
                {
                   FX_Pickups?.field_Protected_HashSet_1_MeshFilter_0?.AddIfNotPresent(x);
                }));
            }
           
        }
        }   
    }


internal class Wallhacks : MonoBehaviour
{

    public Wallhacks(IntPtr ptr) : base(ptr) {}
    public static Wallhacks? Instance { get; private set; }
      

        private void Awake()
        {
            if (Instance == null) // Use == null for Unity objects
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // Apply to gameObject, not 'this' directly for MonoBehaviours
            }
            else Destroy(this);
        }
    
    
    private SkinnedMeshRenderer _meshs;
    public bool Enable {get; set;} = true;

    
    public void AddNameplateOverlay(VRC.Player player) => Nameplates(player, enabled);
    public void AddPlayerOverlay(VRC.Player player) => Player(player, enabled);

    void Nameplates(VRC.Player player , bool enable = true) 
    {
             if (player != null) 
             {
               
                PlayerNameplate _nameplate = player._vrcplayer.Nameplate;
                 if(enable) 
                 {
                    _nameplate.NameplatePositioner.transform.GetChild(0).gameObject.layer = GameUtils.Gameplay.RenderLayers.InternalUI;
                    return;
                 }
                 else
                 {
                    _nameplate.NameplatePositioner.transform.GetChild(0).gameObject.layer = 0;
                    return;
                 }
                
             }   
    }

    void Player(VRC.Player player , bool enable = true) 
    {
        if(player != null) 
        {
                
            SkinnedMeshRenderer[] _avatarender = player.GetComponentsInChildren<SkinnedMeshRenderer>(false);
            foreach (SkinnedMeshRenderer meshs in _avatarender)
            {
                if(enable) 
                {
                    meshs.gameObject.layer = GameUtils.Gameplay.RenderLayers.InternalUI;
                }
                 else 
                {
                    meshs.gameObject.layer = 0;
                }
              }
            }
         }
    }
