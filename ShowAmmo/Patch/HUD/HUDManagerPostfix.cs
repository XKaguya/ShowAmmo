using HarmonyLib;
using ShowAmmo.Generic;

#pragma warning disable 1591

namespace ShowAmmo.Patch.HUD
{
    [HarmonyPatch(typeof(HUDManager), "Awake")]
    internal static class HUDManagerPostfixPatch
    {
        static void Postfix(HUDManager __instance)
        {
            GlobalVariables.HUDManagerInstance = __instance;
        }
    }
}