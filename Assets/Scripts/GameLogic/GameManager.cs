using Project.Behaviours.FOV;
using Project.Generation;
using Project.Display;
using Project.Input;
using UnityEngine;
using Project.Tiles;
using System;

namespace Project.Logic
{

    public class GameManager : MonoBehaviour
    {
        #region Fields

        [field: SerializeField] private GameObject _helpCanvas { get; set; }
        [field: SerializeField] private DungeonGenerationSettingsSO _settings { get; set; }
        [field: SerializeField] private Vector2Int _dungeonSize { get; set; } = new Vector2Int(97, 34);
        [field: SerializeField] private int _turnsPassedOnWait { get; set; } = 50;

        private int _nbTurnsPassed { get; set; } = 0;   //Increments each time the Player takes an action

        #endregion


        #region Methods

        // Start is called before the first frame update
        void Start()
        {
            
            SetupComponents();

            MessageLog.Print(GameSystem.c_ShowHelpText);
            MapLog.ChangeTitle();
            GenerateNewDungeon();
        }

        private void SetupComponents()
        {
            if (_helpCanvas)
            {
                _helpCanvas.SetActive(false);
            }
        }





        private void Update()
        {
            //Checks if the player has pressed some buttons
            PlayerInput.Update();

            {
                if (GameSystem.s_IsGameOver)
                {
                    //Reset everything needed here for another game

                    return;
                }

                if (PlayerInput.s_ToggleHelp)
                {
                    _helpCanvas.SetActive(!_helpCanvas.activeSelf);
                }
                //We don't want the Player to do anything while the Commands menu is active
                if (_helpCanvas.activeSelf)
                {
                    return;
                }

#if UNITY_EDITOR
                if (PlayerInput.s_RightClick)
                {
                    GenerateNewDungeon();
                }
#endif

                if (PlayerInput.s_IsCheckingTiles)
                {
                    MapLog.GetTileUnderMouse();
                    return;
                }

                //If we wait, we add a certain amount of turns to the count.
                //Then we get how many times we have to call certain functions that should have been called
                //during the waiting time, and we call them.
                if (PlayerInput.s_Waits)
                {
                    MessageLog.Print($"You wait for {_turnsPassedOnWait} turns.");

                    _nbTurnsPassed += _turnsPassedOnWait;
                    int nbCalls = _turnsPassedOnWait % _settings.SpawnRate;
                    for (int i = 0; i < nbCalls; i++)
                    {
                        DungeonGenerator.AddEnemies(1);
                    }

                    AutoRedrawMap();

                    //Updates the player's stat UI
                    PlayerLog.DisplayPlayerStats(DungeonInfo.s_Player.TileName, DungeonInfo.s_Player.Stats);
                }
            }
            //If the player moves the character...
            if (PlayerInput.s_IsMoving)
            {
                //Update the player's FOV and position
                FOV.Clear();
                InspectorLog.ClearHealthbarsList();

                DungeonInfo.s_Player.OnTick();

                //Then do the same for all enemies in the dungeon
                for (int i = 0; i < DungeonInfo.s_AllActors.Count; i++)
                {
                    ActorTile actor = DungeonInfo.s_AllActors[i];
                    if (!actor.Equals(DungeonInfo.s_Player))
                    {
                        actor.OnTick();
                        if (actor.Stats != null && DungeonInfo.GetCellAt(actor.Position).IsInPlayerFov)
                        {
                            InspectorLog.DisplayHealth(actor);
                        }
                    }
                }

                //One turn has passed. If enough turns have passed, we spawn a new Enemy
                _nbTurnsPassed++;
                if(_nbTurnsPassed % _settings.SpawnRate == 0)
                {
                    DungeonGenerator.AddEnemies(1);
                }


                //Then we redraw the map
                FOV.ShowExploredTiles();
                MapLog.Draw(DungeonInfo.s_Size, DungeonInfo.s_Map);

                //Then we draw the healthbars of all visible Enemies
                //Needs to be called after FOV.ShowExploredTiles() to set Cell.IsInPlayerFov to true
                for (int i = 0; i < DungeonInfo.s_AllEnemies.Count; i++)
                {
                    EnemyTile enemy = DungeonInfo.s_AllEnemies[i];
                    enemy.OnTick();
                    if (enemy.Stats != null && DungeonInfo.GetCellAt(enemy.Position).IsInPlayerFov)
                    {
                        InspectorLog.DisplayHealth(enemy);
                    }
                }

                //Updates the player's stat UI
                PlayerLog.DisplayPlayerStats(DungeonInfo.s_Player.TileName, DungeonInfo.s_Player.Stats);
            }
            else if (PlayerInput.s_Interacts)
            {
                //We get all Tiles underneath the Player and we call OnActorInteracted on their TileBehaviour
                DungeonInfo.GetCellAt(DungeonInfo.s_Player.Position).OnActorInteracted(DungeonInfo.s_Player);
            }
        }



        public void GenerateNewDungeon()
        {
            DungeonInfo.ClearMap();
            DungeonGenerator.Generate(_settings, _dungeonSize);

            AutoRedrawMap();

            //Updates the player's stat UI
            PlayerLog.DisplayPlayerStats(DungeonInfo.s_Player.TileName, DungeonInfo.s_Player.Stats);

        }
        private void AutoRedrawMap()
        {
            //Update the actors' FOVs and positions
            FOV.Clear();
            for (int i = 0; i < DungeonInfo.s_AllActors.Count; i++)
            {
                if (DungeonInfo.s_AllActors[i].Fov)
                {
                    DungeonInfo.s_AllActors[i].Fov.OnTick(DungeonInfo.s_AllActors[i]);
                }
            }

            //Then we redraw the map
            FOV.ShowExploredTiles();
            MapLog.Draw(DungeonInfo.s_Size, DungeonInfo.s_Map);
        }

        #endregion



    }
}