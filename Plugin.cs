using BepInEx;
using BepInEx.Configuration;
using CrewBoomAPI;
using SlopCrew.API;
using System;

namespace DiscordRush
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static string DisplayMissionObjectiveKey = "Display Full Mission Name";

        public static ConfigEntry<bool> DisplayMissionObjective;

        public static RushDiscord gameDiscord;

        private void Awake()
        {
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} loading...");

            DisplayMissionObjective = Config.Bind("General", DisplayMissionObjectiveKey, true);
            gameDiscord = new RushDiscord();

            Logger.LogMessage($"Plugin {PluginInfo.PLUGIN_GUID} {PluginInfo.PLUGIN_VERSION} is now running!");
        }
        private void Update()
        {
            gameDiscord.UpdateDiscord();
        }
    }
}
