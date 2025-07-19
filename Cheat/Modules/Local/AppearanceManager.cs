using System;
using System.Reflection;
using BlanketSDK.Utilities;
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
                List<GameObject> _3DElements = VRCUiPageLoading.field_Internal_Static_VRCUiPageLoading_0.transform.FindChild("3DElements").transform.GetChildren();
                _3DElements.ToList().ForEach(x => {  x.gameObject.layer = 0; }); // Overrender (Default Preventing Ur Meshes from being  covered)
                
                  

             
        
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
