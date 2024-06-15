using System;
using System.Text;

#pragma warning disable 1591

namespace ShowAmmo.Generic
{
    public class API
    {
        private static int GetAmmoCount()
        {
            if (GlobalVariables.Player == null)
            {
                Plugin.mls.LogError("Player is null.");
                
                return -1;
            }
            
            GrabbableObject currentItem = GlobalVariables.Player.ItemSlots[GlobalVariables.Player.currentItemSlot];

            if (currentItem != null && currentItem.itemProperties.itemName == "Shotgun" || currentItem != null && currentItem.itemProperties.itemName == "霰弹枪")
            {
                ShotgunItem shotgunItem = (ShotgunItem)currentItem;
                
                return shotgunItem.shellsLoaded;
            }

            return -1;
        }

        private static (bool, string, string) GetMessage(int num, int language)
        {
            string msg;
            string head;
            bool shouldWarning = false;
            
            if (language == 1)
            {
                head = "霰弹枪";
                
                switch (num)
                {
                    case 0:
                        msg = $"枪里没有子弹";
                        shouldWarning = true;
                        break;
                    
                    default:
                        msg = $"枪里有{num}发子弹";
                        break;
                }
            }
            else
            {
                head = "Shotgun";
            
                switch (num)
                { 
                    case 0:
                        msg = $"There is NO ammo remains in current shotgun.";
                        shouldWarning = true;
                        break;
                    
                    default:
                        msg = $"There are {num} ammos remains in current shotgun.";
                        break;
                }
            }

            return (shouldWarning, head, msg);
        }

        public static bool DisplayShellsLoaded()
        {
            if (GlobalVariables.Player != null && GlobalVariables.HUDManagerInstance != null)
            {
                int shellsLoaded = GetAmmoCount();

                if (shellsLoaded == -1)
                {
                    return false;
                }

                (bool shouldWarning, string head, string msg) = GetMessage(shellsLoaded, GlobalVariables.Language);
                
                GlobalVariables.HUDManagerInstance.DisplayTip(head, msg, shouldWarning);

                return true;
            }

            return false;
        }
    }
}