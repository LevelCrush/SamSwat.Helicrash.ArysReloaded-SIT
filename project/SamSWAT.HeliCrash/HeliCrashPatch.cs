using EFT;
using EFT.Airdrop;
using HarmonyLib;
using System.Linq;
using System.Reflection;
using Comfort.Common;
using StayInTarkov;
using StayInTarkov.Coop.Matchmaker;
using StayInTarkov.Coop.SITGameModes;
using StayInTarkov.Networking;
using ModulePatch = Aki.Reflection.Patching.ModulePatch;

namespace SamSWAT.HeliCrash.ArysReloaded
{
    public class HeliCrashPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(CoopSITGame).GetMethod("CreateExfiltrationPointAndInitDeathHandler", BindingFlags.Public | BindingFlags.Instance);
        }

        [Aki.Reflection.Patching.PatchPostfix]
        public static async void PatchPostfix()
        {
            
            if (SITMatchmaking.IsClient)
            {
                // is normal client. Let host decide where the helicopter spawns
                StayInTarkovHelperConstants.Logger.LogInfo("Waiting on Host to generate Helicopter crash");
            }
            else
            {
                // generate helicopter crash
                StayInTarkovHelperConstants.Logger.LogInfo("Generating Helicopter Crash (if possible)");
                var generated_results = await HeliCrashHelper.Init(Singleton<GameWorld>.Instance.MainPlayer.Location, null, null);

                if (generated_results != null)
                {
                    StayInTarkovHelperConstants.Logger.LogInfo("Sending Helicrash Packet");
                    var packet = new HeliCrashPacket(Singleton<GameWorld>.Instance.MainPlayer.ProfileId);
                    packet.Location = generated_results.Item1;
                    packet.LootResult = generated_results.Item2;
                    GameClient.SendData(packet.Serialize());
                }
            }
        }
    }
}
