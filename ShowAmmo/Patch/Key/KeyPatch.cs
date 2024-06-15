using GameNetcodeStuff;
using HarmonyLib;
using ShowAmmo.Generic;

#pragma warning disable 1591

namespace ShowAmmo.Patch.Key
{
    [HarmonyPatch(typeof(PlayerControllerB), "Update")]
    internal static class KeyPatch
    {
        internal static void Postfix(PlayerControllerB __instance)
        {
            GlobalVariables.Player = __instance;
            
            if (!__instance.IsOwner && !__instance.isTestingPlayer)
            {
                return;
            }

            if (!__instance.isPlayerDead)
            {
                if (Plugin.InputActionInstance.ToggleDeathBoxKey.WasPressedThisFrame())
                {
                    API.DisplayShellsLoaded();
                }
            }
        }
    }
}   