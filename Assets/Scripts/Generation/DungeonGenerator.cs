using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Project.ValueTypes;
using static Project.Utilities.ValueTypes.Enums;
using Project.Tiles;
using Project.Logic;

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

                case DungeonPatternType.StandardRandom:
                    DungeonPatterns.GenerateRandomDungeon(_settings.MinMaxFeatureSize, _settings.MaxRooms);
                    break;

                case DungeonPatternType.StandardRandomDoubleCorridors:
                    DungeonPatterns.GenerateRandomDungeon(_settings.MinMaxFeatureSize, _settings.MaxRooms, true);
                    break;

                default:
                    break;
            }

            //Once we have created all Features, we add Doors between Corridors and Rooms
            DungeonPatterns.GenerateDoors();
            AddStairs();

        }


        /// <summary>
        /// Instantiates the Stairs of the dungeon.
        /// </summary>
        private static void AddStairs()
        {
            Cell spawnCell;

            //If we are at the second floor or deeper, we create a Upstairs Tile
            if (GameSystem.s_FloorLevel > 1)
            {
                //Get a random Walkable Cell in that Room
                //For the upstairs, we set them at Center.x + 1 to not overlap them with the downstairs
                //in case we only generate One Room.
                Vector2Int spawnPos = DungeonInfo.s_AllRooms.First().Bounds.Center;
                spawnPos.x += 1;
                spawnCell = DungeonInfo.GetCellAt(spawnPos);


                //Instantiate the Upstairs in this Cell
                Tile upstairsTile = TileLibrary.Upstairs;
                upstairsTile.Position = spawnCell.Position;
                spawnCell.Tiles.Add(upstairsTile);
                DungeonInfo.s_Upstairs = upstairsTile;
            }
            //If we are at the last floor or higher, we create a Downstairs Tile
            if(GameSystem.s_FloorLevel < GameSystem.s_MaxFloorLevel)
            {
                //Get a random Walkable Cell in that Room
                //For the downstairs, we set them at Center.x - 1 to not overlap them with the upstairs
                //in case we only generate One Room.
                Vector2Int spawnPos = DungeonInfo.s_AllRooms.Last().Bounds.Center;
                spawnPos.x -= 1;
                spawnCell = DungeonInfo.GetCellAt(spawnPos);

                //Instantiate the Downstairs in this Cell
                Tile downstairsTile = TileLibrary.Downstairs;
                downstairsTile.Position = spawnCell.Position;
                spawnCell.Tiles.Add(downstairsTile);
                DungeonInfo.s_Downstairs = downstairsTile;
            }

        }


        /// <summary>
        /// Instantiates the Player in either the first or the last Room of the dungeon, depending on the level he's curently in.
        /// </summary>
        private static void AddPlayer()
        {
            Cell spawnCell = null;

            //If we're climbing down the dungeon...
            if(GameSystem.s_PreviousFloorLevel < GameSystem.s_FloorLevel)
            {
                //We spawn the Player at the center of the first Room, right next to the Upstairs (if any)
                spawnCell = DungeonInfo.GetCellAt(DungeonInfo.s_AllRooms.First().Bounds.Center);
            }
            //Otherwise, if the Player is climbing back up...
            else if (GameSystem.s_PreviousFloorLevel > GameSystem.s_FloorLevel)
            {
                //We spawn the Player at the center of the last Room, right next to the Downstairs (if any)
                spawnCell = DungeonInfo.GetCellAt(DungeonInfo.s_AllRooms.Last().Bounds.Center);
            }

            //Update the previous level flag
            GameSystem.s_PreviousFloorLevel = GameSystem.s_FloorLevel;



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
                List<Cell> walkableCells = randomRoom.Cells.Where(cell => cell.Walkable && !cell.IsInPlayerFov && !cell.Contains<Tile>("Upstairs") && !cell.Contains<Tile>("Downstairs")).ToList();

                //Get a random Walkable Cell in that Room
                Cell spawnCell = null;
                if (walkableCells.Count > 0)
                {
                    spawnCell = walkableCells[Random.Range(0, walkableCells.Count)];
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