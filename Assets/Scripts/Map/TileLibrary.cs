using UnityEngine;

namespace Project.Map
{
    /// <summary>
    /// Stores all types of Tiles and loads them automatically when called
    /// instead of having to manually select which Tile to use 
    /// each time we generate a new dungeon.
    /// </summary>
    public static class TileLibrary
    {
        public static Tile Floor { get { return Object.Instantiate(Resources.Load<Tile>("Tiles/Map/Floor")); } } 
        public static Tile Wall { get { return Object.Instantiate(Resources.Load<Tile>("Tiles/Map/Wall")); } }
        public static Tile Player { get { return Object.Instantiate(Resources.Load<Tile>("Tiles/Actors/Player")); } }
    }
}