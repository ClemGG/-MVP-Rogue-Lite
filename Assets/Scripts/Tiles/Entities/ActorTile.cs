using Project.Behaviours.FOV;
using Project.Behaviours.Movement;
using Project.Combat;
using UnityEngine;

namespace Project.Tiles
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

        [field: SerializeField, Tooltip("The stats of the Actor (health, strength).")]
        public ActorStats Stats { get; set; }


        #endregion



        #region Methods

        //On player turn, compute Movement and show all Cells in FOV
        public void OnTick()
        {
            if (Movement) Movement.OnTick(this);
            if (Fov) Fov.OnTick(this);
        }

        #endregion
    }
}