using System;
using System.Collections.Generic;
using Project.ValueTypes;
using Project.Tiles;

namespace Project.Generation
{
    /// <summary>
    /// Contains info about the structures created by the DungeonGenerator (room type, size, contained cells, etc.)
    /// </summary>
    [Serializable]
    public class Feature
    {
        public FeatureType Type { get; set; }
        public Rectangle Bounds { get; set; }
        public List<Cell> Cells { get; set; } = new List<Cell>();


        public bool ContainsPlayer()
        {
            bool contains = false;

            for (int i = 0; i < Cells.Count; i++)
            {
                if (Cells[i].Contains<PlayerTile>())
                {
                    contains = true;
                    break;
                }
            }

            return contains;
        }
    }
}