using System.Collections.Generic;
using UnityEngine;

namespace SamSWAT.HeliCrash.TyrianReboot
{
    public class Location
    {
        public Vector3 Position { get; set; } = Vector3.zero;
        public Vector3 Rotation { get; set; } = Vector3.zero;
    }

    public class HeliCrashLocations
    {
        public List<Location> Customs { get; set; }
        public List<Location> Woods { get; set; }
        public List<Location> Interchange { get; set; }
        public List<Location> Lighthouse { get; set; }
        public List<Location> Rezerv { get; set; }
        public List<Location> Shoreline { get; set; }
        public List<Location> StreetsOfTarkov { get; set; }
        public List<Location> Develop { get; set; }
    }
}
