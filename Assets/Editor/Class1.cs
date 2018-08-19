using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Assets.Editor
{
    class SpawnPointInfo
    {
        public GameObject Spawn { get; set; }
        public float DistanceSum { get; set; }
        public float SightedPlayerCount { get; set; }
    }
}
