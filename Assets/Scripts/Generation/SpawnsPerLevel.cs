using Project.Tiles;
using UnityEngine;

[System.Serializable]
public struct SpawnsPerLevel
{
    [field: SerializeField]
    public TileSpawnRate[] TileSpawnRates { get; private set; }
}

/// <summary>
/// Determines the chance for a specific Tile to spawn when needed.
/// If randomChance < spawnRate, the Tile should be spawned.
/// Each Tile owns a fraction of the total spawnRate, which all added together reach 100%. 
/// (for instance, 3 Tile might have the spawn rates 5%, 20% and 75%, the 1st being the rarest Tile to appear.)
/// We can use this to spawn Enemies as well as Items when an Enemy is defeated.
/// </summary>
[System.Serializable]
public struct TileSpawnRate
{
    [field: SerializeField]
    public Tile TileToSpawn { get; set; }

    [field: SerializeField, Range(0f, 100f)]
    public float SpawnRate { get; set; }
}