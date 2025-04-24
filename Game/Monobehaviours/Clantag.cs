using UnityEngine;
using System.Collections.Generic;
using Brewchat.Game.Wrappers;

namespace PinkstarRecords
{
    public class PinkstarBehaviour : MonoBehaviour
    {
        // Clantag options
        private List<string> clanTags = new List<string>
        {
            "⭐ P I N K S T A R ⭐",
            "😎 P I N K S T A R 😎",
            "P I N K S T A R . R E C O R D S",
            "✨ P I N K S T A R ✨",
            "",  // Blank for blinking effect
            "P I N K S T A R",
            "",  // Blank for blinking effect
            "P I N K S T A R . R E C O R D S",
            "",  // Blank for blinking effect
            "＞︿＜ P I N K S T A R ＞︿＜",
            "🎵 P I N K S T A R . R E C O R D S 🎵",
            "💖 P 💖 I 💖 N 💖 K 💖 S 💖 T 💖 A 💖 R 💖",
            "",  // Blank for blinking effect
            "【P】【I】【N】【K】【S】【T】【A】【R】",
            "🔥 PINKSTAR.RECORDS 🔥",
            "",  // Blank for blinking effect
            "ℙ𝕀ℕ𝕂𝕊𝕋𝔸ℝ.ℝ𝔼ℂ𝕆ℝ𝔻𝕊",
            "░P░I░N░K░S░T░A░R░",
            "",  // Blank for blinking effect
            "▶️ PINKSTAR.RECORDS ◀️",
            "📀 PINKSTAR 📀",
            "",  // Blank for blinking effect
            "⚡ P I N K S T A R ⚡",
            "🎧 PINKSTAR.RECORDS 🎧",
            "",  // Blank for blinking effect
            "P_I_N_K_S_T_A_R",
            "🌟 PINKSTAR 🌟",
            "",  // Blank for blinking effect
            "『P』『I』『N』『K』『S』『T』『A』『R』",
            "✅ PINKSTAR.RECORDS ✅",
            "",  // Blank for blinking effect
            "⭐ P I N K S T A R ⭐"
        };
        

        private bool isEnabled = true;
        private float delay = 0.3f;
        private float lastChangeTime = 0f;
        private int currentTagIndex = 0;
        
        // This is the method you'll call manually from your hook

        private void LateUpdate() => ManualUpdate();

        public void ManualUpdate()
        {
                UpdateClantag();
        }
        
        // Toggle the clantag feature on/off
        public void ToggleClantag()
        {
            isEnabled = !isEnabled;
            
            // Clear tag when disabling
            if (!isEnabled)
            {
                try
                {
                    GameUtils.VRChatUtils.Events.SendEvent43_Chatbox("");
                }
                catch (System.Exception) { }
            }
        }
        
        // The core function that handles the clantag updates
        private void UpdateClantag()
        {
            float currentTime = Time.time;
            
            // Check if it's time for the next tag
            if (currentTime - lastChangeTime >= delay)
            {
                // Update the timer
                lastChangeTime = currentTime;
                
                // Send current tag
                try
                {
                    GameUtils.VRChatUtils.Events.SendEvent43_Chatbox(clanTags[currentTagIndex]);
                }
                catch (System.Exception) { }
                
                // Move to next tag in sequence
                currentTagIndex = (currentTagIndex + 1) % clanTags.Count;
            }
        }
        
        // Public methods to control from outside
        public void SetEnabled(bool enabled)
        {
            isEnabled = enabled;
        }
        
        public void SetDelay(float newDelay)
        {
            delay = newDelay;
        }
        
        // Method to set a specific clantag immediately
        public void SetSpecificTag(string tag)
        {
            try
            {
                GameUtils.VRChatUtils.Events.SendEvent43_Chatbox(tag);
            }
            catch (System.Exception) { }
        }
    }
}
