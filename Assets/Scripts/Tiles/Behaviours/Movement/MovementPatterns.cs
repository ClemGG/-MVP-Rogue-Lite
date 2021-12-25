using Project.Input;
using Project.Generation;
using UnityEngine;
using Project.Tiles;
using System.Collections.Generic;
using System.Linq;
using Project.Pathfinding.AStar;

namespace Project.Behaviours.Movement
{
    public static class MovementPatterns
    {
        #region Public Constructors

        /// <summary>
        /// Returns a position adjacent to the Actor depending on the player's inputs.
        /// </summary>
        internal static Cell GetInputNextPosition(Vector2Int actorPosition)
        {
            Cell destCell = DungeonInfo.s_Map[actorPosition.x + PlayerInput.s_MoveDirResult.x, actorPosition.y - PlayerInput.s_MoveDirResult.y];

            return destCell;
        }

        /// <summary>
        /// Returns a random position adjacent to the Actor.
        /// </summary>
        internal static Cell GetRandomAdjacentTile(Vector2Int actorPosition)
        {
            //Get all Tiles surrounding the Actor.
            //We'll then pick one of them randomly as our destination, if that Tile is Walkable.
            List<Vector2Int> borderTiles = new List<Vector2Int>();

            for (int y = actorPosition.y - 1; y <= actorPosition.y + 1; y++)
            {
                for (int x = actorPosition.x - 1; x <= actorPosition.x + 1; x++)
                {
                    //If it's not the Actor's position or the limits of the dungeon, add that position to the lsit
                    if (x >= 0 && y >= 0 && x < DungeonInfo.s_Size.x && y < DungeonInfo.s_Size.y)
                    {
                        //We'll then only keep the Tiles that are Walkable
                        Cell c = DungeonInfo.GetCellAt(x, y);
                        if (c.Walkable)
                        {
                            borderTiles.Add(new Vector2Int(x, y));
                        }
                    }
                }
            }


            if (borderTiles.Count == 0)
            {
                return null;
            }
            else
            {
                //Then we'll pick one at random
                return DungeonInfo.GetCellAt(borderTiles[Random.Range(0, borderTiles.Count)]);
            }
        }


        ///// <summary>
        ///// Returns the closest Tile leading to an unexplored FloorTile.
        ///// </summary>
        //internal static Cell GetTileClosestToUnexploredFloorTile(Vector2Int actorPosition)
        //{
        //    return null;
        //}


        ///// <summary>
        ///// Selects a random Tile in a random Room and returns the closest Tile leading to it.
        ///// </summary>
        //internal static Cell GetRandomRoomTile(Vector2Int actorPosition)
        //{
        //    return null;
        //}

        internal static Cell GoToTile(Vector2Int actorPosition, Tile tile)
        {
            return DungeonInfo.GetCellAt(NextCellAStar(actorPosition, tile.Position));
        }

        #endregion



        #region Pathfinding


        //Uses A* pathfinding algorithm to find the next step towards the destination Cell.
        private static Vector2Int NextCellAStar(Vector2Int start, Vector2Int dest)
        {
            return AStar.CalculatePath(start, dest)[0];
        }



        #endregion
    }
}