using Aki.Reflection.Patching;
using Comfort.Common;
using EFT;
using EFT.Airdrop;
using System.Linq;
using System.Reflection;

namespace SamSWAT.HeliCrash.TyrianReboot
{
    public class HeliCrashPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            //return PatchConstants.LocalGameType.BaseType.GetMethod("method_10", BindingFlags.NonPublic | BindingFlags.Instance);
            return typeof(GameWorld).GetMethod(nameof(GameWorld.OnGameStarted));
        }

        [PatchPostfix]
        public static void PatchPostfix()
        {
            var gameWorld = Singleton<GameWorld>.Instance;
            var crashAvailable = LocationScene.GetAll<AirdropPoint>().Any();
            var location = gameWorld.MainPlayer.Location;
            
            if (gameWorld == null || !crashAvailable || !BlessRNG.RngBool(Plugin.HeliCrashChance.Value)) return;
            
            var heliCrash = gameWorld.gameObject.AddComponent<HeliCrash>();
            heliCrash.Init(location);
        }
    }
}
