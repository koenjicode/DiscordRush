using Reptile;
using System;
using UnityEngine;

namespace BombRushDiscord.Utils
{
    /// <summary>
    /// Static utilities class for common functions and properties to be used within your mod code
    /// </summary>
    internal static class ModUtils
    {
        /// <summary>
        /// Example static method to return Players current location / transform
        /// </summary>
        /// <returns></returns>
        public static Player GetReptilePlayer()
        {
            return WorldHandler.instance.GetCurrentPlayer();
        }

        public static SystemLanguage GetGameLanguage() 
        {
            return Application.systemLanguage;
        }

        public static bool IsSlopMultiplayer()
        {
            return false;
        }
    }
}
