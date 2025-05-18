using System;
using System.Collections.Generic;
using BlanketSDK.Core;
using BlanketSDK.QM.Elements.Controls.Operation;
using UnityEngine;
using VRC.UI.Elements;
using VRC.UI.Elements.Controls;
using Resources = brewchat.hybridxcore.bep.Properties.Resources;

internal class VRCChoiceSelection : IQuickMenuElements
{
    private RadialSelector radialSelector;
    private TextMeshProUGUIEx titleText;
    private List<ChoiceOption> choices = new List<ChoiceOption>();
    private int currentIndex = 0;
    
    public class ChoiceOption
    {
        public string Name { get; set; }
        public Action Callback { get; set; }
        
        public ChoiceOption(string name, Action callback)
        {
            Name = name;
            Callback = callback;
        }
    }
    
    public VRCChoiceSelection(VRCPanel vrcPanel, string name, System.Action callback = null)
    {
        // Your existing instantiation code
        if(!QM() || !QMParent() || !QMMenus.QMSubmenus.Menu_QM_GeneralSettings()) 
            throw new Exception($"{name} element has moved to a different location or index contact: {Resources.D_Contact}");
        
        var obj = UnityEngine.Object.Instantiate(QMControls.OptionSelection(), vrcPanel.container);
        
        var rightContainer = obj.GetChild(0); 
        var leftContainer = obj.GetChild(1);
        
        obj.name = $"{Guid.NewGuid().ToString()}-<Blanket.SDK.QM_CHOICE_SELECTION>";
        radialSelector = obj.GetComponent<RadialSelector>();

        titleText = leftContainer.GetComponentInChildren<TextMeshProUGUIEx>();
        titleText.text = name;
        titleText.richText = true;

        // Setup button callbacks
        radialSelector._leftButton.onClick.AddListener(new System.Action(PreviousChoice));
        radialSelector._rightButton.onClick.AddListener( new System.Action(NextChoice));
        
        // Add default callback if provided
        if (callback != null)
        {
            AddChoice("Default", callback);
        }
    }
    
    public VRCChoiceSelection AddChoice(string name, Action callback)
    {
        choices.Add(new ChoiceOption(name, callback));
        
        // If this is the first choice, display it
        if (choices.Count == 1)
        {
            UpdateDisplay();
        }
        
        return this; // For method chaining
    }
    
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
        }
    }
    
    private void ExecuteCurrentChoice()
    {
        if (choices.Count == 0) return;
        
        var currentChoice = choices[currentIndex];
        currentChoice.Callback?.Invoke();
    }
    
    // Helper methods for getting current selection
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
}