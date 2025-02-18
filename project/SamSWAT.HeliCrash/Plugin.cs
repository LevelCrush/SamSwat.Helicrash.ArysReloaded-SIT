﻿using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;

namespace SamSWAT.HeliCrash.ArysReloaded
{
    [BepInPlugin("com.SamSWAT.HeliCrash.ArysReloaded", "SamSWAT.HeliCrash.ArysReloaded", "2.2.1")]
    public class Plugin : BaseUnityPlugin
    {
        internal static HeliCrashLocations HeliCrashLocations;
        internal static string Directory;
        internal static ConfigEntry<int> HeliCrashChance;
        internal static ManualLogSource LogSource;

        private void Awake()
        {
            LogSource = Logger;
            Directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            new HeliCrashPacketPatch().Enable();
            new HeliCrashPatch().Enable();
            var json = File.ReadAllText($"{Directory}/HeliCrashLocations.json");
            HeliCrashLocations = JsonConvert.DeserializeObject<HeliCrashLocations>(json);

            HeliCrashChance = Config.Bind(
                "Main Settings",
                "Helicopter crash site chance",
                10,
                new ConfigDescription("Percent chance of helicopter crash site appearance",
                new AcceptableValueRange<int>(0, 100)));
        }
    }
}
