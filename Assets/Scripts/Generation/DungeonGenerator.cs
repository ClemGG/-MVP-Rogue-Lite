using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Project.ValueTypes;
using static Project.Utilities.ValueTypes.Enums;

namespace Project.Map
{
    public class DungeonGenerator : MonoBehaviour
    {
        [SerializeField] private DungeonPatternType _dungeonPattern = DungeonPatternType.OneRoom;
        [SerializeField] private Vector2Int _dungeonSize = new Vector2Int(97, 34);
        [SerializeField] private Vector2Int _minMaxFeatureSize = new Vector2Int(4, 15);
        [SerializeField] private int _maxRooms = 20;

        private TextMeshProUGUI _mapTextField;

        // Start is called before the first frame update
        public void Init()
        {
            Map.Init(_dungeonSize.x, _dungeonSize.y);
            _mapTextField ??= GameObject.Find("map").GetComponent<TextMeshProUGUI>();
        }

        public void Generate()
        {
            GenerateDungeon(_dungeonPattern);
            AddPlayer();
            Map.Draw(_mapTextField);
        }


        /// <summary>
        /// If we don't call Init(), use this to clean the Map before filling the Cells with new Tiles
        /// </summary>
        public void ClearCells()
        {
            foreach (Cell cell in Map.s_Map)
            {
                cell.Clear();
            }
        }

        private void GenerateDungeon(DungeonPatternType patternType)
        {
            //Resets all infos about the last generated map (if any)
            DungeonInfo.Init();

            switch (patternType)
            {
                //If we input Random, we call GenerateDungeon() again with another random pattern
                case DungeonPatternType.Random:
                    GenerateDungeon(RandomIn<DungeonPatternType>());
                    break;

                case DungeonPatternType.OneRoom:
                    DungeonPatterns.GenerateOneRoom();
                    break;

                case DungeonPatternType.StandardRandom:
                    DungeonPatterns.GenerateRandomDungeon(_minMaxFeatureSize, _maxRooms);
                    break;

                case DungeonPatternType.StandardRandomDoubleCorridors:
                    DungeonPatterns.GenerateRandomDungeon(_minMaxFeatureSize, _maxRooms, true);
                    break;

                default:
                    break;
            }
        }


        /// <summary>
        /// Selects a random Feature whose RoomType = Room and instantiates the Player in one of its Cells
        /// </summary>
        private void AddPlayer()
        {
            //Get a random Room on the map
            Feature randomRoom = DungeonInfo.AllRooms[Random.Range(0, DungeonInfo.AllRooms.Count)];
            List<Cell> walkableCells = randomRoom.Cells.Where(cell => cell.Walkable).ToList();

            //Get a random Walkable Cell in that Room
            Cell playerSpawnPoint = walkableCells[Random.Range(0, walkableCells.Count)];

            //Instantiate the player in this Cell
            playerSpawnPoint.Tiles.Add(TileLibrary.Player);
        }
    }
}