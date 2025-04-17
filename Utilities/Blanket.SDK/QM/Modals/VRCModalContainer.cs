using BlanketSDK.Utilities;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VRC.Udon.Wrapper.Modules;
using VRC.UI.Elements;
using VRC.UI.Elements.Controls;
using VRC.UI.Elements.Menus;

namespace BlanketSDK.QM.Modals
{
    internal class VRCModalContainer : Root
    {
        public static Transform? _obj;
        public static QMSafetyPage ModalLogic;
        public Transform? container;
        public VRCModalContainer(string? header, Sprite? sprite = null)
        {
            if (!QuickMenu() && FourButtons_Modal()) throw new($"modal or quickmenu has moved to a different location or been altered: " +
                $"{brewchat.hybridxcore.bep.Properties.Resources.D_Contact}");
         
            _obj = UnityEngine.Object.Instantiate(FourButtons_Modal(), QMParent());

            _obj.gameObject.SetActive(false);
            _obj.name = $"[Skate_QM_Modal]->{header}";

            #region LOGIC_REQUIRED

            _obj.GetComponent<QMSafetyPage>().enabled = true;
            _obj.GetComponent<MenuPanelRect>().enabled = true;
            _obj.GetComponent<VRCRectMask2D>().enabled = true;
            _obj.GetComponent<GraphicRaycaster>().enabled = true;
            _obj.GetComponent<Canvas>().enabled = true;
            _obj.GetComponent<CanvasGroup>().enabled = true;
            #endregion

            #region HEADER_TITLE
            Transform HEADER = _obj.GetChild(1).GetChild(0);

            Transform LEFT_CONTAINER = HEADER.GetChild(1);
            Transform RIGHT_CONTAINER = HEADER.GetChild(2);
            #endregion

            #region LEFT_CONTAINER
            if (sprite != null) LEFT_CONTAINER.GetChild(0).gameObject.SetActive(true);
            #endregion

            #region RIGHT_CONTAINER
            /* VRCButtonHandle _0 = RIGHT_CONTAINER.GetComponentInChildren<VRCButtonHandle>();
             UnityEngine.UI.Button.ButtonClickedEvent? BUTTONCLICKED_EVENT_0 = _0.onClick;
             BUTTONCLICKED_EVENT_0.RemoveAllListeners();
             BUTTONCLICKED_EVENT_0.AddListener(func);*/
            #endregion

            #region REQUIRED_COMPONENTS

            ModalLogic = _obj.GetComponent<QMSafetyPage>();
            TextMeshProUGUIEx _1 = LEFT_CONTAINER.GetComponentInChildren<TextMeshProUGUIEx>();

            #region BUTTON_CONTAINER_LOGIC_FOR_API
            Transform BUTTON_SHEILD_LEVEL_CONTAINER = _obj.GetChild(1).GetChild(2).DestroyChildren();
            if (container == null) UnityEngine.Object.DestroyImmediate(BUTTON_SHEILD_LEVEL_CONTAINER);
            container = BUTTON_SHEILD_LEVEL_CONTAINER.transform;
            #endregion

            #endregion

            #region TEXT COMPONENT_HEADER
            _1.text = header;
            _1.richText = true;
            #endregion;   


            #region BUTTONS_MODAL_CONTAINER
            VRCButtonHandle _2_ButtonClose = _obj.GetChild(1).GetChild(3).GetChild(0).GetComponent<VRCButtonHandle>();
            UnityEngine.UI.Button.ButtonClickedEvent? BUTTONCLICKED_EVENT_NO = _2_ButtonClose.onClick;

            BUTTONCLICKED_EVENT_NO.RemoveAllListeners();
            System.Action close_button = () => Close();
            BUTTONCLICKED_EVENT_NO?.AddListener(close_button);
            #endregion

            #region BUTTONS_MODAL_CONTAINER_UNUSED
            /*    VRCButtonHandle _2_ButtonYes = obj.GetChild(1).GetChild(3).GetChild(1).GetComponent<VRCButtonHandle>();
                VRCButtonHandle _2_ButtonNo = obj.GetChild(1).GetChild(3).GetChild(2).GetComponent<VRCButtonHandle>();*//*
            #endregion*/
            #endregion

        }


        public void Open()
        {
            _obj.GetComponent<QMSafetyPage>().enabled = true;
            _obj.GetComponent<MenuPanelRect>().enabled = true;
            _obj.GetComponent<VRCRectMask2D>().enabled = true;
            _obj.GetComponent<GraphicRaycaster>().enabled = true;
            _obj.GetComponent<Canvas>().enabled = true;
            _obj.GetComponent<CanvasGroup>().enabled = true;
            _obj.gameObject.SetActive(true);
            ModalLogic.Method_Protected_Void_Boolean_TransitionType_0(true, UIPage.TransitionType.InPlace);
        }
        void Close() => ModalLogic.Method_Protected_Void_Boolean_TransitionType_0(false, UIPage.TransitionType.Bottom);
    }
}