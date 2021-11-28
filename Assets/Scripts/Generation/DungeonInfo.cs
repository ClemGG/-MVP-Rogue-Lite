using Project.Tiles;
using System;
using System.Collections.Generic;

namespace Project.Generation
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
        public static List<EnemyTile> s_AllEnemies { get; set; } = new List<EnemyTile>();

        public static Feature s_RandomRoomWithoutPlayer
        {
            get
            {
                //If there's only one giant room, we still want to spawn new enemies in it
                if(s_AllRooms.Count == 1)
                {
                    return s_AllRooms[0];
                }
                else
                {
                    Feature[] allRoomsWithoutPlayer = Array.FindAll(s_AllRooms.ToArray(), room => !room.ContainsPlayer());
                    return allRoomsWithoutPlayer[UnityEngine.Random.Range(0, allRoomsWithoutPlayer.Length)];
                }
            }
        }
        public static PlayerTile s_Player
        {
            get
            {
                return _player ??= Array.Find(s_AllActors.ToArray(), tile => tile is PlayerTile) as PlayerTile;
            }
            set
            {
                _player = value;
            }
        }
        private static PlayerTile _player;


        public static void Init()
        {
            s_AllFeatures.Clear();
            s_AllRooms.Clear();
            s_AllCorridors.Clear();
            s_AllActors.Clear();
            s_AllEnemies.Clear();
            s_Player = null;
        }

    }
}