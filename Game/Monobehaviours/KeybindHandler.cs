
using UnityEngine;


namespace Brewchat.Game.Monobehaviours;

internal class KeybindManager : UnityEngine.MonoBehaviour
{

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.F))
        {
            Config.Fly = !Config.Fly;
        }
    }

    /*void Fly()
    {
       
    }*/
}