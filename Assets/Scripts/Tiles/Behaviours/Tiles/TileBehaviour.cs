using UnityEngine;
using Project.Tiles;

namespace Project.Behaviours.Tiles
{

    /// <summary>
    /// Describes how the Tile reacts when an Actor interacts with it.
    /// This allows to setup different behaviours for different Tiles
    /// (ex: Non-walkable Tiles will print an error, trap Tiles will affect any Actor walking on it, etc.)
    /// </summary>
    public abstract class TileBehaviour : ScriptableObject
    {
        //Called when thisTile is not Walkable
        public virtual void OnActorCollided(ActorTile other, Cell thisCell, Tile thisTile) { }


        //Called when thisTile is Walkable
        public virtual void OnActorEntered(ActorTile other, Cell thisCell, Tile thisTile) { }



        //Called when the Player presses the Interact button while on this Tile
        public virtual void OnActorInteracted(PlayerTile player, Cell thisCell, Tile thisTile) { }



    }
}