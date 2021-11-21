using Project.Actors.Behaviours.FOV;
using Project.Actors.Behaviours.Movement;
using Project.Map;
using UnityEngine;

namespace Project.Actors.Entities
{
    /// <summary>
    /// All actors (Player, enemy, etc.) derive from this class.
    /// It combines the interfaces for movement, fov, IA, etc.
    /// </summary>
    public class ActorTile : Tile
    {

        #region Fields

        [field: Space(10)]
        [field: Header("Behaviours:")]
        [field: Space(10)]


        [field: SerializeField, Tooltip("The Behaviour determining its field of view.")]
        public FOV Fov { get; private set; }

        [field: SerializeField, Tooltip("The Behaviour determining its movement pattern.")]
        public Movement Movement { get; private set; }


        #endregion



        #region Methods

        //On player turn, compute Movement and show all Cells in FOV
        public void OnTick()
        {
            //Movement.OnTick(Position);
            Fov.OnTick(Position);
        }

        #endregion
    }
}