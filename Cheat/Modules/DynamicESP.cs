using Brewchat.Game.Wrappers;

namespace Brewchat.Cheat.Modules;

public class DynamicESP
{
    static VRC.Player[] players;
    public static void Players()
    {
        players = GameUtils.PlayerModel.Matchmaking.GetPlayers.ToArray();
        /*if (localplayer.prop_APIUser_0.IsSelf)
        {
            foreach (VRC.Player _player in players)
            {
                
            }
        }*/

    }   
}