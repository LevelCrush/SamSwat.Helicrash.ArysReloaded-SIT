# HelicopterCrashSites

DayZ inspired

## Overview

BepInEx plugin that will add random helicopter crash sites. The initial chance is 10%, but it can be adjusted in the configuration manager (`F12` key) or alternatively, if you've launched up the game at least once with this mod, you can find the `com.SamSWAT.HeliCrash.cfg` file in your `BepInEx/config/` folder and change value there.

If you were lucky, after loading into the raid you may find downed UH-60 Blackhawk by thick column of smoke, its position is random and choosed from `HeliCrashLocations.json` file, here you can add your own locations or delete them. There should be a container with loot at the rear of the helicopter, currently, because of some limitations, it's just a copy of what was generated for the airdrop so technically there's no way to alter what will be in the container without affecting airdrops.

## How to install

1. Download the latest release here: [link](https://dev.sp-tarkov.com/SamSWAT/HelicopterCrashSites/releases) -OR- build from source (instructions below)
2. Extract the zip file and drop the folder `SamSWAT.HeliCrash` into `BepInEx/plugins/` directory.

## Preview

![preview](https://media.discordapp.net/attachments/417281262085210112/972622826160930866/Escape_from_Tarkov_2022.04.27-17.43_1.png)

## Requirements

- Visual Studio 2019 (.NET desktop workload)
- .NET Framework 4.7.2

## How to build from source

1. Download/clone this repository
2. VS2019 > File > Open solution > `SamSWAT.HeliCrash.sln`
3. VS2019 > Build > Rebuild solution
4. `SamSWAT.HeliCrash.dll` should appear in `bin\Debug` directory
5. Copy `SamSWAT.HeliCrash.dll` into `mod/SamSWAT.HeliCrash` folder
6. Extract `Assets.7z` here, you should find `sikorsky_uh60_blackhawk.bundle` at `SamSWAT.HeliCrash/Assets/Content/Vehicles/` after that
7. Copy whole `SamSWAT.HeliCrash` folder into `BepInEx/plugins/`