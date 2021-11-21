using Project.Map;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Actors.Behaviours.FOV
{
    /// <summary>
    /// The script used to store all different types of FOV patterns for the Actors to use
    /// (spherical, cone, square, etc.)
    /// </summary>
    public static class FOVPatterns
    {
        #region Internal Constructors

        internal static HashSet<Vector2Int> SquareFOV(Vector2Int actorPosition, FOV fov)
        {
            HashSet<Vector2Int> visibleCells = new HashSet<Vector2Int>();

            foreach (Vector2Int borderTile in fov.BorderTiles)
            {
                foreach (Vector2Int position in GetCellsAlongLine(actorPosition, actorPosition + borderTile))
                {
                    Cell cell = DungeonMap.s_Map[position.x, position.y];
                    visibleCells.Add(position);

                    if (!cell.SeeThrough && !fov.SeeThroughAll)
                    {
                        break;
                    }
                }
            }

            return visibleCells;
        }

        #endregion


        #region Private Methods

        static HashSet<Vector2Int> GetCellsAlongLine(Vector2Int origin, Vector2Int destination)
        {
            HashSet<Vector2Int> cells = new HashSet<Vector2Int>();

            origin.Clamp(Vector2Int.zero, DungeonMap.s_Size - Vector2Int.one);
            destination.Clamp(Vector2Int.zero, DungeonMap.s_Size - Vector2Int.one);
            Vector2Int delta = new Vector2Int
                (
                    Mathf.Abs(destination.x - origin.x),
                    Mathf.Abs(destination.y - origin.y)
                ); 
            Vector2Int step = new Vector2Int
                (
                    origin.x < destination.x ? 1 : -1,
                    origin.y < destination.y ? 1 : -1
                );

            // We calculate the error boundary, which represents a negative of the distance
            // from the point where the line exits the tile to the top edge of the tile.
            // It will be used to decide if the tile will be added to the list or not.
            int err = delta.x - delta.y;

            while(true)
            {
                cells.Add(origin);

                if (origin.x < 0 || origin.y < 0 || origin.x > DungeonMap.s_Size.x || origin.y > DungeonMap.s_Size.y)
                    break;
                if (origin == destination)
                    break;

                int e2 = err * 2;
                if (e2 > -delta.y)
                {
                    err -= delta.y;
                    origin.x += step.x;
                }
                if (e2 < delta.x)
                {
                    err += delta.x;
                    origin.y += step.y;
                }
            }

            return cells;
        }

        #endregion
    }
}