using Comfort.Common;
using EFT;
using EFT.Interactive;
using System.Threading.Tasks;
using Aki.Custom.Airdrops.Utils;
using UnityEngine;
using Aki.Custom.Airdrops.Models;

namespace SamSWAT.HeliCrash.TyrianReboot
{
    public class HeliCrash : MonoBehaviour
    {
        private AssetBundle _heliBundle;

        public async void Init(string location)
        {
            var heliLocation = GetHeliCrashLocation(location);
            var choppa = Instantiate(await LoadChoppaAsync(), heliLocation.Position, Quaternion.Euler(heliLocation.Rotation));
            var container = choppa.GetComponentInChildren<LootableContainer>();

            // New code for 3.7.1
            AirdropLootResultModel lootResult = new ItemFactoryUtil().GetLoot();
            var itemCrate = Utils.CreateItem("goofyahcontainer", "6223349b3136504a544d1608");
            LootItem.CreateLootContainer(container, itemCrate, "Heavy crate", Singleton<GameWorld>.Instance);
            new ItemFactoryUtil().AddLoot(container, lootResult);
        }

        private void OnDestroy()
        {
            _heliBundle.Unload(true);
        }

        private Location GetHeliCrashLocation(string location)
        {
            switch (location)
            {
                case "bigmap":
                    {
                        return Plugin.HeliCrashLocations.Customs.Shuffle().SelectRandom();
                    }
                case "Interchange":
                    {
                        return Plugin.HeliCrashLocations.Interchange.Shuffle().SelectRandom();;
                    }
                case "RezervBase":
                    {
                        return Plugin.HeliCrashLocations.Rezerv.Shuffle().SelectRandom();;
                    }
                case "Shoreline":
                    {
                        return Plugin.HeliCrashLocations.Shoreline.Shuffle().SelectRandom();;
                    }
                case "Woods":
                    {
                        return Plugin.HeliCrashLocations.Woods.Shuffle().SelectRandom();;
                    }
                case "Lighthouse":
                    {
                        return Plugin.HeliCrashLocations.Lighthouse.Shuffle().SelectRandom();;
                    }
                case "TarkovStreets":
                    {
                        return Plugin.HeliCrashLocations.StreetsOfTarkov.Shuffle().SelectRandom();;
                    }
                case "develop":
                    {
                        return Plugin.HeliCrashLocations.Develop.Shuffle().SelectRandom();;
                    }
                default: return new Location();
            }
        }

        private async Task<GameObject> LoadChoppaAsync()
        {
            var path = $"{Plugin.Directory}/Assets/Content/Vehicles/sikorsky_uh60_blackhawk.bundle";

            var bundleLoadRequest = AssetBundle.LoadFromFileAsync(path);

            while (!bundleLoadRequest.isDone)
                await Task.Yield();

            _heliBundle = bundleLoadRequest.assetBundle;

            if (_heliBundle == null)
            {
                Plugin.LogSource.LogFatal("Can't load UH-60 Blackhawk bundle");
                Debug.LogError("[SamSWAT.HeliCrash.TyrianReboot]: Can't load UH-60 Blackhawk bundle");
                return null;
            }

            var assetLoadRequest = _heliBundle.LoadAllAssetsAsync<GameObject>();

            while (!assetLoadRequest.isDone)
                await Task.Yield();

            var requestedGo = assetLoadRequest.allAssets[0] as GameObject;

            if (requestedGo != null) return requestedGo;
            
            Plugin.LogSource.LogFatal("Failed to load heli asset");
            Debug.LogError("[SamSWAT.HeliCrash.TyrianReboot]: failed to load heli asset");
            return null;

        }
    }
}
