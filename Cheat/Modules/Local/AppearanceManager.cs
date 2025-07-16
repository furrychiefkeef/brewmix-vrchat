using System;
using System.Reflection;
using Brewchat.Game.Wrappers;
using UnityEngine;

namespace autumn.ml.Cheat.Modules.Local;


public class AppearanceManager
{
    public struct Custom
    {
        public static void SetLoadingBundle()
        {
            LoadingScreen();
            Config.SaveToFile(Entry.config, "config.json");
        }
        static UnityEngine.GameObject? loadingScreenObj;
        public static void LoadingScreen()
        {
            if (loadingScreenObj == null)
            {
                loadingScreenObj = EngineUtils.Assets.AssetBundle(new(Entry.config.Appearance.inGame.LoadingScreen.Bundle.URL), true);
                loadingScreenObj.transform.parent = VRCUiPageLoading.field_Internal_Static_VRCUiPageLoading_0.transform;
                GameObject? Skybox = VRCUiPageLoading.field_Internal_Static_VRCUiPageLoading_0.transform.FindChild("3DElements").GetComponentInChildren<VRCGlobalRoot>().gameObject;
                Skybox.GetComponent<MeshRenderer>().material.color = Color.red;
                Skybox.layer = 0;

                // loadingScreenObj.GetComponentsInChildren<Transform>().ToList().ForEach(x =>
                // {
                //    x.gameObject.layer = GameUtils.Gameplay.RenderLayers.InternalUI;
                // });
                // loadingScreenObj.GetComponentsInChildren<ParticleSystem>().ToList().ForEach(x =>
                // {
                //    x.main.startColor = Entry.config.Appearance.inGame.VRCUI._lastSelectedColor;              
                // });


            }
        }

    }
    
    
}
