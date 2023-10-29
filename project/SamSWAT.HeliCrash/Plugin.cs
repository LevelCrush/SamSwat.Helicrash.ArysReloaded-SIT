using BepInEx;
using BepInEx.Configuration;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using BepInEx.Logging;

namespace SamSWAT.HeliCrash.TyrianReboot
{
    [BepInPlugin("com.SamSWAT.HeliCrash.TyrianReboot", "SamSWAT.HeliCrash.TyrianReboot", "2.2.0")]
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
            new HeliCrashPatch().Enable();
            var json = File.ReadAllText($"{Directory}/HeliCrashLocations.json");
            HeliCrashLocations = JsonConvert.DeserializeObject<HeliCrashLocations>(json);

            HeliCrashChance = Config.Bind(
                "Main Settings",
                "Helicopter crash site chance",
                10,
                new ConfigDescription("Chance of helicopter crash site appearance in percentages",
                new AcceptableValueRange<int>(0, 100)));
        }
    }
}
