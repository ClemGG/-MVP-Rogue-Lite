using Project.Actors.Entities;
using System;
using System.Collections.Generic;

namespace Project.Map
{
    /// <summary>
    /// Contains info about the current generated dungeon (nb. rooms created, objects placed, etc.)
    /// </summary>
    public static class DungeonInfo
    {
        //All generated Features on the map (rooms, corridors, etc.)
        public static List<Feature> s_AllFeatures { get; set; } = new List<Feature>();
        public static List<Feature> s_AllRooms { get; set; } = new List<Feature>();
        public static List<Feature> s_AllCorridors { get; set; } = new List<Feature>();

        public static List<ActorTile> s_AllActors { get; set; } = new List<ActorTile>();

        public static ActorTile s_Player
        {
            get
            {
                return _player = _player != null ? _player : Array.Find(s_AllActors.ToArray(), tile => tile.TileName == "Player");
            }
            private set
            {
                _player = value;
            }
        }
        private static ActorTile _player;


        public static void Init()
        {
            s_AllFeatures.Clear();
            s_AllRooms.Clear();
            s_AllCorridors.Clear();
            s_AllActors.Clear();
            s_Player = null;
        }

    }
}