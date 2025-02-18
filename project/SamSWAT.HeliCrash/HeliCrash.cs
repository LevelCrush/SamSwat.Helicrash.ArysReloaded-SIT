﻿using System;
using Aki.Custom.Airdrops.Models;
using Aki.Custom.Airdrops.Utils;
using Comfort.Common;
using EFT;
using EFT.Interactive;
using EFT.UI;
using System.Collections.Generic;
using System.Threading.Tasks;
using HarmonyLib;
using StayInTarkov.AkiSupport.Airdrops;
using StayInTarkov.Coop.Components.CoopGameComponents;
using UnityEngine;

namespace SamSWAT.HeliCrash.ArysReloaded
{
    public class HeliCrash : MonoBehaviour
    {
        private AssetBundle _heliBundle;

        public async Task<Tuple<Location, AirdropLootResultModel>> Init(string location,AirdropLootResultModel target_loot_result, Location heli_location)
        {
            var heliLocation = heli_location == null ? GetHeliCrashLocation(location) : heli_location;
#if DEBUG
            ConsoleScreen.Log($"Heli crash site spawned at position x: {heliLocation.Position.x}, y: {heliLocation.Position.y}, z: {heliLocation.Position.z}");
#endif
            var choppa = Instantiate(await LoadChoppaAsync(), heliLocation.Position, Quaternion.Euler(heliLocation.Rotation));
            var container = choppa.GetComponentInChildren<LootableContainer>();

            var itemFactoryUtil = new ItemFactoryUtil();
            var lootResult = target_loot_result == null ? await itemFactoryUtil.GetLoot() : target_loot_result;
            var itemCrate = Singleton<ItemFactory>.Instance.CreateItem("goofyahcontainer", "6223349b3136504a544d1608", null);
            LootItem.CreateLootContainer(container, itemCrate, "Heavy crate", Singleton<GameWorld>.Instance);
            itemFactoryUtil.AddLoot(container, lootResult);
            
            if (SITGameComponent.TryGetCoopGameComponent(out var coopGameComponent))
            {
                coopGameComponent.ListOfInteractiveObjects.AddItem(container);
            }
            return new Tuple<Location, AirdropLootResultModel>(heliLocation, lootResult);
        }

        private void OnDestroy()
        {
            _heliBundle.Unload(true);
        }

        private Location GetHeliCrashLocation(string map)
        {
            List<Location> location;

            switch (map.ToLower())
            {
                case "bigmap":
                    location = Plugin.HeliCrashLocations.Customs;
                    break;
                case "interchange":
                    location = Plugin.HeliCrashLocations.Interchange;
                    break;
                case "rezervbase":
                    location = Plugin.HeliCrashLocations.Rezerv;
                    break;
                case "shoreline":
                    location = Plugin.HeliCrashLocations.Shoreline;
                    break;
                case "woods":
                    location = Plugin.HeliCrashLocations.Woods;
                    break;
                case "lighthouse":
                    location = Plugin.HeliCrashLocations.Lighthouse;
                    break;
                case "tarkovstreets":
                    location = Plugin.HeliCrashLocations.StreetsOfTarkov;
                    break;
                case "sandbox":
                    location = Plugin.HeliCrashLocations.GroundZero;
                    break;
                case "develop":
                    location = Plugin.HeliCrashLocations.Develop;
                    break;
                default:
                    return new Location();
            }

            return location.Shuffle().SelectRandom();
        }

        private async Task<GameObject> LoadChoppaAsync()
        {
            var path = $"{Plugin.Directory}/sikorsky_uh60_blackhawk.bundle";

            var bundleLoadRequest = AssetBundle.LoadFromFileAsync(path);

            while (!bundleLoadRequest.isDone)
                await Task.Yield();

            _heliBundle = bundleLoadRequest.assetBundle;

            if (_heliBundle == null)
            {
                Plugin.LogSource.LogFatal("Can't load UH-60 Blackhawk bundle");
                Debug.LogError("[SamSWAT.HeliCrash.ArysReloaded]: Can't load UH-60 Blackhawk bundle");
                return null;
            }

            var assetLoadRequest = _heliBundle.LoadAllAssetsAsync<GameObject>();

            while (!assetLoadRequest.isDone)
                await Task.Yield();

            var requestedGo = assetLoadRequest.allAssets[0] as GameObject;

            if (requestedGo != null) return requestedGo;

            Plugin.LogSource.LogFatal("Failed to load heli asset");
            Debug.LogError("[SamSWAT.HeliCrash.ArysReloaded]: Failed to load heli asset");
            return null;

        }
    }
}
