using Project.ValueTypes;
using UnityEngine;

namespace Project.Generation
{

    [CreateAssetMenu(fileName = "New Dungeon Generation Settings", menuName = "Rogue/Generation/Dungeon Settings")]
    public class DungeonGenerationSettingsSO : ScriptableObject
    {
        [field: SerializeField]
        public Vector2Int MinMaxFeatureSize { get; private set; } = new Vector2Int(4, 15);

        [field: SerializeField]
        public int MaxRooms { get; private set; } = 20;

        [field: SerializeField]
        public DungeonPatternType[] DungeonPattern { get; private set; } = new DungeonPatternType[0];


    }
}