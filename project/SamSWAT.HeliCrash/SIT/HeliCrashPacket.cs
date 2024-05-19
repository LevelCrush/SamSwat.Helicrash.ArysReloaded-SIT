using System.IO;
using Aki.Custom.Airdrops.Models;
using Comfort.Common;
using Newtonsoft.Json;
using StayInTarkov;
using StayInTarkov.Coop.NetworkPacket;
using StayInTarkov.Coop.NetworkPacket.Player;
using StayInTarkov.Coop.Players;
using StayInTarkov.Networking;

namespace SamSWAT.HeliCrash.ArysReloaded
{
    public class HeliCrashPacket : BasePlayerPacket
    {
        
        public Location Location { get; set; }
        public AirdropLootResultModel LootResult { get; set; }
        
        public HeliCrashPacket()
        {
        }

        public  HeliCrashPacket(string profileId): base(new string(profileId.ToCharArray()), nameof(HeliCrashPacket)) {
    
        }
        public override byte[] Serialize()
        {
            StayInTarkovHelperConstants.Logger.LogInfo($"{nameof(HeliCrashPacket)}:Trying to serialize");
            var ms = new MemoryStream();

            StayInTarkovHelperConstants.Logger.LogInfo($"{nameof(HeliCrashPacket)}:Creating Binary Writer");
            using var writer = new BinaryWriter(ms);

            StayInTarkovHelperConstants.Logger.LogInfo($"{nameof(HeliCrashPacket)}:Writing Header");
            WriteHeaderAndProfileId(writer);

            StayInTarkovHelperConstants.Logger.LogInfo($"{nameof(HeliCrashPacket)}:{Location}");
            writer.Write(Location.ToJson());
    
            var loot_result = LootResult.ToJson();
            StayInTarkovHelperConstants.Logger.LogInfo($"{nameof(HeliCrashPacket)}: Loot Json Generated");
            writer.Write(LootResult.ToJson());
            
            
            StayInTarkovHelperConstants.Logger.LogInfo($"{nameof(HeliCrashPacket)}:Done setting");

            return ms.ToArray();
        }
        
        public override ISITPacket Deserialize(byte[] bytes)
        {
            StayInTarkovHelperConstants.Logger.LogInfo($"{nameof(HeliCrashPacket)}:Creating Binary Reader");
            using var reader = new BinaryReader(new MemoryStream(bytes));
            ReadHeaderAndProfileId(reader);

            var location_json = reader.ReadString();
            Location = JsonConvert.DeserializeObject<ArysReloaded.Location>(location_json);
            StayInTarkovHelperConstants.Logger.LogInfo($"{nameof(HeliCrashPacket)}:{Location} is set");
            
            var  loot_json = reader.ReadString();
            StayInTarkovHelperConstants.Logger.LogInfo($"{nameof(HeliCrashPacket)}: loot json has been received");
    
            LootResult = JsonConvert.DeserializeObject<AirdropLootResultModel>(loot_json);
            StayInTarkovHelperConstants.Logger.LogInfo($"{nameof(HeliCrashPacket)}: Has deserialized the loot model");
    
            return this;
        }

        protected override async void Process(CoopPlayerClient client)
        {
            StayInTarkovHelperConstants.Logger.LogInfo(
                $"{nameof(HeliCrashPacket)}: Processing Helicopter Crash Packet");

            if (client.GetPlayer.IsYourPlayer)
            {
                StayInTarkovHelperConstants.Logger.LogInfo($"{nameof(HeliCrashPacket)}: Ignoring own packet");
                return;
            }
                
            StayInTarkovHelperConstants.Logger.LogInfo($"{nameof(HeliCrashPacket)}: setting up crash site");
            await HeliCrashHelper.Init(client.Location, LootResult, Location);
        }
    }
    
   
}