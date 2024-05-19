using System;
using System.Linq;
using System.Threading.Tasks;
using Aki.Custom.Airdrops.Models;
using Comfort.Common;
using EFT;
using EFT.Airdrop;
using StayInTarkov;

namespace SamSWAT.HeliCrash.ArysReloaded
{
    public class HeliCrashHelper
    {
        public static async Task<Tuple<Location,AirdropLootResultModel>> Init(string target_location, AirdropLootResultModel loot_result, Location heli_location)
        {
            var __instance = Singleton<GameWorld>.Instance;
            
            var gameWorld = __instance;
           //var location = gameWorld.MainPlayer.Location;
            var location = target_location;

            if (loot_result == null)
            {
                var crashAvailable = __instance.MainPlayer.Location.ToLower() == "sandbox" || LocationScene.GetAll<AirdropPoint>().Any();
                if (!crashAvailable || !BlessRNG.RngBool(Plugin.HeliCrashChance.Value))
                {
                    StayInTarkovHelperConstants.Logger.LogInfo("HeliCrash will not generate");
                    return null;
                }
            }
           
            
            var heliCrash = gameWorld.gameObject.AddComponent<HeliCrash>();
            return await heliCrash.Init(location, loot_result, heli_location);
        }
    }
}