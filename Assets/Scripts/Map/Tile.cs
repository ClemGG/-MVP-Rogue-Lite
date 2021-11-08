using Project.Colors;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Map
{


    //Represents the object itself on screen (Player, item, enemy, etc.)
    //Each Tile may have its own behaviour, can be see-through or not, walkable or not, etc.
    [CreateAssetMenu(fileName = "New Tile", menuName = "Rogue/Map/Tile")]
    public class Tile : ScriptableObject
    {
        #region Fields

        [Space(10)]
        [Header("Appearance:")]
        [Space(10)]

        [SerializeField, Tooltip("The name of the Tile, displayed in the message logs.")]
        private string _tileName;

        [SerializeField, Tooltip("The caracter displayed to represent the Tile on screen.")]
        private char _symbol;

        [SerializeField, Tooltip("Can the player see through this Tile? (If any of the Tiles in a Cell is not see-through, the player cannot see through the Cell at all.)")]
        private bool _seeThrough;

        [SerializeField, Tooltip("Can the player walk on this Tile? (If any of the Tiles in a Cell is not walkable, the player cannot walk on the Cell at all.)")]
        private bool _walkable;

        [SerializeField, Tooltip("The color of the symbol when in FOV. Uses a string to retrieve it automatically by reflection.")]
        private ColorInPalette _textColorInFOV;

        [SerializeField, Tooltip("The color of the symbol when in FOV. Uses a string to retrieve it automatically by reflection.")]
        private ColorInPalette _backgroundColorInFOV;

        [SerializeField, Tooltip("The color of the symbol when in FOV. Uses a string to retrieve it automatically by reflection.")]
        private ColorInPalette _textColorOutFOV;

        [SerializeField, Tooltip("The color of the symbol when in FOV. Uses a string to retrieve it automatically by reflection.")]
        private ColorInPalette _backgroundColorOutFOV;

        //[Tooltip("The object associated to this Tile. Can own some Behaviours or not.")]
        //public GameObject mapObject;

        #endregion



        #region Accessors

        public string TileName { get => _tileName; }
        public char Symbol { get => _symbol; }
        public bool SeeThrough { get => _seeThrough; }
        public bool Walkable { get => _walkable; }
        public ColorInPalette TextColorInFOV { get => _textColorInFOV; }
        public ColorInPalette BackgroundColorInFOV { get => _backgroundColorInFOV; }
        public ColorInPalette TextColorOutFOV { get => _textColorOutFOV; }
        public ColorInPalette BackgroundColorOutFOV { get => _backgroundColorOutFOV; }

        #endregion

    }
}