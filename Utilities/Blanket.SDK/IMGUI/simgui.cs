

using UnityEngine;
using UNIXEngine.IL2CPPModding.Utils.Collections;

using Color = UnityEngine.Color;

namespace Syrup.IMGUI;


/// <summary>
/// SyrupIMGUI (Made by Me)
/// </summary>

public class simgui : MonoBehaviour
{
    private List<string> console { get; set; } = sconsole.text;
    static string console_flag = "[SYSTEM]";
    Rect console_container_rect = new (0, 0, 0, 0);
    bool CONSOLE_UI; 
    int fontSize = 11;
    private Dictionary<int, float> textAlpha = new Dictionary<int, float>();
   
    private string v = string.Empty;
    
    public static Vector2 scrollPosition = Vector2.zero;
    private float SH = 0;
    private int i = 0;
    #region RectTransform
    float yOffset = 5;
    float y = 13f;

    float xOffset = 0;
    float x = 5;

    #endregion

    void Awake()
    {
        string? introduct = $"{Guid.NewGuid().ToString()} {console_flag} HI!!!! <color=#9185E6><b>{Environment.UserName}</b></color> !!! get ready to make some noise :D";
        sconsole.print(introduct,0);
        sconsole.internal_log(new string('─',introduct.Length));    
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

            for (i = 0; i < sconsole.text.Count; i++)
            {
                if (!textAlpha.ContainsKey(i))
                {
                    textAlpha[i] = 0f;
                    StartCoroutine(AnimateText(i).WrapToIl2Cpp());
                    
                }
                
                GUIStyle style = new GUIStyle(sparameters.Text(fontSize));
                style.normal.textColor = new Color(style.normal.textColor.r, style.normal.textColor.g, style.normal.textColor.b, textAlpha[i]);
                GUI.Label(new Rect(x, yOffset + i * y, console_container_rect.width - 20, float.MaxValue), sconsole.text[i], style);
            }
            GUI.EndScrollView();
        }
    }
    
    System.Collections.IEnumerator AnimateText(int index)
    {
        while (textAlpha[index] < 1f)
        {
            textAlpha[index] = Mathf.Min(textAlpha[index] + Time.deltaTime * 2f, 1f);
            yield return null;
        }
    }

    void KeysHandler()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Home)) CONSOLE_UI = !CONSOLE_UI;
        if (UnityEngine.Input.GetKeyDown(KeyCode.PageUp)) { sconsole.clr(); }
    }
}

