using Project.Colors;
using UnityEngine;
using Project.Behaviours.FOV;
using Project.Behaviours.Tiles;

namespace Project.Tiles
{


    //Represents the object itself on screen (Player, item, enemy, etc.)
    //Each Tile may have its own behaviour, can be see-through or not, walkable or not, etc.
    [CreateAssetMenu(fileName = "New Tile", menuName = "Rogue/Map/Tile")]
    public class Tile : ScriptableObject
    {

        [field: Space(10)]
        [field: Header("Appearance:")]
        [field: Space(10)]

        [field: SerializeField, Tooltip("The name of the Tile, displayed in the message logs.")]
        public string TileName { get; private set; }

        [field: SerializeField, Tooltip("The caracter displayed to represent the Tile on screen.")]
        public char Symbol { get; private set; }

        [field: SerializeField, Tooltip("Can the player see through this Tile? (If any of the Tiles in a Cell is not see-through, the player cannot see through the Cell at all.)")]
        public bool SeeThrough { get; private set; }

        [field: SerializeField, Tooltip("Can the player walk on this Tile? (If any of the Tiles in a Cell is not walkable, the player cannot walk on the Cell at all.)")]
        public bool Walkable { get; private set; }

        [field: SerializeField, Tooltip("The color of the symbol when in FOV. Uses a string to retrieve it automatically by reflection.")]
        public ColorInPalette TextColorInFOV { get; private set; }

        [field: SerializeField, Tooltip("The color of the symbol when in FOV. Uses a string to retrieve it automatically by reflection.")]
        public ColorInPalette BackgroundColorInFOV { get; private set; }

        [field: SerializeField, Tooltip("The color of the symbol when in FOV. Uses a string to retrieve it automatically by reflection.")]
        public ColorInPalette TextColorOutFOV { get; private set; }

        [field: SerializeField, Tooltip("The color of the symbol when in FOV. Uses a string to retrieve it automatically by reflection.")]
        public ColorInPalette BackgroundColorOutFOV { get; private set; }

        [field: SerializeField, Tooltip("Determines how the Tile reacts when an Entity interacts with it.")]
        public TileBehaviour TileBehaviour { get; set; }

        public Vector2Int Position { get; set; }


        public void OnInteracted(ActorTile actor, Cell thisCell)
        {
            if (TileBehaviour) TileBehaviour.OnInteracted(actor, thisCell, this);
        }

    }
}