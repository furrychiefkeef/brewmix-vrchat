
using System;
using System.Drawing;
using Syrup;
using Syrup.IMGUI;
using UnityEngine;
using UnityEngine.Rendering;
using VRC.Fonts;
namespace Syrup.IMGUI;


/// <summary>
/// SyrupIMGUI (Made by Me)
/// </summary>

public class simgui : MonoBehaviour
{

    static string console_flag = "[SYSTEM]";
    Rect console_container_rect = new Rect(0, 0, 0, 0);
    bool CONSOLE_UI; 
    int fontSize = 14;
    private string v = string.Empty;
    
    public static Vector2 scrollPosition = Vector2.zero;
    private float SH = 0;

    #region RectTransform
    float yOffset = 5;
    float y = 13f;

    float xOffset = 0;
    float x = 5;

    #endregion

    void Start()
    {
        sconsole.print($"{ Guid.NewGuid().ToString() } {console_flag} HI!!!! <color=#9185E6><b>{Environment.UserName}</b></color> !!! get ready to make some noise :D",0);
    }
    void Update() => KeysHandler();

    void OnGUI()
    {
        if (!CONSOLE_UI)
        {
            SH = sconsole.text.Count * y;
            scrollPosition = GUI.BeginScrollView(
                new Rect(0, 0, Screen.width, 470),
                scrollPosition,
                new Rect(0, 0, console_container_rect.width, SH)
            ,false,false,GUIStyle.none,GUIStyle.none);

            for (int i = 0; i < sconsole.text.Count; i++)
            {
                GUI.Label(new Rect(x, yOffset + i * y, console_container_rect.width - 20, float.MaxValue), sconsole.text[i], sparameters.Text(fontSize));
            }
            GUI.EndScrollView();
        }
    }

    void KeysHandler()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Home)) CONSOLE_UI = !CONSOLE_UI;
        if (UnityEngine.Input.GetKeyDown(KeyCode.PageUp)) { sconsole.clr(); sconsole.internal_log("clear!"); sconsole.clr(); }
    }
}

