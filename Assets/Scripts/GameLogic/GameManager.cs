using Project.Behaviours.FOV;
using Project.Generation;
using Project.Display;
using Project.Input;
using UnityEngine;
using Project.Tiles;
using Project.Items;

namespace Project.Logic
{

    public class GameManager : MonoBehaviour
    {
        #region Fields

        [field: SerializeField] private GameObject _helpCanvas { get; set; }
        [field: SerializeField] private DungeonGenerationSettingsSO _dungeonSettings { get; set; }
        [field: SerializeField] private TileGenerationSettingsSO _enemySettings { get; set; }
        [field: SerializeField] private TileGenerationSettingsSO _itemSettings { get; set; }
        [field: SerializeField] private Vector2Int _dungeonSize { get; set; } = new Vector2Int(97, 34);
        [field: SerializeField] private int _turnsPassedOnWait { get; set; } = 50;

        [field: SerializeField, Min(1), Tooltip("How many turns should we wait before spawning a new Enemy ?")]
        public int _turnsBeforeSpawn { get; private set; } = 20;

        [field: SerializeField, Range(0, 100), Tooltip("The chance to spawn a new Enemy each time we've reached [TurnsBeforeSpawn] turns.")]
        public int _enemySpawnChance { get; private set; } = 50;

        private int _nbTurnsPassed { get; set; } = 0;   //Increments each time the Player takes an action

        #endregion


        #region Methods

        // Start is called before the first frame update
        void Start()
        {
            SetupStaticClasses();
            SetupComponents();

            MessageLog.Print(GameSystem.c_ShowHelpText);
            MapLog.ChangeTitle();
            GenerateNewDungeon();

        }

        private void SetupStaticClasses()
        {
            Inventory.Init();
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

//#if UNITY_EDITOR
                    //Doesn' work anymore since we use stairs and level checks to regenerate a new level
//                if (PlayerInput.s_RightClick)
//                {
//                    GenerateNewDungeon();
//                }
//#endif

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
                    int nbCalls = _turnsPassedOnWait / _turnsBeforeSpawn;
                    for (int i = 0; i < nbCalls; i++)
                    {

                        //We check the SpawnChance to see if we should spawn after each call
                        if (Random.Range(0f, 100f) < _enemySpawnChance)
                        DungeonGenerator.AddEnemies(1);
                    }

                    AutoRedrawMap();

                    //Updates the player's stat UI
                    PlayerLog.DisplayPlayerStats(DungeonInfo.s_Player.TileName, DungeonInfo.s_Player.Stats);
                }
            }
            if (PlayerInput.s_UseItem)
            {
                Inventory.UseItem(PlayerInput.s_UseItemIndex);
                SpendOneTurn(false);
            }
            //If the player moves the character or uses an item...
            if (PlayerInput.s_IsMoving)
            {

                //We consume one turn, allowing the Player and the Enemies to move if able
                SpendOneTurn(true);

            }
            else if (PlayerInput.s_Interacts)
            {
                bool containsItem = DungeonInfo.GetCellAt(DungeonInfo.s_Player.Position).Contains<ItemTile>();

                //We get all Tiles underneath the Player and we call OnActorInteracted on their TileBehaviour
                DungeonInfo.GetCellAt(DungeonInfo.s_Player.Position).OnActorInteracted(DungeonInfo.s_Player);

                //We only consume the Player's turn if he tries to pick up an Item.
                if (containsItem)
                {
                    SpendOneTurn(false);
                }
            }
        }

        private void SpendOneTurn(bool shouldPlayerAct)
        {

            //Update the player's FOV and position
            FOV.Clear();
            InspectorLog.ClearHealthbarsList();

            #region Ticks

            //If the Player is allowed to act (ie. not using items, etc.), then activate all its ticks
            if (shouldPlayerAct)
            {
                DungeonInfo.s_Player.OnTick();
            }
            else
            {
                //We update at least the FOV to still be able to see the other Tiles
                DungeonInfo.s_Player.Fov.OnTick(DungeonInfo.s_Player);
            }

            //Then do the same for all enemies in the dungeon
            for (int i = 0; i < DungeonInfo.s_AllActors.Count; i++)
            {
                ActorTile actor = DungeonInfo.s_AllActors[i];
                if (!actor.Equals(DungeonInfo.s_Player))
                {
                    actor.OnTick();
                }
            }

            #endregion


            #region Add Enemies after Turns

            //One turn has passed. If enough turns have passed, we spawn a new Enemy
            _nbTurnsPassed++;
            if (_nbTurnsPassed % _turnsBeforeSpawn == 0)
            {
                //We check the SpawnChance to see if we should spawn after each call
                if (Random.Range(0f, 100f) < _enemySpawnChance)
                    DungeonGenerator.AddEnemies(1);
            }

            #endregion


            //Then we redraw the map
            FOV.ShowExploredTiles();
            MapLog.Draw(DungeonInfo.s_Size, DungeonInfo.s_Map);


            #region InspectorLog

            //Then we draw the healthbars of all visible Enemies
            //Needs to be called after FOV.ShowExploredTiles() to set Cell.IsInPlayerFov to true
            for (int i = 0; i < DungeonInfo.s_AllEnemies.Count; i++)
            {
                EnemyTile enemy = DungeonInfo.s_AllEnemies[i];
                if (enemy.Stats != null && DungeonInfo.GetCellAt(enemy.Position).IsInPlayerFov)
                {
                    InspectorLog.DisplayHealth(enemy);
                }
            }

            //Updates the player's stat UI
            PlayerLog.DisplayPlayerStats(DungeonInfo.s_Player.TileName, DungeonInfo.s_Player.Stats);

            #endregion
        }

        public void GenerateNewDungeon()
        {
            DungeonInfo.ClearMap();
            DungeonGenerator.Generate(_dungeonSettings, _enemySettings, _itemSettings, _dungeonSize);

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