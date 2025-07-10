using System;

namespace autumn.ml.Game.Monobehaviours;

public class RPlayerManager : UnityEngine.MonoBehaviour
{
    public RPlayerManager(IntPtr ptr) : base(ptr) { }
    public static RPlayerManager? Instance;
    public class PlayerData
    {
        public float Speed { get; set; }
        public float Gravity { get; set; }
        public float JumpHeight { get; set; }
        public float WalkSpeed { get; set; }
        public float RunSpeed { get; set; }
    }
    void Awake()
    {
        if (Instance == null) Instance = this;
    }
    void Update()
    {

    }

}
