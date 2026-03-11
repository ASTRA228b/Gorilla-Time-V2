
using Photon.Voice;
using UnityEngine;
using UnityEngine.InputSystem;
using static BetterDayNightManager;

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
    private Texture2D? WTex, BBackground;
    private GUIStyle? WStyle, BStyle;
    private Color WColor = new Color(0.1f, 0.1f, 0.1f, 1f);
    private Color BColor = new Color(0.2f, 0.2f, 0.2f, 1f);
    private Color SColor = new Color(0.15f, 0.15f, 0.15f, 1f);
    private Color STColor = new Color(0.0f, 0.6f, 1f, 1f);
    private bool Open = false;
    private bool StylesINIT = false;

    private void OnGUI()
    {
        if (!StylesINIT)
        {
            INIT();
            StylesINIT = true;
        }
        if (Open)
        {
            Window = GUILayout.Window(12232342, Window, UIMAKER, "Gorilla Time V2", WStyle);
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
        GTimeModController();
        GUILayout.Space(5f);
        if (GUILayout.Button("Close", BStyle))
        {
            Open = !Open;
        }
        GUI.DragWindow();
    }
    private void GTimeModController()
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
        if (GUILayout.Toggle(timeSettings == TimeSettings.Evning, "Evening"))
        {
            timeSettings = TimeSettings.Evning;
        }
        if (GUILayout.Toggle(timeSettings == TimeSettings.Night, "Night"))
        {
            timeSettings = TimeSettings.Night;
        }
        GUILayout.Space(5f);
        GUILayout.Label("Weather");
        if (GUILayout.Button("Start Rain", BStyle))
        {
            StartRain();
        }
        if (GUILayout.Button("Stop Rain", BStyle))
        {
            StopRain();
        }
    }

    private void StartRain()
    {
        var manager = BetterDayNightManager.instance;
        if (manager == null || manager.weatherCycle == null)
            return;
        for (int Yes = 1; Yes < manager.weatherCycle.Length; Yes++)
        {
            manager.weatherCycle[Yes] = (WeatherType)1;
        }
    }
    private void StopRain()
    {
        var manager = BetterDayNightManager.instance;
        if (manager == null || manager.weatherCycle == null)
            return;
        for (int No = 1; No < manager.weatherCycle.Length; No++)
        {
            manager.weatherCycle[No] = (WeatherType)0;
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

    private void INIT()
    {
        WTex = MakeTexture(1, 1, WColor);
        BBackground = MakeTexture(1, 1, BColor);
        WStyle = new GUIStyle(GUI.skin.window);
        WStyle.normal.background = WTex;
        WStyle.hover.background = WTex;
        WStyle.active.background = WTex;
        WStyle.focused.background = WTex;
        WStyle.onActive.background = WTex;
        WStyle.onNormal.background = WTex;
        WStyle.onFocused.background = WTex;
        WStyle.normal.textColor = Color.white;
        WStyle.fontStyle = FontStyle.Normal;
        BStyle = new GUIStyle(GUI.skin.button);
        BStyle.normal.background = BBackground;
        BStyle.active.background = BBackground;
        BStyle.hover.background = BBackground;
        BStyle.focused.background = BBackground;
        BStyle.onHover.background = BBackground;
        BStyle.onNormal.background = BBackground;
        BStyle.onActive.background = BBackground;
        BStyle.onFocused.background = BBackground;
        BStyle.normal.textColor = Color.white;
        BStyle.hover.textColor = Color.blue;
        BStyle.active.textColor = Color.red;
        BStyle.focused.textColor = Color.white;
        BStyle.onNormal.textColor = Color.blue;
        BStyle.onHover.textColor = Color.blue;
        BStyle.onActive.textColor = Color.blue;
        BStyle.onFocused.textColor = Color.blue;
    }

    private Texture2D MakeTexture(int WW, int HH, Color CC)
    {
        Texture2D value = new Texture2D(WW, HH);
        value.SetPixel(0, 0, CC);
        value.Apply();
        return value;
    }
}