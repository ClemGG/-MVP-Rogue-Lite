using Project.Actors.Entities;
using System.Collections.Generic;

namespace Project.Map
{
    /// <summary>
    /// Contains info about the current generated dungeon (nb. rooms created, objects placed, etc.)
    /// </summary>
    public static class DungeonInfo
    {
        //All generated Features on the map (rooms, corridors, etc.)
        public static List<Feature> AllFeatures { get; set; } = new List<Feature>();
        public static List<Feature> AllRooms { get; set; } = new List<Feature>();
        public static List<Feature> AllCorridors { get; set; } = new List<Feature>();

        public static List<ActorTile> AllActors { get; set; } = new List<ActorTile>();

        public static void Init()
        {
            AllFeatures.Clear();
            AllRooms.Clear();
            AllCorridors.Clear();
            AllActors.Clear();
        }

    }
}