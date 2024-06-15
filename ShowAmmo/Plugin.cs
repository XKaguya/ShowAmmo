using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using LethalCompanyInputUtils.Api;
using ShowAmmo.Generic;
using ShowAmmo.Patch.HUD;
using ShowAmmo.Patch.Key;
using UnityEngine;
using UnityEngine.InputSystem;

#pragma warning disable 1591

namespace ShowAmmo
{
    [BepInPlugin(ModGuid, ModName, ModVersion)]
    [BepInDependency("com.rune580.LethalCompanyInputUtils")]
    public class Plugin : BaseUnityPlugin
    {
        private const string ModGuid = "Kaguya.ShowAmmo";
        private const string ModName = "ShowAmmo";
        private const string ModVersion = "1.0.0";

        private readonly Harmony _harmony = new(ModGuid);

        private static Plugin Instance;

        public static ManualLogSource mls;
        
        public static ConfigEntry<int> Language { get; private set; }

        public class InputKey : LcInputActions
        {
            [InputAction("<Keyboard>/v", Name = "Show ammo count")]
            public InputAction ToggleDeathBoxKey { get; set; }
        }

        internal static InputKey InputActionInstance = new();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(ModGuid);

            mls.LogInfo("ShowAmmo has loaded.");
            
            PatchAll();

            LoadConfig();
        }
        
        private void PatchAll()
        {
            _harmony.PatchAll(typeof(HUDManagerPostfixPatch));
            _harmony.PatchAll(typeof(KeyPatch));
        }

        private void LoadConfig()
        {
            Language = Config.Bind("Settings", "Language", 1, "Select the language for tips. Default is Chinese. Set to 0 for English.");
            GlobalVariables.Language = Language.Value;
        }
    }
}
