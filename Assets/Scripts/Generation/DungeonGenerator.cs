using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Project.ValueTypes;
using static Project.Utilities.ValueTypes.Enums;
using Project.Tiles.Actors;
using Project.Tiles;

namespace Project.Generation
{
    public static class DungeonGenerator
    {
        private static DungeonGenerationSettingsSO _settings { get; set; }

        public static void Generate(DungeonGenerationSettingsSO settings)
        {
            //Resets all infos about the last generated map (if any)
            DungeonInfo.Init();

            _settings = settings;
            GenerateRooms(RandomIn(_settings.DungeonPattern));
            AddPlayer();
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
            Cell playerSpawnPoint = walkableCells[Random.Range(0, walkableCells.Count)];

            //Instantiate the player in this Cell
            Tile playerTile = TileLibrary.Player;
            playerTile.Position = playerSpawnPoint.Position;
            playerSpawnPoint.Tiles.Add(playerTile);
            DungeonInfo.s_AllActors.Add(playerTile as ActorTile);
        }
    }
}