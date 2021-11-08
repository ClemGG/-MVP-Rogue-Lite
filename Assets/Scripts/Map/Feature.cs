using System;
using System.Collections.Generic;
using UnityEngine;
using Project.ValueTypes;

namespace Project.Map
{
    /// <summary>
    /// Contains info about the structures created by the DungeonGenerator (room type, size, contained cells, etc.)
    /// </summary>
    [Serializable]
    public class Feature
    {
        public FeatureType Type { get; set; }
        public bool IsDarkRoom { get; set; }   //If true, the Feature's Cells are not marked as explored when the Player enters it.
        public Rectangle Bounds { get; set; }
        public List<Cell> Cells { get; set; } = new List<Cell>();

    }
}