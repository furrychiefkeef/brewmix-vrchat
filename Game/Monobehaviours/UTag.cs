namespace Brewchat.Game.Monobehaviours;

public class UTag : UnityEngine.MonoBehaviour
{
    void Awake(){}
    
    public string identifer { get; set; }
    public void SetTag(string tag)
    {
        identifer = tag;
    }
}