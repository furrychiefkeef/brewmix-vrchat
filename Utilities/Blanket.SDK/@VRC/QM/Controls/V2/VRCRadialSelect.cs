
using System.Runtime.InteropServices;
using BlanketSDK.Core;
using Brewchat.Game.Wrappers;
using UnityEngine;
using VRC.Localization;
using VRC.UI.Elements;
using VRC.UI.Elements.Controls;
using Resources = autumn.ml.Properties.Resources;
namespace BlanketSDK.QM.Elements.Controls.V2;
internal class VRCRadialSelect :  IQuickMenuElements
{
    RadialSelector radialSelector;
    TextMeshProUGUIEx titleText;
    List<ChoiceOption> choices = new();
    int currentIndex = 0;
    
    public class ChoiceOption
    {
        public string Name { get; set; }
        public string Tooltip { get; set; }
        public Action Callback { get; set; }
        public ChoiceOption(string name, string tooltip, Action callback)
        {
            Name = name;
            Tooltip = tooltip;
            Callback = callback;
        }
    }
    
    /// <summary>
    /// VRCRadialSelect this is used to create a RADIALSELECT_QUICKMENU_ELEMENT
    /// </summary>
    /// <param name="parent">The parent transform where the button will be placed.</param>
    /// <param name="name"> The name of the element. </param>
    /// <param name="callback"> ??? < /param>
    /// <exception cref="Exception"></exception>
    /// 
    public VRCRadialSelect(Transform parent, string name, [Optional] string? tooltip, [Optional] bool isChild,  System.Action callback = null)
    {
        // Your existing instantiation code
        if(!QM() || !QMParent() || !QMMenus.QMSubmenus.Menu_QM_GeneralSettings()) 
            throw new Exception($"{name} element has moved to a different location or index contact: {Resources.D_Contact}");
        /*parent ??= MMFallbacks._Fallback_Dashboard_Group_QM_UI_ChoiceSelection_Element();*/
        var obj = UnityEngine.Object.Instantiate(QMControls.OperatorRadialSelection(), parent);
        obj.name = $"{Guid.NewGuid()}-<Blanket.SDK.QM_CHOICE_SELECTION>";
        #region CONTAINERS

        var rightContainer = obj.GetChild(0); 
        var leftContainer = obj.GetChild(1);

        #endregion


        #region REQUIRED_COMPONENTS
        
        radialSelector = obj.GetComponent<RadialSelector>();

        
        #endregion

        #region TEXT_COMPONENT

        titleText = radialSelector._title;
        titleText.text = name;
        titleText.richText = true;

        #endregion

        #region TOOLTIP_COMPONENT
        radialSelector._tooltips[0]._localizableString = tooltip.Localizable();
        radialSelector._childIndicator.SetActive(isChild);
        #endregion
        
        #region BUTTONS_LOGIC

        radialSelector._leftButton.onClick.AddListener(new System.Action(PreviousChoice));
        radialSelector._rightButton.onClick.AddListener( new System.Action(NextChoice));

        #endregion


        #region IFFAILED_LOGIC

        // Add default callback if provided
        if (callback != null)
        {
            AddChoice("Default", "Default", callback);
        }
        #endregion
        
    }
    public VRCRadialSelect(VRCPanelGroupLayout panel, string name, [Optional] string? tooltip, [Optional] bool isChild,  System.Action callback = null) : this(panel._verticalLayoutGroup, name, tooltip, isChild, callback) {}
    
    public VRCRadialSelect AddChoice(string name, string tooltip, Action callback)
    {
        choices.Add(new ChoiceOption(name, tooltip, callback));

        // If this is the first choice, display it
        if (choices.Count == 1)
        {
            UpdateDisplay();
        }

        return this; // For method chaining
    }

    #region LOGIC

    private void PreviousChoice()
    {
        if (choices.Count == 0) return;
        
        currentIndex = (currentIndex - 1 + choices.Count) % choices.Count;
        UpdateDisplay();
        ExecuteCurrentChoice();
    }
    
    private void NextChoice()
    {
        if (choices.Count == 0) return;
        
        currentIndex = (currentIndex + 1) % choices.Count;
        UpdateDisplay();
        ExecuteCurrentChoice();
    }
    
    private void UpdateDisplay()
    {
        if (choices.Count == 0) return;
        
        var currentChoice = choices[currentIndex];
        // Update the display text to show current choice
        if (radialSelector != null && radialSelector._currentOptionText != null)
        {
            radialSelector._currentOptionText.text = currentChoice.Name;
            radialSelector._optionTooltip._localizableString = currentChoice.Tooltip.Localizable();
        }
    }
    
    private void ExecuteCurrentChoice()
    {
        if (choices.Count == 0) return;
        
        var currentChoice = choices[currentIndex];
        QM().field_Private_ModalAlert_0.Method_Public_Void_LocalizableString_PDM_0(choices[currentIndex].Name.Localizable());
        currentChoice.Callback?.Invoke();
    }

    #endregion
   
    
    // Helper methods for getting current selection

    #region DEV_ONLY_ADVANCED_FUNCS

    public string GetCurrentChoiceName()
    {
        return choices.Count > 0 ? choices[currentIndex].Name : "";
    }
    
    public int GetCurrentIndex()
    {
        return currentIndex;
    }
    
    public void SetChoice(int index)
    {
        if (index >= 0 && index < choices.Count)
        {
            currentIndex = index;
            UpdateDisplay();
        }
    }
    
    public void SetChoice(string name)
    {
        for (int i = 0; i < choices.Count; i++)
        {
            if (choices[i].Name == name)
            {
                currentIndex = i;
                UpdateDisplay();
                break;
            }
        }
    }

    #endregion
   
}