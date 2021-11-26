using Project.Input;
using Project.Generation;
using UnityEngine;
using Project.Tiles;

namespace Project.Behaviours.Movement
{
    public static class MovementPatterns
    {
        #region Public Constructors

        /// <summary>
        /// Returns a position adjacent to the Actor depending on the player's inputs.
        /// </summary>
        internal static (Cell, Cell) GetInputNextPosition(Vector2Int actorPosition)
        {
            Cell actorCell = DungeonMap.s_Map[actorPosition.x, actorPosition.y];
            Cell destCell = DungeonMap.s_Map[actorPosition.x + PlayerInput.s_moveDirResult.x, actorPosition.y - PlayerInput.s_moveDirResult.y];

            return (actorCell, destCell);
        }

        /// <summary>
        /// Returns a random position adjacent to the Actor.
        /// </summary>
        internal static (Cell, Cell) GetRandomAdjacentTile(Vector2Int actorPosition)
        {
            return (null, null);
        }


        /// <summary>
        /// Returns the closest Tile leading to an unexplored FloorTile.
        /// </summary>
        internal static (Cell, Cell) GetTileClosestToUnexploredFloorTile(Vector2Int actorPosition)
        {
            return (null, null);
        }

        #endregion

    }
}