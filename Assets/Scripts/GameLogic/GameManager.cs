using Project.Behaviours.FOV;
using Project.Generation;
using Project.Input;
using UnityEngine;

namespace Project.Logic
{

    public class GameManager : MonoBehaviour
    {
        #region Fields

        [field: SerializeField] private DungeonGenerationSettingsSO _settings { get; set; }
        [field: SerializeField] private Vector2Int _dungeonSize { get; set; } = new Vector2Int(97, 34);



        #endregion


        #region Methods

        // Start is called before the first frame update
        void Start()
        {
            PlayerInput.Init();                    //Creates the input map for the player
            DungeonMap.Init(_dungeonSize);   //We can leave this in Start() if the dungeon size doesn't change between generations
            GenerateNewDungeon(_settings);

        }

        private void Update()
        {
            //Checks if the player has pressed some buttons
            PlayerInput.Update();


            //If the player moves the character...
            if (PlayerInput.s_isMoving)
            {
                //Update the actors' FOV and position
                FOV.Clear();

                DungeonInfo.s_Player.OnTick();

                //Then do the same for all enemies in the dungeon
                for (int i = 0; i < DungeonInfo.s_AllActors.Count; i++)
                {
                    if (!DungeonInfo.s_AllActors[i].Equals(DungeonInfo.s_Player))
                    {
                        DungeonInfo.s_AllActors[i].OnTick();
                    }
                }

                //Then we redraw the map
                FOV.ShowExploredTiles();
                DungeonMap.Draw();
            }

            if (PlayerInput.s_rightClick)
            {
                DungeonMap.Clear();
                GenerateNewDungeon(_settings);
            }
        }



        public static void GenerateNewDungeon(DungeonGenerationSettingsSO settings)
        {
            DungeonGenerator.Generate(settings);

            FOV.Clear();
            for (int i = 0; i < DungeonInfo.s_AllActors.Count; i++)
            {
                if (DungeonInfo.s_AllActors[i].Fov)
                {
                    DungeonInfo.s_AllActors[i].Fov.OnTick(DungeonInfo.s_AllActors[i].Position);
                }
            }
            FOV.ShowExploredTiles();

            DungeonMap.Draw();

        }

        #endregion
    }
}