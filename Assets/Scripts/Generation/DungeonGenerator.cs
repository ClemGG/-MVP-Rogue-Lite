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
        private static DungeonGenerationSettingsSO _dungeonSettings { get; set; }
        private static TileGenerationSettingsSO _enemySettings { get; set; }
        private static TileGenerationSettingsSO _itemSettings { get; set; }

        public static void Generate(DungeonGenerationSettingsSO dungeonSettings, TileGenerationSettingsSO enemySettings, TileGenerationSettingsSO itemSettings, Vector2Int dungeonSize)
        {
            //Resets all infos about the last generated map (if any)
            DungeonInfo.Init(dungeonSize);

            _enemySettings = enemySettings;
            _itemSettings = itemSettings;
            _dungeonSettings = dungeonSettings;
            GenerateRooms(RandomIn(dungeonSettings.DungeonPattern));
            AddPlayer();
            AddEnemies(Random.Range(0, enemySettings.MaxTilesOnStart));
            AddItems(Random.Range(0, itemSettings.MaxTilesOnStart));
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
                    DungeonPatterns.GenerateRandomDungeon(_dungeonSettings.MinMaxFeatureSize, _dungeonSettings.MaxRooms);
                    break;

                case DungeonPatternType.StandardRandomDoubleCorridors:
                    DungeonPatterns.GenerateRandomDungeon(_dungeonSettings.MinMaxFeatureSize, _dungeonSettings.MaxRooms, true);
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

            //If we are at the second floor or deeper, we create a Upstairs Tile.
            //OR, if it's the first level and we have the final item, we create a Upstairs to Level 0, which will quit the game.
            if ((GameSystem.s_FloorLevel > 1 && !DungeonInfo.s_Upstairs) || (GameSystem.s_FloorLevel == 1 && GameSystem.s_IsGoalReached))
            {
                //Get a random Walkable Cell in that Room
                //For the upstairs, we set them at Center.x + 1 to not overlap them with the downstairs
                //in case we only generate One Room.
                Vector2Int spawnPos = DungeonInfo.s_AllRooms[0].Bounds.Center;
                spawnPos.x += 1;
                spawnCell = DungeonInfo.GetCellAt(spawnPos);

                //Instantiate the Upstairs in this Cell
                Tile upstairsTile = TileLibrary.Upstairs;
                upstairsTile.Position = spawnCell.Position;
                spawnCell.Tiles.Add(upstairsTile);
                DungeonInfo.s_Upstairs = upstairsTile;
            }
            //If we are at the last floor or higher, we create a Downstairs Tile
            if(GameSystem.s_FloorLevel < GameSystem.c_MaxFloorLevel && !DungeonInfo.s_Downstairs)
            {
                //Get a random Walkable Cell in that Room
                //For the downstairs, we set them at Center.x - 1 to not overlap them with the upstairs
                //in case we only generate One Room.
                Vector2Int spawnPos = DungeonInfo.s_AllRooms[DungeonInfo.s_AllRooms.Count - 1].Bounds.Center;
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
            System.Func<Cell, bool> match = cell => cell.Walkable && !cell.IsInPlayerFov && !cell.Contains<Tile>("Upstairs", "Downstairs");

            for (int i = 0; i < count; i++)
            {
                //Get a random Room on the map without any player in it
                Feature randomRoom = DungeonInfo.s_RandomRoomWithoutPlayer;
                List<Cell> walkableCells = randomRoom.Cells.Where(match).ToList();

                //Get a random Walkable Cell in that Room
                Cell spawnCell = null;
                if (walkableCells.Count > 0)
                {
                    spawnCell = walkableCells[Random.Range(0, walkableCells.Count)];

                    if (spawnCell != null)
                    {
                        //Select a random EnemyTile in the settings' list and add it to the Map
                        Tile enemyTile = _enemySettings.GetRandomTile();

                        if (enemyTile)
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


        /// <summary>
        /// Selects a random Walkable Tile and adds an ItemTile on top of it.
        /// </summary>
        public static void AddItems(int count)
        {
            static bool match(Cell cell) => cell.Walkable && !cell.Contains<Tile>("Upstairs", "Downstairs") && !cell.Contains<ItemTile>();

            //Instantiate each mandatory Item first
            SpawnsPerLevel spawn = _itemSettings.TilesToForceSpawnPerLevel[GameSystem.s_FloorLevel-1];
            if(spawn.TileSpawnRates.Length > 0) {
                for (int j = 0; j < spawn.TileSpawnRates.Length; j++)
                {
                    //Get a random Room on the map without any player in it
                    Feature randomRoom = DungeonInfo.s_RandomRoomWithoutPlayer;
                    List<Cell> walkableCells = randomRoom.Cells.Where(match).ToList();

                    //Get a random Walkable Cell in that Room
                    Cell spawnCell = null;
                    if (walkableCells.Count > 0)
                    {
                        spawnCell = walkableCells[Random.Range(0, walkableCells.Count)];

                        if (spawnCell != null)
                        {
                            Tile itemTile = TileLibrary.GetTile(spawn.TileSpawnRates[j].TileToSpawn.TileName);

                            if (itemTile)
                            {
                                itemTile.Position = spawnCell.Position;
                                spawnCell.Tiles.Add(itemTile);
                                DungeonInfo.s_AllItems.Add(itemTile as ItemTile);
                            }
                        }
                    }
                }
            }
                

            for (int i = 0; i < count; i++)
            {
                //Get a random Room on the map without any player in it
                Feature randomRoom = DungeonInfo.s_RandomRoomWithoutPlayer;
                List<Cell> walkableCells = randomRoom.Cells.Where(match).ToList();

                //Get a random Walkable Cell in that Room
                Cell spawnCell = null;
                if (walkableCells.Count > 0)
                {
                    spawnCell = walkableCells[Random.Range(0, walkableCells.Count)];

                    if (spawnCell != null)
                    {
                        //Select a random EnemyTile in the settings' list and add it to the Map
                        Tile itemTile = _itemSettings.GetRandomTile();

                        if (itemTile)
                        {
                            itemTile.Position = spawnCell.Position;
                            spawnCell.Tiles.Add(itemTile);
                            DungeonInfo.s_AllItems.Add(itemTile as ItemTile);
                        }
                    }
                }
            }
        }

    }
}