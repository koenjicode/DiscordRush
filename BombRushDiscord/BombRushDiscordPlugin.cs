using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using BombRushDiscord.MonoBehaviours;
using HarmonyLib;
using Reptile;
using SlopCrew.API;
using UnityEngine;

namespace BombRushDiscord
{
    // TODO Review this file and update to your own requirements.

    [BepInPlugin(MyGUID, PluginName, VersionString)]
    public class BombRushDiscordPlugin : BaseUnityPlugin
    {
        // Mod specific details. MyGUID should be unique, and follow the reverse domain pattern
        // e.g.
        // com.mynameororg.pluginname
        // Version should be a valid version string.
        // e.g.
        // 1.0.0
        private const string MyGUID = "com.Koenji.BombRushDiscord";
        private const string PluginName = "BombRushDiscord";
        private const string VersionString = "1.2.0";

        // Config entry key strings
        // These will appear in the config file created by BepInEx and can also be used
        // by the OnSettingsChange event to determine which setting has changed.
        public static string DisplayMissionObjectiveKey = "Display Full Mission Name";
        public static string PrintDiscordInfoKey = "Discord Print Info";


        public static ConfigEntry<bool> DisplayMissionObjective;
        public static ConfigEntry<KeyboardShortcut> PrintDiscordInfo;

        private static readonly Harmony Harmony = new Harmony(MyGUID);
        public static ManualLogSource Log = new ManualLogSource(PluginName);

        // Bomb Rush Discord
        private GameObject _modObject;
        private BombRushDiscordComponent _modDiscordComponent;

        public static ISlopCrewAPI slopCrew;

        /// <summary>
        /// Initialise the configuration settings and patch methods
        /// </summary>
        private void Awake()
        {
            // Keyboard shortcut setting example
            // TODO Change this code or remove the code if not required.
            DisplayMissionObjective = Config.Bind("General", DisplayMissionObjectiveKey, true);
            PrintDiscordInfo = Config.Bind("General", PrintDiscordInfoKey, new KeyboardShortcut(KeyCode.F3));

            SlopCrew.API.APIManager.OnAPIRegistered += SlopCrewRegistered;

            Harmony.PatchAll();

            // Add listeners methods to run if and when settings are changed by the player.
            // TODO Change this code or remove the code if not required.
            DisplayMissionObjective.SettingChanged += ConfigSettingChanged;
            PrintDiscordInfo.SettingChanged += ConfigSettingChanged;

            // Apply all of our patches
            Logger.LogInfo($"PluginName: {PluginName}, VersionString: {VersionString} is loading...");
            Logger.LogInfo($"PluginName: {PluginName}, VersionString: {VersionString} is loaded.");

            // Sets up our static Log, so it can be used elsewhere in code.
            // .e.g.
            // BombRushDiscordPlugin.Log.LogDebug("Debug Message to BepInEx log file");
            Log = Logger;

            // Creates new GameObject and adds the Discord Component. Is this even necessary? Probably not, i don't really use Unity!
            _modObject = new GameObject();
            _modObject.name = PluginName;
            _modDiscordComponent = _modObject.AddComponent<BombRushDiscordComponent>();
        }

        private void SlopCrewRegistered(SlopCrew.API.ISlopCrewAPI obj)
        {
            slopCrew = obj;
            Logger.LogMessage("SlopCrew API registered.");
        }

        public void Start()
        {
            Logger.LogInfo("The start event here actually fires.");
        }

        /// <summary>
        /// Code executed every frame. See below for an example use case
        /// to detect keypress via custom configuration.
        /// </summary>
        // TODO - Add your code here or remove this section if not required.
        private void Update()
        {

            if (BombRushDiscordPlugin.PrintDiscordInfo.Value.IsDown())
            {
                Logger.LogInfo("Printing Discord Info");

                if (Core.Instance)
                {
                    Logger.LogInfo(string.Format("Core Information: {0}", Core.Instance.ToString()));
                }
                else
                {
                    Logger.LogWarning("Core Instance was not located.");
                }

                if (WorldHandler.instance)
                {
                    Logger.LogInfo(string.Format("World Information: {0}", WorldHandler.instance.ToString()));
                }
                else
                {
                    Logger.LogWarning("World Instance was not located.");
                }

                Logger.LogInfo(string.Format("Game Chapter: {0}", Reptile.Story.GetCurrentObjectiveInfo().chapter.ToString()));

                Logger.LogInfo(string.Format("Current Stage: {0}", Reptile.Utility.GetCurrentStage()));

                var character = Core.Instance.SaveManager.CurrentSaveSlot.currentCharacter;
                Logger.LogInfo(string.Format("Current Character: {0} || ID: {1}", Core.Instance.Localizer.GetCharacterName(character), character.ToString().ToLower()));

                if (slopCrew != null)
                {
                    Logger.LogInfo(string.Format("Connected To SlopCrew: {0}", slopCrew.PlayerCount > 1));

                    Logger.LogInfo(string.Format("Sloppers Connected: {0}", slopCrew.PlayerCount));
                }
            }
        }

        /// <summary>
        /// Method to handle changes to configuration made by the player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfigSettingChanged(object sender, System.EventArgs e)
        {
            SettingChangedEventArgs settingChangedEventArgs = e as SettingChangedEventArgs;

            // Check if null and return
            if (settingChangedEventArgs == null)
            {
                return;
            }

            // Example Float Shortcut setting changed handler
            if (settingChangedEventArgs.ChangedSetting.Definition.Key == DisplayMissionObjectiveKey)
            {
                // TODO - Add your code here or remove this section if not required.
                // Code here to do something with the new value
            }

            // Example Keyboard Shortcut setting changed handler
            if (settingChangedEventArgs.ChangedSetting.Definition.Key == PrintDiscordInfoKey)
            {
                KeyboardShortcut newValue = (KeyboardShortcut)settingChangedEventArgs.ChangedSetting.BoxedValue;

                // TODO - Add your code here or remove this section if not required.
                // Code here to do something with the new value
            }
        }
    }
}
