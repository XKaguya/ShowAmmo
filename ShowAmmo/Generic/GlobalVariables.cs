using GameNetcodeStuff;

#pragma warning disable 1591

namespace ShowAmmo.Generic
{
    public class GlobalVariables
    {
        public static HUDManager? HUDManagerInstance { get; set; }

        public static PlayerControllerB? Player { get; set; }
        
        public static int Language { get; set; }
    }
}