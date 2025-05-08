using BlanketSDK.Utilities;
using BlanketSDK.Core;
using UnityEngine;
using VRC.UI.Elements;
using VRC.UI.Elements.Controls;
using VRC.UI.Elements.Menus;

namespace BlanketSDK.QM.Elements.Modals
{
    internal class VRCModalContainer : IQuickMenuElements
    {
        public string Name;
        public Transform? _obj = null;
        public UIPage ModalLogic;
        public Transform? parent_menu;
        public Transform? parent_rightbuttons;
        public VRCModalContainer(string? header, Sprite? sprite = null)
        {
            if (!QuickMenu() | !QMElements.FourButtons_Modal()) throw new($"modal or quickmenu has moved to a different location or been altered: " + $"{brewchat.hybridxcore.bep.Properties.Resources.D_Contact}");
            _obj = UnityEngine.Object.Instantiate(QMElements.FourButtons_Modal(), QMParent());
            _obj.name = $"{Guid.NewGuid().ToString()}-<Blanket.SDK.MODAL_CONTAINER-{header}";
            Name = $"Menu_{header}";
           
            
        
            #region REQUIRED_COMPONENTS
            QMSafetyPage? _Instance = _obj.GetComponent<QMSafetyPage>();
            #endregion
            
            #region LOGIC_REQUIRED
            ModalLogic = _Instance;
            
            ModalLogic.enabled = false;
            ModalLogic.field_Public_String_0 = Name;
            ModalLogic.field_Private_Boolean_1 = true;
            ModalLogic.field_Private_List_1_UIPage_0.Add(ModalLogic);
            Il2CppSystem.Collections.Generic.Dictionary<Behaviour, bool>? behaviours = ModalLogic.field_Private_Dictionary_2_Behaviour_Boolean_0;
            behaviours.Clear();
            
            behaviours.Add(_obj.GetComponent<MenuPanelRect>(), true);
            behaviours.Add(_obj.GetComponent<UnityEngine.Canvas>(), true);
            behaviours.Add(_obj.GetComponent<UnityEngine.CanvasGroup>(), true);
            behaviours.Add(_obj.GetComponent<UnityEngine.UI.GraphicRaycaster>(), true);
            
            QM()?.field_Protected_MenuStateController_0.field_Private_Dictionary_2_String_UIPage_0.Add(Name, ModalLogic);
            #endregion

            #region HEADER_TITLE
            Transform HEADER = _obj.GetChild(1).GetChild(0);

            Transform LEFT_CONTAINER = HEADER.GetChild(1);
            Transform RIGHT_CONTAINER = HEADER.GetChild(2);
            #endregion

            #region LEFT_CONTAINER
            TextMeshProUGUIEx _1 = LEFT_CONTAINER.GetComponentInChildren<TextMeshProUGUIEx>();
            if (sprite != null) LEFT_CONTAINER.GetChild(0).gameObject.SetActive(true);
            #endregion

            #region RIGHT_CONTAINER

            Transform _0 = RIGHT_CONTAINER;
             if (_0.transform == null) UnityEngine.Object.DestroyImmediate(_0);
             else if (parent_rightbuttons == null) UnityEngine.Object.Destroy(RIGHT_CONTAINER);
             parent_rightbuttons = _0.transform;
            #endregion

     
            #region BUTTON_CONTAINER_LOGIC_FOR_API
            Transform BUTTON_SHEILD_LEVEL_CONTAINER = _obj.GetChild(1).GetChild(2).DestroyChildren();
            if (parent_menu == null) UnityEngine.Object.DestroyImmediate(BUTTON_SHEILD_LEVEL_CONTAINER);
            parent_menu = BUTTON_SHEILD_LEVEL_CONTAINER.transform;
            #endregion


            #region TEXT COMPONENT_HEADER
            _1.text = header;
            _1.richText = true;
            #endregion   


            #region BUTTONS_MODAL_CONTAINER
            VRCButtonHandle _2_ButtonClose = _obj.GetChild(1).GetChild(3).GetChild(0).GetComponent<VRCButtonHandle>();
            UnityEngine.UI.Button.ButtonClickedEvent? BUTTONCLICKED_EVENT_NO = _2_ButtonClose.onClick;
            #endregion

            #region BUTTONS_MODAL_CONTAINER_UNUSED
            /*    VRCButtonHandle _2_ButtonYes = obj.GetChild(1).GetChild(3).GetChild(1).GetComponent<VRCButtonHandle>();
                VRCButtonHandle _2_ButtonNo = obj.GetChild(1).GetChild(3).GetChild(2).GetComponent<VRCButtonHandle>();*//*
            #endregion*/
            #endregion

        }

        

            
        public void Open()
        { 
            QM()?.field_Protected_MenuStateController_0.Method_Public_Void_UIPage_UIContext_Boolean_TransitionType_0(ModalLogic, null, true, UIPage.TransitionType.InPlace);
        }
    }
}