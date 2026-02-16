using HarmonyLib;

namespace Gorilla_Time.Safe;

public class Pacther
{
    public static void Apply()
    {
        Harmony Har = new Harmony(Constantss.GUID);
        Har.PatchAll();
    }
}