namespace Brewchat.Game.Monobehaviours;

internal class LinbugView : UnityEngine.MonoBehaviour
{
    public static LinbugView Instance { get; set; }
    public void Awake()
    {
        if (Instance is null)
        {
            Instance = this;
            UnityEngine.Object.DontDestroyOnLoad(this);
        }
        else UnityEngine.Object.Destroy(this);
    }

  

    public void OnGUI()
    {
        
    }
}