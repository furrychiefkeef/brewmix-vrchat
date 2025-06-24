using System.Collections;

namespace Brewchat.Game.Monobehaviours;

using System.Diagnostics;
using Brewchat.Cheat.Modules;
using Brewchat.Game.Wrappers;
using MelonLoader;
using Photon.Realtime;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;

public class ESP : MonoBehaviour
{
    public ESP(IntPtr ptr) : base(ptr) {}
    public static ESP? Instance { get; private set; }
        // public static PlayerNameplate _nameplate; // Unused field removed

        private void Awake()
        {
            if (Instance == null) // Use == null for Unity objects
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // Apply to gameObject, not 'this' directly for MonoBehaviours
            }
            else Destroy(this);
        }
    public SkinnedMeshRenderer[]? _avatarender;
    public VRC_Pickup? _pickup;
    public Dictionary <string, MonoBehaviour2PublicSealedReInreInblreSiInCoblUnique>? _unused;
    
    public MonoBehaviour2PublicSealedReInreInblreSiInCoblUnique? FX_Entities;
     public MonoBehaviour2PublicSealedReInreInblreSiInCoblUnique? FX_Pickups;
    public static List<VRC.Player> Players {get; set;} = new();
   
    void Start() {
        
    }
    void OnEnable() 
    {
        if(FX_Entities == null && FX_Pickups == null) 
        {
            FX_Entities = GameUtils.Camera.Get.gameObject.AddComponent<MonoBehaviour2PublicSealedReInreInblreSiInCoblUnique>();
            FX_Pickups = GameUtils.Camera.Get.gameObject.AddComponent<MonoBehaviour2PublicSealedReInreInblreSiInCoblUnique>();
           

            FX_Entities.blurSize = 0.1f;
            FX_Entities.field_Protected_Color_0 = GetCurrentThemeData.Highlight;

            FX_Pickups.blurSize = FX_Entities.blurSize;
            FX_Pickups.field_Protected_Color_0 = EngineUtils.Conversion.HexToColor("#fa590f");
        }

    }
   

    public void Add(VRCPickup pickup)  => HighlightPickups(pickup);
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
            if(enable) 
            {
                
                FX_Entities?.field_Protected_HashSet_1_MeshFilter_0?.AddIfNotPresent(Capsule.GetComponent<MeshFilter>());
                return;
            }
            else 
            {
                FX_Entities?.field_Protected_HashSet_1_MeshFilter_0?.Remove(Capsule.GetComponent<MeshFilter>());
                return;
            }
        }
    }
    void HighlightPickups(VRCPickup pickup, bool enable = true)
    {
        if(_pickup == null) _pickup = pickup;
        if (GameUtils.Camera.Get != null)
        {
            if(enable) 
            {
                FX_Pickups?.field_Protected_HashSet_1_MeshFilter_0?.AddIfNotPresent(_pickup.GetComponent<Renderer>().GetComponent<MeshFilter>());
                return;
            }
            else 
            {
                FX_Pickups?.field_Protected_HashSet_1_MeshFilter_0?.Remove(_pickup.GetComponent<Renderer>().GetComponent<MeshFilter>());
                return;
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

    
    public void AddNameplateOverlay(VRC.Player player) => Nameplates(player, Enable);
    public void AddPlayerOverlay(VRC.Player player) => Player(player, Enable);

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
