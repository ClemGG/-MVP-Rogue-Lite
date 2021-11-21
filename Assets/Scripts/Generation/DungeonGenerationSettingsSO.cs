using Project.ValueTypes;
using UnityEngine;

[CreateAssetMenu(fileName = "New Settings", menuName = "Rogue/Generation/Settings")]
public class DungeonGenerationSettingsSO : ScriptableObject
{
    [field: SerializeField]
    public DungeonPatternType[] DungeonPattern { get; private set; } = new DungeonPatternType[0];

    [field: SerializeField]
    public Vector2Int MinMaxFeatureSize { get; private set; } = new Vector2Int(4, 15);

    [field: SerializeField]
    public int MaxRooms { get; private set; } = 20;
}
