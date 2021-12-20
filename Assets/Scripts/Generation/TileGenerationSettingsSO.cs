using Project.Logic;
using Project.Tiles;
using UnityEngine;

/// <summary>
/// Used to spawn a different set of enemies for each level.
/// </summary>
/// 
namespace Project.Generation
{

    [CreateAssetMenu(fileName = "New Tile Spawn Settings", menuName = "Rogue/Generation/Tile Generation Settings")]
    public class TileGenerationSettingsSO : ScriptableObject
    {

        #region Fields

        [field: SerializeField, Tooltip("How many Tiles to spawn when the dungeon is generated ?")]
        public int MaxTilesOnStart { get; private set; } = 10;

        [field: SerializeField, Tooltip("For each level of the dungeon, we create a set of Tiles with their own spawnRate each.")]
        private SpawnsPerLevel[] SpawnRatesPerLevel { get; set; } = new SpawnsPerLevel[GameSystem.c_MaxFloorLevel];

        [field: SerializeField, Tooltip("For each level of the dungeon, spawn mandatory Tiles (like the Final Item).")]
        public SpawnsPerLevel[] TilesToForceSpawnPerLevel { get; set; } = new SpawnsPerLevel[GameSystem.c_MaxFloorLevel];

        #endregion


        #region Methods

        public Tile GetRandomTile()
        {
            //We get the spawn rate for the current level
            int lvl = GameSystem.s_FloorLevel - 1;
            SpawnsPerLevel curLevel = SpawnRatesPerLevel[lvl];
            if (curLevel.TileSpawnRates.Length == 0)
            {
                Debug.LogError($"Error : The Tiles' spawn rates are not set for the level {lvl} of the dungeon.");
            }
            else
            {

                float alea = UnityEngine.Random.Range(0f, 100f);
                Vector2 interval = Vector2.zero;

                //We get each Tile spawn rate and we compare them to see which one we should spawn
                for (int i = 0; i < curLevel.TileSpawnRates.Length; i++)
                {
                    TileSpawnRate spawn = curLevel.TileSpawnRates[i];

                    //We retrieve the interval between each spawn rate (for instance, if there are 3 Tiles at 10%, 30%, and 60%, this will be 0-10, 10-40, 40-100)
                    if (i == 0)
                    {
                        interval.x = 0f;
                        interval.y = spawn.SpawnRate;
                    }
                    else
                    {
                        interval.x = interval.y;
                        interval.y = spawn.SpawnRate + interval.x;
                    }

                    //If our random number is in one of these intervals, this will be the Tile to spawn
                    if (alea > interval.x && alea < interval.y)
                    {
                        return TileLibrary.GetTile(spawn.TileToSpawn.TileName);
                    }
                }
            }

            return null;
        }

        #endregion
    }

    
}