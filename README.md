![BombRushDiscord](/.github/IMAGES/icon.png?raw=true "Bomb Rush Discord")

**Download:** [Thunderstore](https://thunderstore.io/c/bomb-rush-cyberfunk/p/Koenji/BombRushDiscord/) - [Github](https://github.com/koenjicode/BombRushDiscord/releases)

_To track the updates check here:_ [Update Log](https://github.com/koenjicode/BombRushDiscord/blob/main/UPDATE.md)

## Presentation

![English Text From BombRushDiscord](/.github/IMAGES/image1.png?raw=true)

BombRushDiscord provides **Discord Rich Presence** support to **Bomb Rush Cyberfunk**.

What is **Discord Rich Presence**? Discord Rich Presence provides context-based information related to an application.

For the case of Bomb Rush Cyberfunk it will tell other players on Discord what you're doing along with other things!

**NOTICE:** One of the features of this plugin is displaying the mission objectives text. Chapters 4 and 5 will have these censored with "???" to avoid spoilers.

## Features

* Adjustable options provided by BepInEx, allowing mission objectives flavor text to be toggled on and off.

* Game icons are used for additional Rich Presence graphics.

* Translations for English, French, Japanese, Dutch, German, Italian, Portuguese, Russian and Spanish.

## Installation

For installation, it's highly recommended that you install the plugin through [Thunderstore](https://thunderstore.io/c/bomb-rush-cyberfunk/p/Koenji/BombRushDiscord/).

If not, the newest release of BombRushDiscord can be installed or compiled on [Github](https://github.com/koenjicode/BombRushDiscord/releases).

## Building the Plugin

If you're building the project from Github, you'll need to provide the **Discord Game SDK** files. This currently uses the latest version at the time **v.3.2.1**

You can download the latest version of the SDK files from [Discord's Development Portal](https://discord.com/developers/docs/game-sdk/sdk-starter-guide).

Additionally you'll need the following References from Bomb Rush Cyberfunk:

* 0Harmony
* Assembly-CSharp
* Assembly-CSharp-firstpass
* BepInEx
* UnityEngine
* UnityEngine.CoreModule

Don't forget after compiling to supply **discord_game_sdk.dll** from lib\x86_64.
