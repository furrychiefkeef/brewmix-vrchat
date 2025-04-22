namespace Brewchat.Game.Monobehaviours;

internal class LinbugView : UnityEngine.MonoBehaviour
{
    public static LinbugView Instance { get; set; }
    public void Awake()
    {
        if (Instance is null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(this);
    }

  

    public void OnGUI()
    {
        
    }
}