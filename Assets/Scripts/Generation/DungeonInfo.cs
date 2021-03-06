using Project.Combat;
using Project.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Generation
{
    /// <summary>
    /// Contains info about the current generated dungeon (nb. rooms created, objects placed, etc.)
    /// </summary>
    public static class DungeonInfo
    {
        #region Accessors

        //Map Info
        public static Cell[,] s_Map { get; set; }
        public static Vector2Int s_Size { get; set; }   //Keeps the map size in memory for future iterations through the array


        //All generated Features on the map (rooms, corridors, etc.)
        public static List<Feature> s_AllFeatures { get; set; } = new List<Feature>();
        public static List<Feature> s_AllRooms { get; set; } = new List<Feature>();
        public static List<Feature> s_AllCorridors { get; set; } = new List<Feature>();
        public static List<Tile> s_AllDoors { get; set; } = new List<Tile>();

        public static List<ActorTile> s_AllActors { get; set; } = new List<ActorTile>();
        public static List<EnemyTile> s_AllEnemies { get; set; } = new List<EnemyTile>();
        public static List<ItemTile> s_AllItems { get; set; } = new List<ItemTile>();

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

        //The Player, with its Stats stored separately to allow persistence between generations
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
        public static ActorStats s_PlayerStats;

        //The Stairs allowing the player to generate the next level
        public static Tile s_Upstairs { get; set; }
        public static Tile s_Downstairs { get; set; }

        #endregion



        #region Public Methods


        public static void Init(Vector2Int size)
        {
            s_AllFeatures.Clear();
            s_AllRooms.Clear();
            s_AllCorridors.Clear();
            s_AllDoors.Clear();
            s_AllActors.Clear();
            s_AllEnemies.Clear();
            s_AllItems.Clear();
            s_Player = null;
            s_Upstairs = null;
            s_Downstairs = null;


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

        /// <summary>
        /// Get an IEnumerable of Cells in a line from the Origin Cell to the Destination Cell
        /// The resulting IEnumerable includes the Origin and Destination Cells
        /// Uses Bresenham's line algorithm to determine which Cells are in the closest approximation to a straight line between the two Cells
        /// </summary>
        /// <param name="xOrigin">X location of the Origin Cell at the start of the line with 0 as the farthest left</param>
        /// <param name="yOrigin">Y location of the Origin Cell at the start of the line with 0 as the top</param>
        /// <param name="xDestination">X location of the Destination Cell at the end of the line with 0 as the farthest left</param>
        /// <param name="yDestination">Y location of the Destination Cell at the end of the line with 0 as the top</param>
        /// <returns>IEnumerable of Cells in a line from the Origin Cell to the Destination Cell which includes the Origin and Destination Cells</returns>
        public static IEnumerable<Cell> GetCellsAlongLine(int xOrigin, int yOrigin, int xDestination, int yDestination)
        {
            xOrigin = Mathf.Clamp(xOrigin, 0, s_Size.x - 1);
            yOrigin = Mathf.Clamp(yOrigin, 0, s_Size.y - 1);
            xDestination = Mathf.Clamp(xDestination, 0, s_Size.x - 1);
            yDestination = Mathf.Clamp(yDestination, 0, s_Size.y - 1);

            int dx = Math.Abs(xDestination - xOrigin);
            int dy = Math.Abs(yDestination - yOrigin);

            int sx = xOrigin < xDestination ? 1 : -1;
            int sy = yOrigin < yDestination ? 1 : -1;
            int err = dx - dy;

            while (true)
            {
                yield return s_Map[xOrigin, yOrigin];
                if (xOrigin == xDestination && yOrigin == yDestination)
                {
                    break;
                }
                int e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    xOrigin += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    yOrigin += sy;
                }
            }
        }



        ///Used to retrieve Cells on the map
        public static Cell GetCellAt(Vector2Int pos)
        {
            return s_Map[pos.x, pos.y];
        }
        
        ///Used to retrieve Cells on the map
        public static Cell GetCellAt(int x, int y)
        {
            return s_Map[x, y];
        }

        ///Used to retrieve enemies on the map
        public static ActorTile GetActorAt(Vector2Int pos)
        {
            return GetCellAt(pos).Tiles.FirstOrDefault(tile => tile is ActorTile) as ActorTile;
        }


        internal static void RemoveActor(ActorTile actor)
        {
            GetCellAt(actor.Position).Tiles.Remove(actor);
            s_AllActors.Remove(actor);

            if (actor is Tile)
            {
                s_AllEnemies.Remove(actor as EnemyTile);
            }
            else if(actor is PlayerTile)
            {
                s_Player = null;
            }
        }

        #endregion
    }
}