using Project.Tiles;
using Project.ValueTypes;
using UnityEngine;

namespace Project.Generation
{

    [CreateAssetMenu(fileName = "New Settings", menuName = "Rogue/Generation/Settings")]
    public class DungeonGenerationSettingsSO : ScriptableObject
    {
        [field: SerializeField]
        public Vector2Int MinMaxFeatureSize { get; private set; } = new Vector2Int(4, 15);

        [field: SerializeField]
        public int MaxRooms { get; private set; } = 20;


        [field: SerializeField, Tooltip("How many Enemies to spawn when the dungeon is generated ?")]
        public int MaxEnemiesOnStart { get; private set; } = 10;

        [field: SerializeField, Range(0, 100), Tooltip("The chance to spawn a new Enemy each time we've reached [SpawnRate] turns.")]
        public int SpawnChance { get; private set; } = 50;

        [field: SerializeField, Min(1), Tooltip("How many turns should we wait before spawning a new Enemy ?")]
        public int SpawnRate { get; private set; } = 20;

        [field: SerializeField]
        public DungeonPatternType[] DungeonPattern { get; private set; } = new DungeonPatternType[0];

        [field: SerializeField]
        public EnemyTile[] EnemiesToSpawn { get; private set; } = new EnemyTile[0];


    }
}