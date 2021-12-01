using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Project.ValueTypes;
using static Project.Utilities.ValueTypes.Enums;
using Project.Tiles;


namespace Project.Generation
{
    public static class DungeonGenerator
    {
        private static DungeonGenerationSettingsSO _settings { get; set; }

        public static void Generate(DungeonGenerationSettingsSO settings, Vector2Int dungeonSize)
        {
            //Resets all infos about the last generated map (if any)
            DungeonInfo.Init(dungeonSize);

            _settings = settings;
            GenerateRooms(RandomIn(_settings.DungeonPattern));
            AddPlayer();
            AddEnemies(Random.Range(0, _settings.MaxEnemiesOnStart));
        }


        private static void GenerateRooms(DungeonPatternType patternType)
        {

            switch (patternType)
            {
                //If we input Random, we call GenerateDungeon() again with another random pattern
                case DungeonPatternType.Random:
                    GenerateRooms(RandomIn<DungeonPatternType>());
                    break;

                case DungeonPatternType.OneRoom:
                    DungeonPatterns.GenerateOneRoom();
                    break;

                case DungeonPatternType.OneRoomDark:
                    DungeonPatterns.GenerateOneRoom();
                    break;

                case DungeonPatternType.StandardRandom:
                    DungeonPatterns.GenerateRandomDungeon(_settings.MinMaxFeatureSize, _settings.MaxRooms);
                    break;

                case DungeonPatternType.StandardRandomDoubleCorridors:
                    DungeonPatterns.GenerateRandomDungeon(_settings.MinMaxFeatureSize, _settings.MaxRooms, true);
                    break;

                default:
                    break;
            }
        }


        /// <summary>
        /// Selects a random Feature whose RoomType = Room and instantiates the Player in one of its Cells
        /// </summary>
        private static void AddPlayer()
        {
            //Get a random Room on the map
            Feature randomRoom = DungeonInfo.s_AllRooms[Random.Range(0, DungeonInfo.s_AllRooms.Count)];
            List<Cell> walkableCells = randomRoom.Cells.Where(cell => cell.Walkable).ToList();

            //Get a random Walkable Cell in that Room
            Cell spawnCell = walkableCells[Random.Range(0, walkableCells.Count)];

            //Instantiate the player in this Cell
            Tile playerTile = TileLibrary.Player;
            playerTile.Position = spawnCell.Position;
            spawnCell.Tiles.Add(playerTile);
            DungeonInfo.s_AllActors.Add(playerTile as ActorTile);
        }

        /// <summary>
        /// Selects a random Walkable Tile and adds an EnemyTile on top of it.
        /// </summary>
        public static void AddEnemies(int count)
        {
            for (int i = 0; i < count; i++)
            {
                //Get a random Room on the map without any player in it
                Feature randomRoom = DungeonInfo.s_RandomRoomWithoutPlayer;
                List<Cell> walkableCells = randomRoom.Cells.Where(cell => cell.Walkable && !cell.IsInPlayerFov).ToList();

                //Get a random Walkable Cell in that Room
                Cell spawnCell = null;
                if (walkableCells.Count > 0)
                {
                    spawnCell = walkableCells[UnityEngine.Random.Range(0, walkableCells.Count)];
                }

                //Select a random EnemyTile in the settings' list and add it to the Map
                Tile enemyTile = TileLibrary.GetRandomEnemy(_settings);
                if (enemyTile && spawnCell != null)
                {
                    enemyTile.Position = spawnCell.Position;
                    spawnCell.Tiles.Add(enemyTile);
                    DungeonInfo.s_AllActors.Add(enemyTile as ActorTile);
                    DungeonInfo.s_AllEnemies.Add(enemyTile as EnemyTile);
                }
            }


        }
    }
}