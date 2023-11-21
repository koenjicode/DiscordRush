using Discord;
using Reptile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace DiscordRush
{
    public class RushDiscord
    {
        private Discord.Discord _discord = new Discord.Discord(RushConstants.applicationID, (System.UInt64)Discord.CreateFlags.NoRequireDiscord);
        public RushStatus status = new RushStatus();

        public void FetchGameInformation()
        {
            var core = Core.Instance;
            if (core == null )
            {
                return;
            }

            var world = WorldHandler.instance;
            if (world == null)
            {
                status.details = string.Empty;

                status.largeImage = "main";
                status.largeText = "Bomb Rush Cyberfunk";

                status.smallImage = string.Empty;
                status.smallText = string.Empty;

                status.startTime = 0;
                status.currentStage = Stage.NONE;

                status.currentPartySize = 0;
                status.maxPartySize = 0;

                // Is Loading Screen being shown..
                if (core.UIManager.Overlay.loadingScreen.activeSelf)
                {
                    status.state = string.Format("{0}...", core.Localizer.GetUserInterfaceText("MENU_LOADING"));
                }
                else
                // If Main Menu is showing..
                {
                    status.state = core.Localizer.GetUserInterfaceText("MAIN_MENU_HEADER");
                }
            }
            else
            {
                bool isSlopNetworked = SlopCrew.API.APIManager.API != null && SlopCrew.API.APIManager.API.PlayerCount > 0;

                var currentStage = Utility.GetCurrentStage();
                var currentObjective = Reptile.Story.GetCurrentObjectiveInfo();

                // Set Stage image based on current stage.
                var stageCodeName = currentStage.ToString().ToLower();
                if (RushConstants.specialMaps.Contains(stageCodeName))
                {
                    status.largeImage = stageCodeName;
                }
                else
                {
                    status.largeImage = "main";
                }

                // If the stage is different restart the time.
                if (currentStage != status.currentStage)
                {
                    status.startTime = System.DateTimeOffset.Now.ToUnixTimeMilliseconds();
                }
                status.currentStage = currentStage;

                // If the character has a special name entry we use the appropriate icon for them.
                var character = core.SaveManager.CurrentSaveSlot.currentCharacter;
                var charName = character.ToString().ToLower();

                // Set Special Character Icons and Name
                if (RushConstants.specialNames.Contains(charName))
                {
                    status.smallImage = charName;
                }
                else
                {
                    status.smallImage = "fallback";
                }

                string characterName;
                if (CrewBoomAPI.CrewBoomAPIDatabase.IsInitialized && CrewBoomAPI.CrewBoomAPIDatabase.PlayerCharacterInfo != null)
                {
                    characterName = CrewBoomAPI.CrewBoomAPIDatabase.PlayerCharacterInfo.Name.ToUpper();
                }
                else
                {
                    characterName = core.Localizer.GetCharacterName(character);
                }

                status.smallText = characterName;
                status.largeText = core.Localizer.GetStageName(currentStage);

                status.state = core.Localizer.GetStageName(currentStage);

                // Update Chapter Details
                // Censor the Mission Objective past Chapter 4.
                if (Plugin.DisplayMissionObjective.Value)
                {
                    string objectiveText;
                    if ((int)currentObjective.chapter == 4 || (int)currentObjective.chapter == 5)
                    {
                        objectiveText = "???";
                    }
                    else
                    {
                        objectiveText = core.Localizer.GetObjectiveText(currentObjective.id);
                    }

                    status.details = string.Format("{0} {1} : {2}", core.Localizer.GetUserInterfaceText("SAVESLOT_CHAPTER"),
                        (int)currentObjective.chapter, objectiveText);
                }
                else
                {
                    // If Display Mission Objective is disabled we display less information.
                    status.details = string.Format("{0} {1}", core.Localizer.GetUserInterfaceText("SAVESLOT_CHAPTER"),
                        (int)currentObjective.chapter);
                }

                // If we're sloppin, then we update the details with the player count.
                if (isSlopNetworked)
                {
                    if ((int)currentObjective.chapter == 6)
                    {
                        // Capitals look better in Japanese to be honest
                        string objectiveText = Application.systemLanguage != SystemLanguage.Japanese ? "Slop Crew" : "SLOP CREW";
                        status.details = objectiveText;
                    }

                    status.currentPartySize = SlopCrew.API.APIManager.API.PlayerCount;
                    status.maxPartySize = 256;
                }
                else
                {
                    status.currentPartySize = 0;
                    status.maxPartySize = 0;
                }

                // Are we in a pause menu? then we update the details with pause stuff instead.
                if (core.IsCorePaused)
                {
                    status.details = core.Localizer.GetUserInterfaceText("PAUSE_HEADER");
                }
            }
        }

        public void UpdateGameActivity()
        {
            var activity = new Discord.Activity
            {
                Details = status.details,
                State = status.state,
                Party =
                    {
                      Size =
                        {
                            CurrentSize = status.currentPartySize,
                            MaxSize = status.maxPartySize,
                        }
                    },
                Assets =
                    {
                        LargeImage = status.largeImage,
                        LargeText = status.largeText,
                        SmallImage = status.smallImage,
                        SmallText = status.smallText,
                    },
                Timestamps =
                    {
                        Start = status.startTime,
                        End =  status.endTime,
                    }
            };

            _discord.GetActivityManager().UpdateActivity(activity, (res) =>
            {
                if (res != Discord.Result.Ok) Debug.LogWarning("Failed connecting to Discord!");
            });
        }

        public void UpdateDiscord()
        {
            FetchGameInformation();
            UpdateGameActivity();

            _discord.RunCallbacks();
        }
    }
}
