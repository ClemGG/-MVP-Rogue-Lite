using Project.Actors.Behaviours.FOV;
using Project.Map;
using UnityEngine;

namespace Project.Logic
{

    public class GameManager : MonoBehaviour
    {
        [field: SerializeField] private DungeonGenerationSettingsSO _settings { get; set; }
        [field: SerializeField] private Vector2Int _dungeonSize { get; set; } = new Vector2Int(97, 34);

        // Start is called before the first frame update
        void Start()
        {
            DungeonGenerator.Init(_dungeonSize);   //We can leave this in Start() if the dungeon settings don't change between generations
            GenerateNewDungeon(_settings);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                DungeonMap.Clear();
                GenerateNewDungeon(_settings);
            }
        }



        public static void GenerateNewDungeon(DungeonGenerationSettingsSO settings)
        {
            DungeonGenerator.Generate(settings);

            FOV.Clear();
            for (int i = 0; i < DungeonInfo.AllActors.Count; i++)
            {
                DungeonInfo.AllActors[i].OnTick();
            }
            FOV.ShowExploredTiles();

            DungeonMap.Draw();
        }
    }
}