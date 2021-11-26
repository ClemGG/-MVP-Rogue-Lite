using Project.Generation;
using Project.ValueTypes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Behaviours.FOV
{
    /// <summary>
    /// Describes how the Actor "sees" the environment.
    /// This allows to setup different FOVs for different Actors
    /// (ex: An enemy can see everything around them, some others only in a reduced cone of vision.)
    /// </summary>
    [CreateAssetMenu(fileName = "New FOV Behaviour", menuName = "Rogue/Actors/Behaviours/FOV")]
    public class FOV : ScriptableObject
    {

        #region Fields

        [field: Space(10)]
        [field: Header("Navigation:")]
        [field: Space(10)]


        [field: Min(1), SerializeField, Tooltip("The radius for the Actor's Field Of View.")]
        public int FovRadius { get; private set; }

        [field: SerializeField, Tooltip("Can this Actor see through everything?")]
        public bool SeeThroughAll { get; set; }

        [field: SerializeField, Tooltip("Should this Actor's FOV be drawn on the Map for the player?")]
        private bool ShowForPlayer { get; set; }

        [field: SerializeField, Tooltip("The shape of the FOV (spherical, sqaure, etc.)")]
        private FOVPatternType FOVPattern { get; set; }

        public HashSet<Vector2Int> BorderTiles { get; private set; } = new HashSet<Vector2Int>();     //Contains the limits of our radius
        private HashSet<Vector2Int> _visibleCellsForThisActor { get; set; } = new HashSet<Vector2Int>();
        protected static HashSet<Vector2Int> _allVisibleCells { get; set; } = new HashSet<Vector2Int>();     //Contains the combined explored Cells of all Actors where ShowForPlayer = true

        #endregion


        #region Static Methods


        public static void Clear()
        {
            _allVisibleCells.Clear(); 
            
            //We don't reset IsExplored to keep the explored sections visible
            for (int y = 0; y < DungeonMap.s_Size.y; y++)
            {
                for (int x = 0; x < DungeonMap.s_Size.x; x++)
                {
                    DungeonMap.s_Map[x, y].IsInFov = false;
                }
            }
        }

        public static void ShowExploredTiles()
        {
            Vector2Int[] cellsToShow = _allVisibleCells.ToArray();
            for (int i = 0; i < _allVisibleCells.Count; i++)
            {
                DungeonMap.s_Map[cellsToShow[i].x, cellsToShow[i].y].IsExplored = true;
                DungeonMap.s_Map[cellsToShow[i].x, cellsToShow[i].y].IsInFov = true;
            }
        }

        #endregion


        #region Instance Methods

        public void OnTick(Vector2Int actorPosition)
        {
            GetVisibleCells(actorPosition);
        }

        /// <summary>
        /// Retrives all Cells explored by this Actor and adds them to the total list of all Cells explored by all Actors
        /// </summary>
        protected virtual void GetVisibleCells(Vector2Int actorPosition)
        {
            _visibleCellsForThisActor.Clear();
            BorderTiles.Clear();

            for (int i = -FovRadius; i <= FovRadius; i++)
            {
                BorderTiles.Add(new Vector2Int(i, -FovRadius));
                BorderTiles.Add(new Vector2Int(i, FovRadius));
                BorderTiles.Add(new Vector2Int(-FovRadius, i));
                BorderTiles.Add(new Vector2Int(FovRadius, i));
            }

            switch (FOVPattern)
            {
                case FOVPatternType.Square:
                    _visibleCellsForThisActor.UnionWith(FOVPatterns.SquareFOV(actorPosition, this));
                    break;

                case FOVPatternType.Circle:
                    _visibleCellsForThisActor.UnionWith(FOVPatterns.CircleFOV(actorPosition, this));
                    break;

                default:
                    break;
            }

            if (ShowForPlayer)
            {
                _allVisibleCells.UnionWith(_visibleCellsForThisActor);
            }

        }

        #endregion
    }
}