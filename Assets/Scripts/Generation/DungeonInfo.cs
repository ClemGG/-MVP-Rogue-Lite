using Project.Tiles;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Generation
{
    /// <summary>
    /// Contains info about the current generated dungeon (nb. rooms created, objects placed, etc.)
    /// </summary>
    public static class DungeonInfo
    {
        //Map Info
        public static Cell[,] s_Map { get; set; }
        public static Vector2Int s_Size { get; set; }   //Keeps the map size in memory for future iterations through the array


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
                return _player = _player != null ? _player : Array.Find(s_AllActors.ToArray(), tile => tile is PlayerTile) as PlayerTile;
            }
            set
            {
                _player = value;
            }
        }
        private static PlayerTile _player;


        public static void Init(Vector2Int size)
        {
            s_AllFeatures.Clear();
            s_AllRooms.Clear();
            s_AllCorridors.Clear();
            s_AllActors.Clear();
            s_AllEnemies.Clear();
            s_Player = null;



            s_Size = size;
            s_Map = new Cell[s_Size.x, s_Size.y];

            //Set Cell Position and fills all of them with Walls
            //We'll then carve up Feature in these Walls in the DungeonPatterns class
            for (int y = 0; y < s_Size.y; y++)
            {
                for (int x = 0; x < s_Size.x; x++)
                {
                    s_Map[x, y] = new Cell();
                    s_Map[x, y].Position = new Vector2Int(x, y);
                    s_Map[x, y].Tiles.Add(TileLibrary.Wall);
                }
            }
        }


        /// <summary>
        /// If we don't call Init() before a Generate(), use this to clean the Map before filling the Cells with new Tiles
        /// </summary>
        public static void ClearMap()
        {
            if (s_Map != null)
            {
                foreach (Cell cell in s_Map)
                {
                    cell.Clear();
                    cell.Tiles.Add(TileLibrary.Wall);
                }
            }
        }

    }
}