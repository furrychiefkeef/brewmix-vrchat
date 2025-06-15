using Brewchat.Cheat.Modules;
using Brewchat.Game.Wrappers;
using Transmtn.DTO.Notifications;
using UnityEngine;
using UnityEngine.UI;
using VRC.Core;
using VRC.Localization;
using Resources = autumn.ml.Properties.Resources;

namespace Brewchat.Game.Monobehaviours;

internal class Toolbox : MonoBehaviour
{
    public Toolbox(IntPtr ptr) : base(ptr) {}
    public static Toolbox? Instance { get; private set; }
    private void Awake()
    {
        if (Instance is null)
        {
            Instance = this; DontDestroyOnLoad(this);
        }
        else Destroy(this);
    }
}

