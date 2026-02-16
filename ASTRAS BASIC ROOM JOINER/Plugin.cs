using Gorilla_Time.Safe;
using Gorilla_Time.Core;
using UnityEngine;
using BepInEx;

namespace Gorilla_Time.Plugin;

[BepInPlugin(Constantss.GUID, Constantss.Name, Constantss.Version)]
public class plugin : BaseUnityPlugin
{
    private void Start()
    {
        Pacther.Apply();
    }

    private void Awake()
    {
        GameObject TIME = new GameObject(Constantss.Name);
        TIME.AddComponent<Main>();
        DontDestroyOnLoad(TIME);
    }
}