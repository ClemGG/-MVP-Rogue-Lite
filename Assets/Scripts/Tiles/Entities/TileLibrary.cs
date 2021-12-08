using Project.Generation;
using UnityEngine;
using System.Reflection;

namespace Project.Tiles
{
    /// <summary>
    /// Stores all types of Tiles and loads them automatically when called
    /// instead of having to manually select which Tile to use 
    /// each time we generate a new dungeon.
    /// </summary>
    public static class TileLibrary
    {
        #region Accessors

        //TODO : Use Object Pooling instead of those Instantiate() to recycle the Tiles instead of wasting memory on recreating them.
        public static Tile Floor { get { return Object.Instantiate(Resources.Load<Tile>("Tiles/Map/Floor")); } } 
        public static Tile Wall { get { return Object.Instantiate(Resources.Load<Tile>("Tiles/Map/Wall")); } }
        public static Tile Door { get { return Object.Instantiate(Resources.Load<Tile>("Tiles/Map/Door")); } }
        public static Tile Upstairs { get { return Object.Instantiate(Resources.Load<Tile>("Tiles/Map/Upstairs")); } }
        public static Tile Downstairs { get { return Object.Instantiate(Resources.Load<Tile>("Tiles/Map/Downstairs")); } }
        public static Tile Player { get { return Object.Instantiate(Resources.Load<Tile>("Tiles/Actors/Player")); } }
        public static Tile Rat { get { return Object.Instantiate(Resources.Load<Tile>("Tiles/Actors/Rat")); } }


        private static PropertyInfo[] _fieldsNames
        {
            get
            {
                return _fields ??= typeof(TileLibrary).GetProperties();
            }
        }
        private static PropertyInfo[] _fields;

        #endregion

        #region public Methods

        internal static EnemyTile GetRandomEnemy(DungeonGenerationSettingsSO settings)
        {
            string enemyName = settings.EnemiesToSpawn[Random.Range(0, settings.EnemiesToSpawn.Length)].TileName.Replace(" ", null);
            for (int i = 0; i < _fieldsNames.Length; i++)
            {
                if (enemyName.Equals(_fieldsNames[i].Name))
                {
                    return (EnemyTile)_fieldsNames[i].GetValue(_fieldsNames[i]);
                }
            }

            return null;
        }

        #endregion
    }
}