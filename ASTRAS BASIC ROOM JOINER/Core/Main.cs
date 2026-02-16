
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gorilla_Time.Core;

public class Main : MonoBehaviour
{
    private enum TimeSettings
    {
        None,
        Morning,
        TenAM,
        Day,
        Evning,
        Night
    }
    private TimeSettings timeSettings = TimeSettings.None;
    private Rect Window = new Rect(110f, 110f, 220f, 220f);
    private Color WindowColor = new Color(0.15f, 0.15f, 0.15f, 1f);
    private Color ButtonColor = new Color(0.2f, 0.2f, 0.2f, 1f);
    private bool Open = false;
    private bool StylesINIT = false;
    private Texture2D? windowTex, solidTex;
    private GUIStyle? buttons, windowStyle;

    private void OnGUI()
    {
        if (!StylesINIT)
        {
            if (solidTex != null)
                return;
            
            solidTex = MakeTex(1, 1, new Color(0.15f, 0.15f, 0.15f, 1f));
            windowTex = MakeTex(1, 1, WindowColor);
            windowStyle = new GUIStyle(GUI.skin.window);
            windowStyle.normal.background = windowTex;
            windowStyle.hover.background = windowTex;
            windowStyle.active.background = windowTex;
            windowStyle.focused.background = windowTex;
            windowStyle.onNormal.background = windowTex;
            windowStyle.onHover.background = windowTex;
            windowStyle.onActive.background = windowTex;
            windowStyle.onFocused.background = windowTex;
            windowStyle.normal.textColor = Color.white;
            windowStyle.fontStyle = (FontStyle)1;
            Texture2D background = MakeTex(1, 1, ButtonColor);
            buttons = new GUIStyle(GUI.skin.button);
            buttons.normal.background = background;
            buttons.active.background = background;
            buttons.hover.background = background;
            buttons.focused.background = background;
            buttons.onNormal.background = background;
            buttons.onActive.background = background;
            buttons.onHover.background = background;
            buttons.onFocused.background = background;
            buttons.normal.textColor = Color.white;
            buttons.hover.textColor = Color.blue;
            buttons.active.textColor = Color.red;
            buttons.focused.textColor = Color.white;
            buttons.onNormal.textColor = Color.blue;
            buttons.onHover.textColor = Color.blue;
            buttons.onActive.textColor = Color.blue;
            buttons.onFocused.textColor = Color.blue;
            StylesINIT = true;
        }
        if (Open)
        {
            Window = GUILayout.Window(12232342, Window, UIMAKER, "Gorilla Time V2", windowStyle);
        }
    }

    private void Update()
    {

        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            Open = !Open;
        }
    }
    private void FixedUpdate()
    {
        SystemSwitch();
    }
    void UIMAKER(int id)
    {
        CONTROLLER();
        GUILayout.Space(5f);
        if (GUILayout.Button("Close", buttons))
        {
            Open = !Open;
        }
        GUI.DragWindow();
    }
    private void CONTROLLER()
    {
        if (GUILayout.Toggle(timeSettings == TimeSettings.Morning, "Morning"))
        {
            timeSettings = TimeSettings.Morning;
        }
        if (GUILayout.Toggle(timeSettings == TimeSettings.TenAM, "10AM"))
        {
            timeSettings = TimeSettings.TenAM;
        }
        if (GUILayout.Toggle(timeSettings == TimeSettings.Day, "Day"))
        {
            timeSettings = TimeSettings.Day;
        }
        if (GUILayout.Toggle(timeSettings == TimeSettings.Evning, "Evning"))
        {
            timeSettings = TimeSettings.Evning;
        }
        if (GUILayout.Toggle(timeSettings == TimeSettings.Night, "Night"))
        {
            timeSettings = TimeSettings.Night;
        }
    }
    private void SystemSwitch()
    {
        switch (timeSettings)
        {
            case TimeSettings.Morning:
                BetterDayNightManager.instance.SetTimeOfDay(1);
                break;
            case TimeSettings.TenAM:
                BetterDayNightManager.instance.SetTimeOfDay(3);
                break;
            case TimeSettings.Day:
                BetterDayNightManager.instance.SetTimeOfDay(4);
                break;
            case TimeSettings.Evning:
                BetterDayNightManager.instance.SetTimeOfDay(6);
                break;
            case TimeSettings.Night:
                BetterDayNightManager.instance.SetTimeOfDay(0);
                break;
        }
    }

    private Texture2D MakeTex(int width, int height, Color col)
    {
        Texture2D result = new Texture2D(width, height);
        for (int y = 0; y < height; y++) for (int x = 0; x < width; x++) result.SetPixel(x, y, col);
        result.Apply();
        return result;
    }
}