using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StayInTarkov;
using StayInTarkov.Coop.SITGameModes;

namespace SamSWAT.HeliCrash.ArysReloaded
{
    public class HeliCrashPacketPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(CoopSITGame).GetMethod("CreateExfiltrationPointAndInitDeathHandler", BindingFlags.Public | BindingFlags.Instance);
        }
    
       

        [PatchPostfix]
        public static  void PatchPostfix()
        { 
            
            StayInTarkovHelperConstants.Logger.LogInfo($"Trying to patch in {nameof(HeliCrashPacket)}");
            var sit_types =
                typeof(StayInTarkovHelperConstants).GetField("_sitTypes", BindingFlags.Static | BindingFlags.NonPublic);
           
            StayInTarkovHelperConstants.Logger.LogInfo($"Helicrash is patching in {nameof(HeliCrashPacket)}");
            var new_types = new List<Type>();
            new_types.Add(typeof(HeliCrashPacket));
            var merged = StayInTarkovHelperConstants.SITTypes.Union(new_types).ToArray();
            sit_types.SetValue(null, merged);
            StayInTarkovHelperConstants.Logger.LogInfo($"Helicrash is finished patching {nameof(HeliCrashPacket)}");
                  
           
        }
    }
}