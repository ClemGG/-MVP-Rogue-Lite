using Project.Map;
using UnityEngine;

namespace Project.Logic
{

    public class GameManager : MonoBehaviour
    {
        DungeonGenerator _dungeonGenerator;

        // Start is called before the first frame update
        void Start()
        {
            _dungeonGenerator = GetComponent<DungeonGenerator>();
            GenerateNewDungeon();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                _dungeonGenerator.ClearCells();
                GenerateNewDungeon();
            }
        }



        private void GenerateNewDungeon()
        {
            _dungeonGenerator.Init();   //We can leave this in Start() if the dungeon settings don't change between generations
            _dungeonGenerator.Generate();
        }
    }
}