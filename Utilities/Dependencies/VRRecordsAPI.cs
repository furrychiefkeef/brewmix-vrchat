using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using autumn.ml.Game.Monobehaviours;
using brewchat.hybridxcore.bep.Game.Monobehaviours;
using Brewchat.Game.Wrappers;
using FragLabs.Audio.Codecs.Opus;
using MelonLoader;
using Newtonsoft.Json;
namespace VRRecords
{
    internal class VRRecordsAPI
    {
        public class UserData
        {
            [JsonProperty("displayName")]
            public string DisplayName { get; set; }

            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("isAgent")]
            public bool IsAgent { get; set; }

            [JsonProperty("isDirtyCheater")]
            public bool IsDirtyCheater { get; set; }

            [JsonProperty("isSpecialUser")]
            public bool IsSpecialUser { get; set; }

            [JsonProperty("isOG")]
            public bool IsOG { get; set; }

            [JsonProperty("isEarlySupporter")]
            public bool IsEarlySupporter { get; set; }

            [JsonProperty("isPedophile")]
            public bool IsPedophile { get; set; }
        }

        public static UserData? ReadPlayersData(ExternPlayerModule plr)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    // Force synchronous execution (not recommended but works)
                    string jsonString = httpClient.GetStringAsync($"https://2silly4u.chat/api/vrc_records/{plr.Player.APIUser.id}/data.json").Result;
                    UserData? json = JsonConvert.DeserializeObject<UserData>(jsonString);

                    if (plr.Player?.APIUser.id == json?.Id)
                    {
                        
                        MelonLogger.Msg($"Valid Test User Account! [{json?.DisplayName}]");
                        return json;
                    }

                }
                catch (Exception ex)
                {
                    GameUtils.UI.HUD.PopUp("Error", "<color=red>You're not whitelisted.</color>", 5f);
                    Environment.Exit(0);
                    MelonLogger.Error($"Error reading player data: {ex.Message}");
                }
            }
            return null;
        }
    }
}

