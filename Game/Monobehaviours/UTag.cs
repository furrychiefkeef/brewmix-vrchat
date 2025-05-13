using UnityEngine;

namespace Brewchat.Game.Monobehaviours;


/// <summary>
/// Universial Tags (NOT FOR VRCHAT)
/// </summary>
public class UniversialTags : UnityEngine.MonoBehaviour
{
    public static Dictionary<GameObject, string>? Objects { get; } = new(); 
    
    public void AddTagToObject(string tag)
    {
        Objects?.Add(transform.gameObject, tag);
    }
    public static void Destory(string id)
    {
        Objects?.ToList().ForEach(x =>
        {
            if (x.Value == id) Destroy(x.Key);
        });
    }
}
        
   
