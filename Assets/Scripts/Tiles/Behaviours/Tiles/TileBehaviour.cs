using Project.Tiles.Actors;
using Project.Display;
using UnityEngine;
using Project.Tiles;

namespace Project.Behaviours.Tiles
{

    /// <summary>
    /// Describes how the Tile reacts when an Actor interacts with it.
    /// This allows to setup different behaviours for different Tiles
    /// (ex: Non-walkable Tiles will print an error, trap Tiles will affect any Actor walking on it, etc.)
    /// </summary>
    [CreateAssetMenu(fileName = "New Tile Behaviour", menuName = "Rogue/Actors/Behaviours/Tile")]
    public class TileBehaviour : ScriptableObject
    {
        [field: SerializeField]
        private string TextOnCollision { get; set; }


        public void OnInteracted(ActorTile actor, Cell thisCell, Tile thisTile)
        {
            MessageLog.Print(string.Format(TextOnCollision, actor.TileName, thisTile.TileName));
        }
    }
}