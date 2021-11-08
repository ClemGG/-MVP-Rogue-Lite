using Project.Map;
using UnityEngine;

namespace Project.Actors
{
    /// <summary>
    /// All actors (Player, enemy, etc.) derive from this class.
    /// It combines the interfaces for movement, fov, IA, etc.
    /// </summary>
    public class ActorTile : Tile
    {

        #region Fields

        [Space(10)]
        [Header("Navigation:")]
        [Space(10)]


        [SerializeField, Tooltip("The radius for the Actor's Field Of View.")]
        private int _fovRadius;

        #endregion


        #region Accessors

        public int FovRadius { get; }


        #endregion



        #region Methods

        public Cell[] GetVisibleCells()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}