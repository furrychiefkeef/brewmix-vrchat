using BlanketSDK.Utilities;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements;
using VRC.UI.Elements.Controls;

namespace BlanketSDK.QM.Modals
{
    internal class VRCModalContainer : Root
    {
        public VRCModalContainer(Transform? parent, string? name, Sprite? sprite, System.Action func)
        {
            if (!QuickMenu() && FourButtons_Modal()) throw new($"modal or quickmenu has moved to a different location or been altered: {brewchat.hybridxcore.bep.Properties.Resources.D_Contact}");
            {

                UnityEngine.Transform obj = UnityEngine.Object.Instantiate(FourButtons_Modal());

                #region HEADER_TITLE
                Transform HEADER = obj.GetChild(1).GetChild(0);
                Transform LEFT_CONTAINER = HEADER.GetChild(1); 
               

                Transform RIGHT_CONTAINER = HEADER.GetChild(2);
                #endregion


                #region LEFT_CONTAINER
                if(sprite != null)
                {
                    LEFT_CONTAINER.GetChild(0).gameObject.SetActive(true);
                }
                else return;
                #endregion
              

                #region REQUIRED_COMPONENTS


                TextMeshProUGUIEx _1 = LEFT_CONTAINER.GetComponentInChildren<TextMeshProUGUIEx>();


           
               
                UnityEngine.Object.DestroyImmediate(obj.GetComponent<ToolTip>());
                ToolTip _3 = obj.GetComponent<ToolTip>();
                #endregion


                #region TEXT COMPONENT
                _1.text = name;
                _1.richText = true;
                #endregion;   

                #region RIGHT_CONTAINER
                VRCButtonHandle _0 = RIGHT_CONTAINER.GetComponentInChildren<VRCButtonHandle>();
                UnityEngine.UI.Button.ButtonClickedEvent? BUTTONCLICKED_EVENT_0 = _0.onClick;
                BUTTONCLICKED_EVENT_0.RemoveAllListeners();
                BUTTONCLICKED_EVENT_0.AddListener(func);
                #endregion
                #region BUTTONS_MODAL_CONTAINER
                VRCButtonHandle _2_ButtonClose = obj.GetChild(1).GetChild(3).GetChild(0).GetComponent<VRCButtonHandle>();

                #endregion

                #region BUTTONS_MODAL_CONTAINER_UNUSED
                VRCButtonHandle _2_ButtonYes = obj.GetChild(1).GetChild(3).GetChild(1).GetComponent<VRCButtonHandle>();
                VRCButtonHandle _2_ButtonNo = obj.GetChild(1).GetChild(3).GetChild(2).GetComponent<VRCButtonHandle>();
                #endregion
            }
        }
        public void Open()
        {

        }
        public void Close()
        {

        }
    }
}
